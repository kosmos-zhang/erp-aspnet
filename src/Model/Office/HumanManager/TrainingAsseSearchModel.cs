/**********************************************
 * 类作用：   RectPlan表查询数据模板
 * 建立人：   吴志强
 * 建立时间： 2009/03/31
 ***********************************************/
using System;

namespace XBase.Model.Office.HumanManager
{
    /// <summary>
    /// 类名：TrainingSearchModel
    /// 描述：Training表查询数据模板
    /// 
    /// 作者：吴志强
    /// 创建时间：2009/04/03
    /// 最后修改时间：2009/04/03
    /// </summary>
    ///
    public class TrainingAsseSearchModel
    {
        public string ID { get; set; }//培训考核ID
        public string CompanyCD { get; set; }//公司代码
        public string AsseNo { get; set; }//考核编号
        public string TrainingNo { get; set; }//培训编号
        public string TrainingName { get; set; }//培训名称
        public string TrainingWayID { get; set; }//培训方式ID
        public string TrainingWayName { get; set; }//培训方式名称
        public string TrainingTeacher { get; set; }//培训老师
        public string CheckPerson { get; set; }//考评人
        public string AsseWay { get; set; }//考评方式
        public string AsseDate { get; set; }//考评时间
        public string AsseEndDate { get; set; }//考评时间
    }
}
