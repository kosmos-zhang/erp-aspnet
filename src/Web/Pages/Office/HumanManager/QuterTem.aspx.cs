using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
 
using XBase.Model.Office.HumanManager;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using System.Data.SqlTypes;
using XBase.Common;
using System.Collections;

using XBase.Business.Office.HumanManager;
public partial class Pages_Office_HumanManager_QuterTem : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            QuterModelSelect.DataValueField = "ID";
            QuterModelSelect.DataTextField = "QuterName";
            QuterModelSelect.DataSource = DeptQuarterBus.GetQuarterDescrible();
            QuterModelSelect.DataBind();
            ListItem Item = new ListItem(ConstUtil.CODE_TYPE_INSERT_TEXT, ConstUtil.CODE_TYPE_INSERT_VALUE);
            QuterModelSelect.Items.Insert(0, Item);
            QuterModelSelect.SelectedIndex = 0;
        }
    }
    protected void imbSave_Click(object sender, ImageClickEventArgs e)
    {
        if (!string.IsNullOrEmpty(txtName.Text.Trim()))
        {
            int id;
            int sd=-1;
            if (this.QuterModelSelect.SelectedValue != "")
            {
                sd =  Convert .ToInt32(  this.QuterModelSelect.SelectedValue);
            }

            if (SaveQuarterSet(txtName.Text.Trim(), FCKeditor1.Value,sd , out   id))
            {

                UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];
                //设置公司代码

                IList<QuterModuleSetModel> QuterModuleSetList = new List<QuterModuleSetModel>();

                if (chMMubiao.Checked)
                {

                    if (chMRi.Checked)
                    {
                        QuterModuleSetModel dchMRi = new QuterModuleSetModel();
                         
                        dchMRi.Sign = "1";
                        dchMRi.ModuleID = "1001";
                        dchMRi.QuterDescribID = id.ToString();
                        dchMRi.TypeID = "1";

                        QuterModuleSetList.Add(dchMRi);
                    }
                    if (this.chMZhou.Checked)
                    {
                        QuterModuleSetModel dMZhou = new QuterModuleSetModel();
                         
                        dMZhou.Sign = "1";
                        dMZhou.ModuleID = "1001";
                        dMZhou.QuterDescribID = id.ToString();
                        dMZhou.TypeID = "2";

                        QuterModuleSetList.Add(dMZhou);
                    }
                    if (this.chMYue.Checked)
                    {
                        QuterModuleSetModel dMYue = new QuterModuleSetModel();
                        
                        dMYue.Sign = "1";
                        dMYue.ModuleID = "1001";
                        dMYue.QuterDescribID = id.ToString();
                        
                        dMYue.TypeID = "3";

                        QuterModuleSetList.Add(dMYue);
                    }
                    if (this.chMJi.Checked)
                    {
                        QuterModuleSetModel dMJi = new QuterModuleSetModel();
                   
                        dMJi.Sign = "1";
                        dMJi.ModuleID = "1001";
                        dMJi.QuterDescribID = id.ToString();
                        dMJi.TypeID = "4";

                        QuterModuleSetList.Add(dMJi);
                    }
                    if (this.chMNian.Checked)
                    {
                        QuterModuleSetModel dMNian = new QuterModuleSetModel();
                      
                        dMNian.Sign = "1";
                        dMNian.ModuleID = "1001";
                        dMNian.QuterDescribID = id.ToString();
                        dMNian.TypeID = "5";

                        QuterModuleSetList.Add(dMNian);
                    }



                }




                if (chRRenWu.Checked)
                {

                    if (chRGEren.Checked)
                    {
                        QuterModuleSetModel dchGEren = new QuterModuleSetModel();
                     
                        dchGEren.Sign = "1";
                        dchGEren.ModuleID = "1011";
                        dchGEren.QuterDescribID = id.ToString();
                        dchGEren.TypeID = "1";

                        QuterModuleSetList.Add(dchGEren);
                    }
                    if (this.chRZhipai.Checked)
                    {
                        QuterModuleSetModel dMZhipai = new QuterModuleSetModel();
                     
                        dMZhipai.Sign = "1";
                        dMZhipai.ModuleID = "1011";
                        dMZhipai.QuterDescribID = id.ToString();
                        dMZhipai.TypeID = "2";

                        QuterModuleSetList.Add(dMZhipai);
                    }


                }




                QuterModuleSetModel ModelchGgongzuo = new QuterModuleSetModel();
              
                ModelchGgongzuo.Sign = "1";
                if (chGgongzuo.Checked)
                {
                    ModelchGgongzuo.ModuleID = "1021";
                    ModelchGgongzuo.QuterDescribID = id.ToString();
                }
                QuterModuleSetList.Add(ModelchGgongzuo);



                QuterModuleSetModel ModelchCricheng = new QuterModuleSetModel();
            
                ModelchCricheng.Sign = "1";
                if (chCricheng.Checked)
                {
                    ModelchCricheng.ModuleID = "10411";
                    ModelchCricheng.QuterDescribID = id.ToString();
                }
                QuterModuleSetList.Add(ModelchCricheng);






                if (SaveQuarterSet(QuterModuleSetList))
                {
                    lblErrorMes.Visible = true;
                    lblErrorMes.Text = "保存成功！";
                    ClearInput();
                    
                }
                else
                {
                    lblErrorMes.Visible = true;
                    lblErrorMes.Text = "保存失败！";

                }




                lblErrorMes.Text = "保存成功";
            }
            else {

                lblErrorMes.Text = "保存失败";
            }
        }
        else
        {
            lblErrorMes.Text = "请输入岗位名称";
        
        }
    }
     public static bool SaveQuarterSet(IList<QuterModuleSetModel> QuterModuleSetList   )
        {
            //获取登陆用户信息
            UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];
           
            //定义返回变量
            bool isSucc = false;


            isSucc = sdf(QuterModuleSetList);
               
            return isSucc;
        }
     public static bool DeleteByQuarterSet(IList<QuterModuleSetModel> modeList)
     {

         #region 插入SQL拼写
         StringBuilder insertSql = new StringBuilder();
         insertSql.AppendLine("delete from officedba.QuterModuleSet where Sign='1' and  QuterDescribID=@QuterDescribID   ");
         //  insertSql.AppendLine("     set @ElemID= @@IDENTITY         ");
         #endregion
         //定义插入基本信息的命令
         SqlCommand comm = new SqlCommand();
         comm.CommandText = insertSql.ToString();
         //设置保存的参数
         comm.Parameters.Add(SqlHelper.GetParameterFromString("@QuterDescribID", modeList[0].QuterDescribID));	//公司代码 
         //添加返回参数
         //   comm.Parameters.Add(SqlHelper.GetOutputParameter("@ElemID", SqlDbType.Int));

         //执行插入操作
         bool isSucc = SqlHelper.ExecuteTransWithCommand(comm);

         return isSucc;



     }

     public static bool DeleteByQuarterID( string ID)
     {

         #region 插入SQL拼写
         StringBuilder insertSql = new StringBuilder();
         insertSql.AppendLine("delete from officedba.QuterModuleSet where Sign='1' and  QuterDescribID=@ID   ");
         //  insertSql.AppendLine("     set @ElemID= @@IDENTITY         ");
         #endregion
         //定义插入基本信息的命令
         SqlCommand comm = new SqlCommand();
         comm.CommandText = insertSql.ToString();
         //设置保存的参数
         comm.Parameters.Add(SqlHelper.GetParameterFromString("@ID", ID));	//公司代码 
         //添加返回参数
         //   comm.Parameters.Add(SqlHelper.GetOutputParameter("@ElemID", SqlDbType.Int));

         //执行插入操作
         bool isSucc = SqlHelper.ExecuteTransWithCommand(comm);

         return isSucc;



     }

     public static bool sdf(IList<QuterModuleSetModel> modeList)
     {
         DeleteByQuarterSet(modeList);
         bool isSucc;
         foreach (QuterModuleSetModel model in modeList)
         {


             #region 插入SQL拼写
             StringBuilder insertSql = new StringBuilder();
             insertSql.AppendLine("INSERT INTO officedba.QuterModuleSet ");
             insertSql.AppendLine("           (QuterDescribID             "); 
             insertSql.AppendLine("           ,ModuleID                 ");
             insertSql.AppendLine("           ,TypeID           ");
             insertSql.AppendLine("           ,Sign)                 ");

             insertSql.AppendLine("     VALUES                        ");
             insertSql.AppendLine("           (@QuterDescribID            "); 
             insertSql.AppendLine("           ,@ModuleID               ");
             insertSql.AppendLine("           ,@TypeID          ");
             insertSql.AppendLine("           ,@Sign)                ");
             //  insertSql.AppendLine("     set @ElemID= @@IDENTITY         ");
             #endregion
             //定义插入基本信息的命令
             SqlCommand comm = new SqlCommand();
             comm.CommandText = insertSql.ToString();
             //设置保存的参数
             comm.Parameters.Add(SqlHelper.GetParameterFromString("@QuterDescribID", model.QuterDescribID));	//公司代码 
             comm.Parameters.Add(SqlHelper.GetParameterFromString("@ModuleID", model.ModuleID));	//公司代码 
             comm.Parameters.Add(SqlHelper.GetParameterFromString("@TypeID", model.TypeID));	//公司代码 
             comm.Parameters.Add(SqlHelper.GetParameterFromString("@Sign", model.Sign));	//公司代码 

             //添加返回参数
             //   comm.Parameters.Add(SqlHelper.GetOutputParameter("@ElemID", SqlDbType.Int));

             //执行插入操作
             isSucc = SqlHelper.ExecuteTransWithCommand(comm);
             if (!isSucc)
             {
                 isSucc = false;
                 break;
             }
             else
             {
                 continue;
             }
         }
         return true;

     }

    public static bool SaveQuarterSet(string qutername, string Qutertext,int sd, out int RetValID)
    {
     

    
       

            #region 插入SQL拼写
            StringBuilder insertSql = new StringBuilder();
            if (sd == -1)
            {
                insertSql.AppendLine("INSERT INTO officedba.QuterDescribInfo ");
                insertSql.AppendLine("           (              ");
                insertSql.AppendLine("           QuterContent                ");
                insertSql.AppendLine("           ,QuterName)                 ");

                insertSql.AppendLine("     VALUES                        ");
                insertSql.AppendLine("           (@QuterContent            ");
                insertSql.AppendLine("           ,@QuterName)                ");
                insertSql.AppendLine("     set @ElemID= @@IDENTITY         ");
            }
            else
            {

                insertSql.AppendLine("update    officedba.QuterDescribInfo set  ");
                insertSql.AppendLine("           QuterContent =@QuterContent               ");
                insertSql.AppendLine("           ,QuterName=@QuterName      where ID=@ID            "); 
            
            }
            #endregion



            if (sd == -1)
            {
                SqlParameter[] param;
                param = new SqlParameter[3];

                param[0] = SqlHelper.GetParameter("@QuterContent", Qutertext);
                param[1] = SqlHelper.GetParameter("@QuterName", qutername);
                param[2] = SqlHelper.GetOutputParameter("@ElemID", SqlDbType.Int);
                SqlHelper.ExecuteTransSql(insertSql.ToString(), param);
                RetValID = Convert.ToInt32(param[2].Value.ToString());
                return SqlHelper.Result.OprateCount > 0 ? true : false;
            }
            else
            {
                SqlParameter[] param;
                param = new SqlParameter[3];

                param[0] = SqlHelper.GetParameter("@QuterContent", Qutertext);
                param[1] = SqlHelper.GetParameter("@QuterName", qutername);
                param[2] = SqlHelper.GetParameter("@ID", sd.ToString ()); 
                SqlHelper.ExecuteTransSql(insertSql.ToString(), param);
                RetValID = Convert.ToInt32(sd);
                return SqlHelper.Result.OprateCount > 0 ? true : false;
            
            
            }

         

    }
      protected void ClearInput()
    {
        FCKeditor1.Value = "";
        txtName.Text = "";
        ReadTemplate();
        chCricheng.Checked = false;
        chGgongzuo.Checked = false;
        chMJi.Checked = false;
        chMMubiao.Checked = false;

        this.chMNian .Checked = false;
        this.chMRi.Checked = false;
        this.chMYue.Checked = false;
        this.chMZhou.Checked = false;
        this.chRGEren.Checked = false;
        this.chRRenWu.Checked = false;
        this.chRZhipai.Checked = false;
        

    }
    protected void QuterModelSelect_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (QuterModelSelect.SelectedValue == "")
        {
            ClearInput();
            return;
        }

        DataTable DT = DeptQuarterBus.selectQuarterDescrible(QuterModelSelect.SelectedValue);
        if (DT.Rows.Count > 0)
        {
            FCKeditor1.Value = DT.Rows[0]["QuterContent"].ToString();

            txtName.Text = DT.Rows[0]["QuterName"].ToString();
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

        //this.Page.ClientScript.RegisterStartupScript(this.GetType(), " ", " <script> readssAfter(); </script> ");
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
    protected void BtnRead_Click(object sender, EventArgs e)
    {
        ReadTemplate();
    }

    private void ReadTemplate()
    {
        QuterModelSelect.Items.Clear();
        QuterModelSelect.DataValueField = "ID";
        QuterModelSelect.DataTextField = "QuterName";
        QuterModelSelect.DataSource = DeptQuarterBus.GetQuarterDescrible();
        QuterModelSelect.DataBind();
        ListItem Item = new ListItem(ConstUtil.CODE_TYPE_INSERT_TEXT, ConstUtil.CODE_TYPE_INSERT_VALUE);
        QuterModelSelect.Items.Insert(0, Item);
        QuterModelSelect.SelectedIndex = 0;
    }
    protected void BtnDelete_Click(object sender, EventArgs e)
    {
        if (this.QuterModelSelect.SelectedValue != "")
        {

            #region 插入SQL拼写
            StringBuilder insertSql = new StringBuilder();
       

                insertSql.AppendLine("delete From     officedba.QuterDescribInfo   "); 
                insertSql.AppendLine("            where ID=@ID            ");

      
            #endregion



           
                SqlParameter[] param;
                param = new SqlParameter[1];

                param[0] = SqlHelper.GetParameter("@ID", QuterModelSelect.SelectedValue); 
                SqlHelper.ExecuteTransSql(insertSql.ToString(), param);
             
                bool sign= SqlHelper.Result.OprateCount > 0 ? true : false;

                if (sign)
                {
                    if (DeleteByQuarterID(QuterModelSelect.SelectedValue))
                    {
                        lblErrorMes.Visible = true;
                        lblErrorMes.Text = "删除成功";
                       
                        ClearInput();
                    }
                    else
                    {
                        lblErrorMes.Visible = true;
                        lblErrorMes.Text = "删除失败";
                    }

                }
                else
                {

                    lblErrorMes.Visible = true;
                    lblErrorMes.Text = "删除失败";
                }
        
        }
        else
        {
            lblErrorMes.Visible = true;
            lblErrorMes.Text = "请选择模板";
        
        }
    }
}
