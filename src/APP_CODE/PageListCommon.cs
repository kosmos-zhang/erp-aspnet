/**********************************************
 * 作    者： 吴志强
 * 创建日期： 2009/03/23
 * 描    述： 分页控件共通处理
 * 修改日期： 2009/03/23
 * 版    本： 0.5.0
 ***********************************************/
using System;
using System.Xml.Linq;
using System.Linq;
using System.IO;
using System.Web.Script.Serialization;
using System.Data;

/// <summary>
/// 类名：PageListCommon
/// 描述：分页控件共通处理。
/// 
/// 作者：吴志强
/// 创建时间：2009/03/23
/// 最后修改时间：2009/03/23
/// </summary>
public class PageListCommon
{
    /// <summary>
    /// 将数据从DataTable类型转化为XElement类型数据
    /// </summary>
    /// <param name="xmlDS">数据源</param>
    /// <param name="tableName">DataTable中的表名</param>
    /// <returns></returns>
    public static XElement ConvertDataTableToXML(DataTable xmlDS, string tableName)
    {
        //定义转化需要的变量
        StringWriter sw = new StringWriter();
        //设置获取数据的表名
        xmlDS.TableName = tableName;
        //写当前数据
        xmlDS.WriteXml(sw, System.Data.XmlWriteMode.IgnoreSchema, true);
        //将数据转化为字符串
        string contents = sw.ToString();
        //返回
        return XElement.Parse(contents);
    }

    /// <summary>
    /// 将数据转化为Json类
    /// </summary>
    /// <param name="obj">转化对象</param>
    /// <returns></returns>
    public static string ToJSON(object obj)
    {
        //初始化解析实例
        JavaScriptSerializer serializer = new JavaScriptSerializer();
        //序列化并返回
        return serializer.Serialize(obj);
    }
}