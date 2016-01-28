using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XBase.Model.Office.StorageManager
{

    public class StorageSellBackDetailModel
    {
        public StorageSellBackDetailModel()
        { }
        private string _backno;
        private string _sortno;
        private string _innumber;
        private string _prodcutid;
        private string _storageid;


        /// <summary>
        /// 
        /// </summary>
        public string BackNo
        {
            set { _backno = value; }
            get { return _backno; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string SortNo
        {
            set { _sortno = value; }
            get { return _sortno; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string InNumber
        {
            set { _innumber = value; }
            get { return _innumber; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string ProductID
        {
            set { _prodcutid = value; }
            get { return _prodcutid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string StorageID
        {
            set { _storageid = value; }
            get { return _storageid; }
        }
    }
}
