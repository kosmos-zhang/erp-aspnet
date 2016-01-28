using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XBase.Model.Office.DefManager
{
    [Serializable]
    public class StructTable
    {
        #region Model
        private int _id;
        private string _ccode;
        private string _cname;
        private string _type;
        private int? _length;
        private int? _isshow;
        private int? _seq;
        private int? _isempty;
        private int? _ismultiline;
        private int? _typeflag;
        private int? _tableid;
        private int? _issearch;
        private int? _iskeyword;
        private int? _isBind;
        private string _dropdownlistValue;
        private string _controllength;
        private int _istotal;
        private int _isheadshow;


        //头部是否显示
        public int isheadshow
        {
            set { _isheadshow = value; }
            get { return _isheadshow; }
        }
        //合计
        public int IsTotal{
            set { _istotal = value; }
            get { return _istotal; }
        }

        /// <summary>
        /// 控件长度
        /// </summary>
        public string ControlLength
        {
            set { _controllength = value; }
            get { return _controllength; }
        }

        /// <summary>
        /// 默认值
        /// </summary>
        public string DropdownlistValue
        {
            set { _dropdownlistValue = value; }
            get { return _dropdownlistValue; }
        }

        /// <summary>
        /// 是否是绑定的数据源
        /// </summary>
        public int? IsBind
        {
            set { _isBind = value; }
            get { return _isBind; }
        }


        public int? IsKeyword
        {
            set { _iskeyword = value; }
            get { return _iskeyword; }
        }

        public int? isSearch
        {
            set { _issearch = value; }
            get { return _issearch; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int id
        {
            set { _id = value; }
            get { return _id; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string ccode
        {
            set { _ccode = value; }
            get { return _ccode; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string cname
        {
            set { _cname = value; }
            get { return _cname; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string type
        {
            set { _type = value; }
            get { return _type; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? length
        {
            set { _length = value; }
            get { return _length; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? isshow
        {
            set { _isshow = value; }
            get { return _isshow; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? seq
        {
            set { _seq = value; }
            get { return _seq; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? isempty
        {
            set { _isempty = value; }
            get { return _isempty; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? ismultiline
        {
            set { _ismultiline = value; }
            get { return _ismultiline; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? typeflag
        {
            set { _typeflag = value; }
            get { return _typeflag; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? TableID
        {
            set { _tableid = value; }
            get { return _tableid; }
        }
        #endregion Model
    }
}
