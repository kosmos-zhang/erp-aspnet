/*****************************************
 *创建人：何小武
 *创建日期：2009-9-14
 *描述：费用报销明细
 *修改：
 *  1. 添加字段SubjectsNo。2010-4-27
 *****************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XBase.Model.Personal.Expenses
{
    public class ReimbDetailsModel
    {
        private int _id;
        private int _sortno;
        private int _expid;
        private int _reimbid;
        private decimal _expamount;
        private decimal _reimbamount;
        private decimal _restoreamount;
        private string _notes;
        //private int _feeBigTypeID;
        private int _feeNameID;
        private string _subjectsNo;
        /// <summary>
        /// ID自动增长
        /// </summary>
        public int ID
        {
            set { _id = value; }
            get { return _id; }
        }
        /// <summary>
        /// 序号
        /// </summary>
        public int SortNo
        {
            set { _sortno = value; }
            get { return _sortno; }
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
        /// 报销单主表ID
        /// </summary>
        public int ReimbID
        {
            set { _reimbid = value; }
            get { return _reimbid; }
        }
        /// <summary>
        /// 申请的费用金额
        /// </summary>
        public decimal ExpAmount
        {
            set { _expamount = value; }
            get { return _expamount; }
        }
        /// <summary>
        /// 报销金额
        /// </summary>
        public decimal ReimbAmount
        {
            set { _reimbamount = value; }
            get { return _reimbamount; }
        }
        /// <summary>
        /// 归还金额
        /// </summary>
        public decimal RestoreAmount
        {
            set { _restoreamount = value; }
            get { return _restoreamount; }
        }
        /// <summary>
        /// 备注
        /// </summary>
        public string Notes
        {
            set { _notes = value; }
            get { return _notes; }
        }
        ///// <summary>
        ///// 费用大类ID
        ///// </summary>
        //public int FeeBigTypeID
        //{
        //    set { _feeBigTypeID = value; }
        //    get { return _feeBigTypeID; }
        //}
        /// <summary>
        /// 费用类别ID（小类ID）
        /// </summary>
        public int FeeNameID
        {
            set { _feeNameID = value; }
            get { return _feeNameID; }
        }
        /// <summary>
        /// 科目编号
        /// </summary>
        public string SubjectsNo
        {
            set { _subjectsNo = value; }
            get { return _subjectsNo; }
        }
    }
}
