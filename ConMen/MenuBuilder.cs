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

        bool _infiniteLoop;
        IMenu _instanceOfMenuClass;

        public MenuBuilder(IMenu instanceOfMenuClass, bool infiniteLoop)
        {
            if (instanceOfMenuClass.GetType().IsInstanceOfType(instanceOfMenuClass))
            {
                _infiniteLoop = infiniteLoop;
                _instanceOfMenuClass = instanceOfMenuClass;
            }
            else
            {
                throw new InvalidOperationException("Menu object is not a class or struct instance.");
            }
        }
        internal static Dictionary<int, string[]> BuildMenu(IMenu instanceOfMenuClass)
        {
            // keep count of the number of menu items
            int count = 1;
            Dictionary<int, string[]> menuItems = new Dictionary<int, string[]>();

            // use reflection to build a dictionary (ex. 1:firstMethod, 2: the2ndMethod) and the loop over dictionary to print menu.

            Type type = instanceOfMenuClass.GetType();
            MethodInfo[] info = type.GetMethods();
            string exitFound = "";
            foreach (MethodInfo i in info)
            {
                foreach (object attr in i.GetCustomAttributes(true))
                {
                    if (attr is MenuItemAttribute)
                    {
                        MenuItemAttribute mi = attr as MenuItemAttribute;
                        if (i.Name == "Exit")
                        {
                            exitFound = mi.Name;
                        }
                        else
                        {
                            menuItems.Add(count, new string[] { mi.Name, i.Name });
                            count++;
                        }
                    }
                }
            }
            if (exitFound != "")
            {
                menuItems.Add(count, new string[] { exitFound, "Exit" });
            }
            return menuItems;
        }

        public void PrintMenu()
        {
            Dictionary<int, string[]> menuItems = BuildMenu(_instanceOfMenuClass);
            

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

                        Type type = _instanceOfMenuClass.GetType();

                        MethodInfo info = type.GetMethod(menuItems[selection][1]);
                        
                        info.Invoke(_instanceOfMenuClass, null);
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
                if (!_infiniteLoop)
                {
                    break;
                }
            }
        }
    }
}
