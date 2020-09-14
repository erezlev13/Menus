using System;
using System.Collections.Generic;
using System.Threading;

namespace Ex04.Menus.Delegates
{
    public class MainMenu
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
        public void CreateMainMenu(params MenuItem[] i_MenuItemsToAdd)
        {
            foreach (MenuItem menuItem in i_MenuItemsToAdd)
            {
                MenuItems.Add(menuItem);
                menuItem.OptionWasClicked += menuItem_OptionWasClicked;
                menuItem.BackWasClicked += menuItem_BackWasClicked;
                menuItem.IsSubscribed = true;
            }
        }

        private void menuItem_BackWasClicked()
        {
            MenuItem previousMenuItem = CurrentMenuItem.PreviousItem;

            if (previousMenuItem != null)
            {
                Console.Clear();
            }

            CurrentMenuItem = previousMenuItem;
        }

        private void menuItem_OptionWasClicked(MenuItem i_MenuItemThatWasClicked)
        {
            Console.Clear();
            if (i_MenuItemThatWasClicked.IsLeaf)
            {
                i_MenuItemThatWasClicked.OnActiveMethod();
                Thread.Sleep(500);
                CurrentMenuItem = CurrentMenuItem.PreviousItem;
            }
            else
            {
                CurrentMenuItem = i_MenuItemThatWasClicked;
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
                Console.WriteLine("{0}. {1}", i, item.HeadLine);
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

                Console.Clear();
                printMainMenu();
                menuOption = getMenuOption();
            }
        }

        private void getToSubMenu()
        {
            Console.Clear();
            CurrentMenuItem.ShowSubMenuItem();
            int subMenuOption = getMenuOption();

            if (subMenuOption != 0)
            {
                CurrentMenuItem = CurrentMenuItem.SubMenu[subMenuOption - 1];
            }

            if (!CurrentMenuItem.IsSubscribed)
            {
                subscribeBackListener();
            }

            CurrentMenuItem.ChooseOptionToClick(subMenuOption);
        }

        private void subscribeBackListener()
        {
            CurrentMenuItem.BackWasClicked += menuItem_BackWasClicked;
            CurrentMenuItem.OptionWasClicked += menuItem_OptionWasClicked;

            CurrentMenuItem.IsSubscribed = true;
        }

        private int getMenuOption()
        {
            int menuOptionIndex;
            string userAnswer;

            Console.Write("Please choose option from the menu (or Back/Exit): ");
            userAnswer = Console.ReadLine();
            while (!isValidNumericInput(userAnswer, out menuOptionIndex) || !isValidMenuIndex(menuOptionIndex))
            {
                Console.WriteLine("Please enter only valid index, and numbers only.");
                userAnswer = Console.ReadLine();
            }

            return menuOptionIndex;
        }

        private bool isValidNumericInput(string i_UserAnswer, out int o_MenuIndex)
        {
            return int.TryParse(i_UserAnswer, out o_MenuIndex);
        }

        private bool isValidMenuIndex(int i_MenuOptionIndex)
        {
            return i_MenuOptionIndex <= MenuItems.Count;
        }
    }
}
