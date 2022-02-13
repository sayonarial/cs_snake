using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snake_Pro_Ver
{
    public class Menu
    {
        string Name;
        List<MenuItem> ItemsList = new List<MenuItem>();
        MenuItem Exit = new MenuItem("Exit", () => Environment.Exit(0));
        int SelectedItem = 0;
        bool KeyPressedFlag = false;
        bool MenuIsDisplayed = false;
        public bool EnterIsPressed = false;
        bool DisplaySnakeLogo = false;

        public Menu(string _name, bool _snakeLogo = false) {
            Name = _name;
            DisplaySnakeLogo = _snakeLogo;
        }

        public void AddItems(List<MenuItem> MenuItems)
        {
            foreach (var item in MenuItems)
            {
                ItemsList.Add(item);
            }
        }

        public void Show()
        {

            if(!MenuIsDisplayed)
            {
                
                Console.SetCursorPosition(0, 0);
                if (DisplaySnakeLogo)SnakeLogo.ShowSnakeLogo();
                ShowMenuInstructions();
                ShowCreator();
                Console.SetCursorPosition(0, 13);
                Console.WriteLine($"--------------------------------------  {Name}  ----------------------------------------");
                UpdateMenu();
                MenuIsDisplayed = true;
            }
            else if (MenuControll() == true)
            {
                UpdateMenu();
            }
            
        }

        void UpdateMenu()
        {
            
            int fecounter = 0;
            foreach (var item in ItemsList)
            {
                Console.SetCursorPosition(35, 15 + fecounter);
                if (fecounter == SelectedItem) Console.Write("--> ");
                else Console.Write("    ");
                item.Show();
                fecounter++;
            }
        }
        bool MenuControll()
        {
            EnterIsPressed = false;
            if (Console.KeyAvailable)
            {
                ConsoleKeyInfo key = Console.ReadKey();
                switch (key.Key)
                {
                   
                    case ConsoleKey.UpArrow:
                        if (SelectedItem == 0) SelectedItem = ItemsList.Count()-1;
                        else SelectedItem--;
                        break;
                    case ConsoleKey.DownArrow:
                        if (SelectedItem == ItemsList.Count() - 1) SelectedItem = 0;
                        else SelectedItem++;
                        break;
                    case ConsoleKey.Enter:
                        EnterIsPressed = true;
                        MenuIsDisplayed = false;
                        Console.Clear();
                        ItemsList[SelectedItem].Selected.Invoke();
                        return false;
                        break;
                }
                return true;
            }
            return false;
        }
        void ShowMenuInstructions()
        {
            
            Terminal.CenteredText("Use (↓) (↑) and Enter to navigate", 90, 25);
        }
        void ShowCreator()
        {
            Terminal.CenteredText("Yevgeniy Onufrak 2022",90,26);
        }
    }
}
