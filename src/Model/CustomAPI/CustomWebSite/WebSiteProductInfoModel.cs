
/**********************************************
 * 类作用   商品扩展表实体类层
 * 创建人   xz
 * 创建时间 2010-3-22 10:37:19 
 ***********************************************/

using System;

namespace XBase.Model.CustomAPI.CustomWebSite
{
    /// <summary>
    /// 商品扩展表实体类
    /// </summary>
    [Serializable]
    public class WebSiteProductInfoModel
    {
        #region 字段

        private Nullable<int> iD = null; //主键，自动生成
        private Nullable<int> productID = null; //物品ID,对应表Officedba.ProductInfo
        private string description = String.Empty; //商品描述
        private Nullable<decimal> price = null; //商品价格
        private string imgDIr = String.Empty; //图片路径
        private string status = String.Empty; //状态1 启用0 禁用
        private string discountStatus = String.Empty; //折扣状态1 启用0 禁用
        
        #endregion
        

        #region 方法
        
        /// <summary>
        /// 默认构造函数
        /// </summary>
        public WebSiteProductInfoModel ()
        {
        }

        /// <summary>
        /// 带参数的构造函数
        /// </summary>
        ///<param name="iD">主键，自动生成</param>
        ///<param name="productID">物品ID,对应表Officedba.ProductInfo</param>
        ///<param name="description">商品描述</param>
        ///<param name="price">商品价格</param>
        ///<param name="imgDIr">图片路径</param>
        ///<param name="status">状态1 启用0 禁用</param>
        ///<param name="discountStatus">折扣状态1 启用0 禁用</param>
        public WebSiteProductInfoModel(
                    int iD,
                    int productID,
                    string description,
                    decimal price,
                    string imgDIr,
                    string status,
                    string discountStatus)
        {
            this.iD = iD; //主键，自动生成
            this.productID = productID; //物品ID,对应表Officedba.ProductInfo
            this.description = description; //商品描述
            this.price = price; //商品价格
            this.imgDIr = imgDIr; //图片路径
            this.status = status; //状态1 启用0 禁用
            this.discountStatus = discountStatus; //折扣状态1 启用0 禁用
        }
        
        #endregion
        
        
        #region 属性
        
        /// <summary>
        /// 主键，自动生成
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
        /// 物品ID,对应表Officedba.ProductInfo
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
        /// 商品描述
        /// </summary>
        public string Description
        {
            get
            {
                return description;
            }
            set
            {
                description = value;
            }
        }
        
        /// <summary>
        /// 商品价格
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
        /// 图片路径
        /// </summary>
        public string ImgDIr
        {
            get
            {
                return imgDIr;
            }
            set
            {
                imgDIr = value;
            }
        }
        
        /// <summary>
        /// 状态1 启用0 禁用
        /// </summary>
        public string Status
        {
            get
            {
                return status;
            }
            set
            {
                status = value;
            }
        }
        
        /// <summary>
        /// 折扣状态1 启用0 禁用
        /// </summary>
        public string DiscountStatus
        {
            get
            {
                return discountStatus;
            }
            set
            {
                discountStatus = value;
            }
        }

        #endregion
    }
}


