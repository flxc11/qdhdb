using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CNVP.Model;
using CNVP.Config;
using System.Data;
using CNVP.Framework.Helper;

namespace CNVP.Data
{
    public class NewsInfoData
    {
        #region 增加新闻
        /// <summary>
        /// 增加新闻
        /// </summary>
        /// <param name="model"></param>
        public int AddNewsInfo(NewsInfoModel model)
        {
            string StrSql = "Insert Into T_NewsInfo (Address,Phone,Recommend,Pop,Complex,Environment,PersonPay,Taste,Facility,Service,ColumnID,Health,Park,Box,Bus,Coordinate,Wifi,ScenicLevel,ScenicDes,OperateTime,Httpurl,NewsTitle,OrderID,NewsState,bz,NewsContent,NewsLogourl,Createtime,AdminID,AdminID2,Audittime) Values (@Address,@Phone,@Recommend,@Pop,@Complex,@Environment,@PersonPay,@Taste,@Facility,@Service,@ColumnID,@Health,@Park,@Box,@Bus,@Coordinate,@Wifi,@ScenicLevel,@ScenicDes,@OperateTime,@Httpurl,@NewsTitle,@OrderID,@NewsState,@bz,@NewsContent,@NewsLogourl,@Createtime,@AdminID,@AdminID2,@Audittime);";
            IDataParameter[] Param = new IDataParameter[]{
                DbHelper.MakeParam("@Address", model.Address) ,            
                        DbHelper.MakeParam("@Phone", model.Phone) ,            
                        DbHelper.MakeParam("@Recommend", model.Recommend) ,            
                        DbHelper.MakeParam("@Pop", model.Pop) ,            
                        DbHelper.MakeParam("@Complex", model.Complex) ,            
                        DbHelper.MakeParam("@Environment", model.Environment) ,            
                        DbHelper.MakeParam("@PersonPay", model.PersonPay) ,            
                        DbHelper.MakeParam("@Taste", model.Taste) ,            
                        DbHelper.MakeParam("@Facility", model.Facility) ,            
                        DbHelper.MakeParam("@Service", model.Service) ,            
                        DbHelper.MakeParam("@ColumnID", model.ColumnID) ,            
                        DbHelper.MakeParam("@Health", model.Health) ,            
                        DbHelper.MakeParam("@Park", model.Park) ,            
                        DbHelper.MakeParam("@Box", model.Box) ,            
                        DbHelper.MakeParam("@Bus", model.Bus) ,            
                        DbHelper.MakeParam("@Coordinate", model.Coordinate) ,            
                        DbHelper.MakeParam("@Wifi", model.Wifi) ,            
                        DbHelper.MakeParam("@ScenicLevel", model.ScenicLevel) ,            
                        DbHelper.MakeParam("@ScenicDes", model.ScenicDes) ,            
                        DbHelper.MakeParam("@OperateTime", model.OperateTime) ,            
                        DbHelper.MakeParam("@Httpurl", model.Httpurl) ,            
                        DbHelper.MakeParam("@NewsTitle", model.NewsTitle) ,            
                        DbHelper.MakeParam("@OrderID", model.OrderID) ,            
                        DbHelper.MakeParam("@NewsState", model.NewsState) ,            
                        DbHelper.MakeParam("@bz", model.bz) ,            
                        DbHelper.MakeParam("@NewsContent", model.NewsContent) ,            
                        DbHelper.MakeParam("@NewsLogourl", model.NewsLogourl) ,            
                        DbHelper.MakeParam("@Createtime", model.Createtime) ,            
                        DbHelper.MakeParam("@AdminID", model.AdminId) ,            
                        DbHelper.MakeParam("@AdminID2", model.AdminId2) ,            
                        DbHelper.MakeParam("@Audittime", DBNull.Value)
            };
            return DbHelper.ExecuteSqlGetMaxID(StrSql, Param);
        }
        #endregion

        #region 编辑新闻
        /// <summary>
        /// 编辑新闻
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int EditNewsInfo(NewsInfoModel model)
        {
            string StrSql = "Update T_NewsInfo Set Address = @Address ,Phone = @Phone ,Recommend = @Recommend ,Pop = @Pop ,Complex = @Complex ,Environment = @Environment ,PersonPay = @PersonPay ,Taste = @Taste ,Facility = @Facility ,Service = @Service ,ColumnID = @ColumnID ,Health = @Health ,Park = @Park ,Box = @Box ,Bus = @Bus ,Coordinate = @Coordinate ,Wifi = @Wifi ,ScenicLevel = @ScenicLevel ,ScenicDes = @ScenicDes ,OperateTime = @OperateTime ,Httpurl = @Httpurl ,NewsTitle = @NewsTitle ,OrderID = @OrderID ,NewsState = @NewsState ,bz = @bz ,NewsContent = @NewsContent ,NewsLogourl = @NewsLogourl ,Createtime = @Createtime Where NewsID=@NewsID";
            IDataParameter[] Param = new IDataParameter[]{
                        DbHelper.MakeParam("@Address", model.Address) ,            
                        DbHelper.MakeParam("@Phone", model.Phone) ,            
                        DbHelper.MakeParam("@Recommend", model.Recommend) ,            
                        DbHelper.MakeParam("@Pop", model.Pop) ,            
                        DbHelper.MakeParam("@Complex", model.Complex) ,            
                        DbHelper.MakeParam("@Environment", model.Environment) ,            
                        DbHelper.MakeParam("@PersonPay", model.PersonPay) ,            
                        DbHelper.MakeParam("@Taste", model.Taste) ,            
                        DbHelper.MakeParam("@Facility", model.Facility) ,            
                        DbHelper.MakeParam("@Service", model.Service) ,            
                        DbHelper.MakeParam("@ColumnID", model.ColumnID) ,            
                        DbHelper.MakeParam("@Health", model.Health) ,            
                        DbHelper.MakeParam("@Park", model.Park) ,            
                        DbHelper.MakeParam("@Box", model.Box) ,            
                        DbHelper.MakeParam("@Bus", model.Bus) ,            
                        DbHelper.MakeParam("@Coordinate", model.Coordinate) ,            
                        DbHelper.MakeParam("@Wifi", model.Wifi) ,            
                        DbHelper.MakeParam("@ScenicLevel", model.ScenicLevel) ,            
                        DbHelper.MakeParam("@ScenicDes", model.ScenicDes) ,            
                        DbHelper.MakeParam("@OperateTime", model.OperateTime) ,            
                        DbHelper.MakeParam("@Httpurl", model.Httpurl) ,            
                        DbHelper.MakeParam("@NewsTitle", model.NewsTitle) ,            
                        DbHelper.MakeParam("@OrderID", model.OrderID) ,            
                        DbHelper.MakeParam("@NewsState", model.NewsState) ,            
                        DbHelper.MakeParam("@bz", model.bz) ,            
                        DbHelper.MakeParam("@NewsContent", model.NewsContent) ,            
                        DbHelper.MakeParam("@NewsLogourl", model.NewsLogourl) ,            
                        DbHelper.MakeParam("@Createtime", model.Createtime) ,                     
                        DbHelper.MakeParam("@NewsID",model.NewsID)
            };
            return DbHelper.ExecuteNonQuery(StrSql, Param);
        }
        #endregion

        #region 删除栏目
        /// <summary>
        /// 删除栏目
        /// </summary>
        /// <param name="model"></param>
        public void DeleteNewsInfo(NewsInfoModel model)
        {
            string StrSql = "Delete From T_NewsInfo where NewsID=@NewsID";
            IDataParameter[] Param = new IDataParameter[]{
                DbHelper.MakeParam("@NewsID",model.NewsID)
            };
            DbHelper.ExecuteNonQuery(StrSql,Param);
        }
        #endregion

        #region 获取信息
        /// <summary>
        /// 获取信息
        /// </summary>
        /// <param name="UserID"></param>
        /// <returns></returns>
        public NewsInfoModel GetNewsInfo(int NewsID)
        {
            string StrSql = "Select * From T_NewsInfo Where NewsID=@NewsID";
            IDataParameter[] Param = new IDataParameter[] { 
                DbHelper.MakeParam("@NewsID",NewsID)
            };
            return DbHelper.ExecuteReader<NewsInfoModel>(StrSql, Param);
        }
        #endregion

        #region 新闻列表
        public List<NewsInfoModel> GetNewsList()
        {
            string StrSql = "Select * from T_NewsInfo order by NewsID Desc";
            return DbHelper.ExecuteTable<NewsInfoModel>(StrSql);
        }
        #endregion

        #region 带条件的新闻列表
        public List<NewsInfoModel> GetNewsList(string Sql)
        {
            string StrSql = "Select * from T_NewsInfo where 1=1 " + Sql + " order by NewsID Desc";
            return DbHelper.ExecuteTable<NewsInfoModel>(StrSql);
        }

        public List<NewsInfoModel> GetNewsList(string Sql,string Num)
        {
            string StrSql = "Select top " + Num + " * from T_NewsInfo where 1=1 " + Sql + " order by NewsID Desc";
            return DbHelper.ExecuteTable<NewsInfoModel>(StrSql);
        }
        #endregion
        /// <summary>
        /// 读取信息
        /// </summary>
        /// <param name="ColumnID"></param>
        /// <param name="FirstRow"></param>
        /// <param name="EndRow"></param>
        /// <returns></returns>
        public DataTable GetNewsList(string ColumnID, int FirstRow, int EndRow)
        {
            string StrSql = string.Format("Select Top {0} * From T_NewsInfo Where ColumnID In (@ColumnID) And NewsState=1", EndRow - FirstRow + 1);
            if (FirstRow > 1)
            {
                StrSql += string.Format(" And NewsID Not In (Select Top {0} NewsID From T_NewsInfo Where ColumnID In(@ColumnID) And NewsState=1 Order By OrderID Desc)", FirstRow - 1);
            }
            StrSql += " Order By OrderID Desc";
            StrSql = StrSql.Replace("@ColumnID", ColumnID);
            return DbHelper.ExecuteTable(StrSql, null);
        }
        #region 更新阅读次数
        /// <summary>
        /// 更新阅读次数
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        //public int updateClick(NewsInfoModel model)
        //{
        //    string StrSql = "Update T_NewsInfo Set Clicks=@Clicks Where NewsID=@NewsID";
        //    IDataParameter[] Param = new IDataParameter[]{
        //        DbHelper.MakeParam("@Clicks",model.Clicks),
        //        DbHelper.MakeParam("@NewsID",model.NewsId),
        //    };
        //    return DbHelper.ExecuteNonQuery(StrSql, Param);
        //}
        #endregion

        #region 审核
        public void ISAudit(NewsInfoModel model)
        {
            string StrSql = "Update " + "T_NewsInfo set NewsState=@NewsState Where NewsID=@ID";
            IDataParameter[] Param = new IDataParameter[] { 
                DbHelper.MakeParam("@NewsState",model.NewsState),
                DbHelper.MakeParam("@ID",model.NewsID)
            };
            DbHelper.ExecuteNonQuery(StrSql, Param);
        }
        #endregion

        /// <summary>
        /// 栏目排序
        /// </summary>
        /// <param name="NewsID"></param>
        /// <param name="OrderID"></param>
        public void NewsInfoEditOrder(int NewsID, int OrderID)
        {
            string StrSql = "Update T_NewsInfo Set OrderID=@OrderID Where NewsID=@NewsID";
            IDataParameter[] param = {  
                                        DbHelper.MakeParam("@OrderID",OrderID),
                                        DbHelper.MakeParam("@NewsID",NewsID)
                                   };
            DbHelper.ExecuteNonQuery(StrSql, param);
        }
    }
}
