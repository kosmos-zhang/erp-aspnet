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

public partial class Pages_Office_CustManager_SendSMBatch : System.Web.UI.Page
{
    private static System.Text.RegularExpressions.Regex mobileNo = new System.Text.RegularExpressions.Regex(@"^\d{7,12}$", System.Text.RegularExpressions.RegexOptions.Compiled);


    private UserInfoUtil UserInfo;
    protected void Page_Load(object sender, EventArgs e)
    {
        txtContent.Attributes.Add("onkeyup", "freshLength(this)");
        ImageButton1.Attributes.Add("onclick", "return checkInput()");

        UserInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];

        //XBase.Model.SystemManager.CompanyOpenServModel entity = XBase.Business.SystemManager.CompanyOpenServBus.GetCompanyOpenServInfo(UserInfo.CompanyCD);
        DataSet ds = new XBase.Business.KnowledgeCenter.MyKeyWord().GetCompanyOpenServ(UserInfo.CompanyCD);
        smCnt.Text = ds.Tables[0].Rows[0]["ManMsgNum"].ToString();

    }
    protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
    {
        string nums = hiddenNumList.Value;
        string txt = txtContent.Text;

        XBase.Model.Personal.MessageBox.MobileMsgMonitor entity;
        XBase.Business.Personal.MessageBox.MobileMsgMonitor bll = new XBase.Business.Personal.MessageBox.MobileMsgMonitor();
        
        DataTable userInfos = XBase.Business.Office.CustManager.LinkManBus.GetLinkManListEx(UserInfo.CompanyCD);
        DataRow[] users = userInfos.Select("ID IN("+nums+")");
        
        //check nums      
        if (users.Length > int.Parse(smCnt.Text))
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), "ddfx", "alert('超过可以发送数量限制');document.location.href='SendSMBatch.aspx';", true);
            return;
        }

        nums = "";
        foreach (DataRow user in users)
        {
            string phonenum = user["Handset"].ToString();
            if (!mobileNo.IsMatch(phonenum))
            {
                continue;
            }

            if (nums != "")
                nums += ",";
            nums += phonenum;

            entity = new XBase.Model.Personal.MessageBox.MobileMsgMonitor();
            entity.CompanyCD = UserInfo.CompanyCD;
            entity.Content = txt;
            entity.CreateDate = DateTime.Now;
            entity.ReceiveMobile = phonenum;
            entity.ReceiveUserID = int.Parse(user["ID"].ToString());
            entity.ReceiveUserName = user["LinkManName"].ToString();
            entity.SendDate = DateTime.Now;
            entity.SendUserID = UserInfo.EmployeeID;
            entity.SendUserName = UserInfo.EmployeeName;
            entity.Status = "1";
            entity.MsgType = "2";

            bll.Add(entity);

            if(cbAddtionalInfo.Checked)
            {
                string prefixName = "";
                var sex = user["Sex"].ToString().Trim();
                if (sex == "1" || sex == "2")
                {
                    prefixName = entity.ReceiveUserName + (sex == "1" ? " 先生:" : "女士:");
                }
                XBase.Common.SMSender.InternalSend(phonenum, prefixName+txt);
            }
        }

        if (!cbAddtionalInfo.Checked)
        {
           XBase.Common.SMSender.SendBatch(nums, txt);
        }

        int reCount = int.Parse(smCnt.Text) - nums.Split(',').Length;
        //
        XBase.Business.SystemManager.CompanyOpenServBus.UpdateCompanyManMsgNum(UserInfo.CompanyCD, reCount);

        ClientScript.RegisterClientScriptBlock(this.GetType(), "ddf", "alert('发送成功');document.location.href='SendSMBatch.aspx';", true);
    }
}
