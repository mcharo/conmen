using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using System.Diagnostics;

namespace ConMen
{
    public class MenuBuilder
    {
        public MenuBuilder(object instanceOfMenuClass)
        {
            if (instanceOfMenuClass.GetType().IsInstanceOfType(instanceOfMenuClass))
            {
                Debug.WriteLine(instanceOfMenuClass.GetType().IsInstanceOfType(instanceOfMenuClass));
                PrintMenu(instanceOfMenuClass);
            }
            else
            {
                throw new InvalidOperationException("Menu object is not a class or struct instance.");
            }
        }
        internal static Dictionary<int, string[]> BuildMenu(object instanceOfMenuClass)
        {
            // keep count of the number of menu items
            int count = 1;
            Dictionary<int, string[]> menuItems = new Dictionary<int, string[]>();

            // use reflection to build a dictionary (ex. 1:firstMethod, 2: the2ndMethod) and the loop over dictionary to print menu.

            Type type = instanceOfMenuClass.GetType();
            MethodInfo[] info = type.GetMethods();
            bool exitFound = false;
            foreach (MethodInfo i in info)
            {
                foreach (object attr in i.GetCustomAttributes(true))
                {
                    if (attr is MenuItemAttribute)
                    {
                        MenuItemAttribute mi = attr as MenuItemAttribute;
                        menuItems.Add(count, new string[] {mi.Name, i.Name});
                        if (i.Name == "Exit")
                        {
                            exitFound = true;
                        }
                        count++;
                    }
                }
            }
            if (!exitFound)
            {
                Debug.WriteLine("Count: {0}", count);
                menuItems.Add(count, new string[] { "Exit", "defaultExit" });
            }
            return menuItems;
        }

        internal void PrintMenu(object instanceOfMenuClass)
        {
            Dictionary<int, string[]> menuItems = BuildMenu(instanceOfMenuClass);
            while (true)
            {
                Console.WriteLine("Please select a number from the menu below:");
                foreach (KeyValuePair<int, string[]> pair in menuItems)
                {
                    Console.WriteLine(pair.Key + ". " + pair.Value[0]);
                }
                Console.Write("Your selection: ");
                try
                {
                    int selection = int.Parse(Console.ReadLine());
                    if (selection <= menuItems.Count)
                    {
                        Console.WriteLine("\nYou selected to run: " + menuItems[selection][0]);

                        Type type = instanceOfMenuClass.GetType();
                        if (menuItems[selection][1] == "defaultExit")
                        {
                            Environment.Exit(0);
                        }

                        MethodInfo info = type.GetMethod(menuItems[selection][1]);
                        
                        info.Invoke(instanceOfMenuClass, null);
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
