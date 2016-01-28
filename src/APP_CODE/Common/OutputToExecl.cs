using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Windows.Forms;
using Microsoft.Office.Interop.Excel;
using System.IO;
using System.Web;

namespace XBase.Common
{
    public class OutputToExecl
    {
        #region 公共方法
        /// <summary>
        /// 报表导出excel共通函数（适用于DataTable绑定DataGridView）
        /// </summary>
        /// <param name="Grid">数据源</param>
        /// <param name="ReportTitle">报表名</param>
        /// <param name="DPTMessage">信息栏信息</param>
        public static void ExportDataTableToExcel(System.Data.DataTable dbSource, string[] header,string ReportTitle, string DPTMessage)
        {
            System.Data.DataTable myTable = dbSource;

            try
            {
                Microsoft.Office.Interop.Excel.Application xlApp = new Microsoft.Office.Interop.Excel.ApplicationClass();
                
                //定义起始行,列
                int rowIndex;
                int colIndex;

                rowIndex = 2;
                colIndex = 0;

                Microsoft.Office.Interop.Excel.Workbook xlBook = xlApp.Workbooks.Add(true);
                Microsoft.Office.Interop.Excel.Range range = xlApp.get_Range(xlApp.Cells[1, 1], xlApp.Cells[1, myTable.Columns.Count]);
                range.MergeCells = true;
                xlApp.ActiveCell.FormulaR1C1 = ReportTitle;
                xlApp.ActiveCell.Font.Size = 14;
                xlApp.ActiveCell.Font.Bold = true;
                //设置禁止弹出保存和覆盖的询问提示框
                xlApp.DisplayAlerts = true;


                //将表中的栏位名称填到Excel的第一行
                //foreach (DataColumn Col in myTable.Columns)
                //{
                //    colIndex = colIndex + 1;
                //    xlApp.Cells[2, colIndex] = Col.ColumnName;
                //}
                for (int i = 0; i < header.Length; i++)
                {
                    colIndex = colIndex + 1;
                    xlApp.Cells[2, colIndex] = header[i].ToString();
                }

                //得到的表所有行,赋值给单元格
                for (int row = 0; row < myTable.Rows.Count; row++)
                {
                    rowIndex = rowIndex + 1;
                    colIndex = 0;
                    for (int col = 0; col < myTable.Columns.Count; col++)
                    {
                        colIndex = colIndex + 1;
                        xlApp.Cells[rowIndex, colIndex] = dbSource.Rows[row][col].ToString();
                    }
                }
                xlApp.get_Range(xlApp.Cells[2, 1], xlApp.Cells[2, colIndex]).Font.Bold = true;
                xlApp.get_Range(xlApp.Cells[2, 1], xlApp.Cells[rowIndex, colIndex]).Borders.LineStyle = 1;

                xlApp.Cells.EntireColumn.AutoFit();
                xlApp.Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.Constants.xlCenter;
                xlApp.Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.Constants.xlCenter;
                xlApp.Visible = true;
                //
                //加载一个合计行
                //
                int rowSum = rowIndex;
                Microsoft.Office.Interop.Excel.Range Totle = xlApp.get_Range(xlApp.Cells[rowSum + 1, 1], xlApp.Cells[rowSum + 1, dbSource.Columns.Count]);
                Totle.MergeCells = true;
                xlApp.Cells[rowSum + 1, 1] = DPTMessage;
                xlApp.ActiveCell.Font.Bold = true;
                //字体
                xlApp.get_Range(xlApp.Cells[rowSum + 1, 1], xlApp.Cells[rowSum + 1, 1]).Font.Size = 10;
                //对齐方式
                xlApp.get_Range(xlApp.Cells[rowSum + 1, 1], xlApp.Cells[rowSum + 1, 1]).HorizontalAlignment = Microsoft.Office.Interop.Excel.Constants.xlLeft;

                //
                //绘制边框
                //
                xlApp.get_Range(xlApp.Cells[3, 1], xlApp.Cells[rowSum, colIndex]).Borders.LineStyle = 1;
                xlApp.get_Range(xlApp.Cells[3, 1], xlApp.Cells[rowSum, 2]).Borders[XlBordersIndex.xlEdgeLeft].Weight = XlBorderWeight.xlThin;//设置左边线加粗
                xlApp.get_Range(xlApp.Cells[3, 1], xlApp.Cells[4, colIndex]).Borders[XlBordersIndex.xlEdgeTop].Weight = XlBorderWeight.xlThin;//设置上边线加粗
                xlApp.get_Range(xlApp.Cells[3, colIndex], xlApp.Cells[rowSum, colIndex]).Borders[XlBordersIndex.xlEdgeRight].Weight = XlBorderWeight.xlThin;//设置右边线加粗
                xlApp.get_Range(xlApp.Cells[rowSum, 1], xlApp.Cells[rowSum, colIndex]).Borders[XlBordersIndex.xlEdgeBottom].Weight = XlBorderWeight.xlThin;//设置下边线加粗

                System.Runtime.InteropServices.Marshal.ReleaseComObject(xlApp);
                GC.Collect();
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        #endregion

        #region 导出到Excel
        /// <summary>
        /// 导出Excel
        /// </summary>
        /// <param name="page"></param>
        /// <param name="dt">DataTable 数据源</param>
        /// <param name="header">标题头</param>
        /// <param name="field">标题头对应字段名称</param>
        /// <param name="fileName">生成保存的文件名(例：WorkCenter,后面的.xls不需要)</param>
        public static void ExportToTable(System.Web.UI.Page page, System.Data.DataTable dt, string[] header,string[] field,string fileName)
        {
            #region 导出
            HttpResponse resp;
            
            resp = page.Response;
            resp.Clear();
            resp.ContentEncoding = System.Text.Encoding.Default;
            System.Web.HttpContext.Current.Response.Charset = "gb2312";
            resp.ContentEncoding = System.Text.Encoding.GetEncoding("GB2312");

            string UserAgent = page.Request.ServerVariables["http_user_agent"].ToLower();
            if (UserAgent.IndexOf("firefox") == -1)//不是ff时
            {
                fileName = HttpUtility.UrlEncode(fileName.Trim(), System.Text.Encoding.UTF8);//utf8编码中文
            }

            string OutFile = fileName;


            resp.AppendHeader("Content-Disposition", "attachment;filename=" + OutFile + ".xls\"");
            resp.ContentType = "application/ms-excel";

            string lsCreator_item = "";
            string lsDateTime_item = "";
            string lsSpace_item = "";
            string lsHeader_item = "";
            string lsField_item = "";

            

            if (dt.Rows.Count > 0)
            {
                if (header.Length == field.Length)
                {
                    if (header.Length > 2)
                    {
                        for (int i = 0; i <= header.Length - 1; i++)
                        {
                            lsHeader_item += header[i].ToString() + Convert.ToChar(9);
                            if (i == 0)
                            {
                                lsCreator_item += "制表人：" + Convert.ToChar(9);
                                lsDateTime_item += "制表日期：" + Convert.ToChar(9);
                                lsSpace_item = "" + Convert.ToChar(9);
                            }
                            else if (i == 1)
                            {
                                lsCreator_item += ((UserInfoUtil)SessionUtil.Session["UserInfo"]).EmployeeName + Convert.ToChar(9);
                                lsDateTime_item += DateTime.Now.ToString("yyyy-MM-dd") + Convert.ToChar(9);
                                lsSpace_item = "" + Convert.ToChar(9);
                            }
                            else if (i == header.Length - 1)
                            {
                                lsCreator_item += "" + Convert.ToChar(13);
                                lsDateTime_item += "" + Convert.ToChar(13);
                                lsSpace_item = "" + Convert.ToChar(13);
                                lsHeader_item += Convert.ToChar(13);

                                resp.Write(lsCreator_item);
                                resp.Write(lsDateTime_item);
                                resp.Write(lsSpace_item);
                                resp.Write(lsHeader_item);
                            }
                            else
                            {
                                lsCreator_item += "" + Convert.ToChar(9);
                                lsDateTime_item += "" + Convert.ToChar(9);
                                lsSpace_item = "" + Convert.ToChar(9);
                            }

                        }
                    }
                    else
                    {
                        for (int i = 0; i <= header.Length - 1; i++)
                        {
                            lsHeader_item += header[i].ToString() + Convert.ToChar(9);
                            if (i == 0)
                            {
                                lsCreator_item += "制表人：" + Convert.ToChar(9);
                                lsDateTime_item += "制表日期：" + Convert.ToChar(9);
                                lsSpace_item = "" + Convert.ToChar(9);
                            }
                            else if (i == 1)
                            {
                                lsCreator_item += ((UserInfoUtil)SessionUtil.Session["UserInfo"]).EmployeeName + Convert.ToChar(9);
                                lsDateTime_item += DateTime.Now.ToString("yyyy-MM-dd") + Convert.ToChar(9);
                                lsSpace_item = "" + Convert.ToChar(9);

                                lsCreator_item += "" + Convert.ToChar(13);
                                lsDateTime_item += "" + Convert.ToChar(13);
                                lsSpace_item = "" + Convert.ToChar(13);
                                lsHeader_item += Convert.ToChar(13);

                                resp.Write(lsCreator_item);
                                resp.Write(lsDateTime_item);
                                resp.Write(lsSpace_item);
                                resp.Write(lsHeader_item);
                            }

                        }
                    }

                }
            }


            for (int j = 0; j < dt.Rows.Count; j++)
            {
                for (int n = 0; n <= field.Length-1; n++)
                {
                    if (n == field.Length - 1)
                    {
                        lsField_item += dt.Rows[j][field[n].ToString()].ToString() + Convert.ToChar(127) + Convert.ToChar(13);
                    }
                    else
                    {
                        lsField_item += dt.Rows[j][field[n].ToString()].ToString() + Convert.ToChar(127) + Convert.ToChar(9);
                    }
                }
                resp.Write(lsField_item);
                lsField_item = "";
            }

            resp.End();
            #endregion
        }
        #endregion

        #region 导出到Excel并合计
        /// <summary>
        /// 导出Excel
        /// </summary>
        /// <param name="page"></param>
        /// <param name="dt">DataTable 数据源</param>
        /// <param name="header">标题头</param>
        /// <param name="field">标题头对应字段名称</param>
        /// <param name="fileName">生成保存的文件名(例：WorkCenter,后面的.xls不需要)</param>
        public static void ExportToTableAndCount(System.Web.UI.Page page, System.Data.DataTable dt, string[] header, string[] field, string fileName, string[] countField, string[] countValue)
        {
            #region 导出
            HttpResponse resp;

            resp = page.Response;
            resp.Clear();
            resp.ContentEncoding = System.Text.Encoding.Default;
            System.Web.HttpContext.Current.Response.Charset = "gb2312";
            resp.ContentEncoding = System.Text.Encoding.GetEncoding("GB2312");

            string UserAgent = page.Request.ServerVariables["http_user_agent"].ToLower();
            if (UserAgent.IndexOf("firefox") == -1)
            {
                fileName = HttpUtility.UrlEncode(fileName.Trim(), System.Text.Encoding.UTF8);//utf8编码中文
            }

            string OutFile = fileName;


            resp.AppendHeader("Content-Disposition", "attachment;filename=" + OutFile + ".xls\"");
            resp.ContentType = "application/ms-excel";

            string lsCreator_item = "";
            string lsDateTime_item = "";
            string lsSpace_item = "";
            string lsHeader_item = "";
            string lsField_item = "";



            if (dt.Rows.Count > 0)
            {
                if (header.Length == field.Length)
                {
                    if (header.Length > 2)
                    {
                        for (int i = 0; i <= header.Length - 1; i++)
                        {
                            lsHeader_item += header[i].ToString() + Convert.ToChar(9);
                            if (i == 0)
                            {
                                lsCreator_item += "制表人：" + Convert.ToChar(9);
                                lsDateTime_item += "制表日期：" + Convert.ToChar(9);
                                lsSpace_item = "" + Convert.ToChar(9);
                            }
                            else if (i == 1)
                            {
                                lsCreator_item += ((UserInfoUtil)SessionUtil.Session["UserInfo"]).EmployeeName + Convert.ToChar(9);
                                lsDateTime_item += DateTime.Now.ToString("yyyy-MM-dd") + Convert.ToChar(9);
                                lsSpace_item = "" + Convert.ToChar(9);
                            }
                            else if (i == header.Length - 1)
                            {
                                lsCreator_item += "" + Convert.ToChar(13);
                                lsDateTime_item += "" + Convert.ToChar(13);
                                lsSpace_item = "" + Convert.ToChar(13);
                                lsHeader_item += Convert.ToChar(13);

                                resp.Write(lsCreator_item);
                                resp.Write(lsDateTime_item);
                                resp.Write(lsSpace_item);
                                resp.Write(lsHeader_item);
                            }
                            else
                            {
                                lsCreator_item += "" + Convert.ToChar(9);
                                lsDateTime_item += "" + Convert.ToChar(9);
                                lsSpace_item = "" + Convert.ToChar(9);
                            }

                        }
                    }
                    else
                    {
                        for (int i = 0; i <= header.Length - 1; i++)
                        {
                            lsHeader_item += header[i].ToString() + Convert.ToChar(9);
                            if (i == 0)
                            {
                                lsCreator_item += "制表人：" + Convert.ToChar(9);
                                lsDateTime_item += "制表日期：" + Convert.ToChar(9);
                                lsSpace_item = "" + Convert.ToChar(9);
                            }
                            else if (i == 1)
                            {
                                lsCreator_item += ((UserInfoUtil)SessionUtil.Session["UserInfo"]).EmployeeName + Convert.ToChar(9);
                                lsDateTime_item += DateTime.Now.ToString("yyyy-MM-dd") + Convert.ToChar(9);
                                lsSpace_item = "" + Convert.ToChar(9);

                                lsCreator_item += "" + Convert.ToChar(13);
                                lsDateTime_item += "" + Convert.ToChar(13);
                                lsSpace_item = "" + Convert.ToChar(13);
                                lsHeader_item += Convert.ToChar(13);

                                resp.Write(lsCreator_item);
                                resp.Write(lsDateTime_item);
                                resp.Write(lsSpace_item);
                                resp.Write(lsHeader_item);
                            }

                        }
                    }

                }
            }


            for (int j = 0; j < dt.Rows.Count; j++)
            {
                for (int n = 0; n <= field.Length - 1; n++)
                {
                    if (n == field.Length - 1)
                    {
                        lsField_item += dt.Rows[j][field[n].ToString()].ToString() + Convert.ToChar(13);
                    }
                    else
                    {
                        lsField_item += dt.Rows[j][field[n].ToString()].ToString() + Convert.ToChar(9);
                    }
                }
                resp.Write(lsField_item);
                lsField_item = "";
            }
            string CountStr = "";
            if (countField.Length == countValue.Length)
            {
                CountStr += "" + Convert.ToChar(13) + "合计信息" + Convert.ToChar(9);
                for (int x = 0; x < countField.Length; x++)
                {
                    CountStr += countField[x] + Convert.ToChar(9);
                    CountStr += countValue[x] + Convert.ToChar(9);
                }
                resp.Write(CountStr);
                CountStr = "";
            }
            resp.End();
            #endregion
        }
        #endregion

        #region 导出到Excel 并格式化日期 浮点型数据保留两位有效数字
        /// <summary>
        /// 导出Excel
        /// </summary>
        /// <param name="page"></param>
        /// <param name="dt">DataTable 数据源</param>
        /// <param name="header">标题头</param>
        /// <param name="field">标题头对应字段名称</param>
        /// <param name="fileName">生成保存的文件名(例：WorkCenter,后面的.xls不需要)</param>
        public static void ExportToTableFormat(System.Web.UI.Page page, System.Data.DataTable dt, string[] header, string[] field, string fileName)
        {
            #region 导出
            HttpResponse resp;

            resp = page.Response;
            resp.Clear();
            resp.ContentEncoding = System.Text.Encoding.Default;
            System.Web.HttpContext.Current.Response.Charset = "gb2312";
            resp.ContentEncoding = System.Text.Encoding.GetEncoding("GB2312");

            string UserAgent = page.Request.ServerVariables["http_user_agent"].ToLower();
            if (UserAgent.IndexOf("firefox") == -1)//不是ff时
            {
                fileName = HttpUtility.UrlEncode(fileName.Trim(), System.Text.Encoding.UTF8);//utf8编码中文
            }

            string OutFile = fileName;


            resp.AppendHeader("Content-Disposition", "attachment;filename=" + OutFile + ".xls\"");
            resp.ContentType = "application/ms-excel";

            string lsCreator_item = "";
            string lsDateTime_item = "";
            string lsSpace_item = "";
            string lsHeader_item = "";
            string lsField_item = "";



            if (dt.Rows.Count > 0)
            {
                if (header.Length == field.Length)
                {
                    if (header.Length > 2)
                    {
                        for (int i = 0; i <= header.Length - 1; i++)
                        {
                            lsHeader_item += header[i].ToString() + Convert.ToChar(9);
                            if (i == 0)
                            {
                                lsCreator_item += "制表人：" + Convert.ToChar(9);
                                lsDateTime_item += "制表日期：" + Convert.ToChar(9);
                                lsSpace_item = "" + Convert.ToChar(9);
                            }
                            else if (i == 1)
                            {
                                lsCreator_item += ((UserInfoUtil)SessionUtil.Session["UserInfo"]).EmployeeName + Convert.ToChar(9);
                                lsDateTime_item += DateTime.Now.ToString("yyyy-MM-dd") + Convert.ToChar(9);
                                lsSpace_item = "" + Convert.ToChar(9);
                            }
                            else if (i == header.Length - 1)
                            {
                                lsCreator_item += "" + Convert.ToChar(13);
                                lsDateTime_item += "" + Convert.ToChar(13);
                                lsSpace_item = "" + Convert.ToChar(13);
                                lsHeader_item += Convert.ToChar(13);

                                resp.Write(lsCreator_item);
                                resp.Write(lsDateTime_item);
                                resp.Write(lsSpace_item);
                                resp.Write(lsHeader_item);
                            }
                            else
                            {
                                lsCreator_item += "" + Convert.ToChar(9);
                                lsDateTime_item += "" + Convert.ToChar(9);
                                lsSpace_item = "" + Convert.ToChar(9);
                            }

                        }
                    }
                    else
                    {
                        for (int i = 0; i <= header.Length - 1; i++)
                        {
                            lsHeader_item += header[i].ToString() + Convert.ToChar(9);
                            if (i == 0)
                            {
                                lsCreator_item += "制表人：" + Convert.ToChar(9);
                                lsDateTime_item += "制表日期：" + Convert.ToChar(9);
                                lsSpace_item = "" + Convert.ToChar(9);
                            }
                            else if (i == 1)
                            {
                                lsCreator_item += ((UserInfoUtil)SessionUtil.Session["UserInfo"]).EmployeeName + Convert.ToChar(9);
                                lsDateTime_item += DateTime.Now.ToString("yyyy-MM-dd") + Convert.ToChar(9);
                                lsSpace_item = "" + Convert.ToChar(9);

                                lsCreator_item += "" + Convert.ToChar(13);
                                lsDateTime_item += "" + Convert.ToChar(13);
                                lsSpace_item = "" + Convert.ToChar(13);
                                lsHeader_item += Convert.ToChar(13);

                                resp.Write(lsCreator_item);
                                resp.Write(lsDateTime_item);
                                resp.Write(lsSpace_item);
                                resp.Write(lsHeader_item);
                            }

                        }
                    }

                }
            }


            for (int j = 0; j < dt.Rows.Count; j++)
            {
                for (int n = 0; n <= field.Length - 1; n++)
                {
                    if (n == field.Length - 1)
                    {
                        //lsField_item += dt.Rows[j][field[n].ToString()].ToString() + Convert.ToChar(13);
                        if (dt.Columns[field[n].ToString()].DataType.ToString() == "System.DateTime")
                            lsField_item += (string.IsNullOrEmpty(dt.Rows[j][field[n].ToString()].ToString()) ? string.Empty : Convert.ToDateTime(dt.Rows[j][field[n].ToString()].ToString()).ToString("yyyy-MM-dd")) +Convert.ToChar(13);
                        else if (dt.Columns[field[n].ToString()].DataType.ToString() == "System.Decimal")
                            lsField_item += (string.IsNullOrEmpty(dt.Rows[j][field[n].ToString()].ToString()) ? "0.00" : Convert.ToDecimal(dt.Rows[j][field[n].ToString()].ToString()).ToString("#0.00"))+ Convert.ToChar(13);
                        else
                            lsField_item += dt.Rows[j][field[n].ToString()].ToString() + Convert.ToChar(13);
                    }
                    else
                    {
                       // lsField_item += dt.Rows[j][field[n].ToString()].ToString() + Convert.ToChar(9);
                        if (dt.Columns[field[n].ToString()].DataType.ToString() == "System.DateTime")
                            lsField_item += (string.IsNullOrEmpty(dt.Rows[j][field[n].ToString()].ToString()) ? string.Empty : Convert.ToDateTime(dt.Rows[j][field[n].ToString()].ToString()).ToString("yyyy-MM-dd"))  + Convert.ToChar(9);
                        else if (dt.Columns[field[n].ToString()].DataType.ToString() == "System.Decimal")
                            lsField_item += (string.IsNullOrEmpty(dt.Rows[j][field[n].ToString()].ToString()) ? "0.00" : Convert.ToDecimal(dt.Rows[j][field[n].ToString()].ToString()).ToString("#0.00"))+ Convert.ToChar(9);
                        else
                            lsField_item += dt.Rows[j][field[n].ToString()].ToString() + Convert.ToChar(9);
                    }
                }
                resp.Write(lsField_item);
                lsField_item = "";
            }

            resp.End();
            #endregion
        }
        #endregion
    }
}
