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
public partial class Pages_DefManager_DefTableOPertion : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.QueryString["ID"] != null) {
            this.HidControlID.Value = Request.QueryString["ID"].ToString();
        }
        if (!Page.IsPostBack)
        {
            //获取表头
            if (HidControlID.Value == "-1")
            {
                lbl_title.Text ="新建" + DefineBus.GetDefineTableByCode(Request.QueryString["tableid"].ToString());
            }
            else
            {
                lbl_title.Text = "修改" + DefineBus.GetDefineTableByCode(Request.QueryString["tableid"].ToString());
            }
            //获取表ID
            this.HidControlTableID.Value = Request.QueryString["tableid"].ToString();
            
            //获取表内字段之间关系
            HidRelation.Value = DefineBus.GetRelationByTableID(Request.QueryString["tableid"].ToString());

            //获取明细横向统计关系
            HidSubRelation.Value = DefineBus.GetRelationByParentTableID(Request.QueryString["tableid"].ToString(),"0");
            
            //获取明细纵向统计关系
            HidDownRelation.Value = DefineBus.GetRelationByParentTableID(Request.QueryString["tableid"].ToString(), "1");

            //获取表之间关系
            HidTablRelation.Value = DefineBus.GetRelationTable(Request.QueryString["tableid"].ToString());
            
            //获取明细表默认行数
            HidTableRowNum.Value = DefineBus.GetTableRows(Request.QueryString["tableid"].ToString());

            //获取明细字段
            HidSubTableCode.Value = DefineBus.GetSubTableByParentID(Request.QueryString["tableid"].ToString());

            DataSet defineControl = DefineBus.GetTableStruct(Request.QueryString["tableid"].ToString());
            
            #region 读取控件名
            for (int i = 0; i < defineControl.Tables[0].Rows.Count; i++)
            {
                HidControlName.Value = HidControlName.Value + "," + defineControl.Tables[0].Rows[i]["ccode"].ToString();//获取控件ID集合
                HidControlList.Value = HidControlList.Value + ",db_" + defineControl.Tables[0].Rows[i]["ccode"].ToString() + "#" + defineControl.Tables[0].Rows[i]["type"].ToString() + "#" + defineControl.Tables[0].Rows[i]["length"].ToString() + "#" + defineControl.Tables[0].Rows[i]["isempty"].ToString() + "#" + defineControl.Tables[0].Rows[i]["cname"].ToString();
            }
            HidControlName.Value = HidControlName.Value.ToString().TrimStart(',');
            HidControlList.Value = HidControlList.Value.ToString().TrimStart(',');
            #endregion

            //判断表使用的模板（标准版/自定义模板）
            if (DefineBus.GetModuleByTableID(Request.QueryString["tableid"].ToString()))
            {
                //自定义模板
                DataSet moduleDS = DefineBus.GetModuleValueByTableID(Request.QueryString["tableid"].ToString());
                String modulevalue = moduleDS.Tables[0].Rows[0]["ModuleContent"].ToString();
                modulevalue = modulevalue.Replace("&nbsp;","");

                //替换详细列表
                //HidTablRelation.value = "A#A1#A2,B#B1#B2,C#C1#C2@a#a1#a2,b#b1#b2,c#c1#c2";
                if (HidTablRelation.Value.Trim().Length > 0)
                {
                    string[] mainstr = HidTablRelation.Value.Split('@');     //mainstr = "A#A1#A2,B#B1#B2";
                    for (int i = 0; i < mainstr.Length; i++)
                    {
                        string[] substr = mainstr[i].Split(',');             //substr="A#A1#A2";
                        string head = string.Empty;
                        for (int j = 0; j < substr.Length; j++)
                        {
                            string[] itemstr = substr[j].Split('#');
                            head = head + "<td valign=\"top\" style=\"background-color:#FFFFFF\">" + itemstr[6] + "</td>";
                            if (j >= substr.Length - 1)
                            {
                                modulevalue = modulevalue.Replace("###" + itemstr[0] + "###", head);
                            }
                        }
                    }
                }

                String sourceValue = modulevalue;
                int strlen=0;
                do
                {
                    int beginNum = modulevalue.IndexOf("{***");
                    int endNum = modulevalue.IndexOf("***}");
                    string tableColum = string.Empty;
                    try
                    {
                        tableColum = modulevalue.Substring(beginNum + 4, endNum - beginNum - 4);
                    }
                    catch { break; }
                    if (tableColum.Length < 1) break;
                    for (int i = 0; i < defineControl.Tables[0].Rows.Count; i++)
                    {
                        //控件替换
                        string control = string.Empty;
                        if (tableColum == defineControl.Tables[0].Rows[i]["ccode"].ToString()) //配对获取的表字段
                        {
                            #region 文本框设置
                            if (defineControl.Tables[0].Rows[i]["typeflag"].ToString() == "0") //文本框
                            {
                                if (defineControl.Tables[0].Rows[i]["ismultiline"].ToString() == "0") //单行
                                {
                                    if (defineControl.Tables[0].Rows[i]["isempty"].ToString() == "0")//不可以为空
                                    {
                                        control = "<input type='text' class='tdinput' autotab='true' style='width:90%;' onblur=\"RelationInLoad()\""
                                            + " name='db_" + tableColum + "' id='db_" + tableColum + "' "
                                            + " maxlength=" + defineControl.Tables[0].Rows[i]["length"].ToString() + " value='" + defineControl.Tables[0].Rows[i]["dropdownlistValue"].ToString() + "'/><span style=\"color:red\">*</span>";
                                    }
                                    else
                                    {
                                        control = "<input type='text' class='tdinput' autotab='true' style='width:90%;' onblur=\"RelationInLoad()\""
                                            + " name='db_" + tableColum + "' id='db_" + tableColum + "' "
                                            + " maxlength=" + defineControl.Tables[0].Rows[i]["length"].ToString() + " value='" + defineControl.Tables[0].Rows[i]["dropdownlistValue"].ToString() + "'/>";
                                    }
                                }
                                else
                                {
                                    if (defineControl.Tables[0].Rows[i]["isempty"].ToString() == "0")
                                    {
                                        control = "<textarea rows='4' cols='20' style='height:60px;width:90%;' class='tdinput' "
                                            + " name='db_" + tableColum + "' id='db_" + tableColum + "' "
                                            + " maxname='" + defineControl.Tables[0].Rows[i]["cname"].ToString() + "' maxlength='" + defineControl.Tables[0].Rows[i]["length"].ToString() + "'"
                                            + " >" + "</textarea><span style=\"color:red\">*</span>";
                                    }
                                    else
                                    {
                                        control = "<textarea rows='4' cols='20' style='height:60px;width:90%;' class='tdinput' "
                                            + " name='db_" + tableColum + "' id='db_" + tableColum + "' "
                                            + " maxname='" + defineControl.Tables[0].Rows[i]["cname"].ToString() + "' maxlength='" + defineControl.Tables[0].Rows[i]["length"].ToString() + "'"
                                            + " >" + "</textarea>";
                                    }
                                }
                            }
                            #endregion

                            #region 下拉框设置
                            if (defineControl.Tables[0].Rows[i]["typeflag"].ToString() == "1")
                            {
                                if (defineControl.Tables[0].Rows[i]["isempty"].ToString() == "0")
                                {
                                    control = "<select tdinput id='db_" + defineControl.Tables[0].Rows[i]["ccode"].ToString() + "' style='width:90%'>" + defineControl.Tables[0].Rows[i]["dropdownlistValue"].ToString() + "</select><span style=\"color:red\">*</span>";
                                }
                                else
                                {
                                    control = "<select id='db_" + defineControl.Tables[0].Rows[i]["ccode"].ToString() + "' style='width:90%'>" + defineControl.Tables[0].Rows[i]["dropdownlistValue"].ToString() + "</select>";
                                }
                            }
                            #endregion
                            
                            #region 日期控件设置
                            if (defineControl.Tables[0].Rows[i]["typeflag"].ToString() == "2")
                            {
                                if (defineControl.Tables[0].Rows[i]["isempty"].ToString() == "0")
                                {
                                    control = "<input class='tdinput' id='db_" + defineControl.Tables[0].Rows[i]["ccode"].ToString() + "' maxlength='30' type='text' style='width:90%' readonly onclick=\"WdatePicker({dateFmt:'yyyy-MM-dd',el:$dp.$('db_" + defineControl.Tables[0].Rows[i]["ccode"].ToString() + "')})\" /><span style=\"color:red\">*</span>";
                                }
                                else
                                {
                                    control = "<input class='tdinput' id='db_" + defineControl.Tables[0].Rows[i]["ccode"].ToString() + "' maxlength='30' type='text' style='width:90%' readonly onclick=\"WdatePicker({dateFmt:'yyyy-MM-dd',el:$dp.$('db_" + defineControl.Tables[0].Rows[i]["ccode"].ToString() + "')})\" /> ";
                                }
                            }
                            #endregion

                            sourceValue = sourceValue.Replace("{***" + tableColum.ToString() + "***}", control);
                            break;
                        }
                    }
                    strlen = tableColum.Length;
                    string submodulevalue = modulevalue.Substring(endNum + 4);
                    modulevalue = submodulevalue;
                } while (strlen > 0);
                ltl_input_custom.Text = sourceValue;
            }
            else
            {
                #region 绑定表结构
                this.StructDL.DataSource = defineControl;
                this.StructDL.DataBind();
                #endregion
            }
        }

    }
    protected void StructDL_ItemDataBound(object sender, DataListItemEventArgs e)
    {
        #region 绑定控件框
        Literal ltl_input = (Literal)(e.Item.FindControl("ltl_input"));
        Literal ltl_tag = (Literal)(e.Item.FindControl("ltl_tag"));
        if (DataBinder.Eval(e.Item.DataItem, "isempty").ToString()=="0")
        {
            ltl_tag.Text = "<span class=\"redbold\">*</span>";
        }

        if (DataBinder.Eval(e.Item.DataItem, "typeflag").ToString() == "0")
        {
            #region 判断是多选还是单选
            if (DataBinder.Eval(e.Item.DataItem, "ismultiline").ToString() == "1")
            {
                ltl_input.Text = "<textarea rows='4' cols='20' style='height:60px;width:200px;' class='TextAreaNormal' "
                    + " name='db_" + DataBinder.Eval(e.Item.DataItem, "ccode").ToString() + "' id='db_" + DataBinder.Eval(e.Item.DataItem, "ccode").ToString() + "' "
                    + " maxname='" + DataBinder.Eval(e.Item.DataItem, "cname").ToString() + "' maxlength='" + DataBinder.Eval(e.Item.DataItem, "length").ToString() + "'"
                    + " >" + "</textarea>";
            }
            else
            {
                ltl_input.Text = "<input type='text' class='InputNormal' autotab='true' style='width:200px;' onblur=\"RelationInLoad()\""
                    + " name='db_" + DataBinder.Eval(e.Item.DataItem, "ccode").ToString() + "' id='db_" + DataBinder.Eval(e.Item.DataItem, "ccode").ToString() + "' "
                    + " maxlength=" + DataBinder.Eval(e.Item.DataItem, "length").ToString() + " value='" + "'/>";
            }
            #endregion
        }
        else if (DataBinder.Eval(e.Item.DataItem, "typeflag").ToString() == "1")
        {
            ltl_input.Text = "<select id='db_" + DataBinder.Eval(e.Item.DataItem, "ccode").ToString() + "' style='width:200px'>"+DataBinder.Eval(e.Item.DataItem,"dropdownlistValue").ToString()+"</select>";
        }
        else
        {
            ltl_input.Text = "<input id='db_" + DataBinder.Eval(e.Item.DataItem, "ccode").ToString() + "' maxlength='30' type='text' style='width:98%' readonly onclick=\"WdatePicker({dateFmt:'yyyy-MM-dd',el:$dp.$('db_" + DataBinder.Eval(e.Item.DataItem, "ccode").ToString() + "')})\" /> ";
        }
        #endregion
    }
}
