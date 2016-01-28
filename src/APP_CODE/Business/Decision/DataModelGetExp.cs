using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XBase.Business.Decision
{
    /// <summary>
    /// 自定义矩阵类
    /// </summary>
    class Matrix
    {
        #region 字段
        /// <summary>
        /// 矩阵的数据
        /// </summary>
        public double[,] data;

        /// <summary>
        /// 行数
        /// </summary>
        public int rows;

        /// <summary>
        /// 列数
        /// </summary>
        public int cols;

        /// <summary>
        /// 随机数
        /// </summary>
        private static Random random = new Random();

        #endregion


        #region 构造函数

        /// <summary>
        /// 构造函数，创建一个空矩阵
        /// </summary>
        /// <param name="row">行数</param>
        /// <param name="col">列数</param>
        public Matrix(int row, int col)
        {
            this.rows = row;
            this.cols = col;
            this.data = new double[rows, cols];
        }

        /// <summary>
        /// 构造函数，创建一个给定值矩阵
        /// </summary>
        /// <param name="row">行数</param>
        /// <param name="col">列数</param>
        /// <param name="value">矩阵元素的值</param>
        public Matrix(int row, int col, double value)
        {
            this.rows = row;
            this.cols = col;
            this.data = new double[rows, cols];
            for (int i = 0; i < rows; i++)
                for (int j = 0; j < cols; j++)
                    data[i, j] = value;
        }

        /// <summary>
        /// 构造函数，根据一个double二维数组创建一个矩阵
        /// </summary>
        /// <param name="m1">已知的二维数组</param>
        public Matrix(double[,] m1)
        {
            this.rows = m1.GetLength(0);
            this.cols = m1.GetLength(1);
            this.data = m1;
        }

        #endregion


        #region 运算符重载

        /// <summary>
        /// 负运算重载
        /// </summary>
        /// <param name="value">输入矩阵</param>
        /// <returns></returns>
        public static Matrix operator -(Matrix value)
        {
            Matrix m1 = new Matrix(value.rows, value.cols);
            double[,] dat = value.data;
            double[,] mdata = m1.data;
            for (int i = 0; i < m1.rows; i++)
                for (int j = 0; j < m1.cols; j++)
                    mdata[i, j] = -dat[i, j];
            return (m1);

        }

        /// <summary>
        /// 加运算重载，矩阵相加
        /// </summary>
        /// <param name="left">矩阵1</param>
        /// <param name="right">矩阵2</param>
        /// <returns></returns>
        public static Matrix operator +(Matrix left, Matrix right)
        {
            Matrix X = new Matrix(left.rows, left.cols);
            double[,] xdata = X.data;

            if (left.rows == right.rows && left.cols == right.cols)
                for (int i = 0; i < left.rows; i++)
                    for (int j = 0; j < left.cols; j++)
                        xdata[i, j] = left.data[i, j] + right.data[i, j];
            return (X);
        }

        /// <summary>
        /// 加运算重载，矩阵加常量
        /// </summary>
        /// <param name="left">矩阵</param>
        /// <param name="value">加上的数值</param>
        /// <returns></returns>
        public static Matrix operator +(Matrix left, double value)
        {
            Matrix X = new Matrix(left.rows, left.cols);
            double[,] xdata = X.data;

            for (int i = 0; i < left.rows; i++)
                for (int j = 0; j < left.cols; j++)
                    xdata[i, j] = left.data[i, j] + value;
            return (X);
        }

        /// <summary>
        /// 减法运算重载，矩阵相减
        /// </summary>
        /// <param name="left">矩阵1</param>
        /// <param name="right">矩阵2</param>
        /// <returns></returns>
        public static Matrix operator -(Matrix left, Matrix right)
        {
            Matrix X = new Matrix(left.rows, left.cols);
            double[,] xdata = X.data;

            for (int i = 0; i < left.rows; i++)
                for (int j = 0; j < left.cols; j++)
                    xdata[i, j] = left.data[i, j] - right.data[i, j];
            return (X);
        }

        /// <summary>
        /// 减法运算重载，矩阵减常量
        /// </summary>
        /// <param name="left">矩阵</param>
        /// <param name="value">减去的数值</param>
        /// <returns></returns>
        public static Matrix operator -(Matrix left, double value)
        {
            Matrix X = new Matrix(left.rows, left.cols);
            double[,] xdata = X.data;

            for (int i = 0; i < left.rows; i++)
                for (int j = 0; j < left.cols; j++)
                    xdata[i, j] = left.data[i, j] - value;
            return (X);
        }

        /// <summary>
        /// 乘法运算重载，矩阵相乘
        /// </summary>
        /// <param name="left">矩阵1</param>
        /// <param name="rigth">矩阵2</param>
        /// <returns></returns>
        public static Matrix operator *(Matrix left, Matrix right)
        {
            int m3r = left.rows;
            int m3c = right.cols;
            Matrix X = new Matrix(left.rows, right.cols);

            if (left.cols == right.rows)
            {
                double value = 0.0;
                for (int i = 0; i < m3r; i++)
                    for (int j = 0; j < m3c; j++)
                    {
                        value = 0;
                        for (int ii = 0; ii < left.cols; ii++)
                            value += left.data[i, ii] * right.data[ii, j];

                        X.data[i, j] = value;
                    }
            }
            else
                throw new Exception("矩阵的行/列数不匹配。");

            return X;

        }

        /// <summary>
        /// 乘法运算符重载，矩阵乘常量
        /// </summary>
        /// <param name="left">矩阵</param>
        /// <param name="value">乘上的数值</param>
        /// <returns></returns>
        public static Matrix operator *(Matrix left, double value)
        {
            Matrix X = new Matrix(left.rows, left.cols);

            for (int i = 0; i < left.rows; i++)
                for (int j = 0; j < left.cols; j++)
                    X.data[i, j] = left.data[i, j] * value;
            return (X);

        }
        /// <summary>
        /// 乘法运算符重载，常量乘矩阵
        /// </summary>
        /// <param name="value">乘上的数值</param>
        /// <param name="left">矩阵</param>
        /// <returns></returns>
        public static Matrix operator *(double value, Matrix left)
        {
            Matrix X = new Matrix(left.rows, left.cols);

            for (int i = 0; i < left.rows; i++)
                for (int j = 0; j < left.cols; j++)
                    X.data[i, j] = left.data[i, j] * value;
            return (X);

        }


        /// <summary>
        /// 乘法重载，矩阵的对应元素相乘，相当于matlab中的 ".*" 运算
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static Matrix operator &(Matrix left, Matrix right)
        {
            Matrix X = new Matrix(left.rows, left.cols);
            double[,] xdata = X.data;

            for (int i = 0; i < left.rows; i++)
                for (int j = 0; j < left.cols; j++)
                    xdata[i, j] = left.data[i, j] * right.data[i, j];
            return (X);
        }

        /// <summary>
        /// 乘方重载,对矩阵中各元素乘方，相当于matalb 中的 ".^" 运算
        /// </summary>
        /// <param name="left">矩阵</param>
        /// <param name="value">次方</param>
        /// <returns></returns>
        public static Matrix operator ^(Matrix left, double value)
        {
            Matrix X = new Matrix(left.rows, left.cols);

            for (int i = 0; i < left.rows; i++)
                for (int j = 0; j < left.cols; j++)
                    X.data[i, j] = Math.Pow(left.data[i, j], value);
            return (X);

        }

        /// <summary>
        /// 除法重载，矩阵各元素除以一常量
        /// </summary>
        /// <param name="left">矩阵</param>
        /// <param name="value">除以的数</param>
        /// <returns></returns>
        public static Matrix operator /(Matrix left, double value)
        {
            Matrix X = new Matrix(left.rows, left.cols);

            for (int i = 0; i < left.rows; i++)
                for (int j = 0; j < left.cols; j++)
                    X.data[i, j] = left.data[i, j] / value;
            return (X);

        }

        /// <summary>
        /// 除法重载，常量除以矩阵各元素，相当于matalb中的 "./" 运算
        /// </summary>
        /// <param name="value">常量</param>
        /// <param name="left">矩阵</param>
        /// <returns></returns>
        public static Matrix operator /(double value, Matrix left)
        {
            Matrix X = new Matrix(left.rows, left.cols);

            for (int i = 0; i < left.rows; i++)
                for (int j = 0; j < left.cols; j++)
                    X.data[i, j] = value / left.data[i, j];
            return (X);

        }

        /// <summary>
        /// 除法重载，两个矩阵对应元素相除，相当于matlab中的 "./" 运算
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static Matrix operator |(Matrix left, Matrix right)
        {
            Matrix X = new Matrix(left.rows, left.cols);
            double[,] xdata = X.data;

            for (int i = 0; i < left.rows; i++)
                for (int j = 0; j < left.cols; j++)
                    xdata[i, j] = left.data[i, j] / right.data[i, j];
            return (X);
        }

        #endregion


        #region 方法

        /// <summary>
        /// 求各列平均值，相当于matlab中的 "mean"函数
        /// </summary>
        /// <returns></returns>
        public Matrix avgMat()
        {
            double sumCol;

            int i, j;

            Matrix X = new Matrix(1, cols);

            for (j = 0; j < cols; j++)
            {
                sumCol = 0;
                for (i = 0; i < rows; i++)
                {
                    sumCol += data[i, j];
                }
                X.data[0, j] = sumCol / rows;
            }
            return X;

        }

        /// <summary>
        /// 求矩阵相同维数的对角阵，相当于matlab中的 "diag"函数 ，这里只能求方阵的主对角阵
        /// </summary>
        /// <returns></returns>
        public Matrix diagMat()
        {

            Matrix X = new Matrix(rows, cols, 0);
            int row = rows;
            int col = row;

            if (rows == cols)
            {
                for (int i = 0; i < row; i++)
                {
                    X.data[i, i] = data[i, i];
                }
            }
            else
            {
                throw new Exception("不是方阵，无法求对角阵");
            }
            return X;

        }

        /// <summary>
        /// 将对角元素做为一列返回
        /// </summary>
        /// <returns></returns>
        public Matrix diagMat2()
        {

            Matrix X = new Matrix(rows, 1);
            int row = rows;
            int col = row;

            if (rows == cols)
            {
                for (int i = 0; i < row; i++)
                {
                    X.data[i, 0] = data[i, i];
                }
            }
            else
            {
                throw new Exception("不是方阵，无法求对角阵");
            }
            return X;

        }

        /// <summary>
        /// 求一维矩阵的最小值及对应下标，相当于matlab中的 "min" 函数
        /// </summary>
        /// <returns>返回格式double类型的1*2矩阵，[最小值,下标]</returns>
        public double[,] minMat()
        {
            double smv;
            int smI = 0;
            smv = data[0, 0];
            if (rows == 1) //行矩阵
            {
                for (int i = 0; i < cols; i++)
                {
                    if (smv > data[0, i])
                    {
                        smv = data[0, i];
                        smI = i;
                    }
                }
            }
            else  //列矩阵
            {
                for (int i = 0; i < rows; i++)
                {
                    if (smv > data[i, 0])
                    {
                        smv = data[i, 0];
                        smI = i;
                    }
                }
            }

            double[,] res = new double[1, 2];
            res[0, 0] = smv;
            res[0, 1] = smI;
            return res;
        }

        /// <summary>
        /// 求一维矩阵的最小值及对应下标，相当于matlab中的 "max" 函数
        /// </summary>
        /// <returns>返回格式double类型的1*2矩阵，[最大值,下标]</returns>
        public double[,] maxMat()
        {
            double smv;
            int smI = 0;
            smv = data[0, 0];
            if (rows == 1) //行矩阵
            {
                for (int i = 0; i < cols; i++)
                {
                    if (smv < data[0, i])
                    {
                        smv = data[0, i];
                        smI = i;
                    }
                }
            }
            else  //列矩阵
            {
                for (int i = 0; i < rows; i++)
                {
                    if (smv < data[i, 0])
                    {
                        smv = data[i, 0];
                        smI = i;
                    }
                }
            }

            double[,] res = new double[1, 2];
            res[0, 0] = smv;
            res[0, 1] = smI;
            return res;
        }

        /// <summary>
        /// 矩阵转秩 ，相当于matlab中的 "'"运算
        /// </summary>
        /// <returns></returns>
        public Matrix transposMat()
        {
            Matrix X = new Matrix(cols, rows);
            double[,] srcm = data;
            double[,] tmpm = new double[srcm.GetLength(1), srcm.GetLength(0)];
            for (int i = 0; i < srcm.GetLength(0); i++)
                for (int j = 0; j < srcm.GetLength(1); j++)
                {
                    if (i != j)
                    {
                        tmpm[j, i] = srcm[i, j];
                    }
                    else
                        tmpm[i, j] = srcm[i, j];
                }
            X.data = tmpm;
            return X;
        }

        /// <summary>
        /// 矩阵输出
        /// </summary>
        /// <returns></returns>
        public string prtMatrix()
        {
            string tmprst;
            tmprst = "\n";
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    tmprst += data[i, j].ToString() + "\t";
                }
                tmprst += "\n";
            }
            return tmprst;
        }


        //R(1:n,end)  前1到n行的最后一列
        public static Matrix partMat1(Matrix mat)
        {
            Matrix X = new Matrix(mat.rows - 1, 1);
            for (int i = 0; i < mat.rows - 1; i++)
            {
                for (int j = 0; j < mat.cols; j++)
                {
                    if (j == mat.cols - 1)
                        X.data[i, 0] = mat.data[i, j];
                }

            }

            return X;
        }

        //R(1:n,1:n) 前n行n列
        public static Matrix partMat2(Matrix mat)
        {
            Matrix X = new Matrix(mat.rows - 1, mat.cols - 1);
            for (int i = 0; i < mat.rows - 1; i++)
            {
                for (int j = 0; j < mat.cols - 1; j++)
                {
                    X.data[i, j] = mat.data[i, j];
                }

            }

            return X;
        }

        //P(In) 此处 P为 3*1 的数组 [2 3 4]' int[] In={1 2} 则 P(In)=[3 4]' ，即取对应位置的元素 
        public static Matrix partMat3(Matrix mat, ArrayList In)
        {
            Matrix X = new Matrix(In.Count, 1);
            int j;
            for (int i = 0; i < In.Count; i++)
            {
                j = (int)In[i];
                X.data[i, 0] = mat.data[j, 0];
            }

            return X;
        }

        //P(In) 此处 P为 1*3 的数组 [2 3 4] int[] In={1 2} 则 P(In)=[3 4] ，即取对应位置的元素 
        public static Matrix partMat6(Matrix mat, ArrayList In)
        {
            Matrix X = new Matrix(1, In.Count);
            int j;
            for (int i = 0; i < In.Count; i++)
            {
                j = (int)In[i];
                X.data[0, i] = mat.data[0, j];
            }

            return X;
        }



        //R(In,end)
        public static Matrix partMat4(Matrix mat, ArrayList In)
        {
            Matrix X = new Matrix(In.Count, 1);
            int j;
            for (int i = 0; i < In.Count; i++)
            {
                for (int p = 0; p < mat.cols; p++)
                {

                }

                j = (int)In[i];
                X.data[i, 0] = mat.data[j, mat.cols - 1];
            }

            return X;
        }


        //L(In,In)
        public static Matrix partMat5(Matrix mat, ArrayList In)
        {
            Matrix X = new Matrix(In.Count, In.Count);
            int p;
            int q;
            for (int i = 0; i < In.Count; i++)
            {
                p = (int)In[i];
                for (int j = 0; j < In.Count; j++)
                {
                    q = (int)In[j];
                    X.data[i, j] = mat.data[p, q];

                }

            }

            return X;
        }

        //对arraylist排序
        public static void sortArry(ArrayList Ln)
        {
            int temp;
            for (int i = 0; i < Ln.Count; i++)
            {
                for (int j = 0; j < i; j++)
                {
                    if ((int)Ln[i] < (int)Ln[j])
                    {
                        temp = (int)Ln[i];
                        Ln[i] = Ln[j];
                        Ln[j] = temp;

                    }

                }
            }

        }


        #endregion


        #region 消去运算

        //取子矩阵，可以写一个类似如matlab的通用过程，这里转化为下面三种，去掉某行某列、去列取某行、去行取某列

        private static double[,] child1Mat(double[,] m1, int k)  //去掉第k行，第k列
        {
            int row = m1.GetLength(0);
            int col = m1.GetLength(1);
            k = k - 1;
            double[,] m2 = new double[row, col - 1]; //先去列
            for (int i = 0; i < row; i++)
            {
                for (int j = 0; j < col; j++)
                {
                    if (j < k)
                        m2[i, j] = m1[i, j];
                    if (j > k)
                        m2[i, j - 1] = m1[i, j];
                }

            }
            double[,] m3 = new double[row - 1, col - 1];
            for (int i = 0; i < row; i++)  //去掉行 ，注意是在已经去掉列的前提下
            {
                for (int j = 0; j < col - 1; j++)
                {
                    if (i < k)
                        m3[i, j] = m2[i, j];
                    if (i > k)
                        m3[i - 1, j] = m2[i, j];
                }

            }

            return m3;

        }

        private static double[,] child2Mat(double[,] m1, int k)  //去掉第k列,取第k行
        {
            int row = m1.GetLength(0);
            int col = m1.GetLength(1);
            k = k - 1;

            //取第k行
            double[,] m3 = new double[1, col - 1];
            for (int i = 0; i < row; i++)
            {
                for (int j = 0; j < col; j++)
                {
                    if (i == k)
                    {
                        if (j < k)
                        {
                            m3[0, j] = m1[i, j];
                        }
                        if (j > k)
                        {
                            m3[0, j - 1] = m1[i, j];
                        }
                    }
                }
            }
            return m3;
        }

        private static double[,] child3Mat(double[,] m1, int k)  //去掉第k行,取第k列
        {
            int row = m1.GetLength(0);
            int col = m1.GetLength(1);
            k = k - 1;


            //取第k列
            double[,] m3 = new double[row - 1, 1];
            for (int i = 0; i < row; i++)
            {
                for (int j = 0; j < col; j++)
                {
                    if (j == k)
                    {
                        if (i < k)
                        {
                            m3[i, 0] = m1[i, j];
                        }
                        if (i > k)
                        {
                            m3[i - 1, 0] = m1[i, j];
                        }

                    }
                }
            }

            return m3;
        }


        //消去变换，改变数组某一部分的值 
        public static double[,] T(Matrix mat, int k)  //应该为方阵
        {
            k += 1;
            double[,] m1 = mat.data;
            int row = m1.GetLength(0);
            int col = row;
            double[,] m2, m3, m4, m5;
            m2 = child1Mat(m1, k);//去掉第k行，第k列
            m3 = child3Mat(m1, k);//去掉第k行,取第k列
            m4 = child2Mat(m1, k);//去掉第k列,取第k行

            Matrix M2 = new Matrix(child1Mat(m1, k));
            m2 = M2.data;
            Matrix M3 = new Matrix(child3Mat(m1, k));
            m3 = M3.data;
            Matrix M4 = new Matrix(child2Mat(m1, k));
            m4 = M4.data;
            m5 = (M2 - (M3 * (M4 / m1[k - 1, k - 1]))).data;
            Matrix M5 = new Matrix(m5);

            // //Console.WriteLine(M2.prtMatrix());
            ////Console.WriteLine(M3.prtMatrix());
            ////Console.WriteLine(M4.prtMatrix());
            ////Console.WriteLine(M5.prtMatrix());
            ////Console.WriteLine("sdf");
            ////Console.WriteLine(mat.prtMatrix());
            //子矩阵部分对应赋值 
            for (int i = 0; i < row; i++)
            {
                for (int j = 0; j < col; j++)
                {
                    if (i < k - 1)
                    {
                        if (j < k - 1)
                            m1[i, j] = m5[i, j];

                        if (j > k - 1)
                            m1[i, j] = m5[i, j - 1];

                    }
                    if (i > k - 1)
                    {
                        if (j < k - 1)
                            m1[i, j] = m5[i - 1, j];

                        if (j > k - 1)
                            m1[i, j] = m5[i - 1, j - 1];
                    }
                }
            }
            ////Console.WriteLine(mat.prtMatrix());

            //去掉第k行后的第k列赋值

            for (int i = 0; i < row; i++)
            {
                for (int j = 0; j < col; j++)
                {
                    if (j == k - 1)
                    {
                        if (i != k - 1)
                            m1[i, j] = -m1[i, j] / m1[k - 1, k - 1];
                    }
                }
            }
            ////Console.WriteLine(mat.prtMatrix());
            //去掉第k列后的第k行赋值
            for (int i = 0; i < row; i++)
            {
                for (int j = 0; j < col; j++)
                {
                    if (i == k - 1)
                    {
                        if (j != k - 1)
                            m1[i, j] = m1[i, j] / m1[k - 1, k - 1];
                    }
                }
            }

            ////Console.WriteLine(mat.prtMatrix());
            //对[k,k]赋值
            m1[k - 1, k - 1] = 1 / m1[k - 1, k - 1];
            ////Console.WriteLine(mat.prtMatrix());
            return m1;
        }



        #endregion



    }

    /// <summary>
    /// 建模类
    /// </summary>
    public class DataModelGetExp
    {

        //属性字段
        private double  Fin ;
        private double Fout ;
        private Matrix  XY;
        private int m; //数据矩阵行数，样本数
        private int n; //数据矩阵列数
        private int f; //引入变量个数
        private int s; //计算步数


        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="fin">F进</param>
        /// <param name="fout">F出</param>
        /// <param name="XY">数据矩阵</param>
        public DataModelGetExp(double fin, double fout, double[,] xy)
        {
            Fin = fin;    
            Fout = fout;
     
              
            m = xy.GetLength (0);
            n = xy.GetLength(1);

            //**********************************************************************************************************************************
            //将xy[,]中目标变量的多值转为二值，将小于平均值的设为0，大于平均值的设为1
            double midy = 0;
            for (int i = 0; i < m; i++) 
            {
                midy += xy[i, n - 1]; //sum
            }
            midy = midy / m; //avg

            for (int i = 0; i < m; i++) 
            {
                if (xy[i, n - 1] <= midy)
                    xy[i, n - 1] = 0;
                else
                    xy[i, n - 1] = 1;
            }
            //**********************************************************************************************************************************

            //处理输入矩阵的y值 ，用户应该输入的y即p应该为二值即 0和1 ，这里将这转为log(p/(1-p)) ，因为不能直接取0或1，所以将0变为 0.01，将1变为0.99
            double p=0;
            for (int i = 0; i < xy.GetLength(0); i++) 
            {
                if (xy[i,n-1]==0)
                    p=0.01;
                if (xy[i,n-1]==1)
                    p=0.99;
                xy[i, n - 1] = Math.Log10(p / (1 - p));
            }


            n = n - 1; //自变量个数

            XY = new Matrix(xy);

            //Console.WriteLine(XY .prtMatrix ());
            f = 0;     //引入变量个数
            s = 0;     //计算步数
        }

        /// <summary>
        /// 回归过程计算
        /// </summary>
        /// <returns>返回计算结果</returns>
        public bool toRegre(out string result)
        {
            StringBuilder strout = new StringBuilder();
            Matrix  mXY = XY.avgMat() ; //一维的行矩阵，求各列平均值
           
            //L=XY'*XY-m*mXY'*mXY;%计算离差阵L(n+1,n+1)
            Matrix L=XY.transposMat()*XY-((m*mXY.transposMat())*mXY );


            //计算相关系数阵R(n+1,n+1) ,R=diag(1./sqrt(diag(L)))
            Matrix R;
            R=(1/((L.diagMat())^0.5)).diagMat();
            R = R * L * R;

            //Console.WriteLine(R.prtMatrix ());
            //int [] In=null; //已引入的所有变量下标索引数组
            //int [] Out=null;//未引入的所有变量下标索引数组
            ArrayList In =new ArrayList ();
            ArrayList Out =new  ArrayList ();
            for (int i = 0; i < n; i++) 
            {
                Out.Add(i);
            }

            //开始逐步回归计算
            Matrix P;
            Matrix tt;
            double[,] pi;
            double pmin;
            double imin;
            double pmax;
            double imax;
            double fx;

            while (true)
            {
                //计算引入变量的偏回归平方和，和未引入变量的引入贡献
                //P=R(1:n,end).^2./diag(R(1:n,1:n));
                tt=Matrix.partMat1(R);
                tt = tt ^ 2;
                P = tt|(Matrix.partMat2 (R).diagMat2 ()); //矩阵相除

                if (s > 1) 
                {
                   pi =( Matrix.partMat3(P, In)).minMat();
                    
                     pmin = pi[0, 0];
                     imin=pi[0,1];
                    //Fx=(m-f-1)*pmin/R(end,end);%计算待剔除变量的F检验值
                     fx = (m - f - 1) * pmin / R.data [R.rows-1 , R.cols -1];

                    if (fx <= Fout) 
                    {
                        s += 1;
                        //Console.WriteLine("第"+s+"步");
                        //Console.WriteLine("变量x" + ((int)In[(int)imin]) + "被剔除,F" + In[(int)imin] + "=" + fx + "<=F出=" + Fout + "。");


                        //R=T(R,In(imin));%消去变换
                        int kk =(int) In[(int)imin];
                        R .data = Matrix.T(R, kk);

                        //Out(end+1)=In(imin);%将剔除的变量下标加入未引入变量索引数组
                        Out.Add(In[(int)imin]);
                       

                        //In(imin)=[];%将剔除的变量下标从引入变量索引数组中删除 
                        In.RemoveAt((int)imin);

                        f = f - 1;
                        continue;
                    }

                }

                if (Out.Count > 0) 
                {
                pi = (Matrix.partMat3(P, Out)).maxMat();

                 pmax = pi[0, 0];
                 imax = pi[0, 1];

                 //Fx=(m-f-2)*pmax/(R(end,end)-pmax);%计算待选入变量的F检验值
                 fx = (m - f - 2)*(pmax) /(R.data[R.rows -1,R.cols-1 ]-(pmax));

                 if (fx >= Fin ) 
                 {
                     s += 1;
                     //Console.WriteLine("第" + s + "步");
                     //Console.WriteLine("变量x"+((int)Out[(int)imax])+"被选入,F"+Out[(int)imax]+"="+fx+"<=F出="+Fin+"。");

                     //R=T(R,Out(imax));%消去变换
                     int kk = (int)Out[(int)imax];
                     R.data = Matrix.T(R, kk);

                     //In(end+1)=Out(imax);%将选入的变量下标加入引入变量索引数组 %相当于数组的元素
                     In.Add(Out[(int)imax]);
                     //Out(imax)=[]
                     Out.RemoveAt((int)imax);
                     f += 1;

                     continue;

                 }
                }
                

                 //Console.WriteLine("第" + (s+1) + "步");
                 //Console.WriteLine("既不能选入，也不能剔除，结束逐步计算.");
                 break;
            }


            if (In.Count == 0)
            {
                result = "数据太少或没有匹配到有效的数据，建模失败！";
                return false;
            }


            Matrix.sortArry(In);
            //B=R(In,end).*sqrt(L(end,end)./diag(L(In,In)));%选入变量的系数
            Matrix b1=Matrix .partMat5 (L,In);
            b1=b1.diagMat2 ();
            b1=(double )(L.data[L.rows -1,L.cols -1])/b1;
            b1=b1^0.5;

            Matrix B = (Matrix.partMat4(R, In)) & b1;
           
            //b0=mXY(end)-mXY(In)*B;%常数项
            Matrix b2=Matrix .partMat6 (mXY,In);
            b2=b2*B;

            double b0 = mXY.data [0,mXY.cols - 1] - b2.data [0, 0];
            ////Console.WriteLine("回归方程:");

            //disp(sprintf('y=%f%s',b0,sprintf('%+fx%d',[B';In])));%显示回归方程
            //Console.WriteLine("常数项："+b0);
            strout.Append(b0 +"|");
            Matrix b3 = B.transposMat();
            string ttst=string.Empty ;
            for (int i = 0; i < In.Count; i++) 
            {
                ttst += In[i] + "," + b3.data[0, i] + ";";
                //Console.WriteLine("变量：" + In[i]+"  系数："+b3.data[0,i]  );
            }
            strout.Append(ttst.Substring (0,ttst.Length -1));


            result = strout.ToString();
            return true;


            #region others
            // //disp(['残差平方和 Q = ' num2str(L(end,end)*R(end,end))])
           // double Q = L.data[L.rows - 1, L.cols-1] * R.data[R.rows - 1, R.cols - 1];
           //// Console .WriteLine ("残差平方和Q："+Q);
           //// strout.Append("残差平方和Q：" + Q + "|");

           // //disp(['剩余标准差 S = ' num2str(sqrt(L(end,end)*R(end,end)/(m-f-1)))])
           // double S = Math.Sqrt(Q / (m - f - 1));
           // //Console.WriteLine("剩余标准差S：" + S);
           //// strout.Append(S + "|");

           // //disp(['复相关系数 R = ' num2str(sqrt(1-R(end,end)))])
           // double RR = Math.Sqrt(1 - R.data[R.rows - 1, R.cols - 1]);
           // //Console.WriteLine("复相关系数 R ：" + RR);
           // //strout.Append(RR + "|");

           // //disp(['回归方程显著性检验 F = ' num2str((m-f-1)*(1-R(end,end))/(f*R(end,end)))])  (m-f-1)*(  1-R(end,end)  )/(   f*R(end,end)   )
           // double FF = (m - f - 1) * (1 - R.data[R.rows - 1, R.cols - 1]) / (f * R.data[R.rows - 1, R.cols - 1]);
           // //Console.WriteLine("回归方程显著性检验 F ：" + FF);
           // //strout.Append(FF + "|");

           // //CC=diag(R(In,In))*R(end,end);
           // Matrix CC = Matrix.partMat5(R, In);
           // CC = CC.diagMat2();
           // CC = CC * (R.data[R.rows - 1, R.cols - 1]);
            
           // //disp(['各回归系数的t检验值:',sprintf('%10f\t',R(In,end)./sqrt(CC/(m-f-1)))])
           // Matrix tempR = Matrix.partMat4(R, In);
           // tempR = tempR | (CC / (m - f - 1) ^ 0.5);
           // string strtempr=string.Empty ;
           // string strf;
           // for (int i = 0; i < tempR.rows; i++) 
           // {
           //     strf = tempR.data[i, 0].ToString ();
           //     strtempr +=strf +",";
           // }
           //  //Console.WriteLine("各回归系数的t检验值:"+strtempr.Substring (0,strtempr .Length -1) );
           //// strout.Append(strtempr.Substring(0, strtempr.Length - 1) + "|");


           //     Matrix Rend = Matrix.partMat4(R, In);
           //     Matrix Rend2 = (CC + (Rend ^ 2)) ^ 0.5;
           //     Rend = Rend | Rend2;
           //     string strtempr2 = string.Empty;
           //     string strf2;
           //  for (int i = 0; i < Rend.rows; i++)
           //  {
           //      strf2 = Rend.data[i, 0].ToString();
           //      strtempr2 += strf2 + ",";
           //  }
           //  //Console.WriteLine("引入变量的偏相关系数:" + strtempr2.Substring(0, strtempr2.Length - 1));
           // // strout.Append(strtempr2.Substring(0, strtempr2.Length - 1));
                        
           //  result = strout.ToString();
            //  return true;
            #endregion

        }
                  





    }


}
