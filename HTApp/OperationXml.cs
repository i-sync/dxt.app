using System;
using System.Collections.Generic;
using System.Text;

using System.Xml;
using System.Data;

namespace HTApp
{
    public class OperationXml
    {
        /// <summary>
        /// 找到U8.xml的路径
        /// </summary>
        /// <returns>路径</returns>
        private static string searchXml()
        {
            string filePath = System.IO.Path.GetDirectoryName
                (System.Reflection.Assembly.GetExecutingAssembly().ManifestModule.FullyQualifiedName);
            filePath = System.IO.Path.Combine(filePath, "U8.xml");
            return filePath;
        }

        static public DataTable DataGridhead(string gridname)
        {
            DataTable dt = new DataTable();
            DataRow dr = null;
            dt.Columns.Add("id", typeof(string));
            dt.Columns.Add("width", typeof(string));
            XmlDocument xd = new XmlDocument();
            //xd.Load(@searchXml());
            xd.Load(searchXml());
            XmlNodeList xnl = xd.FirstChild.SelectSingleNode("Grid").ChildNodes;
            foreach (XmlNode xn in xnl)
            {
                XmlElement xe = (XmlElement)xn;
                if (xe.GetAttribute("frmname") != gridname)
                {
                    continue;
                }
                else
                {
                    foreach (XmlNode xnhead in xn.ChildNodes)
                    {
                        dr = dt.NewRow();
                        dr["id"] = xnhead.SelectSingleNode("id").InnerText;
                        dr["width"] = xnhead.SelectSingleNode("width").InnerText;
                        dt.Rows.Add(dr);
                    }
                    break;
                }
            }
            return dt;
        }

        /// <summary>
        /// 返回配置参数
        /// </summary>
        /// <param name="pro">版块名</param>
        /// <param name="par">参数名</param>
        /// <returns>具体数据</returns>
        static public string ConfigStr(string pro, string par)
        {
            XmlDocument xd = new XmlDocument();
            xd.Load(searchXml());
            return (xd.FirstChild.SelectSingleNode(pro).SelectSingleNode(par).InnerText);
        }

        //服务器的路径
        static public string ServiceUrl()
        {
            XmlDocument xd = new XmlDocument();
            xd.Load(searchXml());
            return xd.FirstChild.SelectSingleNode("webservice").InnerText;
        }

        static public int setConfig(string pro, string par, string val)
        {
            XmlDocument xd = new XmlDocument();
            string path = searchXml();
            xd.Load(path);
            try
            {
                xd.FirstChild.SelectSingleNode(pro).SelectSingleNode(par).InnerText = val;
                xd.Save(path);
                return 0;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        static public string getConfig(string pro, string par)
        {
            XmlDocument xd = new XmlDocument();
            xd.Load(searchXml());
            try
            {
                string str = xd.FirstChild.SelectSingleNode(pro).SelectSingleNode(par).InnerText;
                xd = null;
                return str;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
        }

        static public int setParConfig(string pro, string val)
        {
            XmlDocument xd = new XmlDocument();
            string xml = searchXml();
            xd.Load(xml);
            try
            {
                xd.FirstChild.SelectSingleNode(pro).InnerText = val;

                xd.Save(xml);
                xd = null;
                return 0;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
        }
    }
}
