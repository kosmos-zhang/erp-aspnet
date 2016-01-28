using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XBase.Common
{
 public class PageListUtil
    {
        private int _pageIndex = 1;
        private int _pageSize = 10;
        private int _counts = 0;
        private int PageCount
        {
            get 
            {
                if (Counts % pageSize == 0)
                {
                   return Counts / pageSize;
                }
                else
                {
                    return (int)(Counts / pageSize) + 1;
                }
            }
        }

        public int pageIndex
        {
            get { return _pageIndex; }
            set { _pageIndex = value; }
        }

        public int pageSize
        {
            get { return _pageSize; }
            set { _pageSize = value; }
        }

        public int Counts
        {
            get { return _counts; }
            set { _counts = value; }
        }

        public string GetPageNumbers(string url)
        {
            return GetPageNumbers(url, null);
        }

        public string GetPageNumbers(string url, string pagetag)
        {
            return GetPageNumbers(url, pagetag, 0);
        }

        public string GetPageNumbers(string url, int extendPage)
        {
            return GetPageNumbers(url, null, extendPage);
        }

        public string GetPageNumbers(string url, string pagetag, int extendPage)
        {
            if (pagetag == null)
            {
                pagetag = "p";
            }
            if (extendPage == 0)
            {
                extendPage = 10;
            }
            return GetPageNumbers(_pageIndex, PageCount, url, extendPage, pagetag);
        }


        /// <summary>
        /// 获得页码显示链接
        /// </summary>
        /// <param name="curPage">当前页数</param>
        /// <param name="countPage">总页数</param>
        /// <param name="url">超级链接地址</param>
        /// <param name="extendPage">周边页码显示个数上限</param>
        /// <returns>页码html</returns>
        public static string GetPageNumbers(int curPage, int countPage, string url, int extendPage)
        {
            return GetPageNumbers(curPage, countPage, url, extendPage, "p");
        }

        /// <summary>
        /// 获得页码显示链接
        /// </summary>
        /// <param name="curPage">当前页数</param>
        /// <param name="countPage">总页数</param>
        /// <param name="url">超级链接地址</param>
        /// <param name="extendPage">周边页码显示个数上限</param>
        /// <param name="pagetag">页码标记</param>
        /// <returns>页码html</returns>
        public static string GetPageNumbers(int curPage, int countPage, string url, int extendPage, string pagetag)
        {
            return GetPageNumbers(curPage, countPage, url, extendPage, pagetag, null);

        }

        /// <summary>
        /// 获得页码显示链接
        /// </summary>
        /// <param name="curPage">当前页数</param>
        /// <param name="countPage">总页数</param>
        /// <param name="url">超级链接地址</param>
        /// <param name="extendPage">周边页码显示个数上限</param>
        /// <param name="pagetag">页码标记</param>
        /// <param name="anchor">锚点</param>
        /// <returns>页码html</returns>
        public static string GetPageNumbers(int curPage, int countPage, string url, int extendPage, string pagetag, string anchor)
        {
            return GetPageNumbers(curPage, countPage, url, extendPage, pagetag, anchor, null);
        }


        /// <summary>
        /// 获得页码显示链接
        /// </summary>
        /// <param name="curPage">当前页数</param>
        /// <param name="countPage">总页数</param>
        /// <param name="url">超级链接地址</param>
        /// <param name="extendPage">周边页码显示个数上限</param>
        /// <param name="pagetag">页码标记</param>
        /// <param name="anchor">锚点</param>
        /// <param name="classname">当前页面样式</param>
        /// <returns>页码html</returns>
        public static string GetPageNumbers(int curPage, int countPage, string url, int extendPage, string pagetag, string anchor, string classname)
        {
            if (pagetag == "")
                pagetag = "page";
            if (classname == null)
                classname = "oth";
            int startPage = 1;
            int endPage = 1;

            if (url.IndexOf("?") > -1)
            {
                url = url + "&";
            }
            else
            {
                url = url + "?";
            }
            string t1 = "<a>" + countPage + "/" + curPage + "</a><a href=\"" + url + pagetag + "=1";
            string t2 = "<a href=\"" + url + pagetag + "=" + countPage;
            if (anchor != null)
            {
                t1 += anchor;
                t2 += anchor;
            }
            t1 += "\">首页</a>";
            t2 += "\">末页</a>";

            if (countPage < 1)
                countPage = 1;
            if (extendPage < 3)
                extendPage = 2;

            if (countPage > extendPage)
            {
                if (curPage - (extendPage / 2) > 0)
                {
                    if (curPage + (extendPage / 2) < countPage)
                    {
                        startPage = curPage - (extendPage / 2);
                        endPage = startPage + extendPage - 1;
                        t1 = t1 + "<a href=\"" + url + "&" + pagetag + "=" + (curPage - extendPage) + "\">...</a>";
                        t2 = "<a href=\"" + url + pagetag + "=" + (curPage + extendPage) + "\">...</a>" + t2;
                    }
                    else
                    {
                        endPage = countPage;
                        startPage = endPage - extendPage + 1;
                        t1 = t1 + "<a href=\"" + url + pagetag + "=" + (curPage - extendPage) + "\">...</a>";
                        t2 = "";
                    }
                }
                else
                {
                    endPage = extendPage;
                    t1 = "<a>" + countPage + "/" + curPage + "</a>";
                    t2 = "<a href=\"" + url + pagetag + "=" + (curPage + extendPage) + "\">...</a>" + t2;
                }
            }
            else
            {
                startPage = 1;
                endPage = countPage;
                t1 = "<a>" + countPage + "/" + curPage + "</a>";
                t2 = "";
            }

            StringBuilder s = new StringBuilder("");

            s.Append(t1);
            for (int i = startPage; i <= endPage; i++)
            {

                if (i == curPage)
                {
                    s.Append("<a class=\"" + classname + "\" href=\"");
                }
                else
                {
                    s.Append("<a href=\"");
                }
                s.Append(url);
                s.Append(pagetag);
                s.Append("=");
                s.Append(i);
                if (anchor != null)
                {
                    s.Append(anchor);
                }
                s.Append("\">");
                s.Append(i);
                s.Append("</a>");
            }
            s.Append(t2);

            return s.ToString();
        }
    }
}


