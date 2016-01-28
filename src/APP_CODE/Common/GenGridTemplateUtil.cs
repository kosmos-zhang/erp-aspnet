
/***********************************************************************
 * 
 * Module Name: XBase.Common.StringUtil
 * Current Version: 1.0
 * Creator: jiangym
 * Auditor:2009-01-05 00:00:00
 * End Date:
 * Description:
 * Version History:
 ***********************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
namespace XBase.Common
{
    public class GenGridTemplateUtil:System.Web.UI.ITemplate
    {
        private string strColumnName;
        private DataControlRowType dcrtColumnType;
        /// <summary>
        /// 动态添加模版列
        /// </summary>
        /// <param name="strColumnName">列名</param>
        /// <param name="dcrtColumnType">列的类型</param>
        public GenGridTemplateUtil(string strColumnName, DataControlRowType dcrtColumnType)
        {
            this.strColumnName = strColumnName;
            this.dcrtColumnType = dcrtColumnType;
        }

        /// <summary>
        /// 生成girdview列
        /// </summary>
        /// <param name="ctlContainer"></param>
        public void InstantiateIn(System.Web.UI.Control ctlContainer)
        {

            switch (dcrtColumnType)
            {
                case DataControlRowType.Header: //列标题
                    Literal ltr = new Literal();
                    ltr.Text = strColumnName;
                    ctlContainer.Controls.Add(ltr);
                    break;
                case DataControlRowType.DataRow: //模版列内容——加载CheckBox 
                    CheckBox cb = new CheckBox();
                    cb.ID = "CheckBox";
                   // cb.AutoPostBack = true;
                
                    cb.Checked = false;
                    cb.Text = "1";
                    ctlContainer.Controls.Add(cb);
                    break;
            }
        }


    }
}
