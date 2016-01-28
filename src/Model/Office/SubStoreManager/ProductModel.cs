using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XBase.Model.Office.SubStoreManager
{
    public class ProductModel
    {
        private string _productid;
        private string _companycd;
        private string _deptid;
        private string _productcount;
        private string _storageid;
        private string _sortno;

        public string ProductID
        {
            set { _productid = value; }
            get { return _productid; }
        }
        public string CompanyCD
        {
            set { _companycd = value; }
            get { return _companycd; }
        }
        public string DeptID
        {
            set { _deptid = value; }
            get { return _deptid; }
        }
        public string ProductCount
        {
            set { _productcount = value; }
            get { return _productcount; }
        }
        public string StorageID
        {
            set { _storageid = value;}
            get { return _storageid; }
        }
        public string SortNo
        {
            set { _sortno = value; }
            get { return _sortno; }
        }
    }
}
