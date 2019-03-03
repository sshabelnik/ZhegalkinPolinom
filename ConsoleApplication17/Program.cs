using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace ConsoleApplication17
{
    class Program
    {
        public static void Main(string[] args)
        {
            ZhegalkinPolinom zh = new ZhegalkinPolinom("x1x2x3+1+x2x3");
            zh.Insert(new Konj(12));
            zh.SortByLength();
            var temp = zh.PolinomWith(1);
            Console.WriteLine(zh.Polinom[0].Info);
            Console.WriteLine(zh.Polinom[1].Info);
            Console.WriteLine(zh.Polinom[2].Info);
            Console.WriteLine(zh.Polinom[3].Info);
            Console.WriteLine(zh.ToString());
            Console.WriteLine(temp.ToString());
        }
    }
    
    public class ZhegalkinPolinom
    {
        public List<Konj> Polinom;

        public ZhegalkinPolinom(string s)
        {
            Polinom = new List<Konj>();
            var a = s.Split('+');
            foreach (var i in a)
            {
                string result = null;
                if (i == "1")
                {
                    result += 0;
                    Polinom.Add(new Konj(int.Parse(result)));
                    continue;
                }
                foreach (var j in i)
                {
                    if (Char.IsDigit(j))
                        result += j;    
                }
                if (result != null)
                    Polinom.Add(new Konj(int.Parse(result)));
            }

        }
        
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            foreach (var i in Polinom)
            {
                sb.Append(i.Info + " ");
            }

            var temporary = sb.ToString();
            var temp = temporary.Split(new[] {' '}, StringSplitOptions.RemoveEmptyEntries);
            string result = null;
            int counter = 1;
            foreach (var j in temp)
            {
                foreach (var z in j)
                {
                    if (z == '0')
                        result += 1;
                    else
                        result += "x" + z;
                }

                result += "+";
            }
            if (result != null)
                return result.TrimEnd('+');
            throw new Exception("Полином Жегалкина пуст");
        }

        public void Insert(Konj k)
        {
            foreach (var m in Polinom)
            {
                if (m.Info == k.Info)
                    throw new Exception("Полином уже содержит данный конъюнкт");
            }
            Polinom.Add(k);
        }

        public void SortByLength()
        {
            Polinom = (from j in Polinom orderby j.Info descending select j).ToList();
        }

        public ZhegalkinPolinom PolinomWith(int i)
        {
            string find = i.ToString();
            string result = null;
            foreach (var j in Polinom)
            {
                var temp = j.Info.ToString();
                if (temp.Contains(find))
                {
                    result += temp + "+";
                }
            }
            return new ZhegalkinPolinom(result.TrimEnd('+'));
        }
    }

    public class Konj : List<int>
    {
        public int Info;
        public Konj(int info)
        {
            Info = info;
        }
        
    }
}