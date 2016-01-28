/**********************************************
 * 类作用：      新建自我鉴定      
 * 建立人：    王保军
 * 建立时间： 2009.5.10
 ***********************************************/
using System;
using XBase.Common;
using System.Data;
using System.Collections;
using System.Text;
using XBase.Business.Common;

public partial class Pages_Office_HumanManager_PerformancePersonal :  BasePage 
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            // 新建自我鉴定编号初期处理
            AimNum.CodingType = ConstUtil.CODING_RULE_TYPE_PERFORMANCEPERSONAL;
            AimNum.ItemTypeID = ConstUtil.CODING_RULE_HUMEN_PERFORMANCEPERSONAL;

        }
    }
}
