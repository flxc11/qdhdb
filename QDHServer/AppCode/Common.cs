using System;
using System.Collections.Generic;
//using System.Linq;
using System.Web;
using System.IO;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Security.Cryptography;
using QDHConfig;

namespace QDHServer.AppCode
{
    public class Common
    {
        public static string connectionString =BaseConfig.DataConnectString();
        private string result;

        public string Result
        {
            get { return result; }
            set { result = value; }
        }
        private string errMsg;

        public string ErrMsg
        {
            get { return errMsg; }
            set { errMsg = value; }
        }

        public SqlConnection _DataConnect;
        public string FConnectString;

        public Common()
        {
            this.FConnectString = BaseConfig.DataConnectString();
            this._DataConnect = new SqlConnection(this.FConnectString);
        }

        public void CloseConnect()
        {
            this._DataConnect.Close();
        }

        public SqlConnection GetConnection()
        {
            return this._DataConnect;
        }

        /// <summary>
        /// 通用查询函数
        /// </summary>
        /// <param name="TableName">表名</param>
        /// <param name="WhereValue">条件</param>
        /// <param name="SQLValue">SQL语句</param>
        /// <param name="Option">是否需要条件及完全使用SQL语句  0 不需要条件 1 需要条件  2 完全使用SQL语句</param>
        /// <returns></returns>
        public DataSet GetGeneralTable(string TableName, string WhereValue, string SQLValue, int Option)
        {
            DataSet ds = new DataSet();
            string strSQL = "";
            try
            {
                switch (Option)
                {
                    case 0:
                        strSQL = "select * from " + TableName;
                        break;
                    case 1:
                        strSQL = "select * from " + TableName + " where " + WhereValue;
                        break;
                    case 2:
                        strSQL = SQLValue;
                        break;
                }
                SqlDataAdapter oda = new SqlDataAdapter(strSQL, this._DataConnect);
                oda.Fill(ds);
            }
            catch (Exception ex)
            {
                this.result = "9999";
                this.errMsg = ex.ToString();
            }
            this._DataConnect.Close();
            return ds;
        }

        //广告日志  
        public void writeOfTxt(string Mome)
        {
            try
            {

                string f;
                f = DateTime.Now.ToString("yyyyMM");

                string fday;
                fday = DateTime.Now.ToString("yyyyMMdd");

                string strDirectory = HttpContext.Current.Server.MapPath("~") + "\\logs\\" + "广告" + "\\";

                if (!Directory.Exists(strDirectory))
                {
                    Directory.CreateDirectory(strDirectory);
                }

                string Filename = strDirectory + "\\" + fday + ".txt";

                FileStream fs = new FileStream(@Filename, FileMode.OpenOrCreate, FileAccess.Write, FileShare.Write);
                StreamWriter m_streamWriter = new StreamWriter(fs);
                m_streamWriter.BaseStream.Seek(0, SeekOrigin.End);
                m_streamWriter.WriteLine("" + Mome + "," + DateTime.Now.ToString() + ""); //写入文件
                m_streamWriter.Flush();
                m_streamWriter.Close();
                fs.Close();
            }
            catch
            {
                return;
            }
        }

        //查询
        public static DataSet Query(string SQLString)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                DataSet ds = new DataSet();

                try
                {
                    connection.Open();
                    SqlDataAdapter command = new SqlDataAdapter(SQLString, connection);
                    command.Fill(ds, "ds");
                }
                catch (SqlException ex)
                {
                    throw new Exception(ex.Message);
                }
                finally
                {
                    connection.Close();
                }
                return ds;
            }
        }

        /// <summary>
        /// 删除文件夹下的东西
        /// </summary>
        /// <param name="Dir">目录</param>
        /// <returns></returns>
        public bool deleteFile(string Dir)
        {
            if (Directory.Exists(Dir))
            {
                foreach (string d in Directory.GetFileSystemEntries(Dir))
                {
                    if (File.Exists(d))
                    {
                        File.Delete(d);
                    }
                }
                Directory.Delete(Dir, true);
            }
            return true;
        }

        /// 删除指定文件夹下的文件
        /// </summary>
        /// <param name="dir">目录路径</param>
        /// <param name="delname">待删除文件或文件夹名称</param>
        public void DeleteFolder(string dir, string delname)
        {
            if (Directory.Exists(dir))
            {
                foreach (string d in Directory.GetFileSystemEntries(dir))
                {
                    string tmpd = d.Substring(0, d.LastIndexOf(@"\")) + "\\" + delname;
                    if (Directory.Exists(d))
                    {
                        if (d == tmpd)
                            Directory.Delete(d, true);
                        else
                            DeleteFolder(d, delname);//递归删除子文件夹   
                    }
                    else if (File.Exists(d))
                    {
                        if (d == tmpd)
                            File.Delete(d);
                    }
                }
            }
        }

        /// <summary>
        /// 大写常规MD5加密，字节码编码格式为指定格式。
        /// </summary>
        /// <param name="sourceString">待加密明文字符串</param>
        /// <param name="byteEncoding">字节码编码名称</param>
        /// <returns></returns>
        public static string NormalMD5UpperEncrypt(string sourceString, System.Text.Encoding byteEncoding)
        {
            byte[] bytSourceString = byteEncoding.GetBytes(sourceString);

            MD5 md5 = new MD5CryptoServiceProvider();

            byte[] bytEncryptedPassword = md5.ComputeHash(bytSourceString);
            StringBuilder sbEncryptedPassword = new StringBuilder();

            for (int i = 0; i < bytEncryptedPassword.Length; i++)
            {
                sbEncryptedPassword.AppendFormat("{0:X2}", bytEncryptedPassword[i]);
            }
            return sbEncryptedPassword.ToString().ToUpper();
        }

        /// <summary>
        /// 小写常规MD5加密，字节码编码格式为指定格式。
        /// </summary>
        /// <param name="sourceString">待加密明文字符串</param>
        /// <param name="byteEncoding">字节码编码名称</param>
        /// <returns></returns>
        public static string NormalMD5LowerEncrypt(string sourceString, System.Text.Encoding byteEncoding)
        {
            byte[] bytSourceString = byteEncoding.GetBytes(sourceString);

            MD5 md5 = new MD5CryptoServiceProvider();

            byte[] bytEncryptedPassword = md5.ComputeHash(bytSourceString);
            StringBuilder sbEncryptedPassword = new StringBuilder();

            for (int i = 0; i < bytEncryptedPassword.Length; i++)
            {
                sbEncryptedPassword.AppendFormat("{0:X2}", bytEncryptedPassword[i]);
            }
            return sbEncryptedPassword.ToString().ToLower();
        }
    }
}