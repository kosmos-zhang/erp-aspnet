/**********************************************
 * 类作用：   RoleFunction表数据模板
 * 建立人：   江贻明
 * 建立时间： 2009/01/10
 ***********************************************/
using System;

namespace XBase.Model.Office.SystemManager
{
  public  class RoleFunModel
    {
      private string _RoleFunctionID;
      private string _CompanyCD;
      private int _RoleID;
      private int _ModuleID;
      private int _FunctionID;
      private DateTime? _ModifiedDate = null;
      private string _ModifiedUserID;
      private string _remark;

      /// <summary>
      /// 角色功能编码
      /// </summary>
      public string RoleFunctionID
      {
          get { return _RoleFunctionID; }
          set { _RoleFunctionID = value; }
      }

      /// <summary>
      ///  企业编码
      /// </summary>
      public string CompanyCD
      {
          get { return _CompanyCD; }
          set { _CompanyCD = value; }
      }

      /// <summary>
      /// 角色编码
      /// </summary>
      public int RoleID
      {
          get { return _RoleID; }
          set { _RoleID = value; }
      }

      /// <summary>
      /// 模块编码
      /// </summary>
      public int ModuleID
      {
          get { return _ModuleID; }
          set { _ModuleID = value; }
      }

      /// <summary>
      /// 功能编码
      /// </summary>
      public int FunctionID
      {
          get { return _FunctionID; }
          set { _FunctionID = value; }
      }

      /// <summary>
      /// 修改时间
      /// </summary>
      public DateTime ? ModifiedDate
      {
          get { return _ModifiedDate; }
          set { _ModifiedDate = value; }
      }

      /// <summary>
      /// 修改用户ID
      /// </summary>
      public string ModifiedUserID
      {
          get { return _ModifiedUserID; }
          set { _ModifiedUserID = value; }
      }

      /// <summary>
      /// 备注
      /// </summary>
      public string Remark
      {
          get { return _remark; }
          set { _remark = value; }
      }

    }
}
