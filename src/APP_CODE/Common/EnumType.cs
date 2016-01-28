using System;

namespace XBase.Common
{
    /// <summary>
    /// 设备状态
    /// </summary>
    public sealed class EnumType
    {
        /// <summary>
        /// 0	空闲
        /// </summary>
        public const string Free = "0";
        /// <summary>
        /// 1	使用中
        /// </summary>
        public const string InUse = "1";
        /// <summary>
        /// 2	申请维修
        /// </summary>
        public const string Repaire = "2";
        /// <summary>
        /// 3	维修中
        /// </summary>
        public const string Repairing = "3";
        /// <summary>
        /// 4  申请报废
        /// </summary>
        public const string Destroy = "4";
        /// <summary>
        /// 5  报废
        /// </summary>
        public const string Destroied = "5";
    }
    
}
