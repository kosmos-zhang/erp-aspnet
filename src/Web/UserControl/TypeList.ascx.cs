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

using XBase.Common;

public partial class UserControl_Personal_TypeList : System.Web.UI.UserControl
{
    private string selValue = string.Empty;
    public string SelValue
    {
        get { return this.keywordType.SelectedValue; }
        set {
            selValue = value;
        }
    }

    private string pid = "0";
    public string Pid
    {
        get { return pid; }
        set
        {
            pid = value;
        }
    }

    public DropDownList Select {
        get { return this.keywordType; }
    }

    private int nodeLevel = 1;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.IsPostBack)
        {
            LoadKnowledgeType();

            for (int i = 0; i < this.keywordType.Items.Count; i++)
            {
                if (this.keywordType.Items[i].Value == selValue)
                {
                    this.keywordType.SelectedIndex = i;
                    break;
                }
            }


        }
    }

    private void LoadKnowledgeType()
    {
        UserInfoUtil UserInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];

        XBase.Business.Personal.Culture.CultureType bll = new XBase.Business.Personal.Culture.CultureType();
        DataTable dt = bll.GetList("CompanyCD='" + UserInfo.CompanyCD+"'");

        this.keywordType.Items.Add(new ListItem("请选择分类", "-1"));

        foreach (DataRow row in dt.Select("SupperTypeID=" + Pid))
        {
            this.keywordType.Items.Add(new ListItem(getSep() + row["TypeName"].ToString(), row["ID"].ToString()));

            LoadSubType(row["ID"].ToString(), dt);
        }

    }

    private void LoadSubType(string id, DataTable ds)
    {
        nodeLevel++;

        foreach (DataRow row in ds.Select("SupperTypeID=" + id))
        {
            this.keywordType.Items.Add(new ListItem(getSep() + row["TypeName"].ToString(), row["ID"].ToString()));

            LoadSubType(row["ID"].ToString(), ds);
        }

        nodeLevel--;

    }

    private string getSep()
    {
        string ret = "";
        for (int i = 0; i < nodeLevel; i++)
        {
            ret += "|-";
        }

        return ret;
    }


}
