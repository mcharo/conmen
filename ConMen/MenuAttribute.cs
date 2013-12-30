using System;

namespace ConMen
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
