using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XBase.Model.CustomAPI.CustomWebSite
{
    public class WebSiteSellOrderModel
    {
        #region 字段
        private int _id;
        private string _companycd;
        private int _customid;
        private int _loginid;
        private string _orderno;
        private string _title;
        private DateTime _orderdate;
        private DateTime _consignmentdate;
        private string _status;
        #endregion

        #region 属性
        /// <summary>
        /// 主键
        /// </summary>
        public int ID
        {
            get { return _id; }
            set { _id = value; }
        }

        /// <summary>
        /// 公司编码
        /// </summary>
        public string CompanyCD
        {
            set {  _companycd=value; }
            get { return _companycd; }
        }


        public int CustomID
        {
            get { return _customid; }
            set { _customid = value; }
        }
        /// <summary>
        /// 订单编号
        /// </summary>
        public string OrderNo
        {
            get { return _orderno; }
            set { _orderno = value; }
        }
        /// <summary>
        /// 登录用户ID
        /// </summary>
        public int LoginID
        {
            get { return _loginid; }
            set { _loginid = value; }
        }

        /// <summary>
        /// 订单主题
        /// </summary>
        public string Title
        {
            get { return _title; }
            set { _title = value; }
        }
        /// <summary>
        /// 下单日期
        /// </summary>
        public DateTime OrderDate
        {
            get { return _orderdate; }
            set { _orderdate = value; }
        }

        /// <summary>
        /// 最迟发货时间
        /// </summary>
        public DateTime ConsignmentDate
        {
            get { return _consignmentdate; }
            set { _consignmentdate = value; }
        }
        /// <summary>
        /// 订单状态 1：待处理，2：处理中，3：发货中，4：结单
        /// </summary>
        public string Status
        {
            get { return _status; }
            set { _status = value; }
        }
        #endregion
    }
}
