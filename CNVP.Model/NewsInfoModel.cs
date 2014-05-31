using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CNVP.Model
{
    public class NewsInfoModel
    {
        private int id;

        public int ID
        {
            get { return id; }
            set { id = value; }
        }
        /// <summary>
        /// 主键
        /// </summary>		
        public int NewsID { get; set; }
        /// <summary>
        /// ColumnID
        /// </summary>		
        public int ColumnID { get; set; }
        /// <summary>
        /// NewsTitle
        /// </summary>		
        public string NewsTitle { get; set; }
        /// <summary>
        /// NewsContent
        /// </summary>		
        public string NewsContent { get; set; }
        /// <summary>
        /// NewsLogourl
        /// </summary>		
        public string NewsLogourl { get; set; }
        /// <summary>
        /// Createtime
        /// </summary>		
        public DateTime Createtime { get; set; }
        /// <summary>
        /// AdminID
        /// </summary>		
        public int AdminId { get; set; }
        /// <summary>
        /// AdminID2
        /// </summary>		
        public int AdminId2 { get; set; }
        /// <summary>
        /// Audittime
        /// </summary>		
        public DateTime Audittime { get; set; }
        /// <summary>
        /// Address
        /// </summary>		
        public string Address { get; set; }
        /// <summary>
        /// Phone
        /// </summary>		
        public string Phone { get; set; }
        /// <summary>
        /// Recommend
        /// </summary>		
        public string Recommend { get; set; }
        /// <summary>
        /// Pop
        /// </summary>		
        public string Pop { get; set; }
        /// <summary>
        /// Complex
        /// </summary>		
        public string Complex { get; set; }
        /// <summary>
        /// Environment
        /// </summary>		
        public string Environment { get; set; }
        /// <summary>
        /// PersonPay
        /// </summary>		
        public string PersonPay { get; set; }
        /// <summary>
        /// Taste
        /// </summary>		
        public string Taste { get; set; }
        /// <summary>
        /// Facility
        /// </summary>		
        public string Facility { get; set; }
        /// <summary>
        /// Service
        /// </summary>		
        public string Service { get; set; }
        /// <summary>
        /// Health
        /// </summary>		
        public string Health { get; set; }
        /// <summary>
        /// 车位:0,无;1,有
        /// </summary>		
        public int Park { get; set; }
        /// <summary>
        /// 包厢:0,无;1,有
        /// </summary>		
        public int Box { get; set; }
        /// <summary>
        /// Bus
        /// </summary>		
        public string Bus { get; set; }
        /// <summary>
        /// 经度,纬度
        /// </summary>		
        public string Coordinate { get; set; }
        /// <summary>
        /// Wifi:0,无;1,有
        /// </summary>		
        public int Wifi { get; set; }
        /// <summary>
        /// ScenicLevel
        /// </summary>		
        public int ScenicLevel { get; set; }
        /// <summary>
        /// ScenicDes
        /// </summary>		
        public string ScenicDes { get; set; }
        /// <summary>
        /// OperateTime
        /// </summary>		
        public string OperateTime { get; set; }
        /// <summary>
        /// Httpurl
        /// </summary>		
        public string Httpurl { get; set; }
        /// <summary>
        /// OrderID
        /// </summary>		
        public int OrderID { get; set; }
        /// <summary>
        /// 状态:0:未审核;1:已审核;2:删除
        /// </summary>		
        public int NewsState { get; set; }
        /// <summary>
        /// bz
        /// </summary>		
        public string bz { get; set; }        
    }
}
