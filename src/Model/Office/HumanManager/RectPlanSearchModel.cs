/**********************************************
 * 类作用：   RectPlan表查询数据模板
 * 建立人：   吴志强
 * 建立时间： 2009/03/31
 ***********************************************/
using System;

namespace XBase.Model.Office.HumanManager
{
    /// <summary>
    /// 类名：RectPlanSearchModel
    /// 描述：RectPlan表查询数据模板
    /// 
    /// 作者：吴志强
    /// 创建时间：2009/03/31
    /// 最后修改时间：2009/03/31
    /// </summary>
    ///
    public class RectPlanSearchModel
    {
        public string ID { get; set; }//活动ID
        public string RectPlanNo { get; set;}//活动编号
        public string Title { get; set; }//主题
        public string StartDate { get; set; }//开始时间
        public string EndDate { get; set; }//开始时间
        public string StartToDate { get; set; }//开始时间
        public string PrincipalID { get; set; }//负责人ID
        public string PrincipalName { get; set; }//负责人姓名
        public string PersonCount { get; set; }//招聘人数
        public string StatusID { get; set; }//活动状态
        public string StatusName { get; set; }//活动状态名称
        public string EmployStatus { get; set; }//已录用
        public string ReviewStatus { get; set; }//已面试
        public string CompanyCD { get; set; }//公司代码
    }
}
