
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
/*
思路：不去做复杂、多层的假设与推理，根据每道题中最基本的条件：“四个命题只有一个为真”，对所有的组合答案进行筛选，经过10道题的筛选，剩下的就是所要求解答案。
*/
namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {
            //计时器
            Stopwatch wc = new Stopwatch();
            wc.Start();

            string[] options = { "A", "B", "C", "D" };

            List<string> list = new List<string>();
            //【1】先列出10道题答案的所有可能的集合，一共有4^10条
            string[] record = new string[10];

            //使用递归执行多个嵌套的for循环生成集合
            //还可以用Linq的写法更简单，见页尾
            for (int i = 0; i < options.Length; i++)
            {
                record[0] = options[i];
                M1(options, record, 0 + 1, list);
            }
            Console.WriteLine($"集合总数：{list.Count}");

            //【2】遍历集合，按照每题给出的条件进行筛选
            List<string> listStr = new List<string>();
            foreach (var str in list)
            {
                if (!T2(str))
                    continue;
                if (!T3(str))
                    continue;
                if (!T4(str))
                    continue;
                if (!T5(str))
                    continue;
                if (!T6(str))
                    continue;
                if (!T7(str))
                    continue;
                if (!T8(str))
                    continue;
                if (!T9(str))
                    continue;
                if (!T10(str))
                    continue;
                listStr.Add(str);
            }

            //输出结果
            Console.WriteLine($"剩余数量：{listStr.Count}");
            if (listStr.Count == 1)
                Console.WriteLine($"最终答案：{listStr.First()}");
            Console.WriteLine($"运行时间：{wc.Elapsed}");
            Console.ReadKey();
        }

        //递归方法用于实现嵌套for循环，record:把string[]当char[]用了
        static void M1(string[] options, string[] record, int num, List<string> list)
        {
            for (int j = 0; j < options.Length; j++)
            {
                record[num] = options[j];
                if (num == record.Length - 1)
                {
                    string[] tmp = new string[record.Length];
                    record.CopyTo(tmp, 0);
                    list.Add(string.Join("",tmp));
                }
                else
                {
                    M1(options, record, num + 1, list);
                }
            }
        }

        //题2 2=A且5=C，2=B且5=D，2=C且5=A，2=C且5=B中，仅存一个为真
        static bool T2(string str)
        {
            bool[] bools = new bool[4];
            bools[0] = str[1] == 'A' && str[4] == 'C';
            bools[1] = str[1] == 'B' && str[4] == 'D';
            bools[2] = str[1] == 'C' && str[4] == 'A';
            bools[3] = str[1] == 'D' && str[4] == 'B';
            if (1 == TrueCount(bools))
                return true;
            else
                return false;
        }

        //题3 2，3，4，6四题中有三个答案相同，模式为 xxAx或BBBx或CxCC或DDxD
        static bool T3(string str)
        {
            bool[] bools = new bool[4];
            bools[0] = str[3] == 'A'&&str[1]==str[2]&&str[1]==str[5];
            bools[1] = str[1] == str[2]&& str[1] == str[3]&&str[1]=='B'&&str[5]!='B';
            bools[2] = str[1] == str[3]&& str[1] == str[5]&&str[1]=='C'&&str[2]!='C';
            bools[3] = str[1] == str[2]&& str[1] == str[5]&&str[1]=='D'&&str[3]!='D';
            if (bools[0] || bools[1] || bools[2] || bools[3])
                return true;
            else
                return false;
        }

        //题4 4=A且1=5，4=B且2=7，4=C且1=9，4=D且6=10，仅存一个为真
        static bool T4(string str)
        {
            bool[] bools = new bool[4];
            bools[0] = str[0] == str[4] && str[3] == 'A';
            bools[1] = str[1] == str[6] && str[3] == 'B';
            bools[2] = str[0] == str[8] && str[3] == 'C';
            bools[3] = str[5] == str[9] && str[3] == 'D';
            if (1 == TrueCount(bools))
                return true;
            else
                return false;
        }

        //题5 5=A=8,5=B=4,5=C=9,5=D=7,仅存一个为真
        static bool T5(string str)
        {
            bool[] bools = new bool[4];
            bools[0] = str[4] == str[7]&&str[4]=='A';
            bools[1] = str[4] == str[3]&&str[4]=='B';
            bools[2] = str[4] == str[8]&&str[4]=='C';
            bools[3] = str[4] == str[6]&&str[4]=='D';
            if (1 == TrueCount(bools))
                return true;
            else
                return false;
        }

        //题6 8=A=2=4，8=B=1=6,8=C=3=10,8=D=5=9,仅存一个为真
        static bool T6(string str)
        {
            bool[] bools = new bool[4];
            bools[0] = str[7] == str[1] && str[7] == str[3] && str[5] == 'A';
            bools[1] = str[7] == str[0] && str[7] == str[5] && str[5] == 'B';
            bools[2] = str[7] == str[2] && str[7] == str[9] && str[5] == 'C';
            bools[3] = str[7] == str[4] && str[7] == str[8] && str[5] == 'D';
            if (1 == TrueCount(bools))
                return true;
            else
                return false;
        }

        //题7 7=A且C为最少选项，7=B且B为最少选项，7=C且A为最少选项，7=D且D为最少选项
        static bool T7(string str)
        {
            bool[] bools = new bool[4];
            bools[0] = str[6] == 'A'&&MinOptCount(str)[0]=='C';
            bools[1] = str[6] == 'B' && MinOptCount(str)[0] == 'B';
            bools[2] = str[6] == 'C' && MinOptCount(str)[0] == 'A';
            bools[3] = str[6] == 'D' && MinOptCount(str)[0] == 'D';
            if (1 == TrueCount(bools))
                return true;
            else
                return false;
        }

        //题8 8=A且A-7!=1，8=B且A-5!=1，8=C且A-2!=1，8=D且A-10!=1，仅存一个为真
        static bool T8(string str)
        {
            bool[] bools = new bool[4];
            bools[0] = Math.Abs(str[6] - str[0])!=1;
            bools[1] = Math.Abs(str[4] - str[0]) != 1;
            bools[2] = Math.Abs(str[1] - str[0]) != 1;
            bools[3] = Math.Abs(str[9] - str[0]) != 1;
            if (1 == TrueCount(bools))
                return true;
            else
                return false;
        }

        //题9(1=6且5与2，6，9，10仅存一个不相等)或者(1不等6且5与2，6，9，10中仅存一个相等)
        static bool T9(string str)
        {
            //if1==6,则5和 269 10存在一个不等
            bool[] bools = new bool[4];
            bools[0] = str[4] != str[5] && str[8] == 'A';
            bools[1] = str[4] != str[9] && str[8] == 'B';
            bools[2] = str[4] != str[1] && str[8] == 'C';
            bools[3] = str[4] != str[8] && str[8] == 'D';
            bool _1_6_true = TrueCount(bools)==1;

            bools[0] = str[4] == str[5] && str[8] == 'A';
            bools[1] = str[4] == str[9] && str[8] == 'B';
            bools[2] = str[4] == str[1] && str[8] == 'C';
            bools[3] = str[4] == str[8] && str[8] == 'D';
            bool _1_6_false = TrueCount(bools)==1;

            if ((str[0] == str[6] && _1_6_true) || (str[0] != str[6] && _1_6_false))
                return true;
            else
                return false;
        }

        //题10 最多选项次数-最少选项次数等于1
        static bool T10(string str)
        {
            bool a= MaxCount(str)-MinOptCount(str)[1]>=1;
            bool b = MaxCount(str) - MinOptCount(str)[1] <= 4;
            if (a && b)
                return true;
            else
                return false;
        }

        //计算条件为true的数量
        static int TrueCount(Boolean[] bools)
        {
            int trueCount = 0;
            for (int i = 0; i < bools.Length; i++)
            {
                if (bools[i])
                    trueCount++;
            }
            return trueCount;
        }

        //返回值int[0]代表选项字符，int[1]代表最小数量
        static int[] MinOptCount(string str)
        {
            Dictionary<char, int> dic = new Dictionary<char, int>();
            dic.Add('A', 0);
            dic.Add('B', 0);
            dic.Add('C', 0);
            dic.Add('D', 0);
            for (int i = 0; i < str.Length; i++)
                dic[str[i]]++;
            int min=Math.Min(Math.Min(dic['A'], dic['B']), Math.Min(dic['C'], dic['D']));
            foreach (var key in dic.Keys)
            {
                if (dic[key] == min)
                {
                    int[] arr = { key,min}; ;
                    return arr;
                }
            }
            return new int[]{ 999,999 };
            
        }

        //返回最大数量
        static int MaxCount(string str)
        {
            int rep = 0;
            List<char> list = new List<char>();
            for (int i = 0; i < str.Length; i++)
            {
                list.Add(str[i]);
                rep = Math.Max(rep, list.Count(n => n == str[i]));
            }
            return rep;
        }


        #region
        /* 分析
         * 题2 2=A且5=C，2=B且5=D，2=C且5=A，2=C且5=B中，仅存一个为真
         * 题3 2，3，4，6四题中有三个答案相同，模式为 xxAx或BBBx或CxCC或DDxD
         * 题4 4=A且1=5，4=B且2=7，4=C且1=9，4=D且6=10，仅存一个为真
         * 题5 5=A=8,5=B=4,5=C=9,5=D=7,仅存一个为真
         * 题6 8=A=2=4，8=B=1=6,8=C=3=10,8=D=5=9,仅存一个为真
         * 题7 7=A且C为最少选项，7=B且B为最少选项，7=C且A为最少选项，7=D且D为最少选项，
         * 题8 8=A且A-7!=1，8=B且A-5!=1，8=C且A-2!=1，8=D且A-10!=1，仅存一个为真
         * 题9 (1=6且5与2，6，9，10仅存一个不相等)或者(1不等6且5与2，6，9，10中仅存一个相等)
         * 题10 最多选项次数-最少选项次数等于1
         */

        /* 2018刑侦科推理试题
        1.这道题的答案是：
        A.A B.B C.C D.D
        2.第5题的答案是：
        A.C B.D C.A D.B
        3.以下选项中哪一题的答案与其他三项不同：
        A.第3题   B.第6题   C.第2题   D.第4题
        4.以下选项中哪两道题的答案相同：
        A.1,5题 B.第2,7题 C.第1,9题 D.第6,10题
        5.以下选项中哪一题的答案与本题相同：
        A.第8题   B.第4题   C.第9题   D.第7题
        6.以下选项中哪两题的答案与第8题相同：
        A.第2,4题  B.第1,6题  C.第3，10题  D.第5,9题
        7.在此十道题中，被选中次数最少的选项字母为：
        A.C B.B C.A D.D
        8.以下选项中哪一题的答案与第1题的答案在字母中不相连：
        A.第7题   B.第5题   C.第2题   D.第10题
        9.已知“第1题与第6题的答案相同”与“第X题与第5题的答案相同”的真假性相反，那么X为：
        A.第6题   B.第10题  C.第2题   D.第9题
        10.在此10题中，ABCD四个字母出现次数最少者的差为：
        A.3 B.2 C.4 D.1
         */
        #endregion

        //var list = from s0 in options
        //         from s1 in options
        //         from s2 in options
        //         from s3 in options
        //         from s4 in options
        //         from s5 in options
        //         from s6 in options
        //         from s7 in options
        //         from s8 in options
        //         from s9 in options
        //         select s0 + s1 + s2 + s3 + s4 + s5 + s6 + s7 + s8 + s9;
    }
}
