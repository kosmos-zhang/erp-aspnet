/***********************************************
 * 类作用：   SubStorageManager表数据模板      *
 * 建立人：   宋飞                             *
 * 建立时间： 2009/05/18                       *
 ***********************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XBase.Model.Office.SubStoreManager
{
    /// <summary>
    /// 类名：SubStorageInModel
    /// 描述：SubStorageIn表数据模板
    /// 
    /// 作者：宋飞
    /// 创建时间：2009/05/18
    /// 最后修改时间：2009/06/03
    /// </summary>
    ///
    public class SubStorageInModel
    {
        #region Model
        private int _id;
        private string _companycd;
        private int _deptid;
        private string _inno;
        private string _title;
        private decimal _totalprice;
        private decimal _counttotal;
        private string _intype;
        private string _remark;
        private int _creator;
        private DateTime ? _createdate;
        private string _billstatus;
        private int _confirmor;
        private DateTime ? _confirmdate;
        private DateTime ? _modifieddate;
        private string _modifieduserid;
        /// <summary>
        /// 自动生成
        /// </summary>
        public int ID
        {
            set { _id = value; }
            get { return _id; }
        }
        /// <summary>
        /// 公司编号
        /// </summary>
        public string CompanyCD
        {
            set { _companycd = value; }
            get { return _companycd; }
        }
        /// <summary>
        /// 分店ID
        /// </summary>
        public int DeptID
        {
            set { _deptid = value; }
            get { return _deptid; }
        }
        /// <summary>
        /// 入库单号
        /// </summary>
        public string InNo
        {
            set { _inno = value; }
            get { return _inno; }
        }
        /// <summary>
        /// 主题
        /// </summary>
        public string Title
        {
            set { _title = value; }
            get { return _title; }
        }
        /// <summary>
        /// 入库金额合计
        /// </summary>
        public decimal TotalPrice
        {
            set { _totalprice = value; }
            get { return _totalprice; }
        }
        /// <summary>
        /// 入库数量合计
        /// </summary>
        public decimal CountTotal
        {
            set { _counttotal = value; }
            get { return _counttotal; }
        }
        /// <summary>
        /// 库存初始化标志（0否，1是）
        /// </summary>
        public string InType
        {
            set { _intype = value; }
            get { return _intype; }
        }
        /// <summary>
        /// 备注
        /// </summary>
        public string Remark
        {
            set { _remark = value; }
            get { return _remark; }
        }
        /// <summary>
        /// 制单人
        /// </summary>
        public int Creator
        {
            set { _creator = value; }
            get { return _creator; }
        }
        /// <summary>
        /// 制单日期
        /// </summary>
        public DateTime ? CreateDate
        {
            set { _createdate = value; }
            get { return _createdate; }
        }
        /// <summary>
        /// 单据状态（1制单，5自动结单）
        /// </summary>
        public string BillStatus
        {
            set { _billstatus = value; }
            get { return _billstatus; }
        }
        /// <summary>
        /// 确认人
        /// </summary>
        public int Confirmor
        {
            set { _confirmor = value; }
            get { return _confirmor; }
        }
        /// <summary>
        /// 确认日期
        /// </summary>
        public DateTime ? ConfirmDate
        {
            set { _confirmdate = value; }
            get { return _confirmdate; }
        }
        /// <summary>
        /// 最后更新日期
        /// </summary>
        public DateTime ? ModifiedDate
        {
            set { _modifieddate = value; }
            get { return _modifieddate; }
        }
        /// <summary>
        /// 最后更新用户（对应操作用户表中的UserID）
        /// </summary>
        public string ModifiedUserID
        {
            set { _modifieduserid = value; }
            get { return _modifieduserid; }
        }
        #endregion Model

        #region 扩展属性

        private string extField1 = String.Empty; //扩展属性1
        private string extField2 = String.Empty; //扩展属性2
        private string extField3 = String.Empty; //扩展属性3
        private string extField4 = String.Empty; //扩展属性4
        private string extField5 = String.Empty; //扩展属性5
        private string extField6 = String.Empty; //扩展属性6
        private string extField7 = String.Empty; //扩展属性7
        private string extField8 = String.Empty; //扩展属性8
        private string extField9 = String.Empty; //扩展属性9
        private string extField10 = String.Empty; //扩展属性10

        /// <summary>
        /// 扩展属性1
        /// </summary>
        public string ExtField1
        {
            get
            {
                return extField1;
            }
            set
            {
                extField1 = value;
            }
        }

        /// <summary>
        /// 扩展属性2
        /// </summary>
        public string ExtField2
        {
            get
            {
                return extField2;
            }
            set
            {
                extField2 = value;
            }
        }

        /// <summary>
        /// 扩展属性3
        /// </summary>
        public string ExtField3
        {
            get
            {
                return extField3;
            }
            set
            {
                extField3 = value;
            }
        }

        /// <summary>
        /// 扩展属性4
        /// </summary>
        public string ExtField4
        {
            get
            {
                return extField4;
            }
            set
            {
                extField4 = value;
            }
        }

        /// <summary>
        /// 扩展属性5
        /// </summary>
        public string ExtField5
        {
            get
            {
                return extField5;
            }
            set
            {
                extField5 = value;
            }
        }

        /// <summary>
        /// 扩展属性6
        /// </summary>
        public string ExtField6
        {
            get
            {
                return extField6;
            }
            set
            {
                extField6 = value;
            }
        }

        /// <summary>
        /// 扩展属性7
        /// </summary>
        public string ExtField7
        {
            get
            {
                return extField7;
            }
            set
            {
                extField7 = value;
            }
        }

        /// <summary>
        /// 扩展属性8
        /// </summary>
        public string ExtField8
        {
            get
            {
                return extField8;
            }
            set
            {
                extField8 = value;
            }
        }

        /// <summary>
        /// 扩展属性9
        /// </summary>
        public string ExtField9
        {
            get
            {
                return extField9;
            }
            set
            {
                extField9 = value;
            }
        }

        /// <summary>
        /// 扩展属性10
        /// </summary>
        public string ExtField10
        {
            get
            {
                return extField10;
            }
            set
            {
                extField10 = value;
            }
        }
        #endregion
    }
}
