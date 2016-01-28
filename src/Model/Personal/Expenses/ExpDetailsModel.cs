/*************************
 * 创建人：何小武
 * 创建日期：2009-9-4
 * 描述：费用明细model
 **************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XBase.Model.Personal.Expenses
{
    public class ExpDetailsModel
    {
        #region Model
        private int _id;
        private int _expid;
        private int _sortNo;
        private int _exptype;
        private decimal _amount;
        private string _expRemark;
        /// <summary>
        /// ID
        /// </summary>
        public int ID
        {
            set { _id = value; }
            get { return _id; }
        }
        /// <summary>
        /// 费用申请单ID
        /// </summary>
        public int ExpID
        {
            set { _expid = value; }
            get { return _expid; }
        }
        /// <summary>
        /// 序号（行号）
        /// </summary>
        public int SortNo
        {
            set { _sortNo = value; }
            get { return _sortNo; }
        }
        /// <summary>
        /// 费用类别
        /// </summary>
        public int ExpType
        {
            set { _exptype = value; }
            get { return _exptype; }
        }
        /// <summary>
        /// 金额
        /// </summary>
        public decimal Amount
        {
            set { _amount = value; }
            get { return _amount; }
        }
        /// <summary>
        /// 备注
        /// </summary>
        public string ExpRemark
        {
            set { _expRemark = value; }
            get { return _expRemark; }
        }
        #endregion Model
    }
}
