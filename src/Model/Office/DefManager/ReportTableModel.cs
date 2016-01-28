using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XBase.Model.Office.DefManager
{
    public class ReportTableModel
    {
        public ReportTableModel()
        { }

        private int _iD;
        private string _menuname;
        private string _sqlStr;
        private int _timeFlag;
        private string _tablelist;
        private string _excelhead;
        /// <summary>
        /// ID
        /// </summary>
        public int ID
        {
            set { _iD = value; }
            get { return _iD; }
        }
        /// <summary>
        /// Menuname
        /// </summary>
        public string Menuname
        {
            set { _menuname = value; }
            get { return _menuname; }
        }
        /// <summary>
        /// SqlStr
        /// </summary>
        public string SqlStr
        {
            set { _sqlStr = value; }
            get { return _sqlStr; }
        }
        /// <summary>
        /// timeFlag
        /// </summary>
        public int timeFlag
        {
            set { _timeFlag = value; }
            get { return _timeFlag; }
        }

        public string Tablelist
        {
            set { _tablelist = value; }
            get { return _tablelist; }
        }

        public string Excelhead
        {
            set { _excelhead = value; }
            get { return _excelhead; }
        }
    }
}
