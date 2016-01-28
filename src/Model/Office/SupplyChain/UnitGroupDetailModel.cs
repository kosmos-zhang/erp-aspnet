/**********************************************
 * 类作用   计量单位组明细实体类层
 * 创建人   xz
 * 创建时间 2010-3-11 16:50:36 
 ***********************************************/

using System;

namespace XBase.Model.Office.SupplyChain
{
    /// <summary>
    /// 计量单位组明细实体类
    /// </summary>
    [Serializable]
    public class UnitGroupDetailModel
    {
        #region 字段

        private Nullable<int> iD = null; //主键,自动生成
        private string companyCD = String.Empty; //企业代码
        private string groupUnitNo = String.Empty; //计量单位组编号
        private Nullable<int> unitID = null; //计量单位ID(对应计量单位表)
        private Nullable<decimal> exRate = null; //换算比率(相对于基本计量单位，也就是一个计量单位=少个基本计量单位，如：一箱为10块，则此处换算率为10)
        private string remark = String.Empty; //备注

        #endregion


        #region 方法

        /// <summary>
        /// 默认构造函数
        /// </summary>
        public UnitGroupDetailModel()
        {
        }

        /// <summary>
        /// 带参数的构造函数
        /// </summary>
        ///<param name="iD">主键,自动生成</param>
        ///<param name="companyCD">企业代码</param>
        ///<param name="groupUnitNo">计量单位组编号</param>
        ///<param name="unitID">计量单位ID(对应计量单位表)</param>
        ///<param name="exRate">换算比率(相对于基本计量单位，也就是一个计量单位=少个基本计量单位，如：一箱为10块，则此处换算率为10)</param>
        ///<param name="remark">备注</param>
        public UnitGroupDetailModel(
                    int iD,
                    string companyCD,
                    string groupUnitNo,
                    int unitID,
                    decimal exRate,
                    string remark)
        {
            this.iD = iD; //主键,自动生成
            this.companyCD = companyCD; //企业代码
            this.groupUnitNo = groupUnitNo; //计量单位组编号
            this.unitID = unitID; //计量单位ID(对应计量单位表)
            this.exRate = exRate; //换算比率(相对于基本计量单位，也就是一个计量单位=少个基本计量单位，如：一箱为10块，则此处换算率为10)
            this.remark = remark; //备注
        }

        #endregion


        #region 属性

        /// <summary>
        /// 主键,自动生成
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
        /// 企业代码
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
        /// 计量单位组编号
        /// </summary>
        public string GroupUnitNo
        {
            get
            {
                return groupUnitNo;
            }
            set
            {
                groupUnitNo = value;
            }
        }

        /// <summary>
        /// 计量单位ID(对应计量单位表)
        /// </summary>
        public Nullable<int> UnitID
        {
            get
            {
                return unitID;
            }
            set
            {
                unitID = value;
            }
        }

        /// <summary>
        /// 换算比率(相对于基本计量单位，也就是一个计量单位=少个基本计量单位，如：一箱为10块，则此处换算率为10)
        /// </summary>
        public Nullable<decimal> ExRate
        {
            get
            {
                return exRate;
            }
            set
            {
                exRate = value;
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