using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snake_Pro_Ver
{
    public class MenuItem
    {

        public string Name { get; }
        public Action Selected { get; }
       
        public List<string> ItemOptions {get;}
        int SelectedOptionIndex;

        public MenuItem(string name, Action selected)
        {
            Name = name;
            Selected = selected;
        }



        public void Show()
        {
            Console.Write(Name);
            Console.WriteLine();
        }

    }
}
