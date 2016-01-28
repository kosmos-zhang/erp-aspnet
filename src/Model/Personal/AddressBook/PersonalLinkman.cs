using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XBase.Model.Personal.AddressBook
{
        public class PersonalLinkman
        {
            public PersonalLinkman()
            { }
            #region Model
            private int _id;
            private string _companycd;
            private int  _creator;
            private int  _linkmangroupid;
            private string _name;
            private string _companyname;
            private string _mobilephone;
            private string _companyphone;
            private string _email;
            private string _fax;
            private string _qq;
            private string _icq;
            private string _msn;
            private string _companywebsite;
            private string _companyaddress;
            private string _principalship;
            private string _remark;
            private string _birthday;
            private string _sex;
            private string _editflag;


            /// <summary>
            /// 
            /// </summary>
            public int ID
            {
                set { _id = value; }
                get { return _id; }
            }
            /// <summary>
            /// 
            /// </summary>
            public string CompanyCD
            {
                set { _companycd = value; }
                get { return _companycd; }
            }
            /// <summary>
            /// 
            /// </summary>
            public int  Creator
            {
                set { _creator = value; }
                get { return _creator; }
            }
            /// <summary>
            /// 
            /// </summary>
            public int  LinkmanGroupID
            {
                set { _linkmangroupid = value; }
                get { return _linkmangroupid; }
            }
            /// <summary>
            /// 
            /// </summary>
            public string Name
            {
                set { _name = value; }
                get { return _name; }
            }
            /// <summary>
            /// 
            /// </summary>
            public string CompanyName
            {
                set { _companyname = value; }
                get { return _companyname; }
            }
            /// <summary>
            /// 
            /// </summary>
            public string MobilePhone
            {
                set { _mobilephone = value; }
                get { return _mobilephone; }
            }
            /// <summary>
            /// 
            /// </summary>
            public string CompanyPhone
            {
                set { _companyphone = value; }
                get { return _companyphone; }
            }
            /// <summary>
            /// 
            /// </summary>
            public string Email
            {
                set { _email = value; }
                get { return _email; }
            }
            /// <summary>
            /// 
            /// </summary>
            public string Fax
            {
                set { _fax = value; }
                get { return _fax; }
            }
            /// <summary>
            /// 
            /// </summary>
            public string QQ
            {
                set { _qq = value; }
                get { return _qq; }
            }
            /// <summary>
            /// 
            /// </summary>
            public string ICQ
            {
                set { _icq = value; }
                get { return _icq; }
            }
            /// <summary>
            /// 
            /// </summary>
            public string MSN
            {
                set { _msn = value; }
                get { return _msn; }
            }
            /// <summary>
            /// 
            /// </summary>
            public string CompanyWebsite
            {
                set { _companywebsite = value; }
                get { return _companywebsite; }
            }
            /// <summary>
            /// 
            /// </summary>
            public string CompanyAddress
            {
                set { _companyaddress = value; }
                get { return _companyaddress; }
            }
            /// <summary>
            /// 
            /// </summary>
            public string principalship
            {
                set { _principalship = value; }
                get { return _principalship; }
            }
            /// <summary>
            /// 
            /// </summary>
            public string Remark
            {
                set { _remark = value; }
                get { return _remark; }
            }
            /// <summary>
            /// 
            /// </summary>
            public string Birthday
            {
                set { _birthday = value; }
                get { return _birthday; }
            }
            /// <summary>
            /// 
            /// </summary>
            public string EditFlag
            {
                set { _editflag = value; }
                get { return _editflag; }
            }

            /// <summary>
            /// 
            /// </summary>
            public string Sex
            {
                set { _sex = value; }
                get { return _sex; }
            }
            #endregion Model

        }
}
