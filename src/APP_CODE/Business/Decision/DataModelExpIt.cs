using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

using System.Data;

namespace XBase.Business.Decision
{
    /// <summary>
    /// 模型求值类
    /// </summary>
    public class DataModelExpIt
    {
        private string mdlexp;
        private double[,] matrix;


        private double constSec;
        private string[] varSecs;

        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="exp">-2.66337903592676|1,0.00244135098964175;9,0.000421991980806346</param>
        /// <param name="xy"></param>
        public DataModelExpIt(string exp, double[,] xy)
        {
            matrix = xy;
            mdlexp = exp;

            constSec = double.Parse(exp.Split('|')[0]);
            varSecs = exp.Split('|')[1].Split(';');


        }

        /// <summary>
        /// 求值
        /// </summary>
        /// <returns></returns>
        public double[,] ExpIt(int lvCnt)
        {           
            int xl = matrix.GetLength(0);
            int yl = matrix.GetLength(1);

            //lvCnt++;

            for (int i = 0; i < xl; i++)
            {
                double result = constSec;
                for (int j = 0; j < varSecs.Length; j++)
                {
                    string[] pairs = varSecs[j].Split(',');

                    result += matrix[i,int.Parse(pairs[0])] * double.Parse(pairs[1]);
                }

                double p = Math.Pow(Math.E, result) / (1 + Math.Pow(Math.E, result));

                int rr = (int)(p*lvCnt);

                if ((int)matrix[i, yl - 1] == rr)
                {
                    matrix[i, yl - 1] = 1;
                }
                else {
                    matrix[i, yl - 1] = 0;
                }

            }

            return matrix;
        }


        /// <summary>
        /// 求值
        /// </summary>
        /// <param name="input"></param>
        /// <param name="companyCD"></param>
        /// <returns></returns>
        public static  DataTable ExpDataTable(DataTable input,string companyCD,string expType,int moveRows)
        {
            DataSet ds0 = new XBase.Business.Decision.DataCustLevel().GetList("CompanyCD='" + companyCD + "'");
            int levelCnt = ds0.Tables[0].Rows.Count;
            levelCnt--;

            DataSet ds = new XBase.Business.Decision.DataModelExp().GetList("CompanyCD='" + companyCD + "' AND ExpType='" + expType + "'");
            if (ds.Tables[0].Rows.Count == 0)
                return input;

            string exp = ds.Tables[0].Rows[0]["Expressions"].ToString();

            double tconstSec = double.Parse(exp.Split('|')[0]);
            string[] tvarSecs = exp.Split('|')[1].Split(';');

            foreach (DataRow row in input.Rows)
            {
                double result = tconstSec;
                for (int j = 0; j < tvarSecs.Length; j++)
                {
                    string[] pairs = tvarSecs[j].Split(',');

                    result += double.Parse(row[moveRows+ int.Parse(pairs[0])].ToString()) * double.Parse(pairs[1]);
                }

                double p = Math.Pow(Math.E, result) / (1 + Math.Pow(Math.E, result));

                if (Double.IsNaN(p))
                {
                    p = 1;
                }


                int lvIdx = (int)(p * levelCnt);

             

                if (levelCnt == -1)
                {
                    row["CustGrade"] = "未配置等级";
                }
                else
                {
                    row["CustGrade"] = ds0.Tables[0].Rows[lvIdx]["GName"].ToString();
                }
            }

            return input;
        }




    }
}
