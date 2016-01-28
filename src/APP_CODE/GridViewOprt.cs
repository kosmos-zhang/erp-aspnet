using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Collections;
/// <summary>
///GridViewOprt 的摘要说明
/// </summary>
public class GridViewOprt
{
    public GridViewOprt()
    {
        //
        //TODO: 在此处添加构造函数逻辑
        //
    }

    public class ReverserClass : IComparer
    {
        // Calls CaseInsensitiveComparer.Compare with the parameters reversed.
        int IComparer.Compare(Object x, Object y)
        {
            return ((new CaseInsensitiveComparer()).Compare(y, x));
        }
    }


    /// <summary>
    /// 对表字段进行排序
    /// </summary>
    /// <param name="dt">表</param>
    /// <param name="Expression">排序条件</param>
    /// <returns>DataTable</returns>
    public static DataTable SortingTable(
        DataTable dt, string Expression)
    {
        if (Expression == null) return dt;
        DataTable NewDt = null;
        DataView dv = dt.DefaultView;
        dv.Sort = Expression;
        NewDt = dv.ToTable();

        return NewDt;
    }
    //按条件对数组进行排序
    public static ArrayList SortingArrayList(ArrayList List)
    {
        if (List == null) return List;
        ArrayList NewList = null;
        List.Reverse();
        NewList = List;
        return NewList;
    }

}
