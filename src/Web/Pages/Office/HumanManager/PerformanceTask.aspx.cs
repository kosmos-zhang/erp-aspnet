/**********************************************
 * 类作用：       绩效考核任务      
 * 建立人：    王保军
 * 建立时间： 2009.5.4
 ***********************************************/
using System;
using XBase.Common;
using System.Data;
using System.Collections;
using System.Text;
using XBase.Business.Common;

public partial class Pages_Office_HumanManager_PerformanceTask : BasePage 
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //绩效考核指标编号初期处理
            AimNum.CodingType = ConstUtil.CODING_RULE_TYPE_PERFORMANCETASK;
            AimNum.ItemTypeID = ConstUtil.CODING_RULE_HUMEN_PERFORMANCETASK;

            string requestParam = Request.QueryString.ToString();
            //通过参数个数来判断是否从菜单过来
            int firstIndex = requestParam.IndexOf("&");
            //从列表过来时
            if (firstIndex > 0)
            {
                //返回按钮可见
        
                //获取列表的查询条件
                string searchCondition = requestParam.Substring(firstIndex);
                //设置检索条件
                hidSearchCondition.Value = searchCondition;
            }
        }

    }
}
