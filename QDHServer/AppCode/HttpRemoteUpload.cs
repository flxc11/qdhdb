using System;
using System.Collections.Generic;
//using System.Linq;
using System.Web;
using System.Net;

namespace QDHServer.AppCode
{
    public class HttpRemoteUpload : RemoteUpload
    {
        /// <summary>
        /// Http文件上传
        /// </summary>
        /// <param name="fileData">文件数据</param>
        /// <param name="fileNamePath">文件名</param>
        /// <param name="urlString">远程服务器地址</param>
        public HttpRemoteUpload(byte[] fileData, string fileNamePath, string urlString)
            : base(fileData, fileNamePath, urlString)
        {
        }
        public override bool UploadFile()
        {
            byte[] postData;
            try
            {
                postData = this.FileData1;
                using (WebClient client = new WebClient())
                {
                    client.Credentials = CredentialCache.DefaultCredentials;
                    client.Headers.Add("Content-Type", "application/x-www-form-urlencoded");
                    client.Headers.Add("User-Agent", "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.1; SV1)");
                    client.Headers.Add("Accept-Language: zh-cn");
                    client.Headers.Add("Accept: */*");
                    client.UploadData(this.UrlString1, "PUT", postData);
                }
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception("文件上传失败", ex.InnerException);

            }
        }
    }
}
