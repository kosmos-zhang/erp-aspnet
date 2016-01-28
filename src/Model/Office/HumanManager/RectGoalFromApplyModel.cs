/**********************************************
 * 类作用：   从招聘申请中获取招聘目标数据模板
 * 建立人：   吴志强
 * 建立时间： 2009/04/01
 ***********************************************/
using System;

namespace XBase.Model.Office.HumanManager
{
    /// <summary>
    /// 类名：RectGoalFromApplyModel
    /// 描述：从招聘申请中获取招聘目标数据模板
    /// 
    /// 作者：吴志强
    /// 创建时间：2009/04/01
    /// 最后修改时间：2009/04/01
    /// </summary>
    ///
    public class RectGoalFromApplyModel
    {
        public string ID { get; set; }//ID
        public string DeptID { get; set; }//部门ID
        public string DeptName { get; set; }//部门名称
        public string JobName { get; set; }//职务名称
        public string RectCount { get; set; }//人员数量
        public string SexID { get; set; }//性别ID
        public string SexName { get; set; }//性别名称
        public string CultureLevelID { get; set; }//学历ID
        public string CultureLevelName { get; set; }//学历名称
        public string ProfessionalID { get; set; }//专业ID
        public string ProfessionalName { get; set; }//专业名称
        public string Age { get; set; }//年龄
        public string WorkNeeds { get; set; }//工作要求
        public string CompleteDate { get; set; }//完成时间
        public string WorkAge { get; set; }//完成时间
        public string WorkAgeName { get; set; }//完成时间
        public string PositionID { get; set; }//岗位ID
      

    }
}
