using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using System.IO;
using System.Threading;

namespace Update
{
    public delegate void UpdateFileName(string name);
    public delegate void UpdateProcessMove();
    public delegate void ExitApplication();
    public partial class UpdateForm : Form
    {
        /// <summary>
        /// 主进程ID
        /// </summary>
        private int processID;
        public UpdateForm()
        {
            InitializeComponent();
        }

        public UpdateForm(int processID,string url)
            : this()
        {
            this.processID = processID;

            service = new Update.Service.Service();
            service.Url = url;
        }

        /// <summary>
        /// 更新文件列表
        /// </summary>
        private List<UpdateFile> list;

        /// <summary>
        /// 服务器对象
        /// </summary>
        private Service.Service service;
        /// <summary>
        /// 主程序目录
        /// </summary>
        private string mainPath;

        Thread thread;

        /// <summary>
        /// 自动升级程序窗体加载事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UpdateForm_Load(object sender, EventArgs e)
        {
            //显示在屏幕中间
            int SW = Screen.PrimaryScreen.Bounds.Width;
            int SH = Screen.PrimaryScreen.Bounds.Height;

            this.Top = (SH - this.Height) / 2;
            this.Left = (SW - this.Left) / 2;

            //获取要更新的主进程    
            System.Diagnostics.Process mainProcess = System.Diagnostics.Process.GetProcessById(processID);
            if (mainProcess != null)
                mainProcess.Kill();

            mainPath = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().ManifestModule.FullyQualifiedName);

            //获取配置文件
            XmlDocument doc = new XmlDocument();
            doc.Load(string.Format(@"{0}\{1}", mainPath ,"update.xml"));

            XmlElement root = doc.DocumentElement;
            XmlNode updateNode = root.SelectSingleNode("filelist");
            string path = updateNode.Attributes["sourcepath"].InnerText;

            list = new List<UpdateFile>();
            UpdateFile uFile;
            ///循环
            for (int i = 0; i < updateNode.ChildNodes.Count; i++)
            {
                uFile = new UpdateFile();
                uFile.Path = path; 
                uFile.Name = updateNode.ChildNodes[i].ChildNodes[0].InnerText;
                uFile.Size = Convert.ToInt32( updateNode.ChildNodes[i].ChildNodes[1].InnerText);
                uFile.Version = updateNode.ChildNodes[i].ChildNodes[2].InnerText;
                list.Add(uFile);
            }

            thread = new Thread(new ThreadStart(DownLoadFile));
            thread.Start();
        }

        /// <summary>
        /// 下载文件
        /// </summary>
        private void DownLoadFile()
        {
            if (list == null)
                return;
            foreach (UpdateFile uFile in list)
            {
                //更新当前名称
                UpdateFileName ufn = new UpdateFileName(UpdateCurrentFileName);
                this.Invoke(ufn, uFile.Name);
                //判断目标文件是否存在，如果存在删除
                string filePath = string.Format(@"{0}\{1}",mainPath,uFile.Name);
                if (File.Exists(filePath))
                {
                    File.Delete(filePath);                
                }

                ///获取文件字节数组
                byte [] array = service.GetUpdateFile(string.Format(@"{0}\{1}",uFile.Path,uFile.Name));
                if (array == null)
                {
                    MessageBox.Show(string.Format("下载{0}文件失败！", uFile.Name));
                    continue;
                }
                //创建新文件
                FileStream fs = new FileStream(filePath, FileMode.Create, FileAccess.Write);
                BinaryWriter bw = new BinaryWriter(fs);
                bw.Write(array);
                bw.Close();
                fs.Close();
            }

            MessageBox.Show("更新完成");

            try
            {
                //重新打开应用程序
                UpdateFileName ufn = new UpdateFileName(UpdateCurrentFileName);
                this.Invoke(ufn, "正在重新启动应用程序...");
                System.Diagnostics.Process.Start(string.Format(@"{0}\{1}",mainPath, "HTApp.exe"), "");
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            //退出更新程序
            this.Invoke(new ExitApplication(Application.Exit));
        }

        /// <summary>
        /// 更新名称
        /// </summary>
        /// <param name="name"></param>
        private void UpdateCurrentFileName(string name)
        {
            lblCurrentName.Text = name;
        }

        /// <summary>
        /// 更新图标
        /// </summary>
        private void ProgressMove()
        {
            if (this.InvokeRequired)
            {
                base.Invoke(new UpdateProcessMove(this.ProgressMove));
            }
            else if (lblProcess.Text == "|")
                lblProcess.Text = "--";
            else
                lblProcess.Text = "|";         
        }


        /// <summary>
        /// 时间
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timer_Tick(object sender, EventArgs e)
        {
            ProgressMove();
        }
    }

    public class UpdateFile
    {
        public string Path { get; set; }

        public string Name { get; set; }

        public int Size { get; set; }

        public string Version { get; set; }
    }
}