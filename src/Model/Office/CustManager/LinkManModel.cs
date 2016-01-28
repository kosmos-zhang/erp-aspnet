/**********************************************
 * 类作用：   CustLinkMan表数据模板
 * 建立人：   张玉圆
 * 建立时间： 2009/03/10
 ***********************************************/

using System;

namespace XBase.Model.Office.CustManager
{
    public class LinkManModel
    {
        #region Model
        private int _id;
        private string _companycd;
        private string _custno;
        private string _linkmanname;
        private string _sex;
        private string _important;
        private string _company;
        private string _appellation;
        private string _department;
        private string _position;
        private string _operation;
        private string _worktel;
        private string _fax;
        private string _handset;
        private string _mailaddress;
        private string _hometel;
        private string _msn;
        private string _qq;
        private string _post;
        private string _homeaddress;
        private string _remark;
        private string _age;
        private string _likes;
        private int _linktype;
        private DateTime? _birthday;
        private string _papertype;
        private string _papernum;
        private string _photo;
        private DateTime? _modifieddate;
        private string _modifieduserid;
        private string _canviewuser;
        private string _canviewusername;
        private string _creator;
        private string _createddate;

        private string _hometown;
        private string _nationalid;
        private string _birthcity;
        private string _culturelevel;
        private string _professional;
        private string _graduateschool;
        private string _incomeyear;
        private string _fuooddrink;
        private string _lovemusic;
        private string _lovecolor;
        private string _lovesmoke;
        private string _lovedrink;
        private string _lovetea;
        private string _lovebook;
        private string _lovesport;
        private string _loveclothes;
        private string _cosmetic;
        private string _nature;
        private string _appearance;
        private string _adoutbody;
        private string _aboutfamily;
        private string _car;
        private string _livehouse;
        private string _professionaldes;

        /// <summary>
        /// 专业描述
        /// </summary>
        public string ProfessionalDes
        {
            set { _professionaldes = value; }
            get { return _professionaldes; }
        }
        /// <summary>
        /// 创建者ID
        /// </summary>
        public string Creator
        {
            set { _creator = value; }
            get { return _creator; }
        }
        /// <summary>
        /// 创建日期
        /// </summary>
        public string CreatedDate
        {
            set { _createddate = value; }
            get { return _createddate; }
        }
        /// <summary>
        /// 可查看该客户档案的人员ID
        /// </summary>
        public string CanViewUser
        {
            set { _canviewuser = value; }
            get { return _canviewuser; }
        }
        /// <summary>
        /// 可查看该客户档案的人员姓名
        /// </summary>
        public string CanViewUserName
        {
            set { _canviewusername = value; }
            get { return _canviewusername; }
        }
        /// <summary>                                               
        /// 联系人ID，自动生成                                      
        /// </summary>                                              
        public int ID
        {
            set { _id = value; }
            get { return _id; }
        }
        /// <summary>                                               
        /// 公司代码                                                
        /// </summary>                                              
        public string CompanyCD
        {
            set { _companycd = value; }
            get { return _companycd; }
        }
        /// <summary>                                               
        /// 客户编号（对应客户信息表中的客户编号）
        /// </summary>                                              
        public string CustNo
        {
            set { _custno = value; }
            get { return _custno; }
        }
        /// <summary>                                               
        /// 联系人姓名                                              
        /// </summary>                                              
        public string LinkManName
        {
            set { _linkmanname = value; }
            get { return _linkmanname; }
        }
        /// <summary>                                               
        /// 性别（1男2女）                                          
        /// </summary>                                              
        public string Sex
        {
            set { _sex = value; }
            get { return _sex; }
        }
        /// <summary>                                               
        /// 重要程度                                                
        /// </summary>                                              
        public string Important
        {
            set { _important = value; }
            get { return _important; }
        }
        /// <summary>                                               
        /// 单位                                                    
        /// </summary>                                              
        public string Company
        {
            set { _company = value; }
            get { return _company; }
        }
        /// <summary>                                               
        /// 称谓                                                    
        /// </summary>                                              
        public string Appellation
        {
            set { _appellation = value; }
            get { return _appellation; }
        }
        /// <summary>                                               
        /// 部门                                                    
        /// </summary>                                              
        public string Department
        {
            set { _department = value; }
            get { return _department; }
        }
        /// <summary>                                               
        /// 职务                                                    
        /// </summary>                                              
        public string Position
        {
            set { _position = value; }
            get { return _position; }
        }
        /// <summary>                                               
        /// 负责业务                                                
        /// </summary>                                              
        public string Operation
        {
            set { _operation = value; }
            get { return _operation; }
        }
        /// <summary>                                               
        /// 工作电话                                                
        /// </summary>                                              
        public string WorkTel
        {
            set { _worktel = value; }
            get { return _worktel; }
        }
        /// <summary>                                               
        /// 传真                                                    
        /// </summary>                                              
        public string Fax
        {
            set { _fax = value; }
            get { return _fax; }
        }
        /// <summary>                                               
        /// 移动电话                                                
        /// </summary>                                              
        public string Handset
        {
            set { _handset = value; }
            get { return _handset; }
        }
        /// <summary>                                               
        /// 邮件地址                                                
        /// </summary>                                              
        public string MailAddress
        {
            set { _mailaddress = value; }
            get { return _mailaddress; }
        }
        /// <summary>                                               
        /// 家庭电话                                                
        /// </summary>                                              
        public string HomeTel
        {
            set { _hometel = value; }
            get { return _hometel; }
        }
        /// <summary>                                               
        /// MSN                                                     
        /// </summary>                                              
        public string MSN
        {
            set { _msn = value; }
            get { return _msn; }
        }
        /// <summary>                                               
        /// QQ                                                      
        /// </summary>                                              
        public string QQ
        {
            set { _qq = value; }
            get { return _qq; }
        }
        /// <summary>                                               
        /// 邮编                                                    
        /// </summary>                                              
        public string Post
        {
            set { _post = value; }
            get { return _post; }
        }
        /// <summary>                                               
        /// 住址                                                    
        /// </summary>                                              
        public string HomeAddress
        {
            set { _homeaddress = value; }
            get { return _homeaddress; }
        }
        /// <summary>                                               
        /// 备注                                                    
        /// </summary>                                              
        public string Remark
        {
            set { _remark = value; }
            get { return _remark; }
        }
        /// <summary>                                               
        /// 年龄                                                    
        /// </summary>                                              
        public string Age
        {
            set { _age = value; }
            get { return _age; }
        }
        /// <summary>                                               
        /// 爱好                                                    
        /// </summary>                                              
        public string Likes
        {
            set { _likes = value; }
            get { return _likes; }
        }
        /// <summary>                                               
        /// 联系人类型ID                                            
        /// </summary>                                              
        public int LinkType
        {
            set { _linktype = value; }
            get { return _linktype; }
        }
        /// <summary>                                               
        /// 生日                                                    
        /// </summary>                                              
        public DateTime? Birthday
        {
            set { _birthday = value; }
            get { return _birthday; }
        }
        /// <summary>                                               
        /// 证件类型                                                
        /// </summary>                                              
        public string PaperType
        {
            set { _papertype = value; }
            get { return _papertype; }
        }
        /// <summary>                                               
        /// 证件号                                                  
        /// </summary>                                              
        public string PaperNum
        {
            set { _papernum = value; }
            get { return _papernum; }
        }
        /// <summary>                                               
        /// 照片                                                    
        /// </summary>                                              
        public string Photo
        {
            set { _photo = value; }
            get { return _photo; }
        }
        /// <summary>                                               
        /// 最后更新日期                                            
        /// </summary>                                              
        public DateTime? ModifiedDate
        {
            set { _modifieddate = value; }
            get { return _modifieddate; }
        }
        /// <summary>                                               
        /// 最后更新用户ID                                          
        /// </summary>                                              
        public string ModifiedUserID
        {
            set { _modifieduserid = value; }
            get { return _modifieduserid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string HomeTown
        {
            set { _hometown = value; }
            get { return _hometown; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string NationalID
        {
            set { _nationalid = value; }
            get { return _nationalid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string birthcity
        {
            set { _birthcity = value; }
            get { return _birthcity; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string CultureLevel
        {
            set { _culturelevel = value; }
            get { return _culturelevel; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Professional
        {
            set { _professional = value; }
            get { return _professional; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string GraduateSchool
        {
            set { _graduateschool = value; }
            get { return _graduateschool; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string IncomeYear
        {
            set { _incomeyear = value; }
            get { return _incomeyear; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string FuoodDrink
        {
            set { _fuooddrink = value; }
            get { return _fuooddrink; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string LoveMusic
        {
            set { _lovemusic = value; }
            get { return _lovemusic; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string LoveColor
        {
            set { _lovecolor = value; }
            get { return _lovecolor; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string LoveSmoke
        {
            set { _lovesmoke = value; }
            get { return _lovesmoke; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string LoveDrink
        {
            set { _lovedrink = value; }
            get { return _lovedrink; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string LoveTea
        {
            set { _lovetea = value; }
            get { return _lovetea; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string LoveBook
        {
            set { _lovebook = value; }
            get { return _lovebook; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string LoveSport
        {
            set { _lovesport = value; }
            get { return _lovesport; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string LoveClothes
        {
            set { _loveclothes = value; }
            get { return _loveclothes; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Cosmetic
        {
            set { _cosmetic = value; }
            get { return _cosmetic; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Nature
        {
            set { _nature = value; }
            get { return _nature; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Appearance
        {
            set { _appearance = value; }
            get { return _appearance; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string AdoutBody
        {
            set { _adoutbody = value; }
            get { return _adoutbody; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string AboutFamily
        {
            set { _aboutfamily = value; }
            get { return _aboutfamily; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Car
        {
            set { _car = value; }
            get { return _car; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string LiveHouse
        {
            set { _livehouse = value; }
            get { return _livehouse; }
        }
        #endregion Model
    }
}
