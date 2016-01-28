using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

using XBase.Business.DefManager;

public partial class Pages_Office_DefManager_DefTableList : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            #region 绑定检索条件
            DataSet defineControl = DefineBus.GetTableSearch(Request.QueryString["tableid"].ToString());
            if (defineControl == null | defineControl.Tables == null | defineControl.Tables[0].Rows.Count <= 0)
            {
                trSearchImg.Visible = false;
                trSearchLine.Visible = false;
            }
            else
            {
                DataSet defineShowControl = DefineBus.GetTableSearch(Request.QueryString["tableid"].ToString());
                //绑定搜索列
                string headsearch = "<table width=\"100%\" border=\"0\" align=\"center\" cellpadding=\"0\" cellspacing=\"1\" bgcolor=\"#999999\">";
                int tdnum = 1;
                if (defineControl != null && defineControl.Tables[0].Rows.Count > 0)
                {
                    int rownum = defineControl.Tables[0].Rows.Count;
                    for(int i=0;i<defineControl.Tables[0].Rows.Count;i++)
                    {
                        int flag = int.Parse(defineControl.Tables[0].Rows[i]["typeflag"].ToString());
                        if (i % 3 == 0)
                        {
                            headsearch += "<tr style=\"height:25px\"><td align=\"right\" class=\"tdColTitle\" style=\"width:10%\">" + defineControl.Tables[0].Rows[i]["cname"].ToString() + "</td>";
                        }
                        else
                        {
                            headsearch += "<td align=\"right\" class=\"tdColTitle\" style=\"width:10%\">" + defineControl.Tables[0].Rows[i]["cname"].ToString() + "</td>";
                            tdnum++;
                        }
                        switch(flag)
                        {
                            case 0:
                                headsearch += "<td bgcolor=\"#FFFFFF\" class=\"tdColInput\"><input type='text'  autotab='true' style='width:200px;' "
                                            + " id='db_" + defineControl.Tables[0].Rows[i]["ccode"].ToString() + "' "
                                            + " maxlength=" + defineControl.Tables[0].Rows[i]["length"].ToString() + " value='" + "'/></td>"; ;
                            break;
                            case 1:
                                headsearch += "<td bgcolor=\"#FFFFFF\" class=\"tdColInput\"><select id='db_" + defineControl.Tables[0].Rows[i]["ccode"].ToString() + ">" + defineControl.Tables[0].Rows[i]["dropdownlistValue"].ToString() + "</select></td>";
                            break;
                            case 2:
                                headsearch += "<td bgcolor=\"#FFFFFF\" class=\"tdColInput\"><input id='db_" + defineControl.Tables[0].Rows[i]["ccode"].ToString() + "1' maxlength='30'   style='width:80px;'  type='text'   value='" + DateTime.Now.ToString("yyyy-MM-01") + "'  readonly onclick=\"WdatePicker({dateFmt:'yyyy-MM-dd',el:$dp.$('db_" + defineControl.Tables[0].Rows[i]["ccode"].ToString() + "')})\" />~ ";
                                headsearch += "<input id='db_" + defineControl.Tables[0].Rows[i]["ccode"].ToString() + "2' maxlength='30' style='width:80px;'  type='text'  value='" + DateTime.Now.ToString("yyyy-MM-dd") + "' readonly onclick=\"WdatePicker({dateFmt:'yyyy-MM-dd',el:$dp.$('db_" + defineControl.Tables[0].Rows[i]["ccode"].ToString() + "')})\" /> </td>";
                            break;
                        }
                        if ((i-2) % 2 == 0 && i-2>0)
                        {
                            headsearch += "</tr>";
                        }
                    }
                }
                headsearch += "<tr id=\"trSearch\" runat=\"server\" ><td colspan=\""+tdnum*2+"\" height=\"30\" class=\"tdColInput\" align=\"center\"><img src=\"../../../Images/Button/Bottom_btn_search.jpg\" runat=\"server\" alt=\"检索\" id=\"imgSearch\"  style=\"cursor: hand\" onclick=\"SearchList(1);\" /></td></tr></table>";
                lbl_search.Text = headsearch;
                InitSearch(defineShowControl);//检索绑定
            }
            #endregion
            DataSet headds = DefineBus.GetTableHead(Request.QueryString["tableid"].ToString());
            this.HidTableID.Value = Request.QueryString["TableID"].ToString();
            InitPage(headds);//初始化列表头
        }
    }

    private void InitPage(DataSet ds)
    {
        string field = string.Empty;
        string tagname = string.Empty;
        if (ds == null | ds.Tables == null)
        {
            return;
        }
        else
        {
            DataTable dt = ds.Tables[0];
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    field += "," + dr["ccode"] + "#" + dr["type"] + "#" + dr["length"];
                    tagname += "," + dr["cname"];
                }
                field = field.Substring(1);
                tagname = tagname.Substring(1);
            }
        }
        this.HidHeadName.Value = field;
        this.HidTagName.Value = tagname;
    }

    private void InitSearch(DataSet ds)
    {
        string field = string.Empty;
        string tagname = string.Empty;
        if (ds == null | ds.Tables == null)
        {
            return;
        }
        else
        {
            DataTable dt = ds.Tables[0];
            foreach (DataRow dr in dt.Rows)
            {
                field += "," + dr["ccode"] + "#" + dr["type"] + "#" + dr["length"];
                tagname += "," + dr["cname"];
            }
            field = field.Substring(1);
            tagname = tagname.Substring(1);
            this.HidColName.Value = field;
        }
    }
    protected void btnImport_Click(object sender, ImageClickEventArgs e)
    {
        DataTable dt = DefineBus.GetDataTableList(Request.QueryString["tableid"].ToString());
        if (this.HidHeadName.Value.Length > 0 && this.HidTagName.Value.Length > 0)
        {
            int num = HidTagName.Value.Split(',').Length;
            string[,] ht = new string[num, 2];
            string[] arr = HidTagName.Value.Split(',');
            string headname = HidHeadName.Value.Replace("#", ",");
            string[] arr1 = headname.Split(',');
            string[] endarr = new string[arr1.Length / 3];
            for (int i = 0, j = 0; i < endarr.Length; i++)
            {
                endarr[i] = arr1[j];
                j += 3;
            }
            for (int i = 0; i < num; i++)
            {
                for (int ii = 0; ii < 2; ii++)
                {
                    if (ii == 0)
                    {
                        ht[i, ii] = arr[i];
                    }
                    else
                    {
                        ht[i, ii] = endarr[i];
                    }
                }
            }
            ExportExcel(dt, ht, "", HidTableHeadName.Value);
        }
    }
}
