using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using System.Diagnostics;

namespace ConMen
{
    class MenuBuilder
    {
        Menu _menu;
        public MenuBuilder()
        {
            _menu = new Menu();
            PrintMenu();
        }

        public static Dictionary<int, string> BuildMenu()
        {
            // keep count of the number of menu items
            int count = 0;
            Dictionary<int, string> menuItems = new Dictionary<int,string>();

            // use reflection to build a dictionary (ex. 1:firstMethod, 2: the2ndMethod) and the loop over dictionary to print menu.
            Type type = typeof(Menu);
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

        internal static string GetMethodByAttributeName(string attrName)
        {
            Type type = typeof(Menu);
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


        public void PrintMenu()
        {
            Dictionary<int, string> menuItems = new Dictionary<int, string>();
            menuItems = BuildMenu();
            while (true)
            {
                Console.WriteLine("Please select a number from the menu below:");
                foreach (KeyValuePair<int, string> pair in menuItems)
                {
                    Console.WriteLine(pair.Key + ". " + pair.Value);
                }
                Console.Write("Your selection: ");
                try
                {
                    int selection = int.Parse(Console.ReadLine());
                    if (selection < menuItems.Count)
                    {
                        Console.WriteLine("\nYou selected to run: " + menuItems[selection]);

                        Type type = typeof(Menu);
                        MethodInfo info = type.GetMethod(GetMethodByAttributeName(menuItems[selection]));
                        info.Invoke(_menu, null);
                        //info.Invoke(null, new object[] { });
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
    }
}
