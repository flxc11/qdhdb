using System;
using System.Collections.Generic;
//using System.Linq;
using System.Web;
using System.Net;
using System.Text;
using System.IO;

namespace QDHServer.AppCode
{
    public class downFiles
    {
        /// <summary>
        /// 下载文件
        /// </summary>
        /// <param name="url">下载地址</param>
        /// <param name="fileName">下载名字</param>
        /// <returns></returns>
        public bool downFile(string url, string fileName)
        {
            WebClient client = new WebClient();
            try
            {
                WebRequest myWebRequest = WebRequest.Create(url);
            }
            catch (Exception)
            {
                return false;
            }
            try
            {
                client.DownloadFile(url, fileName);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

      
        public string GetHttpData(string url)
        {
            Encoding code = Encoding.GetEncoding("UTF-8");
            StreamReader sr = null;
            string str = null;
            //读取远程路径
            WebRequest temp = WebRequest.Create(url);
            WebResponse myTemp = temp.GetResponse();
            sr = new StreamReader(myTemp.GetResponseStream(), code);
            //读取
            try
            {
                sr = new StreamReader(myTemp.GetResponseStream(), code);
                str = sr.ReadToEnd();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                sr.Close();
            }
            return str;
        }


        //判断服务器端是否存在文件
        public int GetUrlError(string curl)
        {
            int num = 200;
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(new Uri(curl));
            ServicePointManager.Expect100Continue = false;
            try
            {
                ((HttpWebResponse)request.GetResponse()).Close();
            }
            catch (WebException exception)
            {
                if (exception.Status != WebExceptionStatus.ProtocolError)
                {
                    return num;
                }
                if (exception.Message.IndexOf("500 ") > 0)
                {
                    return 500;
                }
                if (exception.Message.IndexOf("401 ") > 0)
                {
                    return 401;
                }
                if (exception.Message.IndexOf("404") > 0)
                {
                    num = 404;
                }
            }
            return num;
        }
    }
}