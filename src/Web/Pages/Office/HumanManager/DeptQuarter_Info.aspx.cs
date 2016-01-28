/**********************************************
 * 类作用：   岗位设置
 * 建立人：   吴志强
 * 建立时间： 2009/04/13
 ***********************************************/
using System;
using XBase.Common;
using System.Data;
using XBase.Business.Office.HumanManager;
using System.Text;
using XBase.Model.Office.HumanManager; 
using XBase.Business.Common;
using System.Collections.Generic;
using System.Web.UI.WebControls;
public partial class Pages_Office_HumanManager_DeptQuarter_Info : BasePage
{

    /// <summary>
    /// 类名：DeptQuarter_Info
    /// 描述：岗位设置
    /// 
    /// 作者：吴志强
    /// 创建时间：2009/04/13
    /// 最后修改时间：2009/04/13
    /// </summary>
    ///
    protected void Page_Load(object sender, EventArgs e)
    { //编号初期处理
     
        //页面初期设值
        if (!IsPostBack)
        {
            UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];
            divCompany.InnerHtml = userInfo.CompanyName  ;
            codeRule.CodingType = ConstUtil.CODING_RULE_TYPE_ZERO;
            codeRule.ItemTypeID = ConstUtil.CODING_BASE_ITEM_QUARTER;
            //岗位分类
            ddlQuarterType.TypeFlag = ConstUtil.CODE_TYPE_HUMAN;
            ddlQuarterType.TypeCode = ConstUtil.CODE_TYPE_QUARTER;
            ddlQuarterType.IsInsertSelect = true;
            //岗位级别
            ddlQuarterLevel.TypeFlag = ConstUtil.CODE_TYPE_HUMAN;
            ddlQuarterLevel.TypeCode = ConstUtil.CODE_TYPE_QUARTER_LEVEL;
            ddlQuarterLevel.IsInsertSelect = true;

            QuterModelSelect.DataValueField = "ID";
            QuterModelSelect.DataTextField = "QuterName";
          
            QuterModelSelect.DataSource = DeptQuarterBus.GetQuarterDescrible();
            QuterModelSelect.DataBind();
            ListItem Item = new ListItem(ConstUtil.CODE_TYPE_INSERT_TEXT, ConstUtil.CODE_TYPE_INSERT_VALUE);
            QuterModelSelect.Items.Insert(0, Item);
            QuterModelSelect.SelectedValue = "0";

            txtDeptName.Attributes["readonly"] = "readonly";
            txtSuperQuarterName.Attributes["readonly"] = "readonly";
        }
    }

    protected void imbSave_Click(object sender, System.Web.UI.ImageClickEventArgs e)
    {

        string EditFlag = hidEditFlag.Value.Trim();

   
        //定义Model变量
        DeptQuarterModel model = new DeptQuarterModel();
        //编辑标识
        model.EditFlag = EditFlag;
        string codeRules = string.Empty;
        //获取编号
         string quarterNo=string .Empty ;
         if (!EditFlag.Equals("INSERT"))
         {
             //quarterNo = txtDisplayCode.Value.Trim();
             quarterNo = hfdNo.Value;
         }
         else
         {
             
             //获取编码规则ID
             codeRules = codeRule.GetCodeRuleID();
             //手工输入的时候
             if (codeRules == string.Empty)
             {
                 


                 quarterNo = codeRule.GetDisplaycode();
             }
            
         
         
         
         
         }

        //新建时
        if (ConstUtil.EDIT_FLAG_INSERT.Equals(model.EditFlag))
        {
            //编号为空时，通过编码规则编号获取编号
            if (string.IsNullOrEmpty(quarterNo))
            {
                //获取编码规则编号
              string   codeRuleID = codeRule.GetCodeRuleID();
                //通过编码规则代码获取编号
                quarterNo = ItemCodingRuleBus.GetCodeValue(codeRuleID, ConstUtil.CODING_RULE_TABLE_DEPTQUARTER
                                , ConstUtil.CODING_RULE_COLUMN_DEPTQUARTER_NO);
            }
            //判断是否存在
            bool isAlready = PrimekeyVerifyBus.CheckCodeUniq(ConstUtil.CODING_RULE_TABLE_DEPTQUARTER
                                , ConstUtil.CODING_RULE_COLUMN_DEPTQUARTER_NO, quarterNo);
            //存在的场合
            if (!isAlready)
            {
                lblErrorMes.Visible = true;
                lblErrorMes.Text = "该编号已被使用，请输入未使用的编号！";
             
                return;
            }
            else
            {
                lblErrorMes.Visible = false ;
                lblErrorMes.Text = " ";
            
            }
        }
        //设置岗位编号
        model.QuarterNo = quarterNo;


        //所属机构
        model.DeptID = txtDeptID.Value .Trim ();
        //上级岗位
        model.SuperQuarterID = hidSuperQuarter.Value.Trim();
        //岗位名称
        model.QuarterName = txtQuarterName.Text .Trim ();
        //拼音代码
        model.PYShort = txtPYShort.Text .Trim ();
        //是否关键岗位
        model.KeyFlag = ddlKeyFlag.SelectedValue ;
        //岗位分类
        model.TypeID = ddlQuarterType.SelectedValue ;
        //岗位级别
        model.LevelID = ddlQuarterLevel.SelectedValue ;
        //描述信息
        model.Description = txtDescription.Text.Trim(); ;
        //启用状态
        model.UsedStatus = ddlUsedStatus.SelectedValue ;
        //附件
        model.Attachment = hfAttachment.Value.Trim() ;
        model.PageAttachment = hfPageAttachment.Value .Trim ();
        //岗位职责
        model.Duty = txtDuty.Text .Trim ();
        //任职资格
        model.DutyRequire = txtDutyRequire.Text .Trim ();

    //
        model.QuterContent = FCKeditor1.Value;




      
   



        bool isSucce = DeptQuarterBus.SaveDeptQuarterInfo(model);
        //保存成功时
        if (isSucce)
        {


            UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];
            //设置公司代码

            IList<QuterModuleSetModel> QuterModuleSetList = new List<QuterModuleSetModel>();
            
            if (chMMubiao.Checked)
            {
             
                if (chMRi.Checked)
                {
                    QuterModuleSetModel dchMRi = new QuterModuleSetModel();
                    dchMRi.CompanyCD = userInfo.CompanyCD;
                    dchMRi.Sign = "2";
                    dchMRi.ModuleID = "1001";
                    dchMRi.QuarterNo = model.QuarterNo;
                    dchMRi.DeptID = model.DeptID;
                    dchMRi.TypeID = "1";

                    QuterModuleSetList.Add(dchMRi);
                }
                if (this.chMZhou.Checked)
                {
                    QuterModuleSetModel dMZhou = new QuterModuleSetModel();
                    dMZhou.CompanyCD = userInfo.CompanyCD;
                    dMZhou.Sign = "2";
                    dMZhou.ModuleID = "1001";
                    dMZhou.QuarterNo = model.QuarterNo;
                    dMZhou.DeptID = model.DeptID;
                    dMZhou.TypeID = "2";

                    QuterModuleSetList.Add(dMZhou);
                }
                if (this.chMYue.Checked)
                {
                    QuterModuleSetModel dMYue = new QuterModuleSetModel();
                    dMYue.CompanyCD = userInfo.CompanyCD;
                    dMYue.Sign = "2";
                    dMYue.ModuleID = "1001";
                    dMYue.QuarterNo = model.QuarterNo;
                    dMYue.DeptID = model.DeptID;
                    dMYue.TypeID = "3";

                    QuterModuleSetList.Add(dMYue);
                }
                if (this.chMJi.Checked)
                {
                    QuterModuleSetModel dMJi = new QuterModuleSetModel();
                    dMJi.CompanyCD = userInfo.CompanyCD;
                    dMJi.Sign = "2";
                    dMJi.ModuleID = "1001";
                    dMJi.QuarterNo = model.QuarterNo;
                    dMJi.DeptID = model.DeptID;
                    dMJi.TypeID = "4";

                    QuterModuleSetList.Add(dMJi);
                }
                if (this.chMNian.Checked)
                {
                    QuterModuleSetModel dMNian = new QuterModuleSetModel();
                    dMNian.CompanyCD = userInfo.CompanyCD;
                    dMNian.Sign = "2";
                    dMNian.ModuleID = "1001";
                    dMNian.QuarterNo = model.QuarterNo;
                    dMNian.DeptID = model.DeptID;
                    dMNian.TypeID = "5";

                    QuterModuleSetList.Add(dMNian);
                }



            }
      


           
            if (chRRenWu.Checked)
            {
               
                if (chRGEren.Checked)
                {
                    QuterModuleSetModel dchGEren = new QuterModuleSetModel();
                    dchGEren.CompanyCD = userInfo.CompanyCD;
                    dchGEren.Sign = "2";
                    dchGEren.ModuleID = "1011";
                    dchGEren.QuarterNo = model.QuarterNo;
                    dchGEren.DeptID = model.DeptID;
                    dchGEren.TypeID = "1";

                    QuterModuleSetList.Add(dchGEren);
                }
                if (this.chRZhipai.Checked)
                {
                    QuterModuleSetModel dMZhipai = new QuterModuleSetModel();
                    dMZhipai.CompanyCD = userInfo.CompanyCD;
                    dMZhipai.Sign = "2";
                    dMZhipai.ModuleID = "1011";
                    dMZhipai.QuarterNo = model.QuarterNo;
                    dMZhipai.DeptID = model.DeptID;
                    dMZhipai.TypeID = "2";

                    QuterModuleSetList.Add(dMZhipai);
                }


            }
           



            QuterModuleSetModel ModelchGgongzuo = new QuterModuleSetModel();
            ModelchGgongzuo.CompanyCD = userInfo.CompanyCD;
            ModelchGgongzuo.Sign = "2";
            if (chGgongzuo.Checked)
            {
                ModelchGgongzuo.ModuleID = "1021";
                ModelchGgongzuo.QuarterNo = model.QuarterNo;
                ModelchGgongzuo.DeptID = model.DeptID;
            }
            QuterModuleSetList.Add(ModelchGgongzuo);



            QuterModuleSetModel ModelchCricheng = new QuterModuleSetModel();
            ModelchCricheng.CompanyCD = userInfo.CompanyCD;
            ModelchCricheng.Sign = "2";
            if (chCricheng.Checked)
            {
                ModelchCricheng.ModuleID = "10411";
                ModelchCricheng.QuarterNo = model.QuarterNo;
                ModelchCricheng.DeptID = model.DeptID;
            }
            QuterModuleSetList.Add(ModelchCricheng);






            if (DeptQuarterBus.SaveQuarterSet(QuterModuleSetList))
            {
                lblErrorMes.Visible = true;
                lblErrorMes.Text = "保存成功！";
                hidEditFlag.Value = ConstUtil.EDIT_FLAG_UPDATE;
                txtDisplayCode.Value = model.QuarterNo;
                txtDisplayCode.Disabled = true;
                hidDeptInfo.Value = model.DeptID;
                this.Page.ClientScript.RegisterStartupScript(this.GetType(), " ", " <script> SaveAfter(); </script> ");
            }
            else
            {
                lblErrorMes.Visible = true;
                lblErrorMes.Text = "保存失败！";
            
            }






































          


        }
        //保存未成功时
        else
        {
            lblErrorMes.Visible = true;
            lblErrorMes.Text = "保存失败！";
        }
        

               
             
                      
              

    


    }
    protected void imbEdit_Click(object sender, System.Web.UI.ImageClickEventArgs e)
    {
         
        hidEditFlag.Value = "UPDATE";
        string quterID = hidquarterID.Value;
        DataTable dtDeptInfo = DeptQuarterBus.GetDeptQuarterInfoWithID(quterID);
        if (dtDeptInfo.Rows.Count > 0)
        {

            txtDeptID.Value = dtDeptInfo.Rows[0]["DeptID"] == null ? "" : dtDeptInfo.Rows[0]["DeptID"].ToString();

            txtDisplayCode.Value = dtDeptInfo.Rows[0]["QuarterNo"] == null ? "" : dtDeptInfo.Rows[0]["QuarterNo"].ToString();
            txtDisplayCode.Disabled = true;
            txtDeptName.Text  = dtDeptInfo.Rows[0]["DeptName"] == null ? "" : dtDeptInfo.Rows[0]["DeptName"].ToString();
            txtSuperQuarterName.Text = dtDeptInfo.Rows[0]["SuperQuarterName"] == null ? "" : dtDeptInfo.Rows[0]["SuperQuarterName"].ToString(); //上级岗位

            txtQuarterName.Text = dtDeptInfo.Rows[0]["QuarterName"] == null ? "" : dtDeptInfo.Rows[0]["QuarterName"].ToString(); //岗位名称
            txtPYShort.Text = dtDeptInfo.Rows[0]["PYShort"] == null ? "" : dtDeptInfo.Rows[0]["PYShort"].ToString(); //拼音代码


            ddlKeyFlag.SelectedValue = dtDeptInfo.Rows[0]["KeyFlag"] == null ? "" : dtDeptInfo.Rows[0]["KeyFlag"].ToString(); //是否关键岗位

            ddlQuarterType.SelectedValue = dtDeptInfo.Rows[0]["TypeID"] == null ? "" : dtDeptInfo.Rows[0]["TypeID"].ToString(); //岗位分类

            ddlQuarterLevel.SelectedValue = dtDeptInfo.Rows[0]["LevelID"] == null ? "" : dtDeptInfo.Rows[0]["LevelID"].ToString(); //岗位级别


            txtDescription.Text = dtDeptInfo.Rows[0]["Description"] == null ? "" : dtDeptInfo.Rows[0]["Description"].ToString(); //描述信息
            ddlUsedStatus.SelectedValue = dtDeptInfo.Rows[0]["UsedStatus"] == null ? "" : dtDeptInfo.Rows[0]["UsedStatus"].ToString(); //启用状态

            string attachment = dtDeptInfo.Rows[0]["Attachment"] == null ? "" : dtDeptInfo.Rows[0]["Attachment"].ToString(); //附件
            hidaddd.Value = attachment;

            hfAttachment.Value = dtDeptInfo.Rows[0]["Attachment"] == null ? "" : dtDeptInfo.Rows[0]["Attachment"].ToString(); //启用状态
            string resumeUrl = hfAttachment.Value;
            int j = resumeUrl.LastIndexOf("\\") + 1;
            spanAttachmentName.InnerHtml = resumeUrl.Substring(j, resumeUrl.Length - j);


            hfPageAttachment.Value = dtDeptInfo.Rows[0]["Attachment"] == null ? "" : dtDeptInfo.Rows[0]["Attachment"].ToString(); //启用状态
            txtDuty.Text = dtDeptInfo.Rows[0]["Duty"] == null ? "" : dtDeptInfo.Rows[0]["Duty"].ToString(); //岗位职责
            txtDutyRequire.Text = dtDeptInfo.Rows[0]["DutyRequire"] == null ? "" : dtDeptInfo.Rows[0]["DutyRequire"].ToString(); //任职资格

            FCKeditor1.Value = dtDeptInfo.Rows[0]["QuterContent"] == null ? "" : dtDeptInfo.Rows[0]["QuterContent"].ToString(); //任职资格


          DataTable dtSet=   DeptQuarterBus.GetQuarterModelSet(txtDeptID.Value, txtDisplayCode.Value);

         


          if (dtSet.Rows.Count > 0)
          {



              DataTable dtMUbIAO = GetNewDataTable(dtSet, "ModuleID='1001'", "TypeID asc");
              if (dtMUbIAO.Rows.Count > 0)
              {
                  chMMubiao.Checked = true;
             
                  for (int a = 0; a < dtMUbIAO.Rows.Count; a++)
                  {
                      string mubiao = dtMUbIAO.Rows[a]["TypeID"] == null ? "" : dtMUbIAO.Rows[a]["TypeID"].ToString();
                      if (mubiao == "1")
                      {
                          chMRi.Checked = true;
                      }
                      else if (mubiao == "2")
                      {
                          chMZhou.Checked = true;
                      }
                      else if (mubiao == "3")
                      {
                          chMYue.Checked = true;
                      }
                      else if (mubiao == "4")
                      {
                          chMJi.Checked = true;
                      }
                      else if (mubiao == "5")
                      {
                          chMNian.Checked = true;
                      } 
                  }
              }
              else
              {
                  chMMubiao.Checked = false;
                  chMRi.Checked = false;
                  chMZhou.Checked = false;
                  chMYue.Checked = false;
                  chMJi.Checked = false;
                  chMNian.Checked = false;
              }



              DataTable dtRenwu = GetNewDataTable(dtSet, "ModuleID='1011'", "TypeID asc");
              if (dtRenwu.Rows.Count > 0)
              {
                  chRRenWu.Checked = true;
                  for (int a = 0; a < dtRenwu.Rows.Count; a++)
                  {
                      string mubiao = dtRenwu.Rows[a]["TypeID"] == null ? "" : dtRenwu.Rows[a]["TypeID"].ToString();
                      if (mubiao == "1")
                      {
                          chRGEren.Checked = true;
                      }
                      else if (mubiao == "2")
                      {
                          chRZhipai.Checked = true;
                      }

                  }
              }
              else
              {
                  chRGEren.Checked = false;
                  chRRenWu.Checked = false ;
                  chRZhipai.Checked = false;
              }

             




              DataTable dtrIZHI = GetNewDataTable(dtSet, "ModuleID='1021'", "TypeID asc");
              if (dtrIZHI.Rows.Count > 0)
              {
                  chGgongzuo.Checked = true;
              }
              else
              {
                  chGgongzuo.Checked = false;
              }
              DataTable dtRICHENG = GetNewDataTable(dtSet, "ModuleID='10411'", "TypeID asc");
              if (dtRICHENG.Rows.Count > 0)
              {
                  chCricheng.Checked = true;
              }
              else
              {
                  chCricheng.Checked = false ;
              }
          }

            this.Page.ClientScript.RegisterStartupScript(this.GetType(), " ", " <script> readAfter(); </script> "); 
        }

      
        
       
      
        
     
     
      
      
        
         
      
    

    }


    private DataTable GetNewDataTable(DataTable dt, string condition, string Order)
    {
        DataTable newdt = new DataTable();
        newdt = dt.Clone();
        DataRow[] dr = dt.Select(condition, Order);
        for (int i = 0; i < dr.Length; i++)
        {
            newdt.ImportRow((DataRow)dr[i]);
        }
        return newdt;//返回的查询结果
    }
    protected void chMMubiao_CheckedChanged(object sender, EventArgs e)
    {
        if (chMMubiao.Checked)
        {
             
            chMRi.Checked = true;
            chMZhou.Checked = true;
            chMYue.Checked = true;
            chMJi.Checked = true;
            chMNian.Checked = true;
        }
        else
        {

            
            chMRi.Checked = false;
            chMZhou.Checked = false;
            chMYue.Checked = false;
            chMJi.Checked = false;
            chMNian.Checked = false;
        }

        this.Page.ClientScript.RegisterStartupScript(this.GetType(), " ", " <script> readssAfter(); </script> "); 

    }
    protected void chMRi_CheckedChanged(object sender, EventArgs e)
    {


        if (((CheckBox)sender).Checked)
        {
            chMMubiao.Checked = true;
             
        }
        this.Page.ClientScript.RegisterStartupScript(this.GetType(), " ", " <script> readssAfter(); </script> "); 
    }
    protected void chRRenWu_CheckedChanged(object sender, EventArgs e)
    {
        if (chRRenWu.Checked)
        {

            chRGEren.Checked = true;
            chRZhipai.Checked = true;
           
        }
        else
        {


            chRGEren.Checked = false ;
            chRZhipai.Checked = false ;
        }
        this.Page.ClientScript.RegisterStartupScript(this.GetType(), " ", " <script> readssAfter(); </script> "); 
    }
    protected void chRZhipai_CheckedChanged(object sender, EventArgs e)
    {
        if (((CheckBox)sender).Checked)
        {
            chRRenWu.Checked = true;

        }
        this.Page.ClientScript.RegisterStartupScript(this.GetType(), " ", " <script> readssAfter(); </script> "); 
    }

  

    protected void QuterModelSelect_SelectedIndexChanged(object sender, EventArgs e)
    {

        string sdsdfdsfsd = txtDeptName.Text + "_" + txtDeptID.Value;
        if (QuterModelSelect.SelectedValue == "")
        {
            chCricheng.Checked = false;
            chMMubiao.Checked = false;
            chMRi.Checked = false;
            chMZhou.Checked = false;
            chMYue.Checked = false;
            chMJi.Checked = false;
            chMNian.Checked = false;
            chRGEren.Checked = false;
            chRRenWu.Checked = false;
            chRZhipai.Checked = false;
            chGgongzuo.Checked = false;
            FCKeditor1.Value = "";
            this.Page.ClientScript.RegisterStartupScript(this.GetType(), " ", " <script> readssAfter(); </script> ");
        }
        else
        {

            DataTable DT = DeptQuarterBus.selectQuarterDescrible(QuterModelSelect.SelectedValue);
            if (DT.Rows.Count > 0)
            {
                FCKeditor1.Value = DT.Rows[0]["QuterContent"].ToString();


                DataTable dtSet = DeptQuarterBus.selectQuarterSet(QuterModelSelect.SelectedValue);




                if (dtSet.Rows.Count > 0)
                {



                    DataTable dtMUbIAO = GetNewDataTable(dtSet, "ModuleID='1001'", "TypeID asc");
                    if (dtMUbIAO.Rows.Count > 0)
                    {
                        chMMubiao.Checked = true;

                        for (int a = 0; a < dtMUbIAO.Rows.Count; a++)
                        {
                            string mubiao = dtMUbIAO.Rows[a]["TypeID"] == null ? "" : dtMUbIAO.Rows[a]["TypeID"].ToString();
                            if (mubiao == "1")
                            {
                                chMRi.Checked = true;
                            }
                            else if (mubiao == "2")
                            {
                                chMZhou.Checked = true;
                            }
                            else if (mubiao == "3")
                            {
                                chMYue.Checked = true;
                            }
                            else if (mubiao == "4")
                            {
                                chMJi.Checked = true;
                            }
                            else if (mubiao == "5")
                            {
                                chMNian.Checked = true;
                            }
                        }
                    }
                    else
                    {
                        chMMubiao.Checked = false;
                        chMRi.Checked = false;
                        chMZhou.Checked = false;
                        chMYue.Checked = false;
                        chMJi.Checked = false;
                        chMNian.Checked = false;
                    }



                    DataTable dtRenwu = GetNewDataTable(dtSet, "ModuleID='1011'", "TypeID asc");
                    if (dtRenwu.Rows.Count > 0)
                    {
                        chRRenWu.Checked = true;
                        for (int a = 0; a < dtRenwu.Rows.Count; a++)
                        {
                            string mubiao = dtRenwu.Rows[a]["TypeID"] == null ? "" : dtRenwu.Rows[a]["TypeID"].ToString();
                            if (mubiao == "1")
                            {
                                chRGEren.Checked = true;
                            }
                            else if (mubiao == "2")
                            {
                                chRZhipai.Checked = true;
                            }

                        }
                    }
                    else
                    {
                        chRGEren.Checked = false;
                        chRRenWu.Checked = false;
                        chRZhipai.Checked = false;
                    }






                    DataTable dtrIZHI = GetNewDataTable(dtSet, "ModuleID='1021'", "TypeID asc");
                    if (dtrIZHI.Rows.Count > 0)
                    {
                        chGgongzuo.Checked = true;
                    }
                    else
                    {
                        chGgongzuo.Checked = false;
                    }
                    DataTable dtRICHENG = GetNewDataTable(dtSet, "ModuleID='10411'", "TypeID asc");
                    if (dtRICHENG.Rows.Count > 0)
                    {
                        chCricheng.Checked = true;
                    }
                    else
                    {
                        chCricheng.Checked = false;
                    }
                }



            }
            else
            {
                FCKeditor1.Value = "";
            }


   
           this.Page.ClientScript.RegisterStartupScript(this.GetType(), " ", " <script> readssAfter('" + sdsdfdsfsd + "'); </script> ");
        }
    }
}
