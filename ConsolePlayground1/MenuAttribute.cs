using System;

namespace ConsolePlayground1
{
    [AttributeUsage(AttributeTargets.Method)]
    public class MenuItemAttribute : Attribute
    {
        private string name;

        public MenuItemAttribute(String name)
        {
            this.name = name;
        }
        public string Name
        {
            get { return name; }
        }
    }
}
