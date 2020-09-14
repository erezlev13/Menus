using System;
using System.Collections.Generic;
using System.Threading;

namespace Ex04.Menus.Interfaces
{
    public class MainMenu : IMenuItemClickedListener, IBackOptionListener
    {
        // Constants:
        private const string k_Exit = "Exit";
        private const int k_ExitIndex = 0;

        // Data Members:
        private readonly string r_Headline;
        private List<MenuItem> m_MenuItems;
        private MenuItem m_CurrentMenuItem;

        // Constructors:
        public MainMenu(string i_Headline)
        {
            r_Headline = i_Headline;
            m_MenuItems = new List<MenuItem>();
            m_CurrentMenuItem = null;
        }

        // Properties:
        public string Headline
        {
            get
            {
                return r_Headline;
            }
        }

        public List<MenuItem> MenuItems
        {
            get
            {
                return m_MenuItems;
            }

            set
            {
                m_MenuItems = value;
            }
        }

        public MenuItem CurrentMenuItem
        {
            get
            {
                return m_CurrentMenuItem;
            }
            
            set
            {
                m_CurrentMenuItem = value;
            }
        }

        // Methods:
        void IMenuItemClickedListener.MenuItemClicked(MenuItem i_MenuItemThatWasClicked)
        {
            Console.Clear();
            if (i_MenuItemThatWasClicked.IsLeaf)
            {
                i_MenuItemThatWasClicked.ActiveMethod();
                CurrentMenuItem = CurrentMenuItem.PreviousItem;
                Thread.Sleep(1500);
            }
            else
            {
                CurrentMenuItem = i_MenuItemThatWasClicked;
            }
        }

        void IBackOptionListener.BackClicked()
        {
            MenuItem previousMenuItem = CurrentMenuItem.PreviousItem;

            if (previousMenuItem != null)
            {
                Console.Clear();
                Thread.Sleep(1500);
            }

            CurrentMenuItem = previousMenuItem;
        }

        public void CreateMainMenu(params MenuItem[] i_MenuItemsToAdd)
        {
            foreach (MenuItem menuItem in i_MenuItemsToAdd)
            {
                MenuItems.Add(menuItem);
                menuItem.OptionListener = this;
                menuItem.BackOptionListener = this;
            }
        }

        public void Show()
        {
            navigateMenu();
        }

        private void printMainMenu()
        {
            int i = 1;

            Console.WriteLine(Headline);
            foreach (MenuItem item in MenuItems)
            {
                Console.WriteLine("{0}. {1}", i.ToString(), item.HeadLine);
                i++;
            }

            Console.WriteLine("{0}. {1}", k_ExitIndex, k_Exit);
        }

        private void navigateMenu()
        {
            printMainMenu();
            int menuOption = getMenuOption();

            while (menuOption != k_ExitIndex)
            {
                CurrentMenuItem = MenuItems[menuOption - 1];
                while (CurrentMenuItem != null)
                {
                    getToSubMenu();
                }

                Thread.Sleep(1500);
                Console.Clear();
                printMainMenu();
                menuOption = getMenuOption();
            }
        }

        private void getToSubMenu()
        {
            Thread.Sleep(1500);
            Console.Clear();
            CurrentMenuItem.ShowSubMenuItem();
            int subMenuOption = getMenuOption();

            if (subMenuOption != 0)
            {
                CurrentMenuItem.InitializeMenuItemListener(this);
                CurrentMenuItem.InitializeBackListener(this);
                CurrentMenuItem = CurrentMenuItem.SubMenu[subMenuOption - 1];
            }

            CurrentMenuItem.ChooseOptionToClick(subMenuOption);
        }

        private int getMenuOption()
        {
            int menuOptionIndex;

            Console.Write("Please choose option from the menu (or Back/Exit): ");
            while (!int.TryParse(Console.ReadLine(), out menuOptionIndex))
            {
                Console.WriteLine("Please enter only valid index, and numbers only.");
            }

            return menuOptionIndex;
        }
    }
}
