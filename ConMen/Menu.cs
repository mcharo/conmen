using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConMen
{
    class Menu
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
        [MenuItem("Exit")]
        public void exitMenu()
        {
            Environment.Exit(0);
        }
    }
}
