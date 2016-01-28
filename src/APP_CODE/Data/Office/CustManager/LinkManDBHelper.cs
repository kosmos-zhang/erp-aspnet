using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using XBase.Model.Office.CustManager;
using XBase.Data.DBHelper;
using System.Data.SqlTypes;

namespace XBase.Data.Office.CustManager
{
    public class LinkManDBHelper
    {
        /// <summary>
        /// 获取联系人列表信息
        /// </summary>
        /// <param name="companyCD">companyCD</param>
        /// <returns></returns>
        public static DataTable GetLinkManListEx(string companyCD)
        {
            string sql = "select ";
            sql += "a.ID,isnull(b.custno,' ') as custno,";
            sql += "isnull(b.CustName,' ') as CustNam,";
            sql += "isnull(a.LinkManName,' ') as LinkManName,";
            sql += "isnull(a.Sex,'') as Sex,";
            sql += "isnull(a.Handset,' ') as Handset";
            sql += " from officedba.CustLinkMan as a";
            sql += " left join officedba.CustInfo as b on a.custno = b.custno and a.companycd = b.companycd";
            sql += " where a.companycd = @companycd";

            SqlParameter[] parameters = {
					new SqlParameter("@companycd", SqlDbType.Char,6)};
            parameters[0].Value = companyCD;

            return SqlHelper.ExecuteSql(sql, parameters);
        }

        #region 添加客户联系人的方法
        /// <summary>
        /// 添加客户联系人的方法
        /// </summary>
        /// <param name="LinkManModel">联系人信息</param>
        /// <returns>bool值</returns>
        public static int LinkManAdd(LinkManModel LinkManModel)
        {
            try
            {
                #region 拼写SQL语句
                StringBuilder sql = new StringBuilder();
                sql.AppendLine("INSERT INTO officedba.CustLinkMan");
                sql.AppendLine("(CompanyCD");
                sql.AppendLine(",CustNo     ");
                sql.AppendLine(",LinkManName");
                sql.AppendLine(",Sex        ");
                sql.AppendLine(",Important  ");
                sql.AppendLine(",Company    ");
                sql.AppendLine(",Appellation");
                sql.AppendLine(",Department ");
                sql.AppendLine(",Position   ");
                sql.AppendLine(",Operation  ");
                sql.AppendLine(",WorkTel    ");
                sql.AppendLine(",Fax        ");
                sql.AppendLine(",Handset    ");
                sql.AppendLine(",MailAddress");
                sql.AppendLine(",HomeTel    ");
                sql.AppendLine(",MSN        ");
                sql.AppendLine(",QQ         ");
                sql.AppendLine(",Post       ");
                sql.AppendLine(",HomeAddress");
                sql.AppendLine(",Remark     ");
                sql.AppendLine(",Age        ");
                sql.AppendLine(",Likes      ");
                sql.AppendLine(",LinkType   ");
                sql.AppendLine(",Birthday   ");
                sql.AppendLine(",PaperType  ");
                sql.AppendLine(",PaperNum   ");
                sql.AppendLine(",Photo");
                sql.AppendLine(",Creator     ");
                sql.AppendLine(",CreatedDate     ");
                sql.AppendLine(",CanViewUser     ");
                sql.AppendLine(",CanViewUserName     ");
                sql.AppendLine(",ModifiedDate");

                sql.AppendLine(",HomeTown");
                if (!string.IsNullOrEmpty(LinkManModel.NationalID))
                {
                    sql.AppendLine(",NationalID");
                }
                sql.AppendLine(",Birthcity");
                if (!string.IsNullOrEmpty(LinkManModel.CultureLevel))
                {
                    sql.AppendLine(",CultureLevel");
                }
                if (!string.IsNullOrEmpty(LinkManModel.Professional))
                {
                    sql.AppendLine(",Professional");
                }
                sql.AppendLine(",GraduateSchool");
                sql.AppendLine(",IncomeYear");
                sql.AppendLine(",FuoodDrink");
                sql.AppendLine(",LoveMusic");
                sql.AppendLine(",LoveColor");
                sql.AppendLine(",LoveSmoke");
                sql.AppendLine(",LoveDrink");
                sql.AppendLine(",LoveTea");
                sql.AppendLine(",LoveBook");
                sql.AppendLine(",LoveSport");
                sql.AppendLine(",LoveClothes");
                sql.AppendLine(",Cosmetic");
                sql.AppendLine(",Nature");
                sql.AppendLine(",Appearance");
                sql.AppendLine(",AdoutBody");
                sql.AppendLine(",AboutFamily");
                sql.AppendLine(",Car");
                sql.AppendLine(",LiveHouse");
                sql.AppendLine(",ProfessionalDes");

                sql.AppendLine(",ModifiedUserID)");
                sql.AppendLine(" values ");
                sql.AppendLine("(@CompanyCD");
                sql.AppendLine(",@CustNo     ");
                sql.AppendLine(",@LinkManName");
                sql.AppendLine(",@Sex        ");
                sql.AppendLine(",@Important  ");
                sql.AppendLine(",@Company    ");
                sql.AppendLine(",@Appellation");
                sql.AppendLine(",@Department ");
                sql.AppendLine(",@Position   ");
                sql.AppendLine(",@Operation  ");
                sql.AppendLine(",@WorkTel    ");
                sql.AppendLine(",@Fax        ");
                sql.AppendLine(",@Handset    ");
                sql.AppendLine(",@MailAddress");
                sql.AppendLine(",@HomeTel    ");
                sql.AppendLine(",@MSN        ");
                sql.AppendLine(",@QQ         ");
                sql.AppendLine(",@Post       ");
                sql.AppendLine(",@HomeAddress");
                sql.AppendLine(",@Remark     ");
                sql.AppendLine(",@Age        ");
                sql.AppendLine(",@Likes      ");
                sql.AppendLine(",@LinkType   ");
                sql.AppendLine(",@Birthday   ");
                sql.AppendLine(",@PaperType  ");
                sql.AppendLine(",@PaperNum   ");
                sql.AppendLine(",@Photo");
                sql.AppendLine(",@Creator     ");
                sql.AppendLine(",@CreatedDate     ");
                sql.AppendLine(",@CanViewUser     ");
                sql.AppendLine(",@CanViewUserName     ");
                sql.AppendLine(",@ModifiedDate");
                sql.AppendLine(",@HomeTown");
                if (!string.IsNullOrEmpty(LinkManModel.NationalID))
                {
                    sql.AppendLine(",@NationalID");
                }
                sql.AppendLine(",@Birthcity");
                if (!string.IsNullOrEmpty(LinkManModel.CultureLevel))
                {
                    sql.AppendLine(",@CultureLevel");
                }
                if (!string.IsNullOrEmpty(LinkManModel.Professional))
                {
                    sql.AppendLine(",@Professional");
                }
                sql.AppendLine(",@GraduateSchool");
                sql.AppendLine(",@IncomeYear");
                sql.AppendLine(",@FuoodDrink");
                sql.AppendLine(",@LoveMusic");
                sql.AppendLine(",@LoveColor");
                sql.AppendLine(",@LoveSmoke");
                sql.AppendLine(",@LoveDrink");
                sql.AppendLine(",@LoveTea");
                sql.AppendLine(",@LoveBook");
                sql.AppendLine(",@LoveSport");
                sql.AppendLine(",@LoveClothes");
                sql.AppendLine(",@Cosmetic");
                sql.AppendLine(",@Nature");
                sql.AppendLine(",@Appearance");
                sql.AppendLine(",@AdoutBody");
                sql.AppendLine(",@AboutFamily");
                sql.AppendLine(",@Car");
                sql.AppendLine(",@LiveHouse");
                sql.AppendLine(",@ProfessionalDes");
                sql.AppendLine(",@ModifiedUserID)");

                #endregion

                #region 设置参数
                SqlParameter[] param = new SqlParameter[58];
                param[0] = SqlHelper.GetParameter("@CompanyCD", LinkManModel.CompanyCD);
                param[1] = SqlHelper.GetParameter("@CustNo", LinkManModel.CustNo);
                param[2] = SqlHelper.GetParameter("@LinkManName", LinkManModel.LinkManName);
                param[3] = SqlHelper.GetParameter("@Sex", LinkManModel.Sex);
                param[4] = SqlHelper.GetParameter("@Important", LinkManModel.Important);
                param[5] = SqlHelper.GetParameter("@Company", LinkManModel.Company);
                param[6] = SqlHelper.GetParameter("@Appellation", LinkManModel.Appellation);
                param[7] = SqlHelper.GetParameter("@Department", LinkManModel.Department);
                param[8] = SqlHelper.GetParameter("@Position", LinkManModel.Position);
                param[9] = SqlHelper.GetParameter("@Operation", LinkManModel.Operation);
                param[10] = SqlHelper.GetParameter("@WorkTel", LinkManModel.WorkTel);
                param[11] = SqlHelper.GetParameter("@Fax", LinkManModel.Fax);
                param[12] = SqlHelper.GetParameter("@Handset", LinkManModel.Handset);
                param[13] = SqlHelper.GetParameter("@MailAddress", LinkManModel.MailAddress);
                param[14] = SqlHelper.GetParameter("@HomeTel", LinkManModel.HomeTel);
                param[15] = SqlHelper.GetParameter("@MSN", LinkManModel.MSN);
                param[16] = SqlHelper.GetParameter("@QQ", LinkManModel.QQ);
                param[17] = SqlHelper.GetParameter("@Post", LinkManModel.Post);
                param[18] = SqlHelper.GetParameter("@HomeAddress", LinkManModel.HomeAddress);
                param[19] = SqlHelper.GetParameter("@Remark", LinkManModel.Remark);
                param[20] = SqlHelper.GetParameter("@Age", LinkManModel.Age);
                param[21] = SqlHelper.GetParameter("@Likes", LinkManModel.Likes);
                param[22] = SqlHelper.GetParameter("@LinkType", LinkManModel.LinkType);
                param[23] = SqlHelper.GetParameter("@Birthday", LinkManModel.Birthday == null
                                       ? SqlDateTime.Null
                                       : SqlDateTime.Parse(LinkManModel.Birthday.ToString()));
                param[24] = SqlHelper.GetParameter("@PaperType", LinkManModel.PaperType);
                param[25] = SqlHelper.GetParameter("@PaperNum", LinkManModel.PaperNum);
                param[26] = SqlHelper.GetParameter("@Photo", LinkManModel.Photo);
                param[27] = SqlHelper.GetParameter("@ModifiedDate", LinkManModel.ModifiedDate);
                param[28] = SqlHelper.GetParameter("@ModifiedUserID", LinkManModel.ModifiedUserID);
                param[29] = SqlHelper.GetParameter("@CanViewUser", LinkManModel.CanViewUser);
                param[30] = SqlHelper.GetParameter("@CanViewUserName", LinkManModel.CanViewUserName);
                param[31] = SqlHelper.GetParameter("@Creator", LinkManModel.Creator);
                param[32] = SqlHelper.GetParameter("@CreatedDate", LinkManModel.CreatedDate);
                param[33] = SqlHelper.GetParameter("@HomeTown", LinkManModel.HomeTown);
                param[34] = SqlHelper.GetParameter("@NationalID", LinkManModel.NationalID == null ? SqlInt32.Null : int.Parse(LinkManModel.NationalID));
                param[35] = SqlHelper.GetParameter("@Birthcity", LinkManModel.birthcity);
                param[36] = SqlHelper.GetParameter("@CultureLevel", LinkManModel.CultureLevel == null ? SqlInt32.Null : int.Parse(LinkManModel.CultureLevel));
                param[37] = SqlHelper.GetParameter("@Professional", LinkManModel.Professional == null ? SqlInt32.Null : int.Parse(LinkManModel.Professional));
                param[38] = SqlHelper.GetParameter("@GraduateSchool", LinkManModel.GraduateSchool);
                param[39] = SqlHelper.GetParameter("@IncomeYear", LinkManModel.IncomeYear);
                param[40] = SqlHelper.GetParameter("@FuoodDrink", LinkManModel.FuoodDrink);
                param[41] = SqlHelper.GetParameter("@LoveMusic", LinkManModel.LoveMusic);

                param[42] = SqlHelper.GetParameter("@LoveColor", LinkManModel.LoveColor);
                param[43] = SqlHelper.GetParameter("@LoveSmoke", LinkManModel.LoveSmoke);
                param[44] = SqlHelper.GetParameter("@LoveDrink", LinkManModel.LoveDrink);
                param[45] = SqlHelper.GetParameter("@LoveTea", LinkManModel.LoveTea);

                param[46] = SqlHelper.GetParameter("@LoveBook", LinkManModel.LoveBook);
                param[47] = SqlHelper.GetParameter("@LoveSport", LinkManModel.LoveSport);
                param[48] = SqlHelper.GetParameter("@LoveClothes", LinkManModel.LoveClothes);
                param[49] = SqlHelper.GetParameter("@Cosmetic", LinkManModel.Cosmetic);

                param[50] = SqlHelper.GetParameter("@Nature", LinkManModel.Nature);
                param[51] = SqlHelper.GetParameter("@Appearance", LinkManModel.Appearance);
                param[52] = SqlHelper.GetParameter("@AdoutBody", LinkManModel.AdoutBody);
                param[53] = SqlHelper.GetParameter("@AboutFamily", LinkManModel.AboutFamily);
                param[54] = SqlHelper.GetParameter("@Car", LinkManModel.Car);
                param[55] = SqlHelper.GetParameter("@LiveHouse", LinkManModel.LiveHouse);
                param[56] = SqlHelper.GetParameter("@ProfessionalDes", LinkManModel.ProfessionalDes);

                #endregion

                SqlParameter paramid = new SqlParameter("@id", SqlDbType.Int);
                paramid.Direction = ParameterDirection.Output;
                param[57] = paramid;

                //创建命令
                SqlCommand comm = new SqlCommand();
                SqlHelper.ExecuteTransStoredProcedure("officedba.insertCustLinkMan", comm, param);
                int contantid = Convert.ToInt32(comm.Parameters["@id"].Value);

                return contantid;
            }
            catch (Exception ex)
            {
                string sss = ex.Message;
                return 0;
            }
        }
        #endregion

        #region 获取客户联系人类型的方法
        /// <summary>
        /// 获取客户联系人类型的方法
        /// </summary>
        /// <param name="CompanyCD">公司代码</param>
        /// <returns>客户联系人类型结果集</returns>
        public static DataTable GetLinkManLinkType(string CompanyCD)
        {
            try
            {
                string sql = "select " +
                                    "id,TypeName" +
                              " from " +
                                    "officedba.CodePublicType" +
                              " where " +
                                    "TypeFlag = 4 and TypeCode = 6 and CompanyCD = @CompanyCD and UsedStatus = 1";
                SqlParameter[] param = new SqlParameter[1];
                param[0] = SqlHelper.GetParameter("@CompanyCD", CompanyCD);
                return SqlHelper.ExecuteSql(sql, param);
            }
            catch 
            {
                return null;
            }
        }
        #endregion

        #region 根据联系人ID获取联系人信息的方法
        /// <summary>
        /// 根据联系人ID获取联系人信息的方法
        /// </summary>
        /// <param name="CompanyCD">公司代码</param>
        /// <param name="linkid">联系人ID</param>
        /// <returns></returns>
        public static DataTable GetLinkInfoByID(string CompanyCD, int linkid)
        {
            try
            {
                string sql = "SELECT cl.id    ," +
                                "cl.CustNo       ," +
                                "ci.CustName CustNam," +
                                "cl.CompanyCD    ," +
                                "cl.LinkManName  ," +
                                "cl.Sex          ," +
                                "cl.Important    ," +
                                "cl.Company      ," +
                                "cl.Appellation  ," +
                                "cl.Department   ," +
                                "cl.Position     ," +
                                "cl.Operation    ," +
                                "cl.WorkTel      ," +
                                "cl.Fax          ," +
                                "cl.Handset      ," +
                                "cl.MailAddress  ," +
                                "cl.HomeTel      ," +
                                "cl.MSN          ," +
                                "cl.QQ           ," +
                                "cl.Post         ," +
                                "cl.HomeAddress  ," +
                                "cl.Remark       ," +
                                "cl.Age          ," +
                                "cl.Likes        ," +
                                "cl.LinkType     ," +
                                 "  cl.HomeTown        ," +
                                 "  cl.NationalID      ," +
                                 "  cl.birthcity       ," +
                                 "  cl.CultureLevel    ," +
                                 "  cl.Professional    ," +
                                 "  cl.GraduateSchool  ," +
                                 "  cl.IncomeYear      ," +
                                 "  cl.FuoodDrink      ," +
                                 "  cl.LoveMusic       ," +
                                 "  cl.LoveColor       ," +
                                 "  cl.LoveSmoke       ," +
                                 "  cl.LoveDrink       ," +
                                 "  cl.LoveTea         ," +
                                 "  cl.LoveBook        ," +
                                 "  cl.LoveSport       ," +
                                 "  cl.LoveClothes     ," +
                                 "  cl.Cosmetic        ," +
                                 "  cl.Nature          ," +
                                 "  cl.Appearance      ," +
                                 "  cl.AdoutBody       ," +
                                 "  cl.AboutFamily     ," +
                                 "  cl.Car             ," +
                                 "  cl.LiveHouse       ," +
                                "CONVERT(varchar(100), cl.Birthday, 23) Birthday," +
                                "cl.PaperType    ," +
                                "cl.CanViewUser     ," +
                                "cl.CanViewUserName     ," +
                                "cl.Creator,ei.EmployeeName," +
                                "CONVERT(varchar(100), cl.CreatedDate , 23) CreatedDate ," +
                                "cl.PaperNum     ," +
                                "cl.Photo,cl.ProfessionalDes        " +
                          " from " +
                                "officedba.CustLinkMan cl left join officedba.CustInfo ci on cl.CustNo = ci.CustNo and cl.CompanyCD = ci.CompanyCD " +
                                "left join officedba.EmployeeInfo ei on ei.id = cl.Creator " +
                          " where " +
                                "cl.id=@id and cl.CompanyCD=@CompanyCD ";
                SqlParameter[] param = new SqlParameter[2];
                param[0] = SqlHelper.GetParameter("@CompanyCD", CompanyCD);
                param[1] = SqlHelper.GetParameter("@id", linkid);
                return SqlHelper.ExecuteSql(sql, param);
            }
            catch 
            {
                return null;
            }
        }
        #endregion

        #region 根据客户编号修改联系人信息的方法
        /// <summary>
        /// 根据客户编号修改联系人信息的方法
        /// </summary>
        /// <param name="LinkManM">联系人信息Model</param>
        /// <param name="CustNo">联系人对应客户编号</param>
        /// <returns>bool值</returns>
        public static bool UpdateLinkMan(LinkManModel LinkManM)
        {
            try
            {
                #region 拼写修改联系人信息SQL语句
                StringBuilder sqllink = new StringBuilder();
                sqllink.AppendLine("UPDATE officedba.CustLinkMan set ");
                sqllink.AppendLine("CompanyCD = @CompanyCD, ");
                sqllink.AppendLine("LinkManName = @LinkManName, ");
                sqllink.AppendLine("Sex = @Sex, ");
                sqllink.AppendLine("Important = @Important, ");
                sqllink.AppendLine("Company = @Company, ");
                sqllink.AppendLine("Appellation = @Appellation, ");
                sqllink.AppendLine("Department = @Department, ");
                sqllink.AppendLine("Position = @Position, ");
                sqllink.AppendLine("Operation = @Operation, ");
                sqllink.AppendLine("WorkTel = @WorkTel, ");
                sqllink.AppendLine("Fax = @Fax, ");
                sqllink.AppendLine("Handset = @Handset, ");
                sqllink.AppendLine("MailAddress = @MailAddress, ");
                sqllink.AppendLine("HomeTel = @HomeTel, ");
                sqllink.AppendLine("MSN = @MSN, ");
                sqllink.AppendLine("QQ = @QQ, ");
                sqllink.AppendLine("Post = @Post, ");
                sqllink.AppendLine("HomeAddress = @HomeAddress, ");
                sqllink.AppendLine("Remark = @Remark, ");
                sqllink.AppendLine("Age = @Age, ");
                sqllink.AppendLine("Likes = @Likes, ");
                sqllink.AppendLine("LinkType = @LinkType, ");
                sqllink.AppendLine("Birthday =@Birthday,");
                sqllink.AppendLine("PaperType = @PaperType, ");
                sqllink.AppendLine("PaperNum = @PaperNum, ");
                sqllink.AppendLine("Photo = @Photo, ");
                sqllink.AppendLine("CanViewUser = @CanViewUser, ");
                sqllink.AppendLine("CanViewUserName = @CanViewUserName, ");
                sqllink.AppendLine("ModifiedDate = @ModifiedDate, ");


                sqllink.AppendLine("HomeTown=@HomeTown,");
                if (!string.IsNullOrEmpty(LinkManM.NationalID))
                {
                    sqllink.AppendLine("NationalID=@NationalID,");
                }
                sqllink.AppendLine("Birthcity=@Birthcity,");
                if (!string.IsNullOrEmpty(LinkManM.CultureLevel))
                {
                    sqllink.AppendLine("CultureLevel=@CultureLevel,");
                }
                if (!string.IsNullOrEmpty(LinkManM.Professional))
                {
                    sqllink.AppendLine("Professional=@Professional,");
                }
                sqllink.AppendLine("GraduateSchool=@GraduateSchool,");
                sqllink.AppendLine("IncomeYear=@IncomeYear,");
                sqllink.AppendLine("FuoodDrink=@FuoodDrink,");
                sqllink.AppendLine("LoveMusic=@LoveMusic,");
                sqllink.AppendLine("LoveColor=@LoveColor,");
                sqllink.AppendLine("LoveSmoke=@LoveSmoke,");
                sqllink.AppendLine("LoveDrink=@LoveDrink,");
                sqllink.AppendLine("LoveTea=@LoveTea,");
                sqllink.AppendLine("LoveBook=@LoveBook,");
                sqllink.AppendLine("LoveSport=@LoveSport,");
                sqllink.AppendLine("LoveClothes=@LoveClothes,");
                sqllink.AppendLine("Cosmetic=@Cosmetic,");
                sqllink.AppendLine("Nature=@Nature,");
                sqllink.AppendLine("Appearance=@Appearance,");
                sqllink.AppendLine("AdoutBody=@AdoutBody,");
                sqllink.AppendLine("AboutFamily=@AboutFamily,");
                sqllink.AppendLine("Car=@Car,");
                sqllink.AppendLine("LiveHouse=@LiveHouse,");
                sqllink.AppendLine(" ProfessionalDes=@ProfessionalDes ");

                sqllink.AppendLine(" WHERE ");
                sqllink.AppendLine("ID = @ID ");
                sqllink.AppendLine("and CompanyCD = @CompanyCD ");
                #endregion

                #region 设置修改联系人信息参数
                SqlParameter[] param = new SqlParameter[55];
                param[0] = SqlHelper.GetParameter("@CompanyCD", LinkManM.CompanyCD);
                param[1] = SqlHelper.GetParameter("@ID", LinkManM.ID);
                param[2] = SqlHelper.GetParameter("@LinkManName", LinkManM.LinkManName);
                param[3] = SqlHelper.GetParameter("@Sex", LinkManM.Sex);
                param[4] = SqlHelper.GetParameter("@Important", LinkManM.Important);
                param[5] = SqlHelper.GetParameter("@Company", LinkManM.Company);
                param[6] = SqlHelper.GetParameter("@Appellation", LinkManM.Appellation);
                param[7] = SqlHelper.GetParameter("@Department", LinkManM.Department);
                param[8] = SqlHelper.GetParameter("@Position", LinkManM.Position);
                param[9] = SqlHelper.GetParameter("@Operation", LinkManM.Operation);
                param[10] = SqlHelper.GetParameter("@WorkTel", LinkManM.WorkTel);
                param[11] = SqlHelper.GetParameter("@Fax", LinkManM.Fax);
                param[12] = SqlHelper.GetParameter("@Handset", LinkManM.Handset);
                param[13] = SqlHelper.GetParameter("@MailAddress", LinkManM.MailAddress);
                param[14] = SqlHelper.GetParameter("@HomeTel", LinkManM.HomeTel);
                param[15] = SqlHelper.GetParameter("@MSN", LinkManM.MSN);
                param[16] = SqlHelper.GetParameter("@QQ", LinkManM.QQ);
                param[17] = SqlHelper.GetParameter("@Post", LinkManM.Post);
                param[18] = SqlHelper.GetParameter("@HomeAddress", LinkManM.HomeAddress);
                param[19] = SqlHelper.GetParameter("@Remark", LinkManM.Remark);
                param[20] = SqlHelper.GetParameter("@Age", LinkManM.Age);
                param[21] = SqlHelper.GetParameter("@Likes", LinkManM.Likes);
                param[22] = SqlHelper.GetParameter("@LinkType", LinkManM.LinkType);
                param[23] = SqlHelper.GetParameter("@Birthday", LinkManM.Birthday == null ? SqlDateTime.Null : SqlDateTime.Parse(LinkManM.Birthday.ToString()));
                //param[23] = SqlHelper.GetParameter("@Birthday", LinkManM.Birthday = DateTime.Now);
                param[24] = SqlHelper.GetParameter("@PaperType", LinkManM.PaperType);
                param[25] = SqlHelper.GetParameter("@PaperNum", LinkManM.PaperNum);
                param[26] = SqlHelper.GetParameter("@Photo", LinkManM.Photo);
                param[27] = SqlHelper.GetParameter("@ModifiedDate", LinkManM.ModifiedDate);
                param[28] = SqlHelper.GetParameter("@ModifiedUserID", LinkManM.ModifiedUserID);
                param[29] = SqlHelper.GetParameter("@CanViewUser", LinkManM.CanViewUser);
                param[30] = SqlHelper.GetParameter("@CanViewUserName", LinkManM.CanViewUserName);

                param[31] = SqlHelper.GetParameter("@HomeTown", LinkManM.HomeTown);

                param[32] = SqlHelper.GetParameter("@NationalID", LinkManM.NationalID == null ? SqlInt32.Null : int.Parse(LinkManM.NationalID));

                param[33] = SqlHelper.GetParameter("@Birthcity", LinkManM.birthcity == "" ? "" : LinkManM.birthcity);

                param[34] = SqlHelper.GetParameter("@CultureLevel", LinkManM.CultureLevel == null ? SqlInt32.Null : int.Parse(LinkManM.CultureLevel));

                param[35] = SqlHelper.GetParameter("@Professional", LinkManM.Professional == null ? SqlInt32.Null : int.Parse(LinkManM.Professional));

                param[36] = SqlHelper.GetParameter("@GraduateSchool", LinkManM.GraduateSchool);
                param[37] = SqlHelper.GetParameter("@IncomeYear", LinkManM.IncomeYear);
                param[38] = SqlHelper.GetParameter("@FuoodDrink", LinkManM.FuoodDrink);
                param[39] = SqlHelper.GetParameter("@LoveMusic", LinkManM.LoveMusic);

                param[40] = SqlHelper.GetParameter("@LoveColor", LinkManM.LoveColor);
                param[41] = SqlHelper.GetParameter("@LoveSmoke", LinkManM.LoveSmoke);
                param[42] = SqlHelper.GetParameter("@LoveDrink", LinkManM.LoveDrink);
                param[43] = SqlHelper.GetParameter("@LoveTea", LinkManM.LoveTea);

                param[44] = SqlHelper.GetParameter("@LoveBook", LinkManM.LoveBook);
                param[45] = SqlHelper.GetParameter("@LoveSport", LinkManM.LoveSport);
                param[46] = SqlHelper.GetParameter("@LoveClothes", LinkManM.LoveClothes);
                param[47] = SqlHelper.GetParameter("@Cosmetic", LinkManM.Cosmetic);

                param[48] = SqlHelper.GetParameter("@Nature", LinkManM.Nature);
                param[49] = SqlHelper.GetParameter("@Appearance", LinkManM.Appearance);
                param[50] = SqlHelper.GetParameter("@AdoutBody", LinkManM.AdoutBody);
                param[51] = SqlHelper.GetParameter("@AboutFamily", LinkManM.AboutFamily);
                param[52] = SqlHelper.GetParameter("@Car", LinkManM.Car);
                param[53] = SqlHelper.GetParameter("@LiveHouse", LinkManM.LiveHouse);
                param[54] = SqlHelper.GetParameter("@ProfessionalDes", LinkManM.ProfessionalDes);
                #endregion

                SqlHelper.ExecuteTransSql(sqllink.ToString(), param);
                return SqlHelper.Result.OprateCount > 0 ? true : false;

            }
            catch 
            {
                return false;
            }
        }
        #endregion

        #region 根据查询条件获取联系人列表信息的方法
        /// <summary>
        /// 根据查询条件获取联系人列表信息的方法
        /// </summary>
        /// <param name="LinkManM">查询条件</param>
        /// <param name="Manager">登陆人</param>
        /// <returns>联系人列表结果集</returns>
        public static DataTable GetLinkManInfoBycondition(string CustNam, LinkManModel LinkManM, string BeginDate, string EndDate, int pageIndex, int pageCount, string ord, ref int TotalCount)
        {
            try
            {
                string sql = "select " +
                                   "link.id,isnull(cust.id,'-1') custid,isnull(link.linkmanname,'') linkmanname,isnull(link.Appellation,'') Appellation," +
                                   "(case link.Important when '1' then '不重要' when '2' then '普通' when '3' then '重要' when '4' then '关键' else '' end) Important," +
                                   "isnull(cust.CustName,'') CustName,isnull(link.WorkTel,'') WorkTel,isnull(link.Handset,'') Handset," +
                                   "isnull(link.QQ,'') QQ,isnull(cp.TypeName,'') TypeName,Convert(varchar(100),link.Birthday,23) Birthday, " +
                                   " cust.CustBig,cust.CustNo,cust.CanViewUser,cust.Manager,cust.Creator " +
                              " from " +
                                  " officedba.custlinkman link" +
                                  " left join officedba.custinfo cust on cust.custno = link.custno and cust.companycd = link.companycd" +
                                  " left join officedba.CodePublicType cp on cp.id = link.LinkType " +
                              " where link.companycd = '" + LinkManM.CompanyCD + "'" +
                              " and (link.CanViewUser like '%" + "," + LinkManM.CanViewUser + "," + "%' or '" + LinkManM.CanViewUser + "' = link.Creator or link.CanViewUser = ',,')";

                //" and  cust.Manager = '"+ Manager +"'";
                if (CustNam != "")
                    sql += " and cust.id = '" + CustNam + "'";
                if (LinkManM.LinkManName != "")
                    sql += " and LinkManName like '%" + LinkManM.LinkManName + "%'";
                if (LinkManM.Handset != "")
                    sql += " and Handset like '%" + LinkManM.Handset + "%'";
                if (LinkManM.WorkTel != "")
                    sql += " and WorkTel like '%" + LinkManM.WorkTel + "%'";
                if (LinkManM.Important != "0")
                    sql += " and Important = '" + LinkManM.Important + "'";
                if (LinkManM.LinkType != 0)
                    sql += " and link.LinkType = '" + LinkManM.LinkType + "'";
                if (BeginDate.ToString() != "")
                    sql += " and link.Birthday >= '" + BeginDate.ToString() + "'";//sql += " and (link.Birthday >= '" + BeginDate.ToString() + "' or link.Birthday is null)";
                if (EndDate.ToString() != "")
                    sql += " and link.Birthday <= '" + EndDate.ToString() + "'";//sql += " and (link.Birthday <= '" + EndDate.ToString() + "' or link.Birthday is null)";
 
                return SqlHelper.CreateSqlByPageExcuteSql(sql.ToString(), pageIndex, pageCount, ord, null, ref TotalCount);
            }
            catch (Exception ex)
            {
                string sss = ex.Message;
                return null;
            }
        }
        #endregion

        #region 根据天数查询联系人生日
        /// <summary>
        /// 根据天数查询联系人生日
        /// </summary>
        /// <param name="Days"></param>
        /// <param name="CompanyCD"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageCount"></param>
        /// <param name="ord"></param>
        /// <param name="TotalCount"></param>
        /// <returns></returns>
        public static DataTable GetLinkRemind(string Days, string CompanyCD, int pageIndex, int pageCount, string ord, ref int TotalCount)
        {
            try
            {
                string sql = "select  " +
                                  " link.id,isnull(cust.id,'-1') custid,isnull(link.linkmanname,'') linkmanname,isnull(link.Appellation,'') Appellation, " +
                                  " (case link.Important when '1' then '不重要' when '2' then '普通' when '3' then '重要' when '4' then '关键' else '' end) Important, " +
                                  " isnull(cust.CustName,'') CustName,isnull(link.WorkTel,'') WorkTel,isnull(link.Handset,'') Handset, " +
                                  " cust.CustNo,cust.CustBig,cust.CanViewUser,cust.Manager,cust.Creator," +
                                  " isnull(link.QQ,'') QQ,isnull(cp.TypeName,'') TypeName,Convert(varchar(100),link.Birthday,23) Birthday" +
                                  " ,datediff(day,getdate(),Convert(varchar(100),(convert(varchar(4),Year(getdate()))+'-'+ " +
                                      " convert(varchar(2),Month(Birthday))+'-'+" +
                                      " convert(varchar(2),Day(Birthday))),23)) days " +
                              " from  " +
                                  " officedba.custlinkman link " +
                                  " left join officedba.custinfo cust on cust.custno = link.custno and cust.companycd = link.companycd " +
                                  " left join officedba.CodePublicType cp on cp.id = link.LinkType" +
                              " where datediff(day,getdate(),Convert(varchar(100),(convert(varchar(4),Year(getdate()))+'-'+ " +
                                      " convert(varchar(2),Month(Birthday))+'-'+" +
                                      " convert(varchar(2),Day(Birthday))),23)) <= '" + Days + "'" +
                              " and datediff(day,getdate(),Convert(varchar(100),(convert(varchar(4),Year(getdate()))+'-'+ " +
                                      " convert(varchar(2),Month(Birthday))+'-'+" +
                                      " convert(varchar(2),Day(Birthday))),23)) >= 0" +
                              " and link.companycd = '" + CompanyCD + "'";


                //if (Days != "")
                //    sql += " and datediff(day,Birthday,getdate()) = '" + Days + "'";

                return SqlHelper.CreateSqlByPageExcuteSql(sql.ToString(), pageIndex, pageCount, ord, null, ref TotalCount);
            }
            catch (Exception ex)
            {
                string sss = ex.Message;
                return null;
            }
        }
        #endregion

        #region 批量删除某张表中的记录
        /// <summary>
        /// 批量删除某张表中的记录
        /// </summary>
        /// <param name="LinkID"></param>
        /// <param name="TabelName">表名</param>
        /// <returns>操作记录数</returns>
        public static int DelLinkInfo(string[] LinkID, string TabelName)
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            string allLinkID = "";
            string[] Delsql = new string[1];

            if (LinkID.Length == 0)
            {
                return 0;
            }

            try
            {
                for (int i = 0; i < LinkID.Length; i++)
                {
                    LinkID[i] = "'" + LinkID[i] + "'";
                    sb.Append(LinkID[i]);
                }

                allLinkID = sb.ToString().Replace("''", "','");
                Delsql[0] = "delete from " + TabelName + " where id in (" + allLinkID + ")";

                SqlHelper.ExecuteTransForListWithSQL(Delsql);
                return SqlHelper.Result.OprateCount > 0 ? SqlHelper.Result.OprateCount : 0;
            }
            catch 
            {
                return 0;
            }
        }
        #endregion

        #region 根据客户编号获取联系人姓名
        /// <summary>
        /// 根据客户编号获取联系人姓名
        /// </summary>
        /// <param name="CompanyCD">公司代码</param>
        /// <param name="CustNo">客户编号</param>
        /// <returns>联系人列表</returns>
        public static DataTable GetLinkManByCustNo(string LinkManName, string Appellation, string Department, string CompanyCD, string CustNo)
        {
            try
            {
                string sql = "select " +
                                   "id,isnull(LinkManName,'') LinkManName,isnull(Appellation,'') Appellation,isnull(Department,'') Department " +
                               "from " +
                                   "officedba.CustLinkMan " +
                               "where " +
                                   "CustNo = @CustNo " +
                               "and " +
                                   "CompanyCD = @CompanyCD";
                if (LinkManName != "")
                    sql += " and LinkManName like '%" + LinkManName + "%'";
                if (Appellation != "")
                    sql += " and Appellation like '%" + Appellation + "%'";
                if (Department != "")
                    sql += " and Department like '%" + Department + "%'";

                SqlParameter[] param = new SqlParameter[2];
                param[0] = SqlHelper.GetParameter("@CustNo", CustNo);
                param[1] = SqlHelper.GetParameter("@CompanyCD", CompanyCD);

                return SqlHelper.ExecuteSql(sql, param);
            }
            catch 
            {
                return null;
            }
        }
        #endregion

        #region 根据联系人ID获取是否含有联系人的方法
        /// <summary>
        /// 根据联系人ID获取是否含有联系人的方法
        /// </summary>
        /// <param name="LinkManID"></param>
        /// <returns></returns>
        public static bool GetLinkManByID(string[] IDList)
        {
            if (IDList.Length == 0)
            {
                return false;
            }

            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            string allCustID = "";

            try
            {
                //string[] LinkManIDs = null;
                for (int i = 0; i < IDList.Length; i++)
                {
                    //LinkManIDs[i] = "'" + IDList[i] + "'";
                    sb.Append("'" + IDList[i] + "'");
                }

                allCustID = sb.ToString().Replace("''", "','");

                string sql = "select id from officedba.CustContact where CustLinkMan in (" + allCustID + ")";
                if (IsHave(sql))
                    return true;
                else
                {
                    string sql2 = "select id from officedba.CustTalk where CustLinkMan in (" + allCustID + ")";
                    if (IsHave(sql2))
                        return true;
                    else
                    {
                        string sql3 = "select id from officedba.CustLove where CustLinkMan in (" + allCustID + ")";
                        if (IsHave(sql3))
                            return true;
                        else
                            return false;
                    }
                }

            }
            catch 
            {
                return false;
            }
        }
        #endregion

        public static bool IsHave(string sql)
        {
            return SqlHelper.ExecuteSql(sql).Rows.Count > 0 ? true : false;
        }

        #region 联系人信息打印
        /// <summary>
        /// 联系人信息打印
        /// </summary>
        /// <param name="CompanyCD"></param>
        /// <param name="linkid"></param>
        /// <returns></returns>
        public static DataTable PrintLink(string CompanyCD, string linkid)
        {
            try
            {
                string sql = "SELECT cl.id    ," +
                                "cl.CustNo       ,ci.CustName,cl.CompanyCD    ," +
                                "cl.LinkManName  ,(case cl.Sex when '1' then '男' when '2' then '女' end) Sex," +
                                " (case cl.Important when '1' then '不重要' when '2' then '普通' when '3' then '重要' when '4' then '关键' end)Important," +
                                "cl.Company,cl.Appellation,cl.Department," +
                                "cl.Position,cl.Operation,cl.WorkTel," +
                                "cl.Fax          ,cl.Handset      ,cl.MailAddress  ," +
                                "cl.HomeTel      ,cl.MSN          ,cl.QQ           ," +
                                "cl.Post         ,cl.HomeAddress  ,cl.Remark       ," +
                                "cl.Age          ,cl.Likes        ," +
                                "cl.LinkType,cp.TypeName LinkTypaNm," +
                                "CONVERT(varchar(100), cl.Birthday, 23) Birthday," +
                                "cl.PaperType    ," +
                                "cl.CanViewUser     ," +
                                "cl.CanViewUserName     ," +
                                "cl.Creator,ei.EmployeeName," +
                                "CONVERT(varchar(100), cl.CreatedDate , 23) CreatedDate ," +
                                "cl.PaperNum     ," +
                                "cl.Photo        ," +

                                "cl.[HomeTown]        ," +
                                "(select a.TypeName from officedba.CodePublicType as a where a.ID=cl.Professional )  as [NationalID]      ," +
                                " cl.[birthcity]       ," +
                                "(select a1.TypeName from officedba.CodePublicType as a1  where  a1.ID=cl.CultureLevel ) as [CultureLevel]    ," +
                                "(select a2.TypeName from officedba.CodePublicType as a2  where  a2.ID=cl.Professional ) [Professional]    ," +
                                "cl.[GraduateSchool]  ," +
                                "cl.[IncomeYear]      ," +
                                "cl.[FuoodDrink]      ," +
                                "cl.[LoveMusic]       ," +
                                "cl.[LoveColor]       ," +
                                "cl.[LoveSmoke]       ," +
                                "cl.[LoveDrink]       ," +
                                "cl.[LoveTea]         ," +
                                "cl.[LoveBook]        ," +
                                "cl.[LoveSport]       ," +
                                "cl.[LoveClothes]     ," +
                                "cl.[Cosmetic]        ," +
                                "cl.[Nature]          ," +
                                "cl.[Appearance]      ," +
                                "cl.[AdoutBody]       ," +
                                "cl.[AboutFamily]     ," +
                                "cl.[Car]             ," +
                                "cl.[LiveHouse],cl.ProfessionalDes     " +


                          " from " +
                                "officedba.CustLinkMan cl left join officedba.CustInfo ci on cl.CustNo = ci.CustNo" +
                                " left join officedba.CodePublicType cp on cp.id = cl.LinkType" +
                                " left join officedba.EmployeeInfo ei on ei.id = cl.Creator" +
                               
                          " where " +
                                "cl.id=@id and cl.CompanyCD=@CompanyCD ";
                SqlParameter[] param = new SqlParameter[2];
                param[0] = SqlHelper.GetParameter("@CompanyCD", CompanyCD);
                param[1] = SqlHelper.GetParameter("@id", linkid);
                return SqlHelper.ExecuteSql(sql, param);
            }
            catch 
            {
                return null;
            }
        }
        #endregion

        #region 导出联系人列表
        /// <summary>
        /// 导出联系人列表
        /// </summary>
        /// <param name="CustNam"></param>
        /// <param name="LinkManM"></param>
        /// <param name="BeginDate"></param>
        /// <param name="EndDate"></param>
        /// <param name="ord"></param>
        /// <returns></returns>
        public static DataTable ExportLinkManInfo(string CustNam, LinkManModel LinkManM, string BeginDate, string EndDate, string ord)
        {
            try
            {
                string sql = "select " +
                                   "link.id,isnull(cust.id,'-1') custid,isnull(link.linkmanname,'') linkmanname,isnull(link.Appellation,'') Appellation," +
                                   "(case link.Important when '1' then '不重要' when '2' then '普通' when '3' then '重要' when '4' then '关键' else '' end) Important," +
                                   "isnull(cust.CustName,'') CustName,isnull(link.WorkTel,'') WorkTel,isnull(link.Handset,'') Handset," +
                                   "isnull(link.QQ,'') QQ,isnull(cp.TypeName,'') TypeName,Convert(varchar(100),link.Birthday,23) Birthday " +
                              " from " +
                                  " officedba.custlinkman link" +
                                  " left join officedba.custinfo cust on cust.custno = link.custno " +
                                  " left join officedba.CodePublicType cp on cp.id = link.LinkType " +
                              " where link.companycd = '" + LinkManM.CompanyCD + "'" +
                              " and (link.CanViewUser like '%" + "," + LinkManM.CanViewUser + "," + "%' or '" + LinkManM.CanViewUser + "' = link.Creator or link.CanViewUser = ',,')";

                if (CustNam != "")
                    sql += " and cust.id = '" + CustNam + "'";
                if (LinkManM.LinkManName != "")
                    sql += " and LinkManName like '%" + LinkManM.LinkManName + "%'";
                if (LinkManM.Handset != "")
                    sql += " and Handset like '%" + LinkManM.Handset + "%'";
                if (LinkManM.WorkTel != "")
                    sql += " and WorkTel like '%" + LinkManM.WorkTel + "%'";
                if (LinkManM.Important != "0")
                    sql += " and Important = '" + LinkManM.Important + "'";
                if (LinkManM.LinkType != 0)
                    sql += " and link.LinkType = '" + LinkManM.LinkType + "'";
                if (BeginDate.ToString() != "")
                    sql += " and link.Birthday >= '" + BeginDate.ToString() + "'";//sql += " and (link.Birthday >= '" + BeginDate.ToString() + "' or link.Birthday is null)";
                if (EndDate.ToString() != "")
                    sql += " and link.Birthday <= '" + EndDate.ToString() + "'";//sql += " and (link.Birthday <= '" + EndDate.ToString() + "' or link.Birthday is null)";

                return SqlHelper.ExecuteSql(sql);
            }
            catch (Exception ex)
            {
                string sss = ex.Message;
                return null;
            }
        }
        #endregion
    }
}


