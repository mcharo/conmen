using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConMen
{
    internal class Menu
    {
        [MenuItem("First Method")]
        public static void FirstMethod()
        {
            Console.WriteLine("You have called firstMethod()");
        }

        [MenuItem("Second Method")]
        public static void The2ndMethod()
        {
            Console.WriteLine("from the2ndMethod!!()");
        }

        [MenuItem("Third Method")]
        public static void ThrirdMethod()
        {
            Console.WriteLine("3rd()");
        }

        [MenuItem("Call me what you would like.")]
        public static void myNewMethod()
        {
            Console.WriteLine("This is AWESOME!!!");
        }
        //[MenuItem("This is my exit.")]
        //public static void Exit()
        //{
        //    Console.WriteLine("Exiting...");
        //    Console.ReadLine();
        //    Environment.Exit(0);
        //}

    }
}
