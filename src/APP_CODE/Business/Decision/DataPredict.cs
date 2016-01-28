using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Data;

namespace XBase.Business.Decision
{
    public class DataPredict
    {
        public DataPredict() { }
        public DataTable LinePredict(DataTable dt,int DateLong) 
        {
            /*输入数组*/
            int n = 1;
            double[] indata = new double[dt.Rows.Count - DateLong];
            for (int i = 0; i < dt.Rows.Count - DateLong; i++) 
            {
                indata[i] = Convert.ToDouble(dt.Rows[i][0]);
            }
            if (dt.Rows.Count - DateLong <= 3)
            {
                n = dt.Rows.Count - DateLong - 1;
            }
            else 
            {
                n = 3;
            }
            clsARMA am = new clsARMA(indata, n, 10, -10, n, 10, -10, 1000, 100, 0.7, 0.3, DateLong);
            ArrayList lastData=am.toARMA();
            
            //double maxvar = (double)lastData[0];
            //double maxvma = (double)lastData[1];

            /*输出数组*/
            double[] outdata = (double[])lastData[2];

            for (int i = 0; i < dt.Rows.Count; i++) 
            {
                if (dt.Rows[i][1].ToString().Length == 6) 
                {
                    dt.Rows[i][1] = dt.Rows[i][1].ToString().Replace("-","-0");
                }
                dt.Rows[i][2] = outdata[i];
            }

            return dt;
        }

    }

    /// <summary>
    /// ARMA
    /// </summary>
    class clsARMA
    {

        private double[] _data;
        private int _ar_n;
        private double _ar_ub;
        private double _ar_lb;
        private int _ma_n;
        private double _ma_ub;
        private double _ma_lb;
        private int _ga_n;
        private int _ga_num;
        private double _ga_pr;
        private double _ga_pm;

        private double[] _x;
        private int _count;

        /// <summary>
        /// arma类，通过输入一个数组，返回一个预测的数组，包括最后一个预测值。
        /// </summary>
        /// <param name="data">历史数据</param>
        /// 
        /// <param name="ar_n">ar模型部分的参数个数，如1到3</param>
        /// <param name="ar_ub">ar模型部分的参数上界，如10</param>
        /// <param name="ar_lb">ar模型部分的参数下界，如-10</param>
        /// 
        /// <param name="ma_n">ma模型部分的参数个数，如1到3</param>
        /// <param name="ma_ub">ma模型部分的参数上界，如10</param>
        /// <param name="ma_lb">ma模型部分的参数下界，如-10</param>
        /// 
        /// <param name="ga_n">ga模型部分的迭代次数，如500或1000等</param>
        /// <param name="ga_num">ga模型部分的种群数量，如50或100等</param>
        /// <param name="ga_pr">ga模型部分的交叉概率，如0.5到0.9之间</param>
        /// <param name="ga_pm">ga模型部分的变异概率，如0.1到0.3之间</param>
        /// <param name="count">预测的期数</param>
        public clsARMA(double[] data, int ar_n, double ar_ub, double ar_lb, int ma_n, double ma_ub, double ma_lb, int ga_n, int ga_num, double ga_pr, double ga_pm, int count)
        {
            _data = data;
            _ar_n = ar_n;
            _ar_ub = ar_ub;
            _ar_lb = ar_lb;
            _ma_n = ma_n;
            _ma_ub = ma_ub;
            _ma_lb = ma_lb;
            _ga_n = ga_n;
            _ga_num = ga_num;
            _ga_pr = ga_pr;
            _ga_pm = ga_pm;
            _x = new double[_data.Length - 1];

            _count = count;

            //取得差分
            getOnedec();
        }

        /// <summary>
        /// 得到一阶差分
        /// </summary>
        /// <returns></returns>
        private void getOnedec()
        {
            for (int i = 0; i < _x.Length; i++)
            {
                _x[i] = (_data[i + 1] - _data[i]) / _data[i];
            }
        }



        public ArrayList toARMA()
        {
            //_x为差分后的值
            //double[] _x ={ 0.0810295519542421, 0.262345679012346, -0.0621725462801258, -0.0446927374301676, -0.00896686159844059, 0.164044059795437, 0.154106116931396, 0.393265007320644, 0.134089953762085, 0.179577464788732, 0.213511390416339, 0.0666752977731745, 0.204029615244569, 0.252620967741935, 0.332528569129245, 0.374078995047711, 0.189038326300985, 0.0767012900602521, 0.0540373523757208, 0.0628297830760211, 0.116637553246912, 0.105085490023877, 0.115879401976854, 0.163357963144307, 0.119146005509642, 0.108581196581197, 0.116095108863258, 0.129094651911413 };
            //double[] _x ={ 0.0810295519542421, 0.262345679012346, -0.0621725462801258, -0.0446927374301676, -0.00896686159844059, 0.164044059795437, 0.154106116931396, 0.393265007320644, 0.134089953762085, 0.179577464788732, 0.213511390416339, 0.0666752977731745, 0.204029615244569, 0.252620967741935, 0.332528569129245, 0.374078995047711, 0.189038326300985, 0.0767012900602521, 0.0540373523757208, 0.0628297830760211, 0.116637553246912, 0.105085490023877, 0.115879401976854, 0.163357963144307, 0.119146005509642, 0.108581196581197, 0.116095108863258 };

            clsga gg = new clsga(_x, _ar_n, _ar_ub, _ar_lb, _ga_n, _ga_num, _ga_pr, _ga_pm);

            //使用GA运行得到ar部分的参数
            ArrayList resar = gg.runGA();
            double[] ar = (double[])resar[0];
            double maxV1 = (double)resar[1];

            //使用ar部分的参数取得预测值 _s为_x与其预测值之间的误差
            double[] _e = gg.getE(ar);

            //为便于计算，将_e补并个数，与_x相等
            double[] _ee = new double[_x.Length];

            clsga ge = new clsga(_e, _ma_n, _ma_ub, _ma_lb, _ga_n, _ga_num, _ga_pr, _ga_pm);

            ArrayList resma = gg.runGA();
            double[] ma = (double[])resma[0];
            double maxV2 = (double)resma[1];

            //double[] ma = ge.runGA();
            int startI = _ar_n + _ma_n;
            //计算最终预测的x,2+3分别为ar和ma部分的参数个数
            double[] predicX = new double[_x.Length + 1];
            for (int i = 0; i < _x.Length; i++)
            {
                predicX[i] = 0;
                _ee[i] = 0;
            }
            for (int i = startI; i < _x.Length; i++)
            {
                _ee[i] = _e[i - startI];
            }


            //根据参数计算预测值
            double sum = 0;
            for (int i = startI; i < _x.Length + 1; i++)
            {
                sum = 0;
                //ar的计算
                for (int j = 0; j < ar.Length; j++)
                {
                    sum += ar[j] * _x[i - j - 1] + ma[j] * _ee[i - j - 1];
                }
                //ma的计算
                for (int j = 0; j < ma.Length; j++)
                {
                    sum += ma[j] * _ee[i - j - 1];
                }

                predicX[i] = sum;
            }


            double[] oridata = new double[predicX.Length + _count];//用来存储原长度预测数据和扩展月份的预测数据
            //赋值原长度预测数据
            for (int i = 0; i < _data.Length; i++)
            {
                oridata[i] = predicX[i];
            }

            for (int i = predicX.Length; i < predicX.Length + _count; i++)
            {

                //根据参数计算预测值

                sum = 0;
                //ar的计算
                for (int j = 0; j < ar.Length; j++)
                {
                    sum += ar[j] * oridata[i - j - 1];// +ma[j] * _ee[i - j - 1];
                }
                //ma的计算，只有一次可用
                if (i == predicX.Length)
                {
                    for (int j = 0; j < ma.Length; j++)
                    {
                        sum += ma[j] * _ee[i - j - 2];
                    }
                }

                oridata[i] = sum;

            }



            //ok
            //将差分转换为现实值
            double[] finaldata = new double[predicX.Length + _count];

            for (int i = 0; i < _data.Length; i++)
            {
                finaldata[i] = _data[i];
            }

            for (int i = startI; i < finaldata.Length; i++)
            {
                finaldata[i] = (1 + oridata[i]) * finaldata[i - 1];
            }

            //根据ar和ma部分的系数计算

            //double[] preout = new double[_count];
            //double[] oridata = new double[_data.Length + _count];//用来存储原始数据加上预测数据
            ////赋值原始数据
            //for (int i = 0; i < _data.Length; i++)
            //{
            //    oridata[i] = _data[i];
            //}
            //for (int i = _data.Length; i < _count; i++)
            //{

            //    //根据参数计算预测值
            //    double sum = 0;
            //    for (int i = 0; i < preout.Length + 1; i++)
            //    {
            //        sum = 0;
            //        //ar的计算
            //        for (int j = 0; j < ar.Length; j++)
            //        {
            //            sum += ar[j] * oridata[i - j - 1];// +ma[j] * _ee[i - j - 1];
            //        }
            //        //ma的计算
            //        if (i == _data.Length)
            //        {
            //            for (int j = 0; j < ma.Length; j++)
            //            {
            //                sum += ma[j] * _ee[i - j - 1];
            //            }
            //        }


            //        oridata[i] = sum;
            //    }

            //}

            ArrayList lastRes = new ArrayList();
            lastRes.Add(maxV1);
            lastRes.Add(maxV2);
            lastRes.Add(finaldata);


            return lastRes;

            //for (int i = 0; i < predicX.Length; i++)
            //{
            //    Console.WriteLine(predicX[i].ToString());
            //}


            ////最终得到的参数
            //Console.WriteLine("out :" + ar.Length);
            //Console.WriteLine(ar[0].ToString());
            //Console.WriteLine(ar[1].ToString());

            //Console.WriteLine("out :" + ma.Length);
            //Console.WriteLine(ma[0].ToString());
            //Console.WriteLine(ma[1].ToString());


            // Console.WriteLine(dt[2].ToString () );

            //Console.WriteLine(dt[2].ToString());

            //double _lb = -10;
            //double _ub = 10;


            //Console.ReadLine();
        }

    }

    //染色体类
    class chrom
    {
        public double[] args;//染色体中的各项，如[5,4,3,9,1,5]'，这是指有6个参数时的情况
        private double b = 3;//系统参数
        private double UB;
        private double LB;
        private int G;
        private int argN;//参数个数


        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="arg">染色体中各参数组成的数组</param>
        /// <param name="ub">参数上界</param>
        /// <param name="lb">参数下界</param>
        /// <param name="g">最大世代数</param>
        public chrom(double[] arg, double ub, double lb, int g)
        {
            args = arg;
            UB = ub;
            LB = lb;
            G = g;
            argN = arg.Length;
        }

        public chrom()
        {

        }

        /// <summary>
        /// 变异，改变当前染色体的args的值
        /// </summary>
        /// <param name="t">当前的世代数</param>
        public void toMut(int t)
        {
            Random rnd = new Random();
            for (int i = 0; i < argN; i++)
            {
                if (rnd.NextDouble() > 0.5)
                {
                    args[i] = args[i] + getdel(t, UB - args[i]);
                }
                else
                {
                    args[i] = args[i] - getdel(t, args[i] - LB);
                }

            }

        }

        private double getdel(int t, double y)
        {
            Random rnd = new Random();
            return y * (1 - Math.Pow(rnd.NextDouble(), Math.Pow(1 - t / G, b)));
        }

    }


    //遗传算法类
    //一类高精度非线性系统参数和阶次辨识的浮点遗传算法.pdf :使用其交叉和变异的公式
    //基于遗传算法的ARMA模型定阶新技术.pdf：使用其中的参数, 本程序中没有使用交叉概率，即每次循环都进行交叉。
    class clsga
    {
        private double[] _x;
        private int _n;
        private double _ub;
        private double _lb;
        private int _gn;
        private int _num;
        private double _pr;
        private double _pm;
        private List<chrom> Chroms = new List<chrom>();

        private chrom parent1; //父代精英1
        private chrom parent2; //父代精英2

        private chrom child1 = new chrom(); //子代1
        private chrom child2 = new chrom(); //子代2

        private List<chrom> parentChroms = new List<chrom>();

        private double maxVar = 0; //最大适应度函数


        private int k;//当前代数
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="x">样本数据，一维数组</param>
        /// <param name="n">变量个数，或者说是染色体中的参数个数，决定了要求解几个系数</param>
        /// <param name="ub">参数上界</param>
        /// <param name="lb">参数下界</param>
        /// <param name="gn">迭代代数</param>
        /// <param name="num">初始种群数目</param>
        /// <param name="pr">交叉概率</param>
        /// <param name="pm">变异概率</param>
        public clsga(double[] x, int n, double ub, double lb, int gn, int num, double pr, double pm)
        {
            _x = x;
            _n = n;
            _ub = ub;
            _lb = lb;
            _gn = gn;
            _num = num;
            _pr = pr;
            _pm = pm;

        }

        /// <summary>
        /// 运行GA
        /// </summary>
        /// <returns>返回求解得到的参数数组</returns>
        public ArrayList runGA()
        {
            //产生初始种群
            Random rnd = new Random();
            for (int i = 0; i < _num; i++)
            {
                //随机产生初始参数
                double[] b = new double[_n];
                for (int j = 0; j < _n; j++)
                {
                    // b[j] = _lb + rnd.NextDouble() * (_ub - _lb);//产生区间中的随机数
                    b[j] = rnd.Next((int)_lb, (int)_ub) + rnd.NextDouble();

                }
                Chroms.Add(new chrom(b, _ub, _lb, _gn));
            }


            //进入GA的循环
            //for (k = 0; k < _gn; k++) 
            //{
            //    //找出最优的两个父代
            //    this.setTwoParents();
            //    Console.WriteLine(Var(parent1 .args ));
            //    //Console.ReadLine();
            //    //生成并添加两个子代
            //    this.addTwoChild();
            //    this.delTwoBad();
            //}

            for (k = 0; k < _gn; k++)
            {
                //更新种群
                if (k > 0)
                    //Console.WriteLine(Var(parent1 .args ));
                    reNewGeneration();

            }




            //找出当前种群中的最优解
            //取得各染色体适应度函数值
            double[] vars = new double[Chroms.Count];
            double[] data;
            for (int i = 0; i < Chroms.Count; i++)
            {
                data = Chroms[i].args;
                vars[i] = Var(data);
            }

            //更新两个父代精英
            getTwoParent(vars);
            ArrayList rt = new ArrayList();
            rt.Add(parent1.args);
            rt.Add(maxVar);
            return rt;
        }


        /// <summary>
        /// 适应度函数，计算当前参数列表对应的适应度
        /// </summary>
        /// <param name="b">参数列表</param>
        /// <returns></returns>
        public double Var(double[] b)
        {
            double[] newx = new double[_x.Length];
            for (int i = 0; i < _x.Length; i++)
            {
                newx[i] = 0;// _x[i];
            }

            //根据参数计算预测值
            double sum = 0;
            for (int i = _n; i < _x.Length; i++)
            {
                sum = 0;
                for (int j = 0; j < _n; j++)
                {
                    sum += b[j] * _x[i - j - 1];
                }
                newx[i] = sum;

            }

            //for (int i = 0; i < _x.Length; i++) { 
            //    Console.Write(newx[i]);
            //    Console.Write(",");
            //}
            //Console.WriteLine("");

            //for (int i = 0; i < _x.Length; i++) { 
            //    Console.Write(_x[i]);
            //    Console.Write(",");
            //}
            //Console.WriteLine("");

            //Console.ReadLine();

            double avg_x = 0;
            for (int i = _n; i < _x.Length; i++)
            {
                avg_x += _x[i];
            }
            avg_x = avg_x / (_x.Length);

            double sum1 = 0;
            double sum2 = 0;
            for (int i = _n; i < _x.Length; i++)
            {
                sum1 += Math.Pow((_x[i] - newx[i]), 2);
            }

            for (int i = _n; i < _x.Length; i++)
            {
                sum2 += Math.Pow((_x[i] - avg_x), 2);
            }
            //Console.WriteLine(1 / (sum1 / sum2));
            if (maxVar < (sum2 / sum1))
            {
                maxVar = sum2 / sum1;
            }

            return 1 / (sum1 / sum2);
        }


        //生成两个子代
        private void geTwoChild(chrom p1, chrom p2)
        {
            //根据parent1和parent2进行交叉，然后各自变异，生成两个子代
            double[] cdata1 = new double[_n];
            double[] cdata2 = new double[_n];

            Random rnd = new Random();

            //以一定的概率进行交叉
            if (rnd.NextDouble() <= _pr)
            {
                double r;
                for (int i = 0; i < _n; i++)
                {
                    r = rnd.NextDouble();
                    cdata1[i] = r * p2.args[i] + (1 - r) * p1.args[i];
                    cdata2[i] = r * p1.args[i] + (1 - r) * p2.args[i];
                }

            }
            else
            {
                cdata1 = p1.args;
                cdata2 = p2.args;
            }

            child1.args = cdata1;
            child2.args = cdata2;


            //以一定概率进行变异
            if (rnd.NextDouble() <= _pm)
            {
                child1.toMut(k);
                child2.toMut(k);
            }

        }

        private void getTwoParent(double[] fitlst)
        {
            //确定两个父代精英
            //找出fitlst的最大值对应的index
            int laIndex = 0;
            double lav = fitlst[0];
            for (int i = 0; i < fitlst.Length; i++)
            {
                if (lav < fitlst[i])
                {
                    lav = fitlst[i];
                    laIndex = i;
                }
            }

            parent1 = new chrom(Chroms[laIndex].args, _ub, _lb, _gn);

            //找出次大值
            int laIndex2 = 0;
            double lav2 = fitlst[0];
            if (laIndex == 0)
            {
                laIndex2 = 1;
                lav2 = fitlst[1];
            }

            for (int i = 0; i < fitlst.Length; i++)
            {
                if (lav2 < fitlst[i] && laIndex != i)
                {
                    lav2 = fitlst[i];
                    laIndex2 = i;
                }
            }

            parent2 = new chrom(Chroms[laIndex2].args, _ub, _lb, _gn);
            //Console.WriteLine(lav +";"+lav2 );

        }


        /// <summary>
        /// 根据当前种群，以及其中各染色体的适应代函数值，计算出相应的概率区间，并选择出num个父代，根据这些父代进行交叉变异后生成num个子代。
        /// </summary>
        private void reNewGeneration()
        {

            //一、选择
            //取得各染色体适应代函数值
            double[] vars = new double[Chroms.Count];
            double[] data;
            for (int i = 0; i < Chroms.Count; i++)
            {
                data = Chroms[i].args;
                vars[i] = Var(data);
                //Console.Write(vars[i]+" , ");
            }
            //Console.WriteLine("");
            //Console.ReadLine();
            //更新两个父代精英
            getTwoParent(vars);


            //将适应度函数值归一化
            double sumf = 0;
            for (int i = 0; i < vars.Length; i++)
            {
                sumf += vars[i];
            }
            for (int i = 0; i < vars.Length; i++)
            {
                vars[i] = vars[i] / sumf;

            }


            //产生概率区间
            List<partV> vp = new List<partV>();
            for (int i = 0; i < vars.Length; i++)
            {
                partV vs = new partV();
                vs.ind = i;
                if (i == 0)
                {
                    vs.lbv = 0;
                    vs.ubv = vars[0];
                }
                else if (i == vars.Length - 1)
                {
                    vs.lbv = vp[i - 1].ubv;
                    vs.ubv = 1;
                }
                else
                {
                    vs.lbv = vp[i - 1].ubv;
                    vs.ubv = vs.lbv + vars[i];
                }
                vp.Add(vs);
            }

            Random rnd = new Random();
            parentChroms.Clear();
            //parentChroms.Add(new chrom ( parent1.args ,_ub,_lb,_gn) );
            //parentChroms.Add(new chrom ( parent2.args ,_ub,_lb,_gn) );

            //选择num-2个父代，减2的原因是已经挑选出了两个精英保留的父代。，比如种群数为10，则这里只要选择8个父代，然后通过这8个父代生成8个子代，再加上之前精英选择的两个父代，成为数目为10的新一代种群。
            for (int i = 0; i < _num - 2; i++)
            {
                double tempr = rnd.NextDouble();
                for (int j = 0; j < vp.Count; j++)
                {
                    if (tempr > vp[j].lbv && tempr <= vp[j].ubv)
                    {
                        parentChroms.Add(new chrom(Chroms[vp[j].ind].args, _ub, _lb, _gn));
                        //Console.Write(vp[j].ind + ",");
                    }
                }
            }
            //Console.WriteLine("");
            //清空上一代种群
            Chroms.Clear();

            //先加入上一代的保留精英
            Chroms.Add(new chrom(parent1.args, _ub, _lb, _gn));
            Chroms.Add(new chrom(parent2.args, _ub, _lb, _gn));

            //二、交叉和变异，生成其它子代
            for (int i = 0; i < parentChroms.Count; i = i + 2)
            {
                //每二个父代生成两个子代
                geTwoChild(parentChroms[i], parentChroms[i + 1]);
                Chroms.Add(new chrom(child1.args, _ub, _lb, _gn));
                Chroms.Add(new chrom(child2.args, _ub, _lb, _gn));

            }

            //至此新的种群生成了。


        }


        /// <summary>
        /// 记录各染色体的概率区间
        /// </summary>
        struct partV
        {
            public int ind; //编号
            public double ubv; //概率上界
            public double lbv; //概率下界

        }

        /// <summary>
        /// 根据系数b得到ar部分的模型，并进行预测，将(真实值-预测值)形成的误差数组，返回。
        /// </summary>
        /// <param name="b">ar部分的参数</param>
        /// <returns>误差数组，注意，原数组 _x 有_n个元素，假如进行ar部分有3个系数，则误差数组应该有n-3个元素，因为开头的几个元素无法预测出来</returns>
        public double[] getE(double[] b)
        {
            double[] newx = new double[_x.Length];
            int _n = b.Length;
            for (int i = 0; i < _x.Length; i++)
            {
                newx[i] = 0;// _x[i];
            }

            //根据参数计算预测值
            double sum = 0;
            for (int i = _n; i < _x.Length; i++)
            {
                sum = 0;
                for (int j = 0; j < _n; j++)
                {
                    sum += b[j] * _x[i - j - 1];
                }
                newx[i] = sum;
            }


            for (int i = 0; i < _x.Length; i++)
            {
                newx[i] = _x[i] - newx[i];
            }

            double[] res = new double[_x.Length - _n];
            for (int i = 0; i < res.Length; i++)
            {
                res[i] = newx[i + _n];
            }

            return res;

        }



    }
}
