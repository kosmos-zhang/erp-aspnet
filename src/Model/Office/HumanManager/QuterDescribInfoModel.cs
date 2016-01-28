using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XBase.Model.Office.HumanManager
{
  public   class QuterDescribInfoModel
    {

        #region Model
        private string _id;
        private string _qutercontent;
        private string _qutername;
        /// <summary>
        /// 自动生成
        /// </summary>
        public string ID
        {
            set { _id = value; }
            get { return _id; }
        }
        /// <summary>
        /// 岗位说明书内容
        /// </summary>
        public string QuterContent
        {
            set { _qutercontent = value; }
            get { return _qutercontent; }
        }
        /// <summary>
        /// 岗位名称
        /// </summary>
        public string QuterName
        {
            set { _qutername = value; }
            get { return _qutername; }
        }
        #endregion Model
    }
}
