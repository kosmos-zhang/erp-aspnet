/**********************************************
 * 类作用：   EmployeeTest表数据查询模板
 * 建立人：   吴志强
 * 建立时间： 2009/04/08
 ***********************************************/
using System;

namespace XBase.Model.Office.HumanManager
{
    /// <summary>
    /// 类名：EmployeeTestSearchModel
    /// 描述：EmployeeTest表数据查询模板
    /// 
    /// 作者：吴志强
    /// 创建时间：2009/04/08
    /// 最后修改时间：2009/04/08
    /// </summary>
    ///
    public class EmployeeTestSearchModel
    {
        public string ID { get; set; }//ID
        public string CompanyCD { get; set; }//公司代码
        public string TestNo { get; set; }//考试编号
        public string Title { get; set; }//主题
        public string TeacherID { get; set; }//考试负责人ID
        public string TeacherName { get; set; }//考试负责人名
        public string StartDate { get; set; }//开始时间
        public string StartToDate { get; set; }//开始时间
        public string EndDate { get; set; }//结束时间
        public string EndToDate { get; set; }//结束时间
        public string Addr { get; set; }//考试地点
        public string StatusID { get; set; }//考试状态
        public string StatusName { get; set; }//考试状态
        public string AbsenceCount { get; set; }//缺考人数
    }
}
