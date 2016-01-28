
/**********************************************
 * 类作用：   EquipmentUsed表数据模板
 * 建立人：   lysong
 * 建立时间： 2009/03/12
 ***********************************************/
using System;
namespace XBase.Model.Office.AdminManager
{
   public class EquipmentReceiveModel
    {

		#region 字段属性
		private int _id;
		private string _recordno;
		private string _equipmentno;
		private string _companycd;
		private string _status;
		private int _proposerid;
		private int _proposerdeptid;
		private string _critical;
		private DateTime _useddate;
		private decimal _usedlong;
		private decimal _usedcount;
		private string _usedtype;
		private string _useddemand;
		private string _applyreason;
		private DateTime _applydate;
		private DateTime _usestartdate;
		private DateTime _useenddate;
		private int _returnuserid;
		private int _returndeptid;
		private string _returnreason;
		private string _returnremark;
		private DateTime _modifieddate;
		private string _modifieduserid;
		/// <summary>
		/// 自增ID
		/// </summary>
		public int ID
		{
		set{ _id=value;}
		get{return _id;}
		}
		/// <summary>
		/// 单据编号
		/// </summary>
		public string RecordNo
		{
		set{ _recordno=value;}
		get{return _recordno;}
		}
		/// <summary>
		/// 设备编号
		/// </summary>
		public string EquipmentNo
		{
		set{ _equipmentno=value;}
		get{return _equipmentno;}
		}
		/// <summary>
		/// 公司代码
		/// </summary>
		public string CompanyCD
		{
		set{ _companycd=value;}
		get{return _companycd;}
		}
		/// <summary>
		/// 设备状态
		/// </summary>
		public string Status
		{
		set{ _status=value;}
		get{return _status;}
		}
		/// <summary>
		/// 申请人ID
		/// </summary>
		public int ProposerID
		{
		set{ _proposerid=value;}
		get{return _proposerid;}
		}
		/// <summary>
		/// 申请人所属部门ID
		/// </summary>
		public int ProposerDeptID
		{
		set{ _proposerdeptid=value;}
		get{return _proposerdeptid;}
		}
		/// <summary>
		/// 紧急程度
		/// </summary>
		public string Critical
		{
		set{ _critical=value;}
		get{return _critical;}
		}
		/// <summary>
		/// 使用日期
		/// </summary>
		public DateTime UsedDate
		{
		set{ _useddate=value;}
		get{return _useddate;}
		}
		/// <summary>
		/// 使用时长
		/// </summary>
		public decimal UsedLong
		{
		set{ _usedlong=value;}
		get{return _usedlong;}
		}
		/// <summary>
		/// 使用数量
		/// </summary>
		public decimal UsedCount
		{
		set{ _usedcount=value;}
		get{return _usedcount;}
		}
		/// <summary>
		/// 使用类型
		/// </summary>
		public string UsedType
		{
		set{ _usedtype=value;}
		get{return _usedtype;}
		}
		/// <summary>
		/// 配置要求
		/// </summary>
		public string UsedDemand
		{
		set{ _useddemand=value;}
		get{return _useddemand;}
		}
		/// <summary>
		/// 申请原因
		/// </summary>
		public string ApplyReason
		{
		set{ _applyreason=value;}
		get{return _applyreason;}
		}
		/// <summary>
		/// 申请日期
		/// </summary>
		public DateTime ApplyDate
		{
		set{ _applydate=value;}
		get{return _applydate;}
		}
		/// <summary>
		/// 领用日期
		/// </summary>
		public DateTime UseStartDate
		{
		set{ _usestartdate=value;}
		get{return _usestartdate;}
		}
		/// <summary>
		/// 归还日期
		/// </summary>
		public DateTime UseEndDate
		{
		set{ _useenddate=value;}
		get{return _useenddate;}
		}
		/// <summary>
		/// 归还人ID
		/// </summary>
		public int ReturnUserID
		{
		set{ _returnuserid=value;}
		get{return _returnuserid;}
		}
		/// <summary>
		/// 归还人所属部门
		/// </summary>
		public int ReturnDeptID
		{
		set{ _returndeptid=value;}
		get{return _returndeptid;}
		}
		/// <summary>
		/// 归还原因
		/// </summary>
		public string ReturnReason
		{
		set{ _returnreason=value;}
		get{return _returnreason;}
		}
		/// <summary>
		/// 归还备注
		/// </summary>
		public string ReturnRemark
		{
		set{ _returnremark=value;}
		get{return _returnremark;}
		}
		/// <summary>
		/// 最后更新日期
		/// </summary>
		public DateTime ModifiedDate
		{
		set{ _modifieddate=value;}
		get{return _modifieddate;}
		}
		/// <summary>
		/// 最后更新用户ID
		/// </summary>
		public string ModifiedUserID
		{
		set{ _modifieduserid=value;}
		get{return _modifieduserid;}
		}
		#endregion Model
    }
}
