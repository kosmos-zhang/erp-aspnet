using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XBase.Model.Office.HumanManager
{
    /// <summary>
    /// 类名：PerformanceElemModel
    /// 描述：PerformanceEle表数据模板（考核指标及评分规则设置）
    /// 
    /// 作者：王保军
    /// 创建日期：2009/04/21
    /// 最后修改日期：2009/04/21
    /// </summary>
  public   class PerformanceElemModel
    {

      private string _id;
      private string _companyCD;
      private string _elemNo;
      private string _elemName;
      private string _parentElemNo;
      private string _scoreRules;
      private string   _standardScore;
      private string _minScore;
      private string _maxScore;
      private string _asseStandard;
      private string _asseFrom;
      private string _remark;
      private string _usedStatus;
      private DateTime  _modifiedDate;
      private string _modifiedUserID;
      private string _editFlag;
      private string _usedstatusname;
      private string _scoreArrange;
      /// <summary>
      /// 内部id，自动生成
      /// </summary>
      public string ID
      {
          set { _id = value; }
          get { return _id; }
      }
      /// <summary>
      /// 公司代码
      /// </summary>
      public string CompanyCD
      {
          set { _companyCD = value; }
          get { return _companyCD; }
      }
      /// <summary>
      /// 指标编号
      /// </summary>
      public string ElemNo
      {
          set { _elemNo = value; }
          get { return _elemNo; }
      }
      /// <summary>
      /// 指标名称
      /// </summary>

      public string ElemName
      {
          set { _elemName = value; }
          get { return _elemName; }
      }

      /// <summary>
      /// 父指标编号
      /// </summary>
      public string ParentElemNo
      {
          set { _parentElemNo = value; }
          get { return _parentElemNo; }
      }
      /// <summary>
      /// 评分细则
      /// </summary>
      public string ScoreRules
      {
          set { _scoreRules = value; }
          get { return _scoreRules; }
      }
      /// <summary>
      /// 标准分
      /// </summary>
      public string StandardScore
      {
          set { _standardScore = value; }
          get { return _standardScore; }
      }
      /// <summary>
      /// 评分最小值
      /// </summary>
      public string MinScore
      {
          set {  _minScore = value; }
          get { return _minScore; }
      }
      /// <summary>
      /// 评分最大值
      /// </summary>
      public string MaxScore
      {
          set { _maxScore = value; }
          get { return _maxScore; }
      }
      /// <summary>
      /// 评分标准
      /// </summary>
      public string AsseStandard
      {
          set { _asseStandard  = value; }
          get { return _asseStandard; }
      }
      /// <summary>
      /// 评分来源(如业绩)
      /// </summary>
      public string AsseFrom
      {
          set { _asseFrom   = value; }
          get { return _asseFrom; }
      }
      /// <summary>
      /// 备注
      /// </summary>
      public string Remark
      {
          set {  _remark = value; }
          get { return _remark; }
      }
      /// <summary>
      /// 启用状态(0 停用，1 启用)

      /// </summary>
      public string UsedStatus
      {
          set { _usedStatus  = value; }
          get { return _usedStatus; }
      }
      /// <summary>
      /// 更新日期
      /// </summary>
      public DateTime  ModifiedDate
      {
          set { _modifiedDate  = value; }
          get { return _modifiedDate; }
      }
      /// <summary>
      /// 更新用户ID
      /// </summary>
      public string ModifiedUserID
      {
          set { _modifiedUserID   = value; }
          get { return _modifiedUserID; }
      }
      /// <summary>
      ///编辑标识
      /// </summary>
      public string EditFlag
      {
          set
          {
              _editFlag = value;
          }
          get
          {
              return _editFlag;
          }
      }
      /// <summary>
      /// 启用状态(0 停用，1 启用) 
      /// </summary>
      public string UsedStatusName
      {
          set { _usedstatusname = value; }
          get { return _usedstatusname; }
      }
      /// <summary>
      /// 评分范围
      /// </summary>
      public string ScoreArrange
      {
          set { _scoreArrange = value; }
          get { return _scoreArrange; }
      }






    }
}
