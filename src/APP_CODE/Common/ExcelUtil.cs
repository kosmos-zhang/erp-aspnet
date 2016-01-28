/**********************************************
 * 作    者： 吴志强
 * 创建日期： 2009.01.04
 * 描    述： Excel文件操作类
 * 版    本： 0.5.0
 * 修改日期:2009.03.07
 * 修改人:胡德正
 ***********************************************/

using System.Data;
using Microsoft.Office.Interop.Excel;
using System.Configuration;
using System;
using System.Reflection;
using System.IO;

namespace XBase.Common
{
    /// <summary>
    /// 类名：ExcelUtil
    /// 描述：提供Excel文件操作一些公用方法
    /// 
    /// 作者：吴志强
    /// 创建时间：2009/01/07
    /// 最后修改时间：2009/01/07
    /// 最后修改时间：2009/03/07
    /// 最后修改人：胡德正
    /// </summary>
    ///
    public class ExcelUtil
    {
        //Excel文件保存路径KEY
        private const string EXCEL_SAVE_PATH_KEY = "EXCEL_SAVE_PATH";
        /// <summary>
        /// Excel文件保存路径
        /// </summary>
        private static string _ExcelSavePath = null;
        //Excel模板文件保存路径KEY
        private const string EXCEL_TEMPLATE_PATH_KEY = "EXCEL_TEMPLATE_PATH";
        /// <summary>
        /// Excel模板文件保存路径
        /// </summary>
        private static string _ExcelTemplatePath = null;

        /// <summary>
        /// 初始化Excel文件保存路径
        /// </summary>
        private static void InitPath()
        {
            //Excel文件保存路径
            _ExcelSavePath = ConfigurationManager.AppSettings[EXCEL_SAVE_PATH_KEY];
            //Excel模板文件保存路径
            _ExcelTemplatePath = ConfigurationManager.AppSettings[EXCEL_TEMPLATE_PATH_KEY];
        }


        #region "直接保存成Excel文件，没有模板"

        /// <summary>
        /// 保存Excel文件,默认标题为 DataTable 中的列名称
        /// 如果文件名未输入，返回false
        /// 如果数据源的数据为空时，返回false
        /// <param name="fileName">Excel文件名,不包含路径</param>
        /// <param name="excelData">要保存的数据源</param>
        /// <returns>保存成功 true 保存失败 false</returns>
        /// </summary>
        public static bool SaveExcelFile(string fileName, System.Data.DataTable excelData)
        {
            //如果文件名未输入，返回false
            if (StringUtil.IsNullOrBlank(fileName))
            {
                return false;
            }
            //如果数据源为空时，返回false
            if (excelData == null || excelData.Rows.Count < 1)
            {
                return false;
            }
            //获得数据源的列数
            int colCount = excelData.Columns.Count;
            //定义标题变量
            string[] header = new string[colCount];
            //设置标题值
            for (int i = 0; i < colCount; i++)
            {
                header[i] = excelData.Columns[i].Caption;
            }
            return SaveExcelFile(fileName, excelData, header, 1, 1);
        }

        /// <summary>
        /// 保存Excel文件,默认标题为 DataTable 中的列名称
        /// 如果文件名未输入，返回false
        /// 如果数据源的数据为空时，返回false
        /// <param name="fileName">Excel文件名,不包含路径</param>
        /// <param name="excelData">要保存的数据源</param>
        /// <param name="header">Excel表格表头</param>
        /// <returns>保存成功 true 保存失败 false</returns>
        /// </summary>
        public static bool SaveExcelFile(string fileName, System.Data.DataTable excelData, string[] header)
        {
            return SaveExcelFile(fileName, excelData, header, 1, 1);
        }

        /// <summary>
        /// 保存Excel文件
        /// 如果文件名未输入，返回false
        /// 如果数据源的数据为空时，返回false
        /// <param name="fileName">Excel文件名,不包含路径</param>
        /// <param name="excelData">要保存的数据源</param>
        /// <param name="header">Excel表格表头</param>
        /// <param name="startRow">Excel文件开始行数</param>
        /// <param name="startColumn">Excel文件开始列数</param>
        /// <returns>保存成功 true 保存失败 false</returns>
        /// </summary>
        public static bool SaveExcelFile(string fileName, System.Data.DataTable excelData, string[] header, int startRow, int startColumn)
        {
            //如果文件名未输入，返回false
            if (StringUtil.IsNullOrBlank(fileName))
            {
                return false;
            }
            //如果数据源为空时，返回false
            if (excelData == null || excelData.Rows.Count < 1)
            {
                return false;
            }
            //打开Excel
            Application excelApp = new Application();
            try
            {
                //新增加一个工作簿
                Workbook excelBook = excelApp.Workbooks.Add(true);
                //取得一个工作表
                Worksheet excelSheet = (Worksheet)excelBook.Worksheets[1];
                //标题列数
                int columnCount;
                //对列数进行设置
                if (header == null || header.Length < 1)
                {
                    //标题为空时，将列数设为0
                    columnCount = 0;
                }
                else
                {
                    //标题为不空时，将列数设为标题长度
                    columnCount = header.Length;
                }
                //如果传入的开始行小于1，则将开始行置为1
                startRow = startRow > 0 ? startRow : 1;
                //如果传入的开始列小于1，则将开始列置为1
                startColumn = startColumn > 0 ? startColumn : 1;
                if (columnCount != 0)//表示表头为空
                {
                    for (int j = 0; j < columnCount; j++)
                    {
                        //获取单元格
                        Range headRange = (Range)excelSheet.Cells[startRow, startColumn + j];
                        //设置单元格值
                        headRange.Value2 = header[j].ToString().Trim();
                        headRange.HorizontalAlignment = XlHAlign.xlHAlignCenter; //水平
                        headRange.VerticalAlignment = XlVAlign.xlVAlignCenter; //垂直
                    }
                    startRow += 1;
                }
                //设置数据明细
                WriteDataToExcel(excelSheet, excelData, startRow, startColumn);
                //excelSheet.get_Range(excelSheet.Cells[1,3], excelSheet.Cells[2, 3]).MergeCells = true;
                //设置禁止弹出保存的提示框
                excelApp.DisplayAlerts = false;
                //设置禁止弹出覆盖询问的提示框
                excelApp.AlertBeforeOverwriting = false;

                //如果Excel保存路径未取得时，先取保存路径
                if (_ExcelSavePath == null || string.Empty.Equals(_ExcelSavePath))
                {
                    InitPath();
                }
                //设置完整Excel文件名
                string fullFileName = Path.Combine(_ExcelSavePath, fileName);
                //保存工作表
                excelSheet.SaveAs(fullFileName, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value);
                //保存工作簿
                excelBook.Save();

                return true;
            }
            catch (Exception ex)
            {
                //输出日志
                ex.ToString();
                return false;
            }
            finally
            {
                //确保Excel进程关闭
                excelApp.Quit();
                excelApp = null;
            }
        }

        #endregion


        #region "通过模板保存成新的Excel文件"

        /// <summary>
        /// 通过Excel模板，将数据源保存到Excel文件
        /// 如果文件名或者模板名未输入，返回false
        /// <param name="templetFileName">模板文件名</param>
        /// <param name="fileName">新Excel文件名</param>
        /// <param name="sheetName">Excel模板的工作表名</param>
        /// <param name="excelData">票据明细表</param>
        /// <param name="dataStartRow">明细的开始行数</param>
        /// <param name="dataStartColumn">明细的开始列数</param>
        /// <param name="Canshu">票据数据</param>
        /// <param name="CanshuSite">票据数据显示位置</param>
        /// <returns>保存成功 true 保存失败 false</returns>
        /// </summary>
        public static bool SaveExcelFromModel(string templetFileName, string fileName, string sheetName
                                                , System.Data.DataTable excelData, int dataStartRow
                                                , int dataStartColumn, string[] Canshu, string[] CanshuSite)
        {
            int ISexcelData = 0;
            int ISCanshu = 0;
            //如果文件名未输入，返回false
            if (StringUtil.IsNullOrBlank(templetFileName) || StringUtil.IsNullOrBlank(fileName))
            {
                return false;
            }
            //如果数据源为空时标志设1
            if (excelData == null || excelData.Rows.Count < 1)
            {
                ISexcelData = 1;
            }
            //如果票据为空时标志设1
            if (Canshu == null || Canshu.Length < 1)
            {
                ISCanshu = 1;
            }

            //如果路径值未取得，则初始化路径
            if (_ExcelTemplatePath == null || string.Empty.Equals(_ExcelTemplatePath)
                                            || _ExcelSavePath == null || string.Empty.Equals(_ExcelSavePath))
            {
                InitPath();
            }
            //获得模板文件全名
            string templateFile = Path.Combine(_ExcelTemplatePath, templetFileName).ToString().Trim();
            //获得Excel文件全名
            string excelFile = Path.Combine(_ExcelSavePath, fileName);
            //从模板拷贝到目标文件
            File.Copy(templateFile, excelFile, true);
            //将目标文件设置为可写的
            new FileInfo(excelFile).Attributes = FileAttributes.Normal;

            //打开Excel
            Application excelApp = new Application();
            try
            {
                //新增加一个工作簿
                Workbook excelBook = excelApp.Workbooks.Open(excelFile, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value);
                //取得模板工作表
                Worksheet excelSheet;
                //如果工作表名没有设置，则取第一个工作表 
                if (StringUtil.IsNullOrBlank(sheetName))
                {
                    excelSheet = (Worksheet)excelBook.Worksheets[1];
                }
                else
                {
                    //如果工作表名有设置，取得对应工作表
                    excelSheet = (Worksheet)excelBook.Worksheets[sheetName];
                }
                //如果传入的开始行小于1，则将开始行置为1
                dataStartRow = dataStartRow > 0 ? dataStartRow : 1;
                //如果传入的开始列小于1，则将开始列置为1
                dataStartColumn = dataStartColumn > 0 ? dataStartColumn : 1;
                if (ISexcelData == 0)
                {
                    //设置数据明细
                    WriteDataToExcel(excelSheet, excelData, dataStartRow, dataStartColumn);
                }
                if (ISCanshu == 0)
                {
                    //写票据数据
                    WriteCanshuToExcel(excelSheet, Canshu, CanshuSite);
                }
                //设置禁止弹出保存的提示框
                excelApp.DisplayAlerts = false;
                //设置禁止弹出覆盖询问的提示框
                excelApp.AlertBeforeOverwriting = false;
                //保存工作表
                //excelSheet.Save();
                //保存工作簿
                excelBook.Save();

                return true;
            }
            catch (Exception ex)
            {
                //输出日志
                ex.ToString();
                return false;
            }
            finally
            {
                //确保Excel进程关闭
                excelApp.Quit();
                excelApp = null;
            }
        }

        #endregion


        # region "将数据写入Excel文件"

        /// <summary>
        /// 将数据明细写入Excel文件
        /// </summary>
        /// <param name="excelSheet">工作表</param>
        /// <param name="excelData">数据源</param>
        /// <param name="startRow">记录开始行号</param>
        /// <param name="startColumn">记录开始列号</param>
        private static void WriteDataToExcel(Worksheet excelSheet, System.Data.DataTable excelData, int startRow, int startColumn)
        {
            //如果数据存在，设置数据明细
            if (excelData != null || excelData.Rows.Count > 0)
            {
                //获取数据源中每条行记录的列数
                int dataColumnCount = excelData.Columns.Count;
                //遍历数据源的每条记录
                for (int i = 0; i < excelData.Rows.Count; i++)
                {
                    //获得行记录
                    DataRow dataRow = excelData.Rows[i];
                    for (int j = 0; j < dataColumnCount; j++)
                    {
                        //获取单元格
                        Range dataRange = (Range)excelSheet.Cells[startRow + i, startColumn + j];
                        //设置单元格值
                        dataRange.Value2 = dataRow[j].ToString().Trim();
                    }
                }
            }
        }
        #endregion

        # region "将票据数据写入Excel文件"

        /// <summary>
        /// 将票据数据写入Excel文件
        /// </summary>
        /// <param name="excelSheet">工作表</param>
        /// <param name="Canshu">票据数据</param>
        /// <param name="CanshuSite">数据位置</param>
        private static void WriteCanshuToExcel(Worksheet excelSheet, string[] Canshu, string[] CanshuSite)
        {
            //如果票据数据存在，设置数据明细
            if (Canshu != null || Canshu.Length > 0 || CanshuSite != null || CanshuSite.Length > 0)
            {

                for (int i = 0; i < Canshu.Length; i++)
                {    //获取表中位置
                    string CanshuSiteString = CanshuSite[i].Trim().ToString();
                    string[] CSA = CanshuSiteString.Split('|');
                    int CSA1 = Convert.ToInt32(CSA[0]);
                    int CSA2 = Convert.ToInt32(CSA[1]);
                    //获取单元格
                    Range dataRange = (Range)excelSheet.Cells[CSA1, CSA2];
                    //设置单元格值
                    dataRange.Value2 = Canshu[i].Trim();
                }

            }

        }



        #endregion

    }
}

