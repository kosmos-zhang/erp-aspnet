
/**********************************************
 * 类作用   门店库存流水账表实体类层
 * 创建人   xz
 * 创建时间 2010-4-20 15:09:51 
 ***********************************************/

using System;

namespace XBase.Model.Office.SubStoreManager
{
    /// <summary>
    /// 门店库存流水账表实体类
    /// </summary>
    [Serializable]
    public class SubStorageAccountModel
    {
        #region 字段

        private Nullable<int> iD = null; //ID，自动生成
        private string companyCD = String.Empty; //公司编码
        private Nullable<int> deptID = null; //分店ID
        private Nullable<int> billType = null; //单据类型
        private Nullable<int> productID = null; //物品ID
        private string batchNo = String.Empty; //批次
        private string billNo = String.Empty; //单据编号
        private Nullable<DateTime> happenDate = null; //出入库时间
        private Nullable<decimal> happenCount = null; //出入库数量
        private Nullable<decimal> productCount = null; //现有存量
        private Nullable<int> creator = null; //业务操作人(取当前登录人的ID)
        private string pageUrl = String.Empty; //页面链接地址
        private Nullable<decimal> price = null; //单价
        private string remark = String.Empty; //备注
        
        #endregion
        

        #region 方法
        
        /// <summary>
        /// 默认构造函数
        /// </summary>
        public SubStorageAccountModel ()
        {
        }

        /// <summary>
        /// 带参数的构造函数
        /// </summary>
        ///<param name="iD">ID，自动生成</param>
        ///<param name="companyCD">公司编码</param>
        ///<param name="deptID">分店ID</param>
        ///<param name="billType">单据类型</param>
        ///<param name="productID">物品ID</param>
        ///<param name="batchNo">批次</param>
        ///<param name="billNo">单据编号</param>
        ///<param name="happenDate">出入库时间</param>
        ///<param name="happenCount">出入库数量</param>
        ///<param name="productCount">现有存量</param>
        ///<param name="creator">业务操作人(取当前登录人的ID)</param>
        ///<param name="pageUrl">页面链接地址</param>
        ///<param name="price">单价</param>
        ///<param name="remark">备注</param>
        public SubStorageAccountModel(
                    int iD,
                    string companyCD,
                    int deptID,
                    int billType,
                    int productID,
                    string batchNo,
                    string billNo,
                    Nullable<DateTime> happenDate,
                    decimal happenCount,
                    decimal productCount,
                    int creator,
                    string pageUrl,
                    decimal price,
                    string remark)
        {
            this.iD = iD; //ID，自动生成
            this.companyCD = companyCD; //公司编码
            this.deptID = deptID; //分店ID
            this.billType = billType; //单据类型
            this.productID = productID; //物品ID
            this.batchNo = batchNo; //批次
            this.billNo = billNo; //单据编号
            this.happenDate = happenDate; //出入库时间
            this.happenCount = happenCount; //出入库数量
            this.productCount = productCount; //现有存量
            this.creator = creator; //业务操作人(取当前登录人的ID)
            this.pageUrl = pageUrl; //页面链接地址
            this.price = price; //单价
            this.remark = remark; //备注
        }
        
        #endregion
        
        
        #region 属性
        
        /// <summary>
        /// ID，自动生成
        /// </summary>
        public Nullable<int> ID
        {
            get
            {
                return iD;
            }
            set
            {
                iD = value;
            }
        }
        
        /// <summary>
        /// 公司编码
        /// </summary>
        public string CompanyCD
        {
            get
            {
                return companyCD;
            }
            set
            {
                companyCD = value;
            }
        }
        
        /// <summary>
        /// 分店ID
        /// </summary>
        public Nullable<int> DeptID
        {
            get
            {
                return deptID;
            }
            set
            {
                deptID = value;
            }
        }
        
        /// <summary>
        /// 单据类型
        /// </summary>
        public Nullable<int> BillType
        {
            get
            {
                return billType;
            }
            set
            {
                billType = value;
            }
        }
        
        /// <summary>
        /// 物品ID
        /// </summary>
        public Nullable<int> ProductID
        {
            get
            {
                return productID;
            }
            set
            {
                productID = value;
            }
        }
        
        /// <summary>
        /// 批次
        /// </summary>
        public string BatchNo
        {
            get
            {
                return batchNo;
            }
            set
            {
                batchNo = value;
            }
        }
        
        /// <summary>
        /// 单据编号
        /// </summary>
        public string BillNo
        {
            get
            {
                return billNo;
            }
            set
            {
                billNo = value;
            }
        }
        
        /// <summary>
        /// 出入库时间
        /// </summary>
        public Nullable<DateTime> HappenDate
        {
            get
            {
                return happenDate;
            }
            set
            {
                happenDate = value;
            }
        }
        
        /// <summary>
        /// 出入库数量
        /// </summary>
        public Nullable<decimal> HappenCount
        {
            get
            {
                return happenCount;
            }
            set
            {
                happenCount = value;
            }
        }
        
        /// <summary>
        /// 现有存量
        /// </summary>
        public Nullable<decimal> ProductCount
        {
            get
            {
                return productCount;
            }
            set
            {
                productCount = value;
            }
        }
        
        /// <summary>
        /// 业务操作人(取当前登录人的ID)
        /// </summary>
        public Nullable<int> Creator
        {
            get
            {
                return creator;
            }
            set
            {
                creator = value;
            }
        }
        
        /// <summary>
        /// 页面链接地址
        /// </summary>
        public string PageUrl
        {
            get
            {
                return pageUrl;
            }
            set
            {
                pageUrl = value;
            }
        }
        
        /// <summary>
        /// 单价
        /// </summary>
        public Nullable<decimal> Price
        {
            get
            {
                return price;
            }
            set
            {
                price = value;
            }
        }
        
        /// <summary>
        /// 备注
        /// </summary>
        public string Remark
        {
            get
            {
                return remark;
            }
            set
            {
                remark = value;
            }
        }

        #endregion
    }
}


