using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using System.Diagnostics;

namespace ConMen
{
    class Program
    {

        public static void Main(string[] args)
        {
            Dictionary<int, string> menuItems = new Dictionary<int, string>();

            menuItems = BuildMenu(menuItems);
            while (true)
            {
                Console.WriteLine("Please select a number from the menu below:");
                PrintMenu(menuItems);            
                Console.Write("Your selection: ");
                try
                {
                    int selection = int.Parse(Console.ReadLine());
                    if (selection < menuItems.Count)
                    {
                        Console.WriteLine("\nYou selected to run: " + menuItems[selection]);

                        Type type = typeof(Program);
                        MethodInfo info = type.GetMethod(GetMethodByAttributeName(menuItems[Convert.ToInt16(selection)]));
                        info.Invoke(null, new object[] { });
                    }
                    else
                    {
                        // on invalid input, clear and reset menu
                        Console.Clear();
                        continue;
                    }
                }
                catch (Exception e)
                {
                    // on invalid input, clear and reset menu
                    Debug.WriteLine(e);
                    Console.Clear();
                    continue;
                }
                // on valid input, pause to view output
                Console.WriteLine("Press <Enter> to continue.");
                Console.ReadLine();
                Console.Clear();
            }
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
                        MenuItemAttribute mi = attr as MenuItemAttribute;
                        menuItems.Add(count, mi.Name);
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

        static string GetMethodByAttributeName(string attrName)
        {
            Type type = typeof(Program);
            MethodInfo[] info = type.GetMethods();

            foreach (MethodInfo i in info)
            {
                foreach (object attr in i.GetCustomAttributes(true))
                {
                    if (attr is MenuItemAttribute)
                    {
                        MenuItemAttribute mi = attr as MenuItemAttribute;
                        if (mi.Name == attrName)
                        {
                            return i.Name.ToString();
                        }
                    }
                }
            }

            return String.Empty;
        }

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
        public static void exitMenu()
        {
            Environment.Exit(0);
        }


    }
}
