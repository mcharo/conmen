using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using System.Diagnostics;

namespace ConMen
{
    internal class Program
    {

        public static void Main(string[] args)
        {
            MyStruct daStruct = new MyStruct();
            daStruct.anumber = 20;
            MenuBuilder mb = new MenuBuilder(daStruct, false);
        }

        struct MyStruct : IMenu
        {
            public int anumber { get; set; }
            [MenuItem("test")]
            public void test()
            {
                Console.WriteLine("test {0}", anumber);
            }
            public void Exit()
            {
                Console.WriteLine("Exiting...");
                Console.ReadLine();
                Environment.Exit(0);
            }

        }
    }

}
