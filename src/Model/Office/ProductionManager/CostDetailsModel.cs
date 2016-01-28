using System;
namespace XBase.Model.Office.ProductionManager
{
   public  class CostDetailsModel
    {
        #region Model
        private int _id;
       
        private int _ctid;
        private decimal _materials;
        private decimal _wage;
        private decimal _overhead;
        private decimal _burningpower;
        private decimal _totalcost;
        private string _ItemName;

        /// <summary>
        /// 
        /// </summary>
        public string ItemName
        {
            set { _ItemName = value; }
            get { return _ItemName; }
        }

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
        public int CTID
        {
            set { _ctid = value; }
            get { return _ctid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal Materials
        {
            set { _materials = value; }
            get { return _materials; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal Wage
        {
            set { _wage = value; }
            get { return _wage; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal Overhead
        {
            set { _overhead = value; }
            get { return _overhead; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal BurningPower
        {
            set { _burningpower = value; }
            get { return _burningpower; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal TotalCost
        {
            set { _totalcost = value; }
            get { return _totalcost; }
        }
        #endregion Model
    }
}
