/**********************************************
 * 作    者： 吴志强
 * 创建日期： 2009.01.10
 * 描    述： 常数文件类
 * 修改日期： 2009.01.10
 * 版    本： 0.5.0
 ***********************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XBase.Common
{
    /// <summary>
    /// 类名：ConstUtil
    /// 描述：提供一些常量，便于代码的维护
    /// 
    /// 作者：吴志强
    /// 创建时间：2009/01/10
    /// 最后修改时间：2009/01/10
    /// </summary>
    ///
    public class ConstUtil
    {

        #region 版本GUID 定义
        public const string Ver_ERP_Guid = "025AA3C8-FE57-45B5-A995-A61C92C8AB74";
        #endregion


        #region 文件上传相关定义

        /* 上传文件类型定义 */
        public static string[] SAVE_PILE_TYPE = { ".doc", ".xls", ".ppt", ".pps", ".docx", ".xlsx", ".pptx", ".mpp", ".vsd", ".wps", ".pdf", ".jpg", ".jpeg", ".gif", ".png", ".bmp", ".rar", ".zip", ".txt", ".csv", ".chm" };
        public static string[] SAVE_PHOTO_TYPE = { ".jpg", ".jpeg", ".gif", ".png", ".bmp" };
        public static string UPLOAD_FILE_ERROR_TYPE = "不支持您上传的文件格式，请重新选择文件！";
        public static string UPLOAD_FILE_ERROR_SINGLE_SIZE = "您上传的文件超过了允许的大小";
        public static string UPLOAD_FILE_ERROR_MAX_SIZE = "贵公司的上传文件大小已经达到上限";
        public static string UPLOAD_FILE_ERROR_MAX_NUM = "贵公司的上传文件个数已经达到上限";
        #endregion

        #region 模块功能ID定义

        /*
         * 最好定义成以后发布之后的ID 不然以后修改比较浪费时间
         * 如果模块功能ID不能确定的，请参照一下 
         *          01 开发库/09 代码库/sql/InitSysModule.sql 文件，以后以这个文件为主
         *     如果模块ID没有，请务必在上述文件中添加，以方便以后维护
         */

        #region 个人桌面

        /* 企业文化 */
        public static string MENU_PERSONAL_COMPANY_CULTURE = "104";

        /*目标类型*/
        public static string CODE_TYPE_AimType = "1";//目标类型 
        public static string Flag_TYPE_AimType = "1";//目标类型

        /*任务类型*/
        public static string CODE_TYPE_TaskType = "3";//目标类型 
        public static string Flag_TYPE_TaskType = "1";//目标类型

        //费用管理
        public static string CODE_TYPE_EXPENSESTYPE = "4";//费用编号规则
        public static string FLAG_TYPE_EXPENSESTYPE = "1";//个人桌面
        public static string CODE_TYPE_REIMBURSEMENTTYPE = "5";//费用报销编号规则
        public static string Flag_TYPE_REIMBURSEMENTTYPE = "1";//

        #region 个人桌面模块ID
        public static string MODULE_ID_AMI_ADD = "10011";//目标添加
        public static string MODULE_ID_AMI_INFO = "10012";//目标修改
        public static string MODULE_ID_TASK_ADD = "10111";//任务添加
        public static string MODULE_ID_TASK_INFO = "10112";//任务修改
        public static string MODULE_ID_AGENDA_ADD = "2191401";//日程添加
        public static string MODULE_ID_AGENDA_INFO = "2191401";//日程修改

        public static string MODULE_ID_EXPENSES_ADD = "10810";//费用添加
        public static string MODULE_ID_EXPENSES_INFO = "10811";//费用列表
        public static string MODULE_ID_REIMBURSEMENT_ADD = "10812";//费用报销添加
        public static string MODULE_ID_REIMBURSEMENT_List = "10813";//费用报销列表

        #endregion
        #endregion

        #region 知识中心

        #region 部门人员选择
        //多选
        public const string TYPE_DUOX_CODE = "2";
        //单选
        public const string TYPE_DANX_CODE = "1";
        //混合选择


        public const string TYPE_HUNH_CODE = "3";
        #endregion



        #endregion

        #region 信息中心



        #endregion

        #region 办公模式

        #region 人事管理
        /* 人事档案 */
        public static string MODULE_ID_HUMAN_EMPLOYEE_ADD = "2011201";//新建人员
        public static string MODULE_ID_HUMAN_EMPLOYEE_WORK = "2011202";//在职人员列表
        public static string MODULE_ID_HUMAN_EMPLOYEE_RESERVE = "2011203";//人才储备列表
        public static string MODULE_ID_HUMAN_EMPLOYEE_LEAVE = "2011204";//离职人员列表
        public static string MODULE_ID_HUMAN_HRPROXY_ADD = "2011205";//新建人才代理
        public static string MODULE_ID_HUMAN_HRPROXY_INFO = "2011206";//人才代理列表
        public static string MODULE_ID_HUMAN_RECTAPPLY_EDIT = "2011304";//新建申请招聘
        public static string MODULE_ID_HUMAN_RECTAPPLY_INFO = "2011305";//申请招聘列表
        public static string MODULE_ID_HUMAN_RECTPLAN_EDIT = "2011306";//新建招聘活动
        public static string MODULE_ID_HUMAN_RECTPLAN_INFO = "2011307";//招聘活动列表
        public static string MODULE_ID_HUMAN_RECTINTERVIEW_EDIT = "2011308";//新建面试记录
        public static string MODULE_ID_HUMAN_RECTINTERVIEW_INFO = "2011309";//面试记录列表
        public static string MODULE_ID_HUMAN_TRAINING_EDIT = "2011501";//新建培训
        public static string MODULE_ID_HUMAN_TRAINING_INFO = "2011502";//培训列表
        public static string MODULE_ID_HUMAN_TRAININGASSE_EDIT = "2011503";//新建培训考核
        public static string MODULE_ID_HUMAN_TRAININGASSE_INFO = "2011504";//培训考核列表
        public static string MODULE_ID_HUMAN_TEST_EDIT = "2011601";//新建考试结果
        public static string MODULE_ID_HUMAN_TEST_INFO = "2011602";//考试结果列表
        public static string MODULE_ID_HUMAN_DEPT_EDIT = "2011101";//组织机构设置
        public static string MODULE_ID_HUMAN_DEPTQUARTER_EDIT = "2011102";//机构岗位设置
        public static string MODULE_ID_HUMAN_RECTCHECKELEM_EDIT = "2011301";//面试评测要素设置
        public static string MODULE_ID_HUMAN_RECTCHECKTEMPLATE_EDIT = "2011302";//面试评测模板设置
        public static string MODULE_ID_HUMAN_RECTCHECKTEMPLATE_INFO = "2011303";//面试评测模板列表
        public static string MODULE_ID_HUMAN_EMPLOYEE_ENTER = "2011401";//待入职
        public static string MODULE_ID_HUMAN_EMPLAPPLY_EDIT = "2011402";//新建调职申请
        public static string MODULE_ID_HUMAN_EMPLAPPLY_INFO = "2011403";//调职申请列表
        public static string MODULE_ID_HUMAN_FAST_SHIFT = "2011404";//快速调职通道
        public static string MODULE_ID_HUMAN_MOVEAPPLY_EDIT = "2011405";//新建离职申请
        public static string MODULE_ID_HUMAN_MOVEAPPLY_INFO = "2011406";//离职申请列表
        public static string MODULE_ID_HUMAN_FAST_LEAVE = "2011407";//快速离职通道
        public static string MODULE_ID_HUMAN_EMPLOYEE_SHIFT_EDIT = "2011408";//新建调职
        public static string MODULE_ID_HUMAN_EMPLOYEE_SHIFT_INFO = "2011409";//调职列表
        public static string MODULE_ID_HUMAN_EMPLOYEE_LEAVE_EDIT = "2011410";//新建离职
        public static string MODULE_ID_HUMAN_EMPLOYEE_LEAVE_INFO = "2011411";//离职列表
        public static string MODULE_ID_HUMAN_EMPLOYEE_CONTRACT_EDIT = "2011207";//新建合同
        public static string MODULE_ID_HUMAN_EMPLOYEE_CONTRACT_INFO = "2011208";//合同列表
        public static string MODULE_ID_HUMAN_SALARY_SET = "2011701";//工资项设置
        public static string MODULE_ID_HUMAN_SALARY_INPUT = "2011702";//工资录入
        public static string MODULE_ID_HUMAN_SALARY_REPORT_NEW = "2011703";//新建工资报表
        public static string MODULE_ID_HUMAN_SALARY_REPORT_LIST = "2011704";//工资报表列表

        /*绩效考核*/
        public static string MODULE_ID_HUMAN_PERFORMANCECHECK = "2011801";//考核设置
        public static string MODULE_ID_HUMAN_PERFORMANCETASKCHECK = "2011802";//考核任务
        public static string MODULE_ID_HUMAN_PERFORMANCEPERSONALCHECK = "2011803";//自我鉴定
        public static string MODULE_ID_HUMAN_PERFORMANCEGRADELIST = "2011804";//考核评分
        public static string MODULE_ID_HUMAN_PERFORMANCESUMMARY = "2011805";//评分汇总
        public static string MODULE_ID_HUMAN_PERFORMANCESUMMARYSEARCH = "2011806";//考核总评
        public static string MODULE_ID_HUMAN_PERFORMANCEEMPLOYEEHECK = "2011807";//员工确认   
        public static string MODULE_ID_HUMAN_PERFORMANCEBETTER = "2011808";//	新建绩效改进计划 
        public static string MODULE_ID_HUMAN_PERFORMANCEQUERY = "2011809";//	考核查询 
        #endregion

        #region 库存管理模块

        public static string MODULE_ID_STORAGE_STORAGEINFO = "2051101";//仓库设置
        public static string MODULE_ID_STORAGE_STORAGEINITAIL_ADD = "2051102";//新建期初库存录入
        public static string MODULE_ID_STORAGE_STORAGEINITAIL_LIST = "2051103";//期初库存列表
        public static string MODULE_ID_STORAGE_STORAGEINPURCHASE_ADD = "2051201";//新建采购入库单
        public static string MODULE_ID_STORAGE_STORAGEINPURCHASE_List = "2051202";//采购入库单
        public static string MODULE_ID_STORAGE_STORAGEINPROCESS_ADD = "2051203";//新建生产完工入库单
        public static string MODULE_ID_STORAGE_STORAGEINPROCESS_LIST = "2051204";//生产完工入库单
        public static string MODULE_ID_STORAGE_STORAGEINOTHER_ADD = "2051205";//新建其他入库单
        public static string MODULE_ID_STORAGE_STORAGEINOTHER_LIST = "2051206";//其他入库单列表
        public static string MODULE_ID_STORAGE_STORAGEINRED_ADD = "2051207";//新建红冲入库单
        public static string MODULE_ID_STORAGE_STORAGEINRED_LIST = "2051208";//红冲入库单列表
        public static string MODULE_ID_STORAGE_STORAGEOUTSELL_ADD = "2051301";//新建销售出库单
        public static string MODULE_ID_STORAGE_STORAGEOUTSELL_LIST = "2051302";//销售出库单列表
        public static string MODULE_ID_STORAGE_STORAGEOUTOTHER_ADD = "2051303";//新建其他出库单
        public static string MODULE_ID_STORAGE_STORAGEOUTOTHER_LIST = "2051304";//其他出库单列表
        public static string MODULE_ID_STORAGE_STORAGEOUTRED_ADD = "2051305";//新建红冲出库单
        public static string MODULE_ID_STORAGE_STORAGEOUTRED_LIST = "2051306";//红冲出库单列表
        public static string MODULE_ID_STORAGE_STORAGELOSS_ADD = "2051801";//新建库存报损单
        public static string MODULE_ID_STORAGE_STORAGELOSS_LIST = "2051802";//库存报损单列表
        public static string MODULE_ID_STORAGE_STORAGE_SEARCH = "2051901";//现有库存查询
        public static string MODULE_ID_STORAGE_STORAGEPRODUCT_ALARM = "2051902";//库存报警
        public static string MODULE_ID_STORAGE_STORAGEJOURNAL = "2051903";//库存流水账
        public static string MODULE_ID_STORAGE_DayEnd = "2052001";//库存日结

        /*开发库 PDD*/
        //public static string MODULE_ID_STORAGE_BORROW_SAVE = "2111401";//新建借货申请单
        //public static string MODULE_ID_STORAGE_BORROW_LIST = "2111402";//借货申请单列表
        //public static string MODULE_ID_STORAGE_RETURN_SAVE = "2111403";//新建借货返还单
        //public static string MODULE_ID_STORAGE_RETURN_LIST = "2111404";//借货返还单列表
        //public static string MODULE_ID_STORAGE_TRANSFER_SAVE = "2111501";//新建调拨单
        //public static string MODULE_ID_STORAGE_TRANSFER_LIST = "2111502";//调拨单列表
        //public static string MODULE_ID_STORAGE_CEHCK_SAVE = "2111701";//新建期末盘点单
        //public static string MODULE_ID_STORAGE_CEHCK_LIST = "2111702";//期末盘点单列表
        /*测试库 PDD*/
        public static string MODULE_ID_STORAGE_BORROW_SAVE = "2051401";//新建借货申请单
        public static string MODULE_ID_STORAGE_BORROW_LIST = "2051402";//借货申请单列表
        public static string MODULE_ID_STORAGE_RETURN_SAVE = "2051403";//新建借货返还单
        public static string MODULE_ID_STORAGE_RETURN_LIST = "2051404";//借货返还单列表
        public static string MODULE_ID_STORAGE_TRANSFER_SAVE = "2051501";//新建调拨单
        public static string MODULE_ID_STORAGE_TRANSFER_LIST = "2051502";//调拨单列表
        public static string MODULE_ID_STORAGE_CEHCK_SAVE = "2051701";//新建期末盘点单
        public static string MODULE_ID_STORAGE_CEHCK_LIST = "2051702";//期末盘点单列表


        public static string MODULE_ID_STORAGE_ADJUST_ADD = "2051601"; //新建日常调整
        public static string MODULE_ID_STORAGE_ADJUST_LIST = "2051602";//日常调整列表
        public static string MODULE_CODING_RULE_TABLE_AdjustInfo = "StorageAdjust";


        /*质检*/
        public static string MODULE_ID_QUALITY_ADD = "2071101"; //新建质检申请
        public static string MODULE_CODING_RULE_TABLE_QuaInfo = "QualityCheckApplay";

        //质检报告
        public static string MODULE_ID_QUALITYREPORT_ADD = "2071201"; //质检报告
        public static string MODULE_CODING_RULE_TABLE_ReportInfo = "QualityCheckReport";

        //不合格
        public static string MODULE_ID_QUALITYNOPASS_ADD = "2071301"; //不合格
        public static string MODULE_CODING_RULE_TABLE_NOPASSInfo = "CheckNotPass";
        //客户建议
        public static string MODULE_ID_CustAdvice_ADD = "2021901"; //新建客户建议
        public static string MODULE_CODING_RULE_TABLE_CustAdvice = "Cust Advice";




        #endregion

        #region 物流配送
        /*开发库 pdd*/
        //public static string MODULE_ID_SUBPRODUCTSENDPRICESETTING = "2111101";//配送价格设置
        //public static string MODULE_ID_SUBPRODUCTSELLPRICESETTING = "2111102";//零售价格设置
        //public static string MODULE_ID_SUBDELIVERYSEND_SAVE = "213201";// 新建配送单
        //public static string MODULE_ID_SUBDELIVERYSEND_LIST = "213202";// 配送单列表
        //public static string MODULE_ID_SUBDELIVERYBACK_SAVE = "213203";//新建配送退货单
        //public static string MODULE_ID_SUBDELIVERYBACK_LIST = "213204";//配送退货单列表
        //public static string MODULE_ID_SUBDELIVERYTRANS_SAVE = "213301";//新建门店调拨单
        //public static string MODULE_ID_SUBDELIVERYTRANS_LIST = "213302";//门店调拨单列表

        /*测试库 pdd*/
        public static string MODULE_ID_SUBPRODUCTSENDPRICESETTING = "2111101";//配送价格设置
        public static string MODULE_ID_SUBPRODUCTSELLPRICESETTING = "2111102";//零售价格设置
        public static string MODULE_ID_SUBDELIVERYSEND_SAVE = "2111201";// 新建配送单
        public static string MODULE_ID_SUBDELIVERYSEND_LIST = "2111202";// 配送单列表
        public static string MODULE_ID_SUBDELIVERYBACK_SAVE = "2111203";//新建配送退货单
        public static string MODULE_ID_SUBDELIVERYBACK_LIST = "2111204";//配送退货单列表
        public static string MODULE_ID_SUBDELIVERYTRANS_SAVE = "2111301";//新建门店调拨单
        public static string MODULE_ID_SUBDELIVERYTRANS_LIST = "2111302";//门店调拨单列表
        #endregion

        #region 物流配送报表
        /*门店配送统计报表*/
        public static string MODULE_ID_SUBDELIVERYSEND_REPORT = "3111101";
        /*门店配送明细表*/
        public static string MODULE_ID_SUBDELIVERYSENDDETAIL_REPORT = "3111102";
        /*门店调拨统计报表*/
        public static string MODULE_ID_SUBDELIVERYTRANS_REPORT = "3111103";
        /*门店调拨明细表*/
        public static string MODULE_ID_SUBDELIVERYTRANSDETAIL_REPORT = "3111104";
        /*配送单查询*/
        public static string MODULE_ID_SUBDELIVERYSEND_QUERY = "3111201";
        /*配送退货单查询*/
        public static string MODULE_ID_SUBDELIVERYBACK_QUERY = "3111202";
        /*门店调拨单查询*/
        public static string MODULE_ID_SUBDELIVERYTRANS_QUERY = "3111203";
        /*门店库存查询*/
        public static string MODULE_ID_SUBSTORE_STORAGE_QUERY = "3111204";
        /*总部库存查询*/
        public static string MODULE_ID_STORE_STORAGE_QUERY = "3111205";
        
        #endregion

        #endregion

        #endregion

        #region 日志输出用常数

        /* 登陆成功 */
        public const string LOG_LOGIN_SUCCESS = "成功";
        /* 登陆失败 */
        public const string LOG_LOGIN_FAILURE = "失败";
        /* 登陆 */
        public const string LOG_LOGIN = "登陆";
        /* 注销 */
        public const string LOG_LOGOUT = "注销";
        /* TAB键值 */
        public const string TAB = "		";
        /* 信息 */
        public const string LOG_SYSTEM_INFO = "信息";
        /* 警告 */
        public const string LOG_SYSTEM_WARNING = "警告";
        /* 错误 */
        public const string LOG_SYSTEM_ERROR = "异常";
        /* 新建 */
        public const string LOG_PROCESS_INSERT = "新建";
        /* 修改 */
        public const string LOG_PROCESS_UPDATE = "修改";
        /* 删除 */
        public const string LOG_PROCESS_DELETE = "删除";
        /* 确认 */
        public const string LOG_PROCESS_CONFIRM = "确认";
        /* 取消确认 */
        public const string LOG_PROCESS_UNCONFIRM = "取消确认";
        /* 终止合同 */
        public const string LOG_PROCESS_ENDCONTRACT = "终止合同";
        /* 终止订单 */
        public const string LOG_PROCESS_ENDORDER = "终止订单";
        /* 结单 */
        public const string LOG_PROCESS_COMPLETE = "结单";
        /* 取消结单 */
        public const string LOG_PROCESS_CONCELCOMPLETE = "取消结单";
        //出库
        public const string LOG_PROCESS_OUT = "出库";
        //结算
        public const string LOG_PROCESS_STTL = "结算";
        /* 操作成功 */
        public const string LOG_PROCESS_SUCCESS = "操作成功";
        /* 操作失败 */
        public const string LOG_PROCESS_FAILED = "操作失败";
        /*调拨出库*/
        public const string LOG_PROCESS_TRANSFER_OUT = "调拨出库";
        /*调拨入库*/
        public const string LOG_PROCESS_TRANSFER_IN = "调拨入库";
        /*库存调整 盘点部分*/
        public const string LOG_PROCESS_CHECK_OPERATE = "库存调整（期末盘点）";
        /*删除明细*/
        public const string LOG_PROCESS_DELDETAIL = "删除明细";
        /*单据状态操作*/
        public const string LOG_PROCESS_BILLOPERATE = "单据操作";
        /*反确认*/
        public const string LOG_PROCESS_ANTIAUDIT = "反确认";

        /*登记凭证*/
        public const string LOG_PROCESS_ACCOUNT = "登记凭证";

        #endregion

        #region 业务内容相关

        #region 公共分类下拉列表
        /* 公共分类代码表 共用分类默认公司代码 */
        public static string CODE_TYPE_COMPANY_PUBLIC = "AAAAAA";
        /* 公共分类代码表 插入的空选项 */
        public static string CODE_TYPE_INSERT_TEXT = "--请选择--";
        public static string CODE_TYPE_INSERT_VALUE = "";

        /* 人事模块分类 */
        public static string CODE_TYPE_HUMAN = "2";//岗位类型
        public static string CODE_TYPE_QUARTER = "1";//岗位类型
        public static string CODE_TYPE_QUARTER_LEVEL = "2";//岗位级别
        public static string CODE_TYPE_COUNTRY = "3";//国家地区
        public static string CODE_TYPE_POSITION = "4";//职称
        public static string CODE_TYPE_RELIGION = "5";//宗教信仰
        public static string CODE_TYPE_MARRIAGE = "6";//婚姻状况
        public static string CODE_TYPE_LANDSACAPE = "7";//政治面貌
        public static string CODE_TYPE_NATIONAL = "8";//民族
        public static string CODE_TYPE_CULTURE = "9";//学历
        public static string CODE_TYPE_PROFESSIONAL = "10";//专业 
        public static string CODE_TYPE_LANGUAGE = "11";//外语种类
        public static string CODE_TYPE_INTERVIEW = "12";//面试方式
        public static string CODE_TYPE_TRAINING = "13";//培训方式
        public static string CODE_TYPE_QUARTER_ADMIN = "14";//岗位职等
        public static string CODE_TYPE_ADMIN_LEVEL = "15";//行政等级
        public static string CODE_TYPE_CONTRACT_NAME = "16";//合同名称
        public static string CODE_TYPE_CHECK_WAY = "17";//考核方式
        #endregion



        /* 是否启用状态 */
        public static string USED_STATUS_ON = "1"; //是
        public static string USED_STATUS_ON_NAME = "启用"; //added by jiangym
        public static string USED_STATUS_OFF = "0";//否
        public static string USED_STATUS_OFF_NAME = "停用";

        #region  编码规则

        /* 编码规则 编码示例 模板 */
        public static string CODING_RULE_DISPLAY_TEXT = "保存时自动生成";//插入的空选项内容
        public static string CODING_RULE_INSERT_TEXT = "手工输入";//插入的空选项内容
        public static string CODING_RULE_INSERT_VALUE = "";//插入的空选项值
        public static string RULE_EXAMPLE_START = "{"; //{
        public static string RULE_EXAMPLE_END = "}";//}
        public static string RULE_EXAMPLE_NUMBER = "N";//N
        public static char RULE_EXAMPLE_ZERO = '0';//0
        public static string CODING_RULE_TYPE_ZERO = "0";//基础数据
        public static string CODING_RULE_BATCH_NO = "13";//批次规则
        /* 单据分类 需要定义 */
        public static string CODING_RULE_TYPE_ONE = "1";//单据分类

        public static string CODING_RULE_DEFAULT_TRUE = "1";//是
        public static string CODING_RULE_DEFAULT_FLASE = "0";//否


        #region 基础数据
        public static string CODING_RULE_EMPLOYEE_NO = "5";//人员编号
        public static string CODING_RULE_PROXY_NO = "10";//代理企业编号
        public static string CODING_RULE_RECTAPPLY_NO = "12";//招聘申请编号
        public static string CODING_BASE_ITEM_DEPT = "4";//组织机构编号
        public static string CODING_BASE_ITEM_QUARTER = "12";//岗位编号
        public static string CODINGA_BASE_ITEM_PRODUCT = "6";//物品
        public static string CODINGA_BASE_ITEM_ADVERSARYINFO = "3";//竞争对手
        public static string CODINGA_BASE_ITEM_FLOW = "10";//流程
        #endregion

        #region 唯一性校验 表名 列名定义
        /* 行政模块 */
        public static string CODING_RULE_TABLE_EQUIPMENT = "EquipmentInfo";//添加设备
        public static string CODING_RULE_COLUMN_EQUIPMENTNO = "EquipmentNo";
        public static string CODING_RULE_TABLE_EQUIPMENTUSED = "EquipmentUsed";//设备领用
        public static string CODING_RULE_COLUMN_EQUIPMENTRECORDNO = "RecordNo";
        public static string CODING_RULE_TABLE_EQUIPMENTREPAIR = "EquipmentRepair";//设备维修
        public static string CODING_RULE_COLUMN_REPAIRRECORDNO = "RecordNo";
        public static string CODING_RULE_TABLE_EQUIPMENTUSELESS = "EquipmentUseless";//设备报废
        public static string CODING_RULE_COLUMN_USELESSECORDNO = "RecordNo";
        public static string MODULE_ID_OFFICETHINGS_LINK_LIST = "2001301";//用品档案
        public static string TABLE_NAME_OFFICETHINGS = "officedba.OfficeThingsInfo";//用品档案表
        public static string MODULE_ID_EQUIPMENTINFO_LINK_LIST = "2001101";//添加设备
        public static string TABLE_NAME_EQUIPMENTINFO = "officedba.EquipmentInfo";//设备表
        public static string TABLE_NAME_EQUIPMENTDETAILINFO = "officedba.EquipmentFittings";//设备表
        public static string MODULE_ID_ATTENDANCEAPPLY_LINK_LIST = "2001204";//考勤申请
        public static string TABLE_NAME_ATTENDANCEAPPLY = "officedba.AttendanceApply";//考勤申请表
        public static string MODULE_ID_CARINFO_LINK_LIST = "2001601";//考勤申请
        public static string TABLE_NAME_CARINFO = "officedba.CarInfo";//考勤申请表
        public static string MODULE_ID_CARDAYRECORD_LINK_LIST = "2001607";//车辆维护记录
        public static string TABLE_NAME_CARGAS = "officedba.CarAddGas";//车辆加油表
        public static string TABLE_NAME_CARREPAIR = "officedba.CarRepair";//车辆维修表
        public static string TABLE_NAME_CARMAINTAIN = "officedba.CarMaintain";//车辆保养表
        public static string TABLE_NAME_CARYEARCHECK = "officedba.CarYearCheck";//车辆年检表
        public static string TABLE_NAME_CARINSURANCE = "officedba.CarInsurance";//车辆保险表
        public static string TABLE_NAME_CARPECCANCY = "officedba.CarPeccancy";//车辆违章表
        public static string TABLE_NAME_CARACCIDENT = "officedba.CarAccident";//车辆事故表
        public static string MODULE_ID_CARCARAPPLY_LINK_LIST = "2001603";//车辆申请
        public static string TABLE_NAME_CARAPPLY = "officedba.CarApply";//车辆申请表
        public static string MODULE_ID_CARCARDISPATCHE_LINK_LIST = "2001605";//车辆派送
        public static string TABLE_NAME_CARDISPATCH = "officedba.CarDispatch";//车辆派送表
        public static string MODULE_ID_DAYATTENDANCE_LINK_LIST = "2001203";//日常考勤
        public static string TABLE_NAME_DAILYATTENDANCE = "officedba.DailyAttendance";//日常考勤表
        public static string MODULE_ID_ATTENDANCEGROUPSET_LINK_LIST = "2001202";//考勤排班
        public static string TABLE_NAME_EMPLOYATTENDANCESET = "officedba.DailyAttendance";//员工考勤设置表
        public static string MODULE_ID_EQUIPMENTDETAILINFO_LINK_LIST = "2001103";//设备明细
        public static string MODULE_ID_EQUIPMENTRECEIVE_LINK_LIST = "2001104";//设备领用
        public static string TABLE_NAME_EQUIPMENTRECEIVE = "officedba.EquipmentUsed";//设备领用表
        public static string MODULE_ID_EQUIPMENTRECEIVELIST_LINK_LIST = "2001105";//设备领用
        public static string MODULE_ID_EQUIPMENTREPAIR_LINK_LIST = "2001106";//设备维修
        public static string MODULE_ID_EQUIPMENTREPAIRLIST_LINK_LIST = "2001107";//设备维修
        public static string TABLE_NAME_EQUIPMENTREPAIR = "officedba.EquipmentRepair";//设备维修表
        public static string MODULE_ID_EQUIPMENTUSELESS_LINK_LIST = "2001108";//设备报废
        public static string MODULE_ID_EQUIPMENTUSELESSLIST_LINK_LIST = "2001108";//设备报废
        public static string TABLE_NAME_EQUIPMENTUSELESS = "officedba.EquipmentUseless";//设备报废表
        public static string MODULE_ID_INSTORAGE_LINK_LIST = "2001303";//入库库存
        public static string TABLE_NAME_INSTORAGE = "officedba.OfficeThingsBuy";//入库库存表
        public static string TABLE_NAME_INSTORAGEDETAIL = "officedba.OfficeThingsBuyDetail";//入库库存明细表
        public static string MODULE_ID_OFFICETHINGSUSED_LINK_LIST = "2001304";//用品领用
        public static string TABLE_NAME_OFFICETHINGSUSED = "officedba.OfficeThingsUsed";//用品领用表
        public static string TABLE_NAME_OFFICETHINGSUSEDDETAIL = "officedba.OfficeThingsUsedDetail";//用品领用明细表
        public static string TABLE_NAME_WORKPLAN = "officedba.WorkPlan";//排班计划
        public static string TABLE_NAME_WORKGROUP = "officedba.WorkGroup";//班组表
        public static string TABLE_NAME_WORKSHIFT = "officedba.WorkshiftSet";//班次表
        public static string TABLE_NAME_WORKSHIFTTIME = "officedba.WorkShiftTime";//班段表
        public static string MODULE_ID_ATTENDANCESET_LINK_LIST = "2001201";//考勤设置
        public static string TABLE_NAME_ATTENDANCESET = "officedba.Holiday";//节假日表
        public static string TABLE_NAME_YEARHOLIDAY = "officedba.YearHoliday";//年休假表
        public static string MODULE_ID_ATTENDANCEREPORT_LINK_LIST = "2001205";//考勤报表
        public static string TABLE_NAME_ATTENDANCEREPORT = "officedba.AttendanceReport";//考勤报表
        public static string TABLE_NAME_ATTENDANCEREPORTDETAIL = "officedba.AttendanceReportMonth";//考勤报表详细

        public static string MODULE_ID_OFFICEPURCHASEAPPLY_ADD = "2001305";//新建采购申请(行政模块)
        public static string MODULE_ID_OFFICEPURCHASEAPPLY_INFO = "2001305";//采购申请列表(行政模块)

        /* 人事模块 */
        public static string CODING_RULE_TABLE_EMPLOYYEEINFO = "EmployeeInfo";//人员信息表
        public static string CODING_RULE_COLUMN_EMPLOYYEENO = "EmployeeNo";//人员信息字段
        public static string CODING_RULE_TABLE_PROXY = "HRProxy";//人才代理表
        public static string CODING_RULE_COLUMN_PROXYNO = "ProxyCompanyCD";//人才代理企业编号字段
        public static string CODING_RULE_TABLE_RECTAPPLY = "RectApply";//招聘申请表
        public static string CODING_RULE_COLUMN_RECTAPPLYNO = "RectApplyNo";//招聘申请编号
        public static string CODING_RULE_TABLE_RECTPLAN = "RectPlan";//招聘计划表
        public static string CODING_RULE_COLUMN_RECTPLAN_PLANNO = "PlanNo";//招聘计划编号
        public static string CODING_RULE_TABLE_TRAINING = "EmployeeTraining";//员工培训表
        public static string CODING_RULE_COLUMN_TRAINING_NO = "TrainingNo";//培训编号
        public static string CODING_RULE_TABLE_TRAININGASSE = "TrainingAsse";//培训考核表
        public static string CODING_RULE_COLUMN_TRAININGASSE_NO = "AsseNo";//培训考核编号
        public static string CODING_RULE_TABLE_TEST = "EmployeeTest";//员工考试表
        public static string CODING_RULE_COLUMN_TEST_NO = "TestNo";//考试编号
        public static string CODING_RULE_TABLE_DEPT = "DeptInfo";//部门表
        public static string CODING_RULE_COLUMN_DEPT_NO = "DeptNO";//部门编号
        public static string CODING_RULE_TABLE_DEPTQUARTER = "DeptQuarter";//机构岗位表
        public static string CODING_RULE_COLUMN_DEPTQUARTER_NO = "QuarterNo";//岗位编号
        public static string CODING_RULE_TABLE_RECTCHECKELEM = "RectCheckElem";//面试评测要素表
        public static string CODING_RULE_TABLE_RECTCHECKTEMPLATE = "RectCheckTemplate";//面试评测模板表
        public static string CODING_RULE_COLUMN_RECTTEMPLATE_NO = "TemplateNo";//模板编号
        public static string CODING_RULE_TABLE_RECTINTERVIEW = "RectInterview";//招聘面试记录表
        public static string CODING_RULE_COLUMN_INTERVIEW_NO = "InterviewNo";//面试记录编号
        public static string CODING_RULE_TABLE_PERFORMANCEELEM = "PerformanceElem";//绩效指标表
        public static string CODING_RULE_COLUMN_PERFORMANCEELEM_ELEMNO = "ElemNo";//绩效指标编号字段
        public static string CODING_RULE_TABLE_PERFORMANCETEMPLATE = "PerformanceTemplate";//绩效模板表
        public static string CODING_RULE_COLUMN_PERFORMANCETEMPLATE_TemplateNo = "TemplateNo";//绩效模板编号字段
        public static string CODING_RULE_TABLE_PERFORMANCETASK = "PerformanceTask";//考核任务表
        public static string CODING_RULE_TABLE_PERFORMANCEPERSONAL = "PerformancePersonal";//考核任务表
        public static string CODING_RULE_TABLE_PERFORMANCEBETTER = "PerformanceBetter";//考核任务表
        public static string CODING_RULE_COLUMN_PERFORMANCETASK_TASKNo = "TaskNo";//考核任务编号字段
        public static string CODING_RULE_COLUMN_PERFORMANCEPERSONAL_TASKNo = "TaskNo";//考核自我鉴定字段
        public static string CODING_RULE_COLUMN_PERFORMANCEBETTER_PLANO = "PlanNo";//考核自我鉴定字段

        public static string CODING_RULE_TABLE_EMPLAPPLY = "EmplApply";//调职申请表
        public static string CODING_RULE_COLUMN_EMPLAPPLY_NO = "EmplApplyNo";//调职申请编号
        public static string CODING_RULE_TABLE_MOVEAPPLY = "MoveApply";//离职申请表
        public static string CODING_RULE_COLUMN_MOVEAPPLY_NO = "MoveApplyNo";//离职申请编号
        public static string CODING_RULE_TABLE_SHIFT = "EmplApplyNotify";//调职表
        public static string CODING_RULE_COLUMN_SHIFT_NO = "NotifyNo";//调职编号
        public static string CODING_RULE_TABLE_LEAVE = "MoveNotify";//离职表
        public static string CODING_RULE_COLUMN_LEAVE_NO = "NotifyNo";//离职编号
        public static string CODING_RULE_TABLE_EMPLOYEE_CONTRACT = "EmployeeContract";//员工合同表
        public static string CODING_RULE_COLUMN_EMPLOYEE_CONTRACT_NO = "ContractNo";//合同编号
        public static string CODING_RULE_TABLE_SALARY_ITEM = "SalaryItem";//工资项设置表
        public static string CODING_RULE_TABLE_PIECEWORK_ITEM = "PieceworkItem";//计件项目表
        public static string CODING_RULE_TABLE_TIME_ITEM = "TimeItem";//计时项目表
        public static string CODING_RULE_TABLE_COMMISSION_ITEM = "CommissionItem";//提成项目表
        public static string CODING_RULE_TABLE_INSU_SOCIAL = "InsuSocial";//社会保险比例
        public static string CODING_RULE_TABLE_SALARY_STANDARD = "SalaryStandard";//工资标准设置表
        public static string CODING_RULE_TABLE_SALARY_EMPL = "SalaryEmployee";//员工工资设置表
        public static string CODING_RULE_TABLE_SALARY_PIECEWORK = "PieceworkSalary";//计件工资表
        public static string CODING_RULE_TABLE_SALARY_TIME = "TimeSalary";//计时工资表
        public static string CODING_RULE_TABLE_SALARY_COMMISSION = "CommissionSalary";//提成工资表
        public static string CODING_RULE_TABLE_INSU_SOCIAL_EMPL = "InsuEmployee";//员工社会保险表
        public static string CODING_RULE_TABLE_SALARY_REPORT = "SalaryReport";//工资报表表
        public static string CODING_RULE_COLUMN_SALARY_REPORT_NO = "ReprotNo";//报表编号

        /* 绩效考核 */
        public static string CODING_RULE_TABLE_PERFORMANCETYPE = "PerformanceType";// 考核类型设置表
        public static string CODING_RULE_TABLE_PERFORMANCELEM = "PerformanceElem";// 考核指标及评分规则设置表
        public static string CODING_RULE_TABLE_PERFORMANCTASK = "PerformanceTask";// 考核任务表
        public static string CODING_RULE_TABLE_PERFORMANCETEMPLATEEMP = "PerformanceTemplateEmp";//人员考核流程表
        public static string CODING_RULE_TABLE_PERFORMANCPERSONAL = "PerformancePersonal";// 考核任务表
        public static string CODING_RULE_TABLE_PERFORMANCBETTER = "PerformanceBetter";// 绩效改进计划表
        public static string CODING_RULE_TABLE_PERFORMANCSUMMARY = "PerformanceSummary";//  考核总评表
        /* 生产管理 */
        public static string CODING_RULE_TABLE_WORKCENTER = "WorkCenter";//工作中心
        public static string CODING_RULE_TABLE_TECHNICSARCHIVES = "TechnicsArchives";//工艺档案
        public static string CODING_RULE_TABLE_STANDARDSEQU = "StandardSequ";//标准工序
        public static string CODING_RULE_TABLE_STANDARDSEQUDETAIL = "StandardSequDetail";//标准工序明细
        public static string CODING_RULE_TABLE_TECHNICSROUTING = "TechnicsRouting";//工艺路线
        public static string CODING_RULE_TABLE_TECHNICSROUTINGDETAIL = "TechnicsRoutingDetail";//工艺路线明细
        public static string CODING_RULE_TABLE_SCHEDULE = "MasterProductSchedule";//主生产计划
        public static string CODING_RULE_TABLE_BOM = "BOM";//物料清单
        public static string CODING_RULE_TABLE_MRP = "MRP";//物料需求计划
        public static string CODING_RULE_TABLE_MANUFACTURETASK = "ManufactureTask";//生产任务单
        public static string CODING_RULE_TABLE_MANUFACTURETASKDETAIL = "ManufactureTaskDetail";//生产任务单
        public static string CODING_RULE_TABLE_REPORT = "ManufactureReport";//生产任务汇报单
        public static string CODING_RULE_TABLE_REPORT_PRODUCT = "ManufactureReportProduct";//生产任务汇报生产状况
        public static string CODING_RULE_TABLE_TAKE = "TakeMaterial";//领料单
        public static string CODING_RULE_TABLE_BACK = "BackMaterial";//退料单

        public static string CODING_RULE_COLUMN_PLAN_NO = "PlanNo";//主生产计划单据编号
        public static string CODING_RULE_COLUMN_BOM_NO = "BomNo";//BOM编号
        public static string CODING_RULE_COLUMN_MRP_NO = "MRPNo";//物料需求计划编号
        public static string CODING_RULE_COLUMN_MANUFACTURETASK_NO = "TaskNo";//生产任务单编号
        public static string CODING_RULE_COLUMN_REPORT_NO = "ReportNo";//生产任务汇报单编号
        public static string CODING_RULE_COLUMN_TAKE_NO = "TakeNo";//领料单编号
        public static string CODING_RULE_COLUMN_BACK_NO = "BackNo";//退料单编号

        /*项目管理*/
        public static string CODING_RULE_TABLE_PROJECTINFO = "ProjectInfo";//项目档案
        public static string CODING_RULE_COLUMN_PROJECT_NO = "ProjectNo";//项目档案编号



        /* 目标模块*/
        public static string CODING_RULE_TABLE_PLANAIM = "PlanAim";//目标信息表
        public static string CODING_RULE_COLUMN_AIMNO = "AimNo";//目标编号字段
        public static string CODING_RULE_AIM_NO = "1";//

        /* 任务模块*/
        public static string CODING_RULE_TABLE_TASK = "Task";//任务信息表
        public static string CODING_RULE_COLUMN_TASKNO = "TaskNo";//任务编号字段
        public static string CODING_RULE_TASK_NO = "2";//

        /*日程模块*/
        public static string CODING_RULE_TABLE_PERSONALDATEARRANGE = "PersonalDateArrange";//日程信息表
        public static string CODING_RULE_COLUMN_ARRANGENO = "ArrangeNo";//日程编号字段
        public static string CODING_RULE_AGENDA_NO = "3";//

        #endregion

        #region 人事相关单据类型
        public static string CODING_TYPE_HUMAN = "2";//人事管理
        public static string CODING_HUMAN_ITEM_SALARY_REPORT = "2";//工资报表
        public static string CODING_HUMAN_ITEM_RECT = "3";//招聘申请
        public static string CODING_HUMAN_ITEM_PROXY = "4";//人才代理单
        public static string CODING_HUMAN_ITEM_RECTPLAN = "5";//招聘活动
        public static string CODING_HUMAN_ITEM_INTERVIEW = "6";//面试记录编号
        public static string CODING_HUMAN_ITEM_RTRAINING = "7";//培训编号
        public static string CODING_HUMAN_ITEM_RTRAININGASSE = "8";//培训考核编号
        public static string CODING_HUMAN_ITEM_TEST = "9";//考试记录编号
        public static string CODING_HUMAN_ITEM_CHECKTEMPLATE = "10";//面试模板编号
        public static string CODING_HUMAN_ITEM_EMPLAPPLY = "12";//调职申请编号
        public static string CODING_HUMAN_ITEM_MOVEAPPLY = "13";//离职申请编号
        public static string CODING_RULE_TYPE_TEN = "2";//绩效考核指标编号
        public static string CODING_RULE_TYPE_TEMPLATE = "2";//绩效考核模板编号
        public static string CODING_RULE_TYPE_PERFORMANCETASK = "2";//绩效考核模板编号
        public static string CODING_RULE_TYPE_PERFORMANCEPERSONAL = "2";//绩效自我鉴定编号
        public static string CODING_RULE_TYPE_PERFORMANCEBETTER = "2";//改进计划编号
        public static string CODING_RULE_HUMEN_NO = "11";//
        public static string CODING_RULE_HUMEN_TEMPLATE = "16";//
        public static string CODING_RULE_HUMEN_PERFORMANCETASK = "18";//
        public static string CODING_RULE_HUMEN_PERFORMANCEPERSONAL = "19";//
        public static string CODING_RULE_HUMEN_PERFORMANCEBETTER = "23";//
        public static string CODING_HUMAN_ITEM_SHIFT = "14";//调职编号
        public static string CODING_HUMAN_ITEM_LEAVE = "15";//离职编号
        public static string CODING_HUMAN_ITEM_CONTRACT = "17";//合同编号
        #endregion

        #region 行政单据类型
        /// <summary>
        /// 行政单据类型定义
        /// </summary>
        public static string CODING_RULE_EQUIPMENT = "3";//行政
        public static string CODING_RULE_EQUIPMENT_APPLY = "1";//设备申请单
        public static string CODING_RULE_EQUIPMENT_RECEIVE = "2";//设备领用单
        public static string CODING_RULE_EQUIPMENT_RETURN = "3";//设备归还单
        public static string CODING_RULE_EQUIPMENT_REPAIR = "4";//设备维修单
        public static string CODING_RULE_EQUIPMENT_USELESS = "5";//设备维修单
        public static string CODING_RULE_OFFICETHINGS_INSTROAGENO = "6";//办公用品入库单据

        public static string CODING_RULE_OFFICETHINGS_PurchaseApply = "26";//办公用品采购申请

        public static string CODING_RULE_OFFICETHINGS_APPLYNO = "7";//办公用品领用
        public static string CODING_RULE_EQUIPMENT_NO = "7";//设备编号（基础数据）
        public static string CODING_RULE_CAR_APPLY = "8";//车辆申请单
        public static string CODING_RULE_CAR_CARDISPATCH = "9";//车辆申请单
        public static string CODING_RULE_OFFICETHINGS_NO = "8";//办公用品编号(基础数据)

        public static string CODING_RULE_EQUIPMENT_MEETINGINFO = "13";//会议通知单
        public static string CODING_RULE_EQUIPMENT_MEETINGRECORD = "14";//会议记录单
        public static string CODING_RULE_EQUIPMENT_MEETINGDECISION = "15";//会议决议单

        public static string CODING_RULE_EQUIPMENT_DOCRECEIVE = "16";//收文编号
        public static string CODING_RULE_EQUIPMENT_DOCSEND = "17";//发文编号
        public static string CODING_RULE_EQUIPMENT_DOCREQUST = "18";//请示编号
        public static string CODING_RULE_EQUIPMENT_DOC = "19";//文档编号
        public static string CODING_RULE_ATTEDDANCEAPPLY_LEAVE = "20";//请假单编号
        public static string CODING_RULE_ATTEDDANCEAPPLY_OVERTIME = "21";//加班单编号
        public static string CODING_RULE_ATTEDDANCEAPPLY_BEOUT = "22";//外出单编号
        public static string CODING_RULE_ATTEDDANCEAPPLY_BUSINESS = "23";//出差单编号
        public static string CODING_RULE_ATTEDDANCEAPPLY_INSTEAD = "24";//替班单编号
        public static string CODING_RULE_ATTEDDANCEAPPLY_YEARHOLIDAY = "25";//请假单编号


        #endregion
        #region 请假类别定义
        /// <summary>
        /// 请假类别定义
        /// </summary>
        public static string CODING_RULE_ADMINTYPE = "3";//行政大类
        public static string CODING_RULE_MEETINGTYPE = "1";//会议类别
        public static string CODING_RULE_RECEIVESENDTYPE = "2";//收发文类别
        public static string CODING_RULE_LEAVETYPE = "3";//请假类别
        public static string CODING_RULE_OFFICETHINGSTYPE = "4";//请假类别
        public static string CODING_RULE_REQUSTTYPE = "5";//请示类别
        #endregion

        #region 库存类别单据编码规则
        public static string CODING_RULE_Storage = "11";//仓库编号
        public static string CODING_RULE_Storage_NO = "8";//仓库单据
        public static string CODING_RULE_StorageIn_NO = "1";//入库单据编号
        public static string CODING_RULE_StorageOut_NO = "2";//出库单编号
        public static string CODING_RULE_STORAGE_BORROW = "3";//借货单编号
        public static string CODING_RULE_StorageAdjust_NO = "4";//调拨单编号
        public static string CODING_RULE_StoAdjust_NO = "7";//调整单编号
        public static string CODING_RULE_StorageLoss_NO = "5";//报损单编号
        public static string CODING_RULE_StorageQuality_NO = "10";//质检
        public static string CODING_RULE_StorageQualityCheck_NO = "2";//质检申请单编号
        public static string CODING_RULE_StorageCheckReport_NO = "3";//质检报告编号
        public static string CODING_RULE_StorageNOPass_NO = "1";//质检不合格编号
        public static string CODING_Public_StorageCheckReportTypeFlag = "10";  //公共分类需要
        public static string CODING_Public_StorageCheckReportTypeCode = "1"; //公共分类需要
        public static string CODING_RULE_STORAGERETURN_NO = "10";//借货返还单
        public static string CODING_RULE_STORAGE_CHECK = "6";//库存盘点单
        #endregion


        #region 物流配送 TypeFlag和TypeCode
        /*物流配送编号*/
        public static string TYPEFLAG_LogisticsDistribution_NO = "12";
        /*配送单*/
        public static string TYPECODE_SubDeliverySend_NO = "1";
        /*配送退货单*/
        public static string TYPECODE_SubDeliveryBack_NO = "2";
        /*门店调拨单*/
        public static string TYPECODE_SubDeliveryTrans_NO = "3";
        #endregion



        #region 生产相关单据类型
        public static string CODING_TYPE_PRODUCTION = "7";//生产管理
        public static string CODING_PRODUCTION_ITEM_SCHEDULE = "1";//主生产计划单
        public static string CODING_PRODUCTION_ITEM_MRP = "2";//物料需求计划单
        public static string CODING_PRODUCTION_ITEM_TASK = "3";//生产任务单
        public static string CODING_PRODUCTION_ITEM_BOM = "5";//物料清单
        public static string CODING_PRODUCTION_ITEM_REPORT = "6";//生产任务汇报单
        public static string CODING_PRODUCTION_ITEM_TAKE = "7";//领料单
        public static string CODING_PRODUCTION_ITEM_BACK = "8";//退料单
        #endregion

        #region 技术管理类型
        public static string CODING_TYPE_PROJECT = "13";//技术管理
        public static string CODING_PROJECT_ITEM_INFO = "1";//项目档案
        #endregion

        #endregion

        /// <summary>
        /// 锁定标志位
        /// </summary>
        public const string LOCK_FLAG_LOCKED = "1";
        public const string LOCK_FLAG_UNLOCKED = "0";

        public const string SEX_MALE_NUMBER = "1";
        public const string SEX_FEMALE_NUMBER = "2";
        public const string SEX_MALE_CHARACT = "男";
        public const string SEX_FEMALE_CHARACT = "女";
        public const string flag = "2";//add by taochun 标志officedba.EmployeeJob  区分
        public const string UsedStatus = "1"; //启用  dd by taochun 用户启用状态
        public const string UsedNOtStatus = "0"; //停用
        #endregion

        #region 财务模块相关

        //基本设置
        public static string CODING_RULE_Finance = "9";//财务大类
        //辅助核算最大数
        public static int ASSISTANT_MAXNUM = 5;
        /* 财务管理字典表 */
        public static string CODING_RULE_TABLE_ACCOUNTSUBJECTS = "AccountSubjects";//科目设置表
        public static string CODING_RULE_TABLE_SUMMARYTYPE = "SummaryType";//摘要类别表
        public static string CODING_RULE_TABLE_SUMMARYSETTING = "SummarySetting";//摘要设置
        public static string CODING_RULE_TABLE_ENDITEMSETTING = "EndItemProcSetting";//期末项目设置
        public static string CODING_RULE_TABLE_FIX = "FixAssetInfo";//固定资产表
        public static string CODING_RULE_TABLE_FIXWITHINFO = "FixWithInfo";//固定资产计提信息表
        public static string CODING_RULE_TABLE_ASSISTANTTYPE = "AssistantType";//辅助核算表
        public static string CODING_RULE_TABLE_CURRENCETYPE = "CurrencyTypeSetting";//币种类别表
        public static string CODING_RULE_TABLE_ATTESTBILL = "AttestBill";//凭证主表
        public static string CODING_RULE_TABLE_ATTESTBILLDETIALS = "AttestBillDetails";//凭证明细表
        public static string CODING_RULE_TABLE_ASSETTYPESETTING = "AssetTypeSetting";//固定资产类别表

        public static string CODING_RULE_TABLE_BILLING = "Billing";//业务单表
        public static string CODING_RULE_TABLE_INSIDECHANGEACCO = "InSideChangeAcco";//内部转账单表
        public static string CODING_RULE_TABLE_PAYBILL = "PayBill";//付款单表
        public static string CODING_RULE_TABLE_INCOMEBILL = "IncomeBill";//收款单表
        public static string CODING_RULE_TABLE_STOREFETCH = "StoreFetchBill";//存取款单

        //财务ModuleID
        public static string MODULE_ID_FINANCEMANAGER_FIX_ADD = "2091202";//新建固定资产
        public static string MODULE_ID_FINANCEMANAGER_FIX_LIST = "2091203";//固定资产列表
        public const string MODULE_ID_BILLING_ADD = "2091302";//新建业务单
        public const string MODULE_ID_BILLING_LIST = "2091303";//业务单列表
        public const string MODULE_ID_VOUCHER_ADD = "2091401";//新建凭证
        public const string MODULE_ID_VOUCHER_LIST = "2091402";//凭证列表

        public const string MODULE_ID_PAYBILL_ADD = "2091306";//新建付款单
        public const string MODULE_ID_PAYBILL_LIST = "2091307";//付款单列表

        public const string MODULE_ID_INSIDECHANGEACCO_ADD = "2091310"; //新建内部转账单
        public const string MODULE_ID_INSIDECHANGEACCO_LIST = "2091311";//内部转账单列表

        public const string MODULE_ID_INCOMEBILL_ADD = "2091304";//新建收款单
        public const string MODULE_ID_INCOMEBILL_LIST = "2091305";//收款单列表
        public const string MODULE_ID_STOREFETCHBILL_ADD = "2091308";//新建存取款单
        public const string MODULE_ID_STOREFETCHBILL_LIST = "2091309";//存取款单列表
        public const string MODULE_ID_ENDITEMPROCESSING = "2091403";//期末处理
        public const string MODULE_ID_PROFITFORMULA_ADD = "2091516";//利润表公式列表
        public const string MODULE_ID_PROFITFORMULA_LIST = "2091517";//利润表列表
        public const string MODULE_ID_CASHFLOWFORMULA_ADD = "2091516";//现金流量表公式列表
        public const string MODULE_ID_CASHFLOWFORMULA_LIST = "2091517";//现金流量表列表
        public const string MODULE_ID_ACCOUNTSUBJECTS_SETTING = "2091108";//会计科目设置
        public const string MODULE_ID_SUMMARYTYPE_LIST = "2091103";//摘要类别设置
        public const string MODULE_ID_SUMMARTSETTING_LIST = "2091104";//摘要设置
        public const string MODULE_ID_ENDITEM_LIST = "2091109";//期末项目设置
        public const string MODULE_ID_ASSISTANTTYPE_LIST = "2091110";//辅助核算类别
        public const string MODULE_ID_CURRENCYTYOE_LIST = "2091111";//币种类别
        public const string MODULE_ID_ASSETTYPE_LIST = "2091201";//固定资产类别设置
        public const string MODULE_ID_ACCOUNTBOOKTOTAL = "2091502";//总分类账列表
        public const string MODULE_ID_ACCOUNTBOOKLIST = "2091501";//明细帐列表
        public const string MODULE_ID_BALAFORMULALIST = "2091514";//资产负债表公式列表
        public const string MODULE_ID_CASHACCOUNT = "2091503";//现金日记账
        public const string MODULE_ID_BANKACCOUNT = "2091504";//银行日记账

        //编号规则ItemTypeID
        public static string CODING_RULE_Finance_Fees = "7";//费用票据

        //科目借贷方向
        public const string SUBJECTS_DIRE_J_CODE = "1";
        public const string SUBJECTS_DIRE_J_NAME = "借";
        public const string SUBJECTS_DIRE_D_CODE = "0";
        public const string SUBJECTS_DIRE_D_NAME = "贷";

        public const string ITEMTYPE_XJ_CODE = "1";
        public const string ITEMTYPE_XJ_NAME = "现金流量表";
        public const string ITEMTYPE_ZC_CODE = "0";
        public const string ITEMTYPE_ZC_NAME = "资产负债表";

        public const string USEDSTATUS_IS_USING = "1";
        public const string USEDSTATUS_IS_NOTUSING = "0";

        public const string EXEC_RESULT_SUCCESS_CODE = "1";
        public const string EXEC_RESULT_SUCCESS_NAME = "成功";
        public const string EXEC_RESULT_FAIL_CODE = "0";
        public const string EXEC_RESULT_FAIL_NAME = "失败";

        public const string ENDITEM_GDZCZK_NAME = "固定资产折旧";
        public const string ENDITEM_WXZCTX_NAME = "无形资产摊销";
        public const string ENDITEM_CQDDFY_NAME = "长期待摊费用";
        public const string ENDITEM_SYJZ_NAME = "损益结转";
        public const string ENDITEM_QITH_NAME = "期末调汇";

        //固定资类别
        public const string ASSETTYPE_GDZC_CODE = "0";
        public const string ASSETTYPE_GDZC_NAME = "固定资产";
        public const string ASSETTYPE_WXZC_CODE = "1";
        public const string ASSETTYPE_WXZC_NAME = "无形资产";
        public const string ASSETTYPE_QTZC_CODE = "2";
        public const string ASSETTYPE_QTZC_NAME = "其他资产";

        //固定资产计提方法

        //年限平均法
        public const string ASSETCOUNT_METHOD_NXPJF_CODE = "0";
        public const string ASSETCOUNT_METHOD_NXPJF_NAME = "年限平均法";
        //工作量法
        public const string ASSETCOUNT_METHOD_GZLF_CODE = "1";
        public const string ASSETCOUNT_METHOD_GZLF_NAME = "工作量法";
        //年限总和法
        public const string ASSETCOUNT_METHOD_NXZHF_CODE = "2";
        public const string ASSETCOUNT_METHOD_NXZHF_NAME = "年限总和法";
        //双倍余额递减法
        public const string ASSETCOUNT_METHOD_SBYETJS_CODE = "3";
        public const string ASSETCOUNT_METHOD_SBYETJS_NAME = "双倍余额递减法";

        //出纳管理更新业务单结算状态
        public const string ACCOUNTS_STATUS_WJS = "0";//未结算
        public const string ACCOUNTS_STATUS_YJSZ = "2";//结算中
        public const string ACCOUNTS_STATUS_YJS = "1";//已结算

        //确认状态
        public const string CONFIRM_STATUS_CODE_OK = "1";//已确认
        public const string CONFIRM_STATUS_NAME_OK = "已确认";
        public const string CONFIRM_STATUS_CODE_NO = "0";//未确认
        public const string CONFIRM_STATUS_NAME_NO = "未确认";

        public const string TYPE_ZC_CODE = "1";
        public const string TYPE_ZC_NAME = "资产类";
        public const string TYPE_FZ_CODE = "2";
        public const string TYPE_FZ_NAME = "负债类";
        public const string TYPE_GT_CODE = "3";
        public const string TYPE_GT_NAME = "共同";
        public const string TYPE_QY_CODE = "4";
        public const string TYPE_QY_NAME = "权益类";
        public const string TYPE_CB_CODE = "5";
        public const string TYPE_CB_NAME = "成本类";
        public const string TYPE_SY_CODE = "6";
        public const string TYPE_SY_NAME = "损益类";

        public const string ATTESTBILL_AUDITOR = "Auditor";//审核状态
        public const string ATTESTBILL_ANTIAUDITOR = "AntiAuditor";//反审核状态
        public const string ATTESTBILL_ACCOUNT = "Account";//登帐状态
        public const string ATTESTBILL_ANTIACCOUNT = "AntiAccount";//反登帐状态


        //摘要定义
        public const string SUMMARY_ZCJZ_NAME = "计提折旧费用";//计提折旧费用
        public const string SUMMARY_JZHDSY_NAME = "结转汇兑损益";//结转汇兑损益
        public const string SUMMARY_JZBQSY_NAMEA = "结转本期损益";//结转本期损益

        //财务付款单类别标识
        public const string TYPE_CWSKD_FLAG_CODE = "9";
        //财务付款单类别代码
        public const string TYPE_CWSKD_BILL_CODE = "1";
        //财务预算单据
        public const string TYPE_BUDGET_BILL_CODE = "6";

        public const string WARNING_MOBILEPHONE_MESSAGE_CODE = "0";
        public const string WARNING_MOBILEPHONE_MESSAGE_NAME = "手机短信";
        public const string WARNING_SITEMESSAGE_MESSAGE_CODE = "1";
        public const string WARNING_SITEMESSAGE_MESSAGE_NAME = "站内短信";

        //财务预警表
        public const string TABLE_FINANCEWARNING_CODE = "officedba.FinanceWarning";
        //资产类别表
        public const string TABLE_ASSETTYPE_NAME = "officedba.AssetTypeSetting";


        #region 现金流量表项目常量
        public const string ItemName1 = "一、经营活动产生的现金流量：";
        public const string ItemName2 = "销售商品、提供劳务收到的现金";
        public const string ItemName3 = "收到的税费返还";
        public const string ItemName4 = "收到的其他与经营活动有关的现金";
        public const string ItemName5 = "现金流入小计";
        public const string ItemName6 = "购买商品、接受劳务支付的现金";
        public const string ItemName7 = "支付给职工以及为职工支付的现金";
        public const string ItemName8 = "支付的各项税费";
        public const string ItemName9 = "支付的其他与经营活动有关的现金";
        public const string ItemName10 = "现金流出小计";
        public const string ItemName11 = "经营活动产生的现金流量净额";
        public const string ItemName12 = "二、投资活动产生的现金流量：";
        public const string ItemName13 = "收回投资所收到的现金";
        public const string ItemName14 = "取得投资收益所收到的现金";
        public const string ItemName15 = "处置固定资产、无形资产和其他长期资产所收回的现金净额";
        public const string ItemName16 = "收到的其他与投资活动有关的现金";
        public const string ItemName17 = "现金流入小计";
        public const string ItemName18 = "购建固定资产、无形资产和其他长期资产所支付的现金";
        public const string ItemName19 = "投资所支付的现金";
        public const string ItemName20 = "支付的其他与投资活动有关的现金";
        public const string ItemName21 = "现金流出小计";
        public const string ItemName22 = "投资活动产生的现金流量净额";
        public const string ItemName23 = "三、筹资活动产生的现金流量：";
        public const string ItemName24 = "吸收投资所收到的现金";
        public const string ItemName25 = "借款所收到的现金";
        public const string ItemName26 = "收到的其他与筹资活动有关的现金";
        public const string ItemName27 = "现金流入小计";
        public const string ItemName28 = "偿还债务所支付的现金";
        public const string ItemName29 = "分配股利、利润或偿付利息所支付的现金";
        public const string ItemName30 = "支付的其他与筹资活动有关的现金";
        public const string ItemName31 = "现金流出小计";
        public const string ItemName32 = "筹资活动产生的现金流量净额";
        public const string ItemName33 = "四、汇率变动对现金的影响";
        public const string ItemName34 = "五、现金及现金等价物净增加额";
        public const string ItemName35 = "1、将净利润调节为经营活动现金流量：";
        public const string ItemName36 = "净利润";
        public const string ItemName37 = "加：计提的资产减值准备";
        public const string ItemName38 = "固定资产折旧";
        public const string ItemName39 = "无形资产摊销";
        public const string ItemName40 = "长期待摊费用摊销";
        public const string ItemName41 = "处置固定资产、无形资产和其他长期资产的损失（减：收益）";
        public const string ItemName42 = "固定资产报废损失";
        public const string ItemName43 = "公允价值变动损失(收益以“－”填列)";
        public const string ItemName44 = "财务费用";
        public const string ItemName45 = "投资损失（减：收益）";
        public const string ItemName46 = "递延所得税资产减少（增加以“－”号填列）";
        public const string ItemName47 = "递延所得税负债增加（减少以“－”号填列）";
        public const string ItemName48 = "存货的减少（减：增加）";
        public const string ItemName49 = "经营性应收项目的减少（减：增加）";
        public const string ItemName50 = "经营性应付项目的增加（减：减少）";
        public const string ItemName51 = "其他";
        public const string ItemName52 = "经营活动产生的现金流量净额";
        public const string ItemName53 = "";
        public const string ItemName54 = "";
        public const string ItemName55 = "";
        public const string ItemName56 = "2、不涉及现金收支的投资和筹资活动：";
        public const string ItemName57 = "债务转为资本";
        public const string ItemName58 = "一年内到期的可转换公司债券";
        public const string ItemName59 = "融资租入固定资产";
        public const string ItemName60 = "";
        public const string ItemName61 = "";
        public const string ItemName62 = "";
        public const string ItemName63 = "3、现金及现金等价物净增加情况：";
        public const string ItemName64 = "现金的期末余额";
        public const string ItemName65 = "减：现金的期初余额";
        public const string ItemName66 = "加：现金等价物的期末余额";
        public const string ItemName67 = "减：现金等价物的期初余额";
        public const string ItemName68 = "现金及现金等价物净增加额";
        #endregion


        #endregion

        #region 人事模块相关
        /* 履历区分 */
        public static string HUMAN_HISTORY_WORK = "1";//工作
        public static string HUMAN_HISTORY_STUDY = "2";//学习
        /* 编辑区分 */
        public static string EDIT_FLAG_INSERT = "INSERT";
        public static string EDIT_FLAG_UPDATE = "UPDATE";
        /* 在职状况 */
        public static string JOB_FLAG_ENTER = "1";//入职
        public static string JOB_FLAG_CHANGE = "2";//调职
        public static string JOB_FLAG_LEAVE = "3";//离职

        public static string EMPLOYEE_FLAG_TALENT = "2";//人才储备

        /* 合同状态 */
        public static string CONTRACT_FLAG_ONE = "1";//有效
        public static string CONTRACT_FLAG_ZERO = "0";//无效

        /* 人员部门 区分*/
        public static string DEPT_EMPLOY_FLAG_EMPLOY = "1";//人员
        public static string DEPT_EMPLOY_FLAG_DEPT = "2";//部门
        public static string DEPT_EMPLOY_SELECT_EMPLOY = "User_";//人员
        public static string DEPT_EMPLOY_SELECT_DEPT = "Dept_";//部门

        public static string SellAreaTypeFlag = "4";
        public static string SellAreaCodeType = "12";
        #endregion

        #region 客户模块相关
        public static string CUST_TYPE_CUST = "4";//客户模块分类

        //客户模块单据打印
        public static int PRINTBILL_CUSTINFO = 1;//客户档案-企业
        public static int PRINTBILL_CUSTINFOLINK = 2;//客户档案-会员

        public static string CUST_INFO_CUSTTYPE = "1";//客户类别
        public static string CUST_INFO_CREDITGRADE = "2";//客户优质级别
        public static string CUST_INFO_LINKCYCLE = "3";//客户联络期限
        public static string CUST_LINK_LINKTYPE = "4";//联系人类型
        public static string CUST_CONTACT_LINKREASONID = "5";//客户联络事由
        public static string CUST_CONTACT_LINKMODE = "6";//客户联络方式
        public static string CUST_SERVICES_SERVETYPE = "7";//客户服务类型
        public static string CUST_SERVICES_FASHION = "8";//客户服务方式
        //public static string CUST_SERVICES_? = "9";//客户服务紧急程度
        public static string CUST_COMPLAIN_TYPE = "10";//客户投诉分类
        public static string CUST_INFO_PAYTYPE = "11";//结算方式
        public static string CUST_INFO_AREAID = "12";//区域
        public static string CUST_INFO_LOVE = "13";//客户关怀分类
        public static string CUST_INFO_MONEYTYPE = "14";//支付方式
        public static string CUST_INFO_TALK = "15";//客户洽谈方式

        public static string CUST_CODINGTYPE = "0";//客户编号分类标志
        public static string CUST_ITEMTYPEID = "1";//客户编号分类编码

        public static string CUST_CODINGTYPE_BILL = "4";//客户单据编号分类标志
        public static string CUST_BILL_Service = "1";//客户服务
        public static string CUST_BILL_Complain = "2";//客户投诉
        public static string CUST_BILL_CONTACT = "3";//客户联络
        public static string CUST_BILL_Love = "4";//客户关怀
        public static string CUST_BILL_Talk = "5";//客户洽谈
        public static string CUST_BILL_CustAdvice = "6";//客户建议

        public static string CUST_TABLENAME = "CustInfo";//客户信息表名
        public static string CUST_CUSTNO = "CustNo";//客户编号字段名
        public static string CUST_TABLENAME_CONTACT = "CustContact";//客户联络表名
        public static string CUST_NO_CONTACTNO = "ContactNo";//客户联络单编号字段名
        public static string CUST_TABLENAME_SERVICE = "CustService";//客户服务表名
        public static string CUST_NO_SERVENO = "ServeNo";//客户服务单编号字段名
        public static string CUST_TABLENAME_COMPLAIN = "CustComplain";//客户服务表名
        public static string CUST_NO_COMPLAIN = "ComplainNo";//客户服务单编号字段名

        public static string MODULE_ID_CUST_INFO_ADD = "2021101";//新建客户信息
        public static string MODULE_ID_CUST_INFO_LIST = "2021102";//客户信息列表
        public static string MODULE_ID_CUST_INFO_EDIT = "2021103";//查看修改客户信息

        public static string MODULE_ID_CUST_LINK_ADD = "2021201";//新建客户联系人
        public static string MODULE_ID_CUST_LINK_LIST = "2021202";//客户联系人信息列表
        public static string MODULE_ID_CUST_LINK_EDIT = "2021203";//查看修改客户联系人

        public static string MODULE_ID_CUST_CONTACT_ADD = "2021301";//新建客户联络
        public static string MODULE_ID_CUST_CONTACT_LIST = "2021302";//客户联络列表
        public static string MODULE_ID_CUST_CONTACT_DEFER = "2021303";//客户联络延期预警

        public static string MODULE_ID_CUST_TALK_ADD = "2021401";//新建客户洽谈
        public static string MODULE_ID_CUST_TALK_LIST = "2021402";//客户洽谈列表

        public static string MODULE_ID_CUST_LOVE_ADD = "2021501";//新建客户关怀
        public static string MODULE_ID_CUST_LOVE_LIST = "2021502";//客户关怀列表

        public static string MODULE_ID_CUST_SERVICE_ADD = "2021601";//新建客户服务
        public static string MODULE_ID_CUST_SERVICE_LIST = "2021602";//客户服务列表
        public static string MODULE_ID_CUST_SERVICE_ANNAL = "2021603";//产品销售记录

        public static string MODULE_ID_CUST_COMPLAIN_ADD = "2021701";//新建客户投诉
        public static string MODULE_ID_CUST_COMPLAIN_LIST = "2021702";//客户投诉列表        

        public static string TABLE_NAME_CUST = "officedba.CustInfo";//客户信息表
        public static string TABLE_NAME_LISIMAN = "officedba.CustLinkMan";//客户联系人表
        public static string TABLE_NAME_CONTACT = "officedba.CustContact";//客户联络表
        public static string TABLE_NAME_SERVICE = "officedba.CustService";//客户服务表
        public static string TABLE_NAME_COMPLAIN = "officedba.CustComplain";//客户投诉表
        public static string TABLE_NAME_LOVE = "officedba.CustLove";//客户关怀表
        public static string TABLE_NAME_TALK = "officedba.CustTalk";//客户洽谈表
        public static string MODULEID_CUSTINFO_ExtAttribute = "2021804"; //客户特性设置

        #endregion

        #region 销售模块相关分类
        public static string SELL_TYPE_CHANCETYPE = "2";//销售机会类型
        public static string SELL_TYPE_HAPSOURCE = "3";//销售机会来源
        public static string SELL_TYPE_FEASIBILITY = "4";//销售机会可能性
        public static string SELL_TYPE_CARRYTYPE = "8";//运送方式
        public static string SELL_TYPE_SELL = "6";//销售模块分类
        public static string SELL_TYPE_SELLTYPE = "10";//销售类别分类
        public static string SELL_TYPE_TAKETYPE = "7";//交货方式
        public static string SELL_TYPE_PACKAGE = "9";//包装方式

        public static string SELL_TYPE_ADVERSARY = "1";//竞争对手类型
        public static string SELL_TYPE_ORDER = "5";//销售订单类型
        public static string SELL_TYPE_ORDERMETHOD = "6";//订货方式


        #endregion

        #region 采购模块相关分类
        public static string PURCHASE_TYPE_PURCHASE = "7";//采购模块分类
        public static string PURCHASE_TYPE_PROVIDER = "1";//供应商类别
        public static string PURCHASE_TYPE_PURCHASETYPE = "5";//采购类别
        public static string PURCHASE_TYPE_TAKETYPE = "6";//交货方式
        public static string PURCHASE_TYPE_CARRYTYPE = "7";//运送方式
        public static string PURCHASE_TYPE_PAYTYPE = "8";//结算方式
        public static string PURCHASE_TYPE_MONEYTYPE = "9";//支付方式
        #endregion

        #region 设备领用状态定义
        /// <summary>
        /// 待领用
        /// </summary>
        public static string STATUS_RULE_EQUIPMENT_BEFORE_RECEIVE = "0";
        /// <summary>
        /// 使用中
        /// </summary>
        public static string STATUS_RULE_EQUIPMENT_MIDDLE_RECEIVE = "1";
        /// <summary>
        /// 已归还
        /// </summary>
        public static string STATUS_RULE_EQUIPMENT_AFTER_RECEIVE = "2";
        #endregion

        #region 设备使用类型定义
        public static string STATUS_RULE_EQUIPMENT_TYPE_RECEIVE = "1";//领用
        public static string STATUS_RULE_EQUIPMENT_MIDDLE_BORROW = "2";//借用

        public static string MODULE_ID_DOCRECEIVE_ADD = "2001401";//新建收文信息
        public static string MODULE_ID_DOCRECEIVE_LIST = "2001402";//收文信息列表
        public static string MODULE_ID_DOCSEND_ADD = "2001403";//新建发文信息
        public static string MODULE_ID_DOCSEND_LIST = "2001404";//发文信息列表
        public static string MODULE_ID_DOCREQUST_ADD = "2001405";//新建请示信息
        public static string MODULE_ID_DOCREQUST_LIST = "2001406";//请示信息列表
        public static string MODULE_ID_DOC_ADD = "2001407";//新建文档信息
        public static string MODULE_ID_DOC_LIST = "2001408";//文档信息列表

        public static string TABLE_NAME_DOCRECEIVE = "officedba.DocReceiveInfo";//收文信息表
        public static string TABLE_NAME_DOCSEND = "officedba.DocSendInfo";//发文信息表
        public static string TABLE_NAME_DOCREQUST = "officedba.DocRequstInfo";//请示信息表
        public static string TABLE_NAME_DOC = "officedba.DocInfo";//文档信息表

        public static string MODULE_ID_MEETINGROOM_LIST = "2001501";//会议室列表
        public static string MODULE_ID_MEETINGROOM_ADD = "2001502";//新建会议室
        public static string MODULE_ID_MEETINGINFO_ADD = "2001503";//新建会议通知
        public static string MODULE_ID_MEETINGINFO_LIST = "2001504";//会议通知列表
        public static string MODULE_ID_MEETINGRECORD_ADD = "2001505";//新建会议记录
        public static string MODULE_ID_MEETINGRECORD_LIST = "2001506";//会议记录列表
        public static string MODULE_ID_MEETINGDECISION_LIST = "2001507";//会议决议核查

        public static string TABLE_NAME_MEETINGROOM = "officedba.MeetingRoom";//会议室信息表
        public static string TABLE_NAME_MEETINGINFO = "officedba.MeetingInfo";//会议信息表
        public static string TABLE_NAME_MEETINGRECORD = "officedba.MeetingRecord";//会议记录表
        public static string TABLE_NAME_MEETINGDECISION = "officedba.MeetingDecision";//会议决议记录表
        public static string TABLE_NAME_MEETINGTALK = "officedba.MeetingTalk";//会议发言记录表

        #endregion

        #region 供应链模块相关分类
        public static string PROVIDE_TYPE_PROVIDE = "5";//供应链模块分类
        public static string PROVIDE_TYPE_FEETYPE = "7";//费用分类
        public static string BankTypeFlag = "4";//银行分类
        #endregion

        #region 单据

        #region 单据类型
        public static string BILL_TYPEFFLAG_PERSONALOFFICE = "1";                             //个人办公
        public static string BILL_TYPEFLAG_HUMAN = "2";                                              //人事
        public static string BILL_TYPEFLAG_EXECUTIVE = "3";                                          //行政
        public static string BILL_TYPEFLAG_CUSTOMER = "4";                                         //客户
        public static string BILL_TYPEFLAG_SALE = "5";                                                    //销售
        public static string BILL_TYPEFLAG_PURCHASE = "6";                                          //采购
        public static string BILL_TYPEFLAG_PRODUCTION = "7";                                     //生产
        public static string BILL_TYPEFLAG_STORAGE = "8";                                            //库存
        public static string BILL_TYPEFLAG_FINANCE = "9";                                             //财务
        
        /// <summary>
        /// 质检管理单据类型编号
        /// </summary>
        public static string BILL_TYPEFLAG_QUALITYCHECK = "10";                                             //质检管理
        /// <summary>
        /// 门店管理单据类型编号
        /// </summary>
        public static string BILL_TYPEFLAG_SUBSTORAGE = "11";                                             //门店管理
        public static string BILL_TYPEFLAG_DELIVERY = "12";                                          //门店配送

        #endregion

        #region  个人桌面单据代码
        /// <summary>
        /// 目标单
        /// </summary>
        public static string BILL_TYPECODE_PERSONAL_AIM = "1";

        #endregion

        #region 生产单据代码
        /// <summary>
        /// [生产] 生产计划单
        /// </summary>
        public static string BILL_TYPECODE_PRODUCTION_SCHEDULE = "1";

        /// <summary>
        /// [生产] 物料需求计划单
        /// </summary>
        public static string BILL_TYPECODE_PRODUCTION_MRP = "2";

        /// <summary>
        /// [生产] 生产任务单
        /// </summary>
        public static string BILL_TYPECODE_PRODUCTION_MANUFACTURETASK = "3";

        /// <summary>
        /// [生产] 工单
        /// </summary>
        public static string BILL_TYPECODE_PRODUCTION_WORKORDER = "4";

        /// <summary>
        /// [生产] 生产日报
        /// </summary>
        public static string BILL_TYPECODE_PRODUCTION_DAILYREPORT = "5";

        /// <summary>
        /// [生产] 领料单
        /// </summary>
        public static string BILL_TYPECODE_PRODUCTION_TAKE = "7";

        /// <summary>
        /// [生产] 退料单
        /// </summary>
        public static string BILL_TYPECODE_PRODUCTION_BACK = "8";
        #endregion

        #region 生产打印模板代码
        public static int PRINTBILL_TYPEFLAG_MASTERPRODUCTSCHEDULE = 1;   /*主生产计划*/
        public static int PRINTBILL_TYPEFLAG_MRP = 2;   /*物料需求计划*/
        public static int PRINTBILL_TYPEFLAG_TASK = 3;   /*生产任务单*/
        public static int PRINTBILL_TYPEFLAG_REPORT = 4;   /*生产任务汇报单*/
        public static int PRINTBILL_TYPEFLAG_TAKE = 5;   /*领料单*/
        public static int PRINTBILL_TYPEFLAG_BACK = 6;   /*退料单*/

        public static string PRINTBILL_TYPEFLAG_DELIVERY_SEND = "2";  /*配送单*/
        public static string PRINTBILL_TYPEFLAG_DELIVERY_BACK = "3";   /*配送退货单*/
        public static string PRINTBILL_TYPEFLAG_DELIVERY_TRANS = "4"; /*门店调拨单*/

        #endregion

        #region 技术管理打印模板代码
        public static string BILL_TYPEFLAG_PROJECT = "13";   //技术管理
        public static int PRINTBILL_TYPEFLAG_PROJECT = 1;   /*项目档案*/

        #endregion

        #region 门店管理打印模板代码
        public static int PRINTBILL_TYPEFLAG_SUBSTORAGECUST = 4;   /*客户*/
        #endregion

        #region 销售模块打印单据标识
        public static int PRINTBILL_SELLPLAN = 1;//销售计划
        public static int PRINTBILL_ADVERSARY = 2;//竞争对手档案
        public static int PRINTBILL_ADVERSARYSELL = 3;//销售竞争分析

        public static int PRINTBILL_SELLCHANCE = 4;//销售机会
        public static int PRINTBILL_SELLOFFER = 5;//销售报价
        public static int PRINTBILL_SELLCONTRANCT = 6;//销售合同
        public static int PRINTBILL_TYPEFLAG_SELLORDER = 7;//销售订单
        public static int PRINTBILL_TYPEFLAG_SELLSEND = 8;//销售发货单
        public static int PRINTBILL_TYPEFLAG_SELLGATHERING = 9;//销售回款计划
        public static int PRINTBILL_TYPEFLAG_SELLBACK = 10;//销售退货单
        public static int PRINTBILL_TYPEFLAG_SELLCHANNELSTTL = 11;//委托代销结算单
        #endregion


        #region 库存打印模板代码
        public static int PRINTBILL_TYPEFLAG_OUTSELL = 6;   /*销售出库*/
        public static int PRINTBILL_TYPEFLAG_OUTOTHER = 7;   /*其他出库*/
        public static int PRINTBILL_TYPEFLAG_OUTRED = 8;   /*红冲出库*/
        public static int PRINTBILL_TYPEFLAG_STORAGECHECK = 13;   /*期末盘点*/
        public static int PRINTBILL_TYPEFLAG_LOSS = 14;   /*库存报损*/
        public static int PRINTBILL_TYPEFLAG_Init = 1;  /*期初库存录入*/
        public static int PRINTBILL_TYPEFLAG_INPURCHASE = 2;  /*采购入库单*/
        public static int PRINTBILL_TYPEFLAG_INPROCESS = 3;  /*生产完工入库单*/
        public static int PRINTBILL_TYPEFLAG_INOTHER = 4;  /*其他入库单*/
        public static int PRINTBILL_TYPEFLAG_INRED = 5;  /*红冲入库单*/
        public static int PRINTBILL_TYPEFLAG_BORROW = 9; /*借货申请单*/
        public static int PRINTBILL_TYPEFLAG_RETURN = 10; /*借货返还单*/
        public static int PRINTBILL_TYPEFLAG_TRANSFER = 11; /*库存调拨单*/
        public static int PRINTBILL_TYPEFLAG_ADJUST = 12; /*日常调整单*/
        public static int PRINTBILL_TYPEFLAG_DayEnd = 15; /*库存日结单*/
        public static int PRINTBILL_TYPEFLAG_SubDayEnd = 16; /*门店日结单*/
        #endregion

        #region 人事模块审批单据
        /// <summary>
        /// [人事] 招聘申请
        /// </summary>
        public static string BILL_TYPECODE_HUMAN_RECT_APPLY = "3";
        public static string BILL_TYPECODE_HUMAN_RECT_INTERVIEW = "6";
        public static string BILL_TYPECODE_HUMAN_EMPL_APPLY = "12";
        public static string BILL_TYPECODE_HUMAN_MOVE_APPLY = "13";
        public static string BILL_TYPECODE_HUMAN_SALARY_REPORT = "2";
        #endregion

        #region 借货单据代码
        /// <summary>
        /// [借货] 借货申请单
        /// </summary>
        public static string BILL_TYPECODE_STORAGE_BORROW = "3";

        /// <summary>
        /// [借货] 借货返还单
        /// </summary>
        public static string BILL_TYPECODE_STORAGE_RETURN = "10";

        #endregion

        #region 调拨
        /// <summary>
        /// [调拨] 调拨单
        /// </summary>
        public static string BILL_TYPECODE_STORAGE_TRANSFER = "4";
        #endregion

        #region 盘点
        public static string BILL_TYPECODE_STORAGE_CHECK = "6";
        #endregion

        #region 质检
        /// <summary>
        /// 质检申请单[打印编号]
        /// </summary>
        public static int PRINTBILL_TYPEFLAG_QUALITYADD = 1;
        /// <summary>
        /// 质检报告单[打印编号]
        /// </summary>
        public static int PRINTBILL_TYPEFLAG_CHECKREPORT = 2;
        /// <summary>
        /// 不合格品处置单[打印编号]
        /// </summary>
        public static int PRINTBILL_TYPEFLAG_NOPASS = 3;

        public static string BILL_TYPECODE_STORAGE_QUALITY = "10";
        public static string BILL_TYPECODE_STORAGE_NOPASS = "1";
        public static string BILL_TYPECODE_STORAGE_QUALITYADD = "2";
        public static string BILL_TYPECODE_STORAGE_REPORT = "3";
        #endregion

        #region 门店管理打印
        /// <summary>
        /// 分店期初库存[打印编号]
        /// </summary>
        public static int PRINTBILL_TYPEFLAG_SUBSTORAGEIN = 1;
        /// <summary>
        /// 销售订单[打印编号]
        /// </summary>
        public static int PRINTBILL_TYPEFLAG_SUBSELLORDER = 2;
        /// <summary>
        /// 销售退货单[打印编号]
        /// </summary>
        public static int PRINTBILL_TYPEFLAG_SUBSELLBACK = 3;
        #endregion

        #region 系统管理
        public static string CodeEquipmentType_Equipment_Flag = "1";//设备
        public static string CodeEquipmentType_Office_Flag = "2"; //办公用品
        public static string FlowPublishUseStatus = "2";
        public static string FlowStopUseStatus = "1";
        #endregion
        #endregion

        #region 采购模块
        public static int PRINTBILL_TYPEFLAG_PurchaseSchedule = 6;   /*采购模块*/
        public static int PRINTBILL_TYPEFLAG_PurchaseApply = 4;   /*采购申请*/
        public static int PRINTBILL_TYPEFLAG_PurchasePlan = 5;   /*采购计划*/
        public static int PRINTBILL_TYPEFLAG_PurchaseAskPrice = 6;   /*采购询价*/
        public static int PRINTBILL_TYPEFLAG_PurchaseContract = 7;   /*采购合同*/
        public static int PRINTBILL_TYPEFLAG_PurchaseOrder = 8;   /*采购订单*/
        public static int PRINTBILL_TYPEFLAG_PurchaseArrive = 9;   /*采购到货*/
        public static int PRINTBILL_TYPEFLAG_PurchaseReject = 10;   /*采购退货*/
        #region 123 服务器
        //采购合同ModuleID
        public static string MODULE_ID_PurchaseContract_Add = "2041701";//新建采购合同
        public static string MODULE_ID_PurchaseContractInfo = "2041702";//采购合同列表
        public static string CODING_RULE_TABLE_PURCHASECONTRACT = "PurchaseContract";//采购合同表
        //采购到货通知ModuleID
        public static string MODULE_ID_PurchaseArrive_Add = "2041901";//新建采购到货通知单
        public static string MODULE_ID_PurchaseArriveInfo = "2041902";//采购到货通知单列表
        public static string CODING_RULE_TABLE_PURCHASEARRIVE = "PurchaseArrive";//采购到货表
        //采购退货单ModuleID
        public static string MODULE_ID_PurchaseReject_Add = "2042001";//新建采购退货单
        public static string MODULE_ID_PurchaseRejectInfo = "2042002";//采购退货单列表
        public static string CODING_RULE_TABLE_PURCHASEREJECT = "PurchaseReject";//采购退货表
        //采购历史价格列表ModuleID
        public static string MODULE_ID_PurchaseHistoryAskPriceInfo = "2042101";//采购历史价格列表
        public static string MODULE_ID_PURCHASEARRIVECOLLECT = "2042103";//采购到货汇总查询
        //供应商档案ModuleID
        public static string MODULE_ID_PROVIDERINFO_ADD = "2041101";//新建供应商档案
        public static string MODULE_ID_PROVIDERINFOINFO = "2041102";//供应商档案列表
        public static string CODING_RULE_TABLE_PROVIDERINFO = "ProviderInfo";//供应商信息表
        public static string MODULE_ID_PROVIDERLINKMAN_ADD = "2041103";//新建供应商联系人
        public static string MODULE_ID_PROVIDERLINKMANINFO = "2041104";//供应商联系人列表
        public static string CODING_RULE_TABLE_PROVIDERLINKMAN = "ProviderLinkMan";//供应商联系人表
        public static string MODULE_ID_PROVIDERPRODUCT_ADD = "2041105";//新建供应商物品信息
        public static string MODULE_ID_PROVIDERPRODUCTINFO = "2041106";//供应商物品信息列表
        public static string CODING_RULE_TABLE_PROVIDERPRODUCT = "ProviderProduct";//供应商物品表
        //供应商联络ModuleID
        public static string MODULE_ID_PROVIDERCONTRACTHISTORY_ADD = "2041201";//新建供应商联络
        public static string MODULE_ID_PROVIDERCONTRACTHISTORYINFO = "2041202";//供应商联络列表
        public static string MODULE_ID_PROVIDERCONTRACTHISTORYWARNING = "2041203";//供应商联络延期警告
        public static string CODING_RULE_TABLE_PROVIDERCONTRACTHISTORY = "ProviderContactHistory";//供应商联络表

        //采购申请ModelID
        public static string MODULE_ID_PURCHASEAPPLY_ADD = "2041301";//新建采购申请
        public static string MODULE_ID_PURCHASEAPPLY_INFO = "2041302";//采购申请列表
        public static string CODING_RULE_TABLE_PURCHASEAPPLY = "PurchaseApply";//采购申请表

        //采购需求ModelID
        public static string MODULE_ID_PURCHASEREQUIRE_INFO = "2041401";//采购需求列表
        public static string CODING_RULE_TABLE_PURCHASEREQUIRE = "PurchaseRequire";//采购需求表

        //采购计划ModelID
        public static string MODULE_ID_PURCHASEPLAN_ADD = "2041501";//新建采购计划
        public static string MODULE_ID_PURCHASEPLAN_INFO = "2041502";//采购计划列表
        public static string CODING_RULE_TABLE_PURCHASEPLAN = "PurchasePlan";//采购计划表

        //采购询价ModelID
        public static string MODULE_ID_PURCHASEASKPRICE_ADD = "2041601";//新建采购询价
        public static string MODULE_ID_PURCHASEASKPRICE_INFO = "2041602";//采购询价列表
        public static string CODING_RULE_TABLE_PURCHASEASKPRICE = "PurchaseAskPrice";//采购询价表

        //采购订单ModelID
        public static string MODULE_ID_PURCHASEORDER_ADD = "2041801";//新建采购订单
        public static string MODULE_ID_PURCHASEORDER_INFO = "2041802";//采购订单列表
        public static string CODING_RULE_TABLE_PURCHASEORDER = "PurchaseOrder";//采购订单表
        #endregion

        #region 199 服务器
        ////采购合同ModuleID
        //public static string MODULE_ID_PurchaseContract_Add = "2061501";//新建采购合同
        //public static string MODULE_ID_PurchaseContractInfo = "2061502";//采购合同列表
        //public static string CODING_RULE_TABLE_PURCHASECONTRACT = "PurchaseContract";//采购合同表
        ////采购到货通知ModuleID
        //public static string MODULE_ID_PurchaseArrive_Add = "2061701";//新建采购到货通知单
        //public static string MODULE_ID_PurchaseArriveInfo = "2061702";//采购到货通知单列表
        //public static string CODING_RULE_TABLE_PURCHASEARRIVE = "PurchaseArrive";//采购到货表
        //采购退货单ModuleID
        //public static string MODULE_ID_PurchaseReject_Add = "2061801";//新建采购退货单
        //public static string MODULE_ID_PurchaseRejectInfo = "2061802";//采购退货单列表
        //public static string CODING_RULE_TABLE_PURCHASEREJECT = "PurchaseReject";//采购退货表
        ////采购历史价格列表ModuleID
        //public static string MODULE_ID_PurchaseHistoryAskPriceInfo = "2061901";//采购历史价格列表
        //供应商档案ModuleID
        //public static string MODULE_ID_PROVIDERINFO_ADD = "2062001";//新建供应商档案
        //public static string MODULE_ID_PROVIDERINFOINFO = "2062002";//供应商档案列表
        //public static string CODING_RULE_TABLE_PROVIDERINFO = "ProviderInfo";//供应商信息表
        //public static string MODULE_ID_PROVIDERLINKMAN_ADD = "2062003";//新建供应商联系人
        //public static string MODULE_ID_PROVIDERLINKMANINFO = "2062004";//供应商联系人列表
        //public static string CODING_RULE_TABLE_PROVIDERLINKMAN = "ProviderLinkMan";//供应商联系人表
        //public static string MODULE_ID_PROVIDERPRODUCT_ADD = "2062005";//新建供应商物品信息
        //public static string MODULE_ID_PROVIDERPRODUCTINFO = "2062006";//供应商物品信息列表
        //public static string CODING_RULE_TABLE_PROVIDERPRODUCT = "ProviderProduct";//供应商物品表
        ////供应商联络ModuleID
        //public static string MODULE_ID_PROVIDERCONTRACTHISTORY_ADD = "2062101";//新建供应商联络
        //public static string MODULE_ID_PROVIDERCONTRACTHISTORYINFO = "2062102";//供应商联络列表
        //public static string MODULE_ID_PROVIDERCONTRACTHISTORYWARNING = "2062103";//供应商联络延期警告
        //public static string CODING_RULE_TABLE_PROVIDERCONTRACTHISTORY = "ProviderContactHistory";//供应商联络表
        #endregion

        ////采购申请ModelID
        //public static string MODULE_ID_PURCHASEAPPLY_ADD = "2061101";//新建采购申请
        //public static string MODULE_ID_PURCHASEAPPLY_INFO = "2061102";//采购申请列表
        //public static string CODING_RULE_TABLE_PURCHASEAPPLY = "PurchaseApply";//采购申请表

        ////采购需求ModelID
        //public static string MODULE_ID_PURCHASEREQUIRE_INFO = "2061201";//采购需求列表
        //public static string CODING_RULE_TABLE_PURCHASEREQUIRE = "PurchaseRequire";//采购需求表

        ////采购计划ModelID
        //public static string MODULE_ID_PURCHASEPLAN_ADD = "2061301";//新建采购计划
        //public static string MODULE_ID_PURCHASEPLAN_INFO = "2061302";//采购计划列表
        //public static string CODING_RULE_TABLE_PURCHASEPLAN = "PurchasePlan";//采购计划表

        ////采购询价ModelID
        //public static string MODULE_ID_PURCHASEASKPRICE_ADD = "2061601";//新建采购询价
        //public static string MODULE_ID_PURCHASEASKPRICE_INFO = "2061602";//采购询价列表
        //public static string CODING_RULE_TABLE_PURCHASEASKPRICE = "PurchaseAskPrice";//采购询价表

        ////采购订单ModelID
        //public static string MODULE_ID_PURCHASEORDER_ADD = "2061401";//新建采购订单
        //public static string MODULE_ID_PURCHASEORDER_INFO = "2061402";//采购订单列表
        //public static string CODING_RULE_TABLE_PURCHASEORDER = "PurchaseOrder";//采购订单表

        public static string BILL_TYPEFLAG_PURCHASE_APPLY = "1";//采购申请单
        public static string BILL_TYPEFLAG_PURCHASE_ORDER = "4";//采购订单
        public static string BILL_TYPEFLAG_PURCHASE_ARRIVE = "5";//采购到货通知单-审批
        public static string BILL_TYPEFLAG_PURCHASE_REJECT = "6";//采购退货单-审批
        public static string BILL_TYPEFLAG_PURCHASE_CONTRACT = "8";//采购合同-审批

        public static string PurchaseReject_ApplyReason_Flag = "21";//采购退货原因flag为21



        #region 采购打印模板代码
        public static int PRINTBILL_TYPEFLAG_PROVIDERINFO = 1;   /*供应商档案*/
        public static int PRINTBILL_TYPEFLAG_PROVIDERLINKMAN = 2;   /*供应商联系人*/
        public static int PRINTBILL_TYPEFLAG_PROVIDERCONTACTHISTORY = 3;   /*供应商联络*/
        #endregion


        #region 采购相关编码规则
        //采购
        public static string CODING_RULE_PURCHASE = "6";//采购分类
        public static string CODING_RULE_PURCHASE_APPLY = "1";//采购申请单
        public static string CODING_RULE_PURCHASE_PLAN = "2";//采购计划单
        public static string CODING_RULE_PURCHASE_ASKPRICE = "3";//采购询价单
        public static string CODING_RULE_PURCHASE_ORDER = "4";//采购订单
        public static string CODING_RULE_PURCHASE_ARRIVE = "5";//采购到货通知单
        public static string CODING_RULE_PURCHASE_REJECT = "6";//采购退货单
        public static string CODING_RULE_PURCHASE_PROVIDERLINKMAN = "7";//供应商联络单
        public static string CODING_RULE_PURCHASE_CONTRACT = "8";//采购合同单
        public static string CODING_RULE_PURCHASE_PROVIDERINFO = "9";//供应商档案
        #endregion
        #region 供应链模块
        public static string Menu_AddProduct = "2081601";//添加物品
        public static string Menu_SerchProduct = "2081602";//查询物品
        public static string CODING_RULE_TABLE_PRODUCTINFO = "ProductInfo";//人员信息表
        public static string Menu_AddProductPrice = "2081603";//添加物品售价
        public static string Menu_SerchProductPrice = "2081604";//查询物品售价
        public static string CODING_RULE_TABLE_PRODUCTINFOPRICE = "ProductPriceChange";//售价表
        public static string Menu_OtherCorpInfo = "2081703";//其他往来单位信息
        public static string Menu_OtherCorpInfoAdd = "2081703";//其他往来单位信息
        public static string CODING_RULE_TABLE_OtherCorpInfo = "OtherCorpInfo";//其他往来单位表
        public static string Menu_BankInfo = "2081702";//银行档案
        public static string CODING_RULE_TABLE_BankInfo = "BankInfo";//银行档案

        public static string Menu_CodeReasonType = "2081302";//原因
        public static string CODING_RULE_TABLE_CodeReasonType = "CodeReasonType";//
        public static string Menu_CodeFeeType = "2081303";//费用
        public static string CODING_RULE_TABLE_CodeFeeType = "CodeFeeType";//
        public static string Menu_CodeUnitType = "2081305";//计量单位
        public static string CODING_RULE_TABLE_CodeUnitType = "CodeUnitType";//
        public static string Menu_Equipment = "2001702";//设备
        public static string CODING_RULE_TABLE_Equipment = "CodeEquipmentType";//

        public static string Menu_DocTypeList = "2001704";//文档
        public static string CODING_RULE_TABLE_DocType = "CodeDocType";//

        public static string Menu_OfficeType = "2001703";//办公用品
        public static string CODING_RULE_TABLE_OfficeType = "CodeEquipmentType";//

        public static string Menu_CompanyType = "2081701";//往来单位
        public static string CODING_RULE_TABLE_CompanyType = "CodeCompanyType";//

        public static string Menu_ProductType = "2081304";//物品分类
        public static string CODING_RULE_TABLE_ProductType = "CodeProductType";//

        public static string Menu_PublicType = "2001701";//分类属性设置
        public static string CODING_RULE_TABLE_PublicType = "CodePublicType";//
        public static string Menu_ItemCodingRule = "2071001";//单据编号设置
        public static string CODING_RULE_TABLE_ItemCodingRule = "ItemCodingRule";//


        public static string Menu_Flow = "2011903";//流程表
        public static string CODING_RULE_TABLE_Flow = "Flow";//


        public static string Menu_AddUserInfo = "2191101";//添加用户
        public static string Menu_SerchUserInfo = "2191102";//用户列表
        public static string CODING_RULE_TABLE_UserInfo = "UserInfo";//人员信息表
        public static string Menu_AddRoleInfo = "2191103";//添加角色
        public static string Menu_SerchRoleInfo = "2191104";//角色列表
        public static string CODING_RULE_TABLE_RoleInfo = "RoleInfo";//角色信息表

        public static string Menu_AddUserRole = "2191105";//添加角色关联
        public static string Menu_SerchUserRole = "2191106";//角色关联列表
        public static string CODING_RULE_TABLE_UserRole = "UserRole";//角色关联信息表

        public static string Menu_SerchRoleFunction = "2191107";
        public static string CODING_RULE_TABLE_RoleFunction = "RoleFunction";//角色权限关联


        #endregion
        #endregion

        #region 生产模块
        public static string MODULE_ID_WORKCENTER_LIST = "2061101";//工作中心列表
        public static string MODULE_ID_TECHNICSARCHIVES_LIST = "2061102";//工艺档案列表
        public static string MODULE_ID_STANDARDSEQU_LIST = "2061103";//标准工序
        public static string MODULE_ID_TECHNICSROUTING_LIST = "2061104";//工艺路线
        public static string MODULE_ID_BOM_EDIT = "2061201";//新建物料清单
        public static string MODULE_ID_BOM_LIST = "2061202";//物料清单列表
        public static string MODULE_ID_SCHEDULE_EDIT = "2061301";//新建主生产计划
        public static string MODULE_ID_SCHEDULE_LIST = "2061302";//主生产计划列表
        public static string MODULE_ID_MRP_EDIT = "2061401";//新建物料需求计划
        public static string MODULE_ID_MRP_LIST = "2061402";//物料需求计划列表
        public static string MODULE_ID_MANUFACTURETASK_EDIT = "2061501";//新建生产任务
        public static string MODULE_ID_MANUFACTURETASK_LIST = "2061502";//生产任务列表
        public static string MODULE_ID_MANUFACTUREREPORT_EDIT = "2061503";//新建生产任务汇报单
        public static string MODULE_ID_MANUFACTUREREPORT_LIST = "2061504";//生产任务汇报单列表
        public static string MODULE_ID_TAKEMATERIAL_EDIT = "2061601";//新建领料单
        public static string MODULE_ID_TAKEMATERIAL_LIST = "2061602";//领料单列表
        public static string MODULE_ID_BACKMATERIAL_EDIT = "2061603";//新建退料单
        public static string MODULE_ID_BACKMATERIAL_LIST = "2061604";//退料单列表
        public static int STORAGEACCOUNT_BILLTYPE_TAKE = 17;/*库存流水账表：领料单据类别*/
        public static int STORAGEACCOUNT_BILLTYPE_BACK = 18;/*库存流水账表：退料单据类别*/
        #endregion

        #region 技术管理
        public static string MODULE_ID_PROJECTINFO_EDIT = "2210301";//新建项目档案
        public static string MODULE_ID_PROJECTINFO_LIST = "2210302";//项目档案列表
        #endregion

        #region 销售模块ModuleID
        //竞争对手ModelID
        public static string MODULE_ID_SELLPLAN_ADD = "2031001";//销售计划添加
        public static string MODULE_ID_SELLPLAN_INFO = "2031002";//销售计划列表
        public static string CODING_RULE_TABLE_SELLPLAN = "SellPlan,SellPlanDetail";//竞争对手表

        //竞争对手ModelID
        public static string MODULE_ID_ADVERSARYINFO_ADD = "2031101";//竞争对手添加
        public static string MODULE_ID_ADVERSARYINFO_INFO = "2031102";//竞争对手列表
        public static string CODING_RULE_TABLE_ADVERSARYINFO = "AdversaryInfo,AdversaryDynamic";//竞争对手表

        //销售竞争分析ModelID
        public static string MODULE_ID_ADVERSARYSELL_ADD = "2031103";//销售竞争分析添加
        public static string MODULE_ID_ADVERSARYSELL_INFO = "2031104";//销售竞争分析列表
        public static string CODING_RULE_TABLE_ADVERSARYSELL = "AdversarySell";//销售竞争分析表

        //销售机会ModelID
        public static string MODULE_ID_SELLCHANCE_ADD = "2031201";//销售机会添加
        public static string MODULE_ID_SELLCHANCE_INFO = "2031202";//销售机会列表
        public static string CODING_RULE_TABLE_SELLCHANCE = "SellChance，SellChancePush";//销售机会表

        //销售报价ModelID
        public static string MODULE_ID_SELLOFFER_ADD = "2031301";//销售报价添加
        public static string MODULE_ID_SELLOFFER_INFO = "2031302";//销售报价列表
        public static string CODING_RULE_TABLE_SELLOFFER = "SellOffer，SellOfferDetail，SellOfferHistory";//销售报价表

        //销售合同ModelID
        public static string MODULE_ID_SELLCONTRANCT_ADD = "2031401";//销售合同添加
        public static string MODULE_ID_SELLCONTRANCT_INFO = "2031402";//销售合同列表
        public static string CODING_RULE_TABLE_SELLCONTRANCT = "SellContract，SellContractDetail";//销售合同表

        //销售订单ModelID
        public static string MODULE_ID_SELLORDER_ADD = "2031501";//销售订单添加
        public static string MODULE_ID_SELLORDER_INFO = "2031502";//销售订单列表
        public static string CODING_RULE_TABLE_SELLORDER = "SellOrder，SellOrderDetail，SellOrderFeeDetail";//销售订单表

        //销售发货ModelID
        public static string MODULE_ID_SELLSEND_ADD = "2031601";//销售发货添加
        public static string MODULE_ID_SELLSEND_INFO = "2031602";//销售发货列表
        public static string CODING_RULE_TABLE_SELLSEND = "SellSend，SellSendDetail";//销售发货表
        public static string MODULE_ID_SELLSENDDETAIL_LIST = "2031603";//销售发货明细列表

        //回款计划ModelID
        public static string MODULE_ID_GATHERING_ADD = "2031701";//回款计划添加
        public static string MODULE_ID_GATHERING_INFO = "2031702";//回款计划列表
        public static string CODING_RULE_TABLE_GATHERING = "SellGathering";//回款计划表

        //销售退货ModelID
        public static string MODULE_ID_SELLBACK_ADD = "2031801";//销售退货添加
        public static string MODULE_ID_SELLBACK_INFO = "2031802";//销售退货列表
        public static string CODING_RULE_TABLE_SELLBACK = "SellBack，SellBackDetail";//销售退货表


        //委托代销ModelID
        public static string MODULE_ID_SELLCHANNELSTTL_ADD = "2031901";//委托代销添加
        public static string MODULE_ID_SELLCHANNELSTTL_INFO = "2031902";//委托代销列表
        public static string CODING_RULE_TABLE_SELLCHANNELSTTL = "SellChannelSttl，SellChannelSttlDetail";//委托代销表

        #endregion

        #region 销售类别单据编码规则
        public static string CODING_RULE_SELL = "5";//销售类别单据
        public static string CODING_RULE_SELLCHANCE_NO = "7";//销售机会编号
        public static string CODING_RULE_SELLPLAN_NO = "8";//销售计划编号
        public static string CODING_RULE_SELLOFFER_NO = "1";//销售报价单编号
        public static string CODING_RULE_SELLCONTRANCT_NO = "2";//销售合同编号

        public static string CODING_RULE_SELLORDER_NO = "3";//销售订单
        public static string CODING_RULE_SELLSEND_NO = "4";//销售发货单
        public static string CODING_RULE_SELLBACK_NO = "5";//销售退货单
        public static string CODING_RULE_SELLCHANNELSTTL_NO = "6";//分销结算单

        public static string CODING_RULE_GATHERING_NO = "10";//回款计划

        #endregion

        #region 门店管理
        #region 199 服务器
        ////门店管理MODELUID=217
        ////门店库存管理MODULEID=21711
        ////门店库存管理
        //public static string MODULE_ID_SUBSTOREMANAGER_SUBSTORAGEINIT = "2171101";//门店分店期初库存录入
        //public static string MODULE_ID_SUBSTOREMANAGER_SUBSTORAGELIST = "2171102";//门店库存查询
        //public static string MODULE_ID_SUBSTOREMANAGER_STORAGELIST = "2071103";//总部库存查询
        //public static string MODULE_ID_SUBSTOREMANAGER_SUBSTORAGEINITLIST = "2171104";//门店分店期初库存列表
        //public static string CODING_RULE_TABLE_SUBSTOREMANAGER_SUBSTORAGEIN = "SubStorageIn";//分店入库单表

        //public static string MODULE_ID_SUBSTOREMANAGER_SUBSELLBACKADD = "2171301";//新建门店销售退货单
        //public static string MODULE_ID_SUBSTOREMANAGER_SUBSELLBACKLIST = "2171302";//门店销售退货单列表
        //public static string CODING_RULE_TABLE_SUBSTOREMANAGER_SUBSELLBACK = "SubSellBack";//分店销售退货单表

        //public static string MODULE_ID_SUBSTOREMANAGER_SELLORDERADD = "2171201";//门店销售订单新建
        //public static string MODULE_ID_SUBSTOREMANAGER_SELLORDERLIST = "2171201";//门店销售订单列表



        #endregion

        #region 123 服务器
        //门店管理MODELUID=212
        //门店库存管理MODULEID=21211
        //门店库存管理
        public static string MODULE_ID_SUBSTOREMANAGER_SUBSTORAGEINIT = "2121101";//门店分店期初库存录入
        public static string MODULE_ID_SUBSTOREMANAGER_SUBSTORAGELIST = "2121103";//门店库存查询
        public static string MODULE_ID_SUBSTOREMANAGER_STORAGELIST = "2021104";//总部库存查询
        public static string MODULE_ID_STORAGE_SubDayEnd = "2121105";//门店日结
        public static string MODULE_ID_SUBSTOREMANAGER_SUBSTORAGEINITLIST = "2121102";//门店分店期初库存列表
        public static string CODING_RULE_TABLE_SUBSTOREMANAGER_SUBSTORAGEIN = "SubStorageIn";//分店入库单表

        public static string MODULE_ID_SUBSTOREMANAGER_SUBSELLBACKADD = "2121203";//新建门店销售退货单
        public static string MODULE_ID_SUBSTOREMANAGER_SUBSELLBACKLIST = "2121204";//门店销售退货单列表
        public static string CODING_RULE_TABLE_SUBSTOREMANAGER_SUBSELLBACK = "SubSellBack";//分店销售退货单表

        public static string MODULE_ID_SUBSTOREMANAGER_SELLORDERADD = "2121201";//门店销售订单新建
        public static string MODULE_ID_SUBSTOREMANAGER_SELLORDERLIST = "2121202";//门店销售订单列表
        public static string CODING_RULE_TABLE_SUBSTOREMANAGER_SUBSELLORDER = "SubSellOrder";//分店销售退货单表
        #endregion

        #region 门店相关编码规则
        public static string CODING_RULE_SUBSTORE = "11";//门店分类
        public static string CODING_RULE_SUBSTORE_SUBSTORAGEIN = "1";//门店入库单
        public static string CODING_RULE_SUBSTORE_SUBSELLORDER = "2";//销售订单
        public static string CODING_RULE_SUBSTORE_SUBSELLBACK = "3";//销售退货单
        #endregion

        #endregion

        #region 技术管理--预算控制 ModuleID
        //分项预算概要ModuleID
        //public static string MODULE_ID_SUBBUDGETADD = "2200301";//分项预算概要编辑页面(开发库中2200301)
        //public static string MODULE_ID_SUBBUDGETLIST = "2200302";//分项预算概要列表(开发库中2200302)
        public static string MODULE_ID_SUBBUDGETADD = "2210401";//分项预算概要编辑页面(开发库中2200301)
        public static string MODULE_ID_SUBBUDGETLIST = "2210402";//分项预算概要列表(开发库中2200302)

        //项目摘要
        //public static string MODULE_ID_PROJECTBUDGETADD = "2200303";//项目摘要编辑页面
        //public static string MODULE_ID_PROJECTBUDGETLIST = "2200304";//项目摘要列表页面
        public static string MODULE_ID_PROJECTBUDGETADD = "2210403";//项目摘要编辑页面
        public static string MODULE_ID_PROJECTBUDGETLIST = "2210404";//项目摘要列表页面

        //项目成本ModuleID
        //public static string MODULE_ID_BUDGETPRICEADD = "2200305";//项目成本编辑页面(开发库中2200305)
        //public static string MODULE_ID_BUDGETPRICELIST = "2200306";//项目成本列表(开发库中2200306)
        public static string MODULE_ID_BUDGETPRICEADD = "2210405";//项目成本编辑页面(开发库中2200305)
        public static string MODULE_ID_BUDGETPRICELIST = "2210406";//项目成本列表(开发库中2200306)

        //已预算项目表
        //public static string MODULE_ID_PROJECTBUDGETPRICEADD = "2200307";//已预算项目表
        //public static string MODULE_ID_PROJECTBUDGETPRICELIST = "2200308";//已预算项目表列表
        public static string MODULE_ID_PROJECTBUDGETPRICEADD = "2210407";//已预算项目表
        public static string MODULE_ID_PROJECTBUDGETPRICELIST = "2210408";//已预算项目表列表

        #endregion
        #region 技术管理--预算控制--项目预算表 TypeFlag,TypeCode
        public static string CODING_RULE_PROJECT = "13";//审批流程中用到flag
        public static string CODING_RULE_PROJECT_NO = "2";//审批流程中用到flagcode
        #endregion

        #region 初始化向导
        public static string MODULE_ID_INIT_SYSTEM = "21915";//系统管理初始化向导
        public static string MODULE_ID_INIT_HUMAN = "20120";//人力资源初始化向导
        #endregion

    }
}
