using System;
using System.Linq;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Update
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [MTAThread]
        static void Main(string [] args)
        {
            try
            {
                if (args.Length!=2)
                {
                    MessageBox.Show("不能从这里启动");
                    return;
                }
                int processID = Convert.ToInt32(args[0]);
                string url = args[1];
                Application.Run(new UpdateForm(processID, url));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}