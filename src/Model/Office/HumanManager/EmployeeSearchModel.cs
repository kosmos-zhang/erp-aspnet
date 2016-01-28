/**********************************************
 * 类作用：   EmployeeInfo表查询数据模板
 * 建立人：   吴志强
 * 建立时间： 2009/03/20
 ***********************************************/
using System;

namespace XBase.Model.Office.HumanManager
{
    /// <summary>
    /// 类名：EmployeeSearchModel
    /// 描述：EmployeeInfo表查询数据模板
    /// 
    /// 作者：吴志强
    /// 创建时间：2009/03/20
    /// 最后修改时间：2009/03/20
    /// </summary>
    ///
    public class EmployeeSearchModel
    {
        public string ID { get; set; }	//ID
        public string EmployeeNo { get; set; }	//编号
        public string EmployeeNum { get; set; }	//工号
        public string PYShort { get; set; }	//拼音缩写
        public string CardID { get; set; }	//身份证
        public string EmployeeName { get; set; }	//姓名
        public string ContractKind { get; set; }	//工种
        public string ContractKindName { get; set; }	//工种名称
        public string AdminLevelID { get; set; }	//行政等级
        public string AdminLevelName { get; set; }	//行政等级
        public string PositionID { get; set; }	//职称ID
        public string PositionName { get; set; }	//职称名称
        public string EnterDate { get; set; }	//入职时间
        public string EnterEndDate { get; set; }	//入职时间
        public string LeaveDate { get; set; }	//离职时间
        public string LeaveEndDate { get; set; }	//离职时间
        public string Dept { get; set; }	//部门
        public string DeptName { get; set; }	//部门名称
        public string TotalTime { get; set; }	//本公司工龄
        public string Origin { get; set; }	//籍贯
        public string SexID { get; set; }	//性别
        public string SexName { get; set; }	//性别名称
        public string Birth { get; set; }	//出生日期
        public string Age { get; set; }	//年龄
        public string ProfessionalID { get; set; }	//专业ID
        public string ProfessionalName { get; set; }	//专业名称
        public string TotalSeniority { get; set; }	//工龄
        public string CompanyCD { get; set; }	//公司代码
        public string SchoolName { get; set; }	//毕业院校 
        public string QuarterID { get; set; }	//岗位ID
        public string QuarterName { get; set; }	//岗位名称 
        public string PositionTitle { get; set; }	//应聘岗位
        public string CultureLevel { get; set; }	//学历ID
        public string CultureLevelName { get; set; }	//学历名称
        public string ModifiedDate { get; set; }	//最后修改日期 
        public string ModifiedUserID { get; set; }	//最后修改人
        public string ReviewStatus { get; set; }	//面试状态
        public string Contact { get; set; }	//联系方式
        public string HomeAddress { get; set; }	//家庭住址
        public string AdminID { get; set; } //行政等级
        public string StartDate { get; set; } //开始日期
        public string EndDate { get; set; } //结束日期
        public string RecordCount { get; set; } //记录数
        public string Flag { get; set; } //人员标志位
        public string Mobile { get; set; } //手机
    }
}
