<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ReportGraphsUC.ascx.cs" Inherits="UserControl_ReportGraphsUC" %>
<%@ Register Assembly="DundasWebChart" Namespace="Dundas.Charting.WebControl" TagPrefix="DCWC" %>
<%@ Register Assembly="CrystalDecisions.Web, Version=10.5.3700.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" Namespace="CrystalDecisions.Web" TagPrefix="CR" %>
<script src="../../../js/JQuery/PrintArea.js" type="text/javascript"></script><script src="../../../js/common/Check.js" type="text/javascript"></script>
<script type="text/javascript">
    //跳转到子页字段切换
    function LinkDetail(Index,Action1,EquipName,TypeID,DeptID,BeginDate,EndDate)
    {
        var IndeId=$("#ReportGraphsUC1_DeptIdList").val().split(",")[parseInt(Index)];
        window.location.href='<%=DetailUrl%>'
    }
</script>
 <table width="95%" height="57" border="0" cellpadding="0" cellspacing="0" class="maintable" id="mainindex">
    <tr>
        <td colspan="2">
            <table width="100%" border="0" cellpadding="0" cellspacing="0" id="tblDetailList">
                <tr>
                    <td valign="top">
                        <img src="../../../images/Main/Line.jpg" width="122" height="7" />
                    </td>
                    <td align="center" valign="top">
                    
                    </td>
                    <td align="center" valign="top" style="display:none;">
                    <asp:DropDownList id="AxisList" runat="server" Width="109px" AutoPostBack="True" tabIndex="5" CssClass="spaceright">
				        <asp:ListItem Value="X Axis" Selected="True">X Axis</asp:ListItem>
				        <asp:ListItem Value="Y Axis">Y Axis</asp:ListItem>
				        <asp:ListItem Value="Both Axis">Both Axis</asp:ListItem>
			        </asp:DropDownList>
			        </td>
                </tr>
                <tr>
                    <td height="30" colspan="3" align="center" valign="top" class="Title"><%=Title%></td>
                </tr>
                <tr>
                    <td height="35" colspan="3" valign="top">
                        <table width="99%" border="0" align="center" cellpadding="0" cellspacing="0" style="border:1px solid #999999">
                            <tr>
                                <td height="28" bgcolor="#FFFFFF" align="right" >
                                <!--START图表切换按钮-->
                                <asp:ImageButton ID="ImgBtn1" ImageUrl="~/images/Button/btn_xian.jpg" OnClientClick="return CheckSearch()" OnClick="btn_line_Click" runat="server" AlternateText="折线图" /> 
                                <asp:ImageButton ID="ImgBtn2" ImageUrl="~/images/Button/btn_zhu.jpg"  OnClientClick="return CheckSearch()"  OnClick="btn_column_Click" runat="server" AlternateText="柱状图" /> 
                                <asp:ImageButton ID="ImageButton2" ImageUrl="~/images/Button/btn_bing.jpg"  OnClientClick="return CheckSearch()"  OnClick="btn_pie_Click" runat="server" AlternateText="饼状图" />
                                 <asp:ImageButton ID="ImageButton1" ImageUrl="~/images/Button/btn_expexcel.jpg"  OnClientClick="return CheckSearch()"  OnClick="btn_exportExcel_Click" runat="server" AlternateText="导出Excel" /> 
                                 <img alt="" src="../../../Images/Button/btn_print.jpg" onclick=" $('div#myPrintArea').printArea();" style="cursor:pointer;" />                             
                                <!--END图表切换按钮-->
                                </td>
                            </tr>
                            <tr>
                                <td>
                                <!--START图表显示-->
                                <div id="myPrintArea">
                                  <table style="width:100%; background-color:#FFFFFF">
                                    <tr id="printhead" style="display:none;" >
                                    <td height="60" align="center" class="Title"  style="font-size:20pt; font-weight:bold">
                                      <%=Title%>
                                    </td>
                                    </tr>
                                    <tr>
                                        <td style="background-color:#FFFFFF" align="center">
                                            <!--dundas报表功能-->
                                            <DCWC:Chart ID="Chart1" runat="server" Width="1000px" Height="400px" BackColor="#FAFAFA" 
                                                BackGradientEndColor="Lavender" BorderLineColor="Silver" 
                                                BorderLineStyle="DashDotDot" Palette="EarthTones" BorderLineWidth="0" 
                                                EnableTheming="False">
                                                <legends>
                                                    <DCWC:Legend BackColor="237, 244, 247" BorderColor="LightSlateGray" 
                                                        Name="Default" ShadowOffset="1">
                                                    </DCWC:Legend>
                                                </legends>
                                                <BorderSkin FrameBackColor="153, 255, 102" 
                                                    FrameBackGradientEndColor="LightBlue" FrameBorderColor="100, 0, 0, 0" 
                                                    FrameBorderWidth="2" PageColor="Transparent" />
                                                <Titles>
                                                    <DCWC:Title Name="Title1">
                                                    </DCWC:Title>
                                                </Titles>
                                                <series>
                                                    <DCWC:Series Name="Series1" BorderWidth="2" ChartType="Spline" 
                                                        MarkerBorderColor="64, 64, 64" MarkerStyle="Circle" ShadowOffset="1">
                                                    </DCWC:Series>
                                                </series>
                                                <ChartAreas>
                                                    <DCWC:ChartArea Name="Default" BackColor="White" 
                                                        BackGradientEndColor="AntiqueWhite" BackGradientType="TopBottom" 
                                                        BorderColor="50, 255, 255, 255" BorderStyle="Solid" BorderWidth="0">
                                                         <AxisY LineColor="220, 0, 0, 0"  TitleFont="宋体,12pt,style=bold">
                                                            <MajorGrid LineColor="40, 0, 0, 0" LineWidth="2" />
                                                            <MinorGrid LineColor="70, 0, 0, 0" />
                                                            <MajorTickMark LineColor="100, 0, 0, 0" />
                                                            <MinorTickMark LineColor="100, 0, 0, 0" Size="2" />
                                                            <LabelStyle FontColor="220, 0, 0, 0" />
                                                        </AxisY>
                                                        <AxisX LineColor="220, 0, 0, 0"  TitleFont="宋体,12pt,style=bold">
                                                            <MajorGrid LineColor="40, 0, 0, 0" LineWidth="2" />
                                                            <MinorGrid LineColor="70, 0, 0, 0" />
                                                            <MajorTickMark LineColor="100, 0, 0, 0" />
                                                            <MinorTickMark LineColor="100, 0, 0, 0" Size="2" />
                                                            <LabelStyle FontColor="220, 0, 0, 0" />
                                                        </AxisX>
                                                        <Area3DStyle Light="Realistic" WallWidth="0" />
                                                    </DCWC:ChartArea>
                                                </ChartAreas>
                                            </DCWC:Chart>
                                        </td>
                                    </tr>
                                    </table>
                                </div>
                                <!--END图表显示-->
                                </td>
                             </tr>
                            
                             <tr runat="server" id="nodata">
                                <td  bgcolor="#FFFFFF" align="center" style="height:300px;">
                                    <img src="../../../Images/error_new.jpg" />
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                 <tr>
                     <td style="height:20px;" colspan="3"></td>
                 </tr>
            </table>
        </td>
    </tr>
</table>
<asp:HiddenField ID="DeptIdList" runat="server" />
    