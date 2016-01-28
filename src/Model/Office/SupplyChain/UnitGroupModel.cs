/**********************************************
 * 类作用   计量单位组实体类层
 * 创建人   xz
 * 创建时间 2010-3-10 15:48:25 
 ***********************************************/

using System;

namespace XBase.Model.Office.SupplyChain
{
    /// <summary>
    /// 计量单位组实体类
    /// </summary>
    [Serializable]
    public class UnitGroupModel
    {
        #region 字段

        private Nullable<int> iD = null; //主键,自动生成
        private string companyCD = String.Empty; //企业代码
        private string groupUnitNo = String.Empty; //计量单位组编号
        private string groupUnitName = String.Empty; //计量单位组名
        private Nullable<int> baseUnitID = null; //基本计量单位ID(对应计量单位表)
        private string remark = String.Empty; //备注

        #endregion


        #region 方法

        /// <summary>
        /// 默认构造函数
        /// </summary>
        public UnitGroupModel()
        {
        }

        /// <summary>
        /// 带参数的构造函数
        /// </summary>
        ///<param name="iD">主键,自动生成</param>
        ///<param name="companyCD">企业代码</param>
        ///<param name="groupUnitNo">计量单位组编号</param>
        ///<param name="groupUnitName">计量单位组名</param>
        ///<param name="baseUnitID">基本计量单位ID(对应计量单位表)</param>
        ///<param name="remark">备注</param>
        public UnitGroupModel(
                    int iD,
                    string companyCD,
                    string groupUnitNo,
                    string groupUnitName,
                    int baseUnitID,
                    string remark)
        {
            this.iD = iD; //主键,自动生成
            this.companyCD = companyCD; //企业代码
            this.groupUnitNo = groupUnitNo; //计量单位组编号
            this.groupUnitName = groupUnitName; //计量单位组名
            this.baseUnitID = baseUnitID; //基本计量单位ID(对应计量单位表)
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
        /// 计量单位组名
        /// </summary>
        public string GroupUnitName
        {
            get
            {
                return groupUnitName;
            }
            set
            {
                groupUnitName = value;
            }
        }

        /// <summary>
        /// 基本计量单位ID(对应计量单位表)
        /// </summary>
        public Nullable<int> BaseUnitID
        {
            get
            {
                return baseUnitID;
            }
            set
            {
                baseUnitID = value;
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