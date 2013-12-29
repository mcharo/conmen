using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace ConsolePlayground1
{
    class Program
    {

        

        public static void Main(string[] args)
        {
            Dictionary<int, string> menuItems = new Dictionary<int, string>();

            menuItems = BuildMenu(menuItems);
            Console.WriteLine("Please select a number from the menu below:");
            PrintMenu(menuItems);
            Console.Write("Your selection: ");
            string selection = Console.ReadLine();
            Console.WriteLine("\nYou selected to run: " + menuItems[Convert.ToInt16(selection)]);

            Type type = typeof(Program);
            MethodInfo info = type.GetMethod(menuItems[Convert.ToInt16(selection)]);
            info.Invoke(null, new object[] { });

            Console.ReadLine();
        }

        static Dictionary<int, string> BuildMenu(Dictionary<int, string> menuItems)
        {
            // keep count of the number of menu items
            int count = 0;

            // use reflection to build a dictionary (ex. 1:firstMethod, 2: the2ndMethod) and the loop over dictionary to print menu.
            Type type = typeof(Program);
            MethodInfo[] info = type.GetMethods();

            foreach (MethodInfo i in info)
            {
                foreach (object attr in i.GetCustomAttributes(true))
                {
                    if (attr is MenuItemAttribute)
                    {
                        menuItems.Add(count, i.Name.ToString());
                        count++;
                    }
                }
            }

            return menuItems; 
        }

        static void PrintMenu(Dictionary<int, string> menuItems)
        {
            foreach (KeyValuePair<int, string> pair in menuItems)
            {
                Console.WriteLine(pair.Key + ". " + pair.Value);
            }
        }

        [MenuItem]
        public static void FirstMethod()
        {
            Console.WriteLine("You have called firstMethod()");
        }
        [MenuItem]
        public static void The2ndMethod()
        {
            Console.WriteLine("from the2ndMethod!!()");
        }

        [MenuItem]
        public static void ThrirdMethod()
        {
            Console.WriteLine("3rd()");
        }
    }
}
