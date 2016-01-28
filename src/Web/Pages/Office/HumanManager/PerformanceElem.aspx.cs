/**********************************************
 * 类作用：       绩效考核指标       
 * 建立人：    王保军
 * 建立时间： 2009.4.22
 ***********************************************/
using System;
using XBase.Common;
using System.Data;
using System.Collections;
using System.Text;
using XBase.Business.Common;

public partial class Pages_Office_HumanManager_PerformanceElem : BasePage 
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //绩效考核指标编号初期处理
            AimNum.CodingType = ConstUtil.CODING_RULE_TYPE_TEN;
            AimNum.ItemTypeID = ConstUtil.CODING_RULE_HUMEN_NO;
        }
    }
}
