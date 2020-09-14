using System;
using System.Collections.Generic;

namespace Ex04.Menus.Delegates
{
    public class MenuItem
    {
        // Constants:
        private const string k_Back = "Back";
        private const int k_BackIndex = 0;

        // Data Members:
        private readonly string r_HeadLine;
        private List<MenuItem> m_SubMenu;
        private MenuItem m_PreviousItem;
        public event Action MethodWasActivated;
        public event Action BackWasClicked;
        public event Action<MenuItem> OptionWasClicked;
        private bool m_IsLeaf;

        // Constructor
        public MenuItem(string i_HeadLine)
        {
            m_SubMenu = new List<MenuItem>();
            r_HeadLine = i_HeadLine;
            m_PreviousItem = null;
            m_IsLeaf = true;
        }

        // Propertries
        public string HeadLine
        {
            get
            {
                return r_HeadLine;
            }
        }

        public List<MenuItem> SubMenu
        {
            get
            {
                return m_SubMenu;
            }
        }

        public MenuItem PreviousItem
        {
            get
            {
                return m_PreviousItem;
            }
        }

        public bool IsLeaf
        {
            get
            {
                return m_IsLeaf;
            }

            set
            {
                m_IsLeaf = value;
            }
        }

        public void AddToSubMenu(params MenuItem[] i_MenuItemsToAdd)
        {
            int indexOfMenuItem = 0;
            IsLeaf = false;

            foreach (MenuItem menuItem in i_MenuItemsToAdd)
            {
                m_SubMenu.Add(menuItem);
                m_SubMenu[indexOfMenuItem].m_PreviousItem = this;
                indexOfMenuItem++;
            }
        }

        public void ShowSubMenuItem()
        {
            int i = 1;

            Console.WriteLine(HeadLine);
            foreach (MenuItem menuItem in m_SubMenu)
            {
                Console.WriteLine(string.Format("{0}. {1}", i, menuItem.HeadLine));
                i++;
            }

            Console.WriteLine("{0}. {1}", k_BackIndex, k_Back);
        }

        public void OnActiveMethod()
        {
            MethodWasActivated?.Invoke();
        }

        public void ChooseOptionToClick(int i_SubMenuChosice)
        {
            if (i_SubMenuChosice == 0)
            {
                OnBackWasClicked();
            }
            else
            {
                OnOptionWasClicked();
            }
        }

        protected virtual void OnBackWasClicked()
        {
            if (BackWasClicked != null)
            {
                BackWasClicked.Invoke();
            }
        }

        protected virtual void OnOptionWasClicked()
        {
            if (OptionWasClicked != null)
            {
                OptionWasClicked.Invoke(this);
            }
        }
    }
}
