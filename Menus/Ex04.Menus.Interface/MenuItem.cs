using System;
using System.Collections.Generic;

namespace Ex04.Menus.Interfaces
{
    public interface IMethodActivator
    {
        void CallMethod();
    }

    public interface IMenuItemClickedListener
    {
        void MenuItemClicked(MenuItem i_MenuItemThatWasClicked);
    }

    public interface IBackOptionListener
    {
        void BackClicked();
    }

    public class MenuItem
    {
        // Constants:
        private const string k_Back = "Back";
        private const int k_BackIndex = 0;

        // Data Members:
        private readonly string r_HeadLine;
        private List<MenuItem> m_SubMenu;
        private MenuItem m_PreviousItem;
        private IMethodActivator m_MethodAction;
        private IBackOptionListener m_BackeWasClicked;
        private IMenuItemClickedListener m_OptionWasClicked;
        private bool m_IsLeaf;

        // Constructor
        public MenuItem(string i_HeadLine)
        {
            m_SubMenu = new List<MenuItem>();
            r_HeadLine = i_HeadLine;
            m_PreviousItem = null;
            m_IsLeaf = true;
        }

        // Propetries
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

        public IMethodActivator MethodActivator
        {
            get
            {
                return m_MethodAction;
            }

            set
            {
                m_MethodAction = value;
            }
        }

        public IBackOptionListener BackOptionListener
        {
            set
            {
                m_BackeWasClicked = value;
            }
        }

        public IMenuItemClickedListener OptionListener
        {
            set
            {
                m_OptionWasClicked = value;
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

        public void InitializeMenuItemListener(IMenuItemClickedListener i_MenuItemListener)
        {
            foreach (MenuItem menuItem in SubMenu)
            {
                menuItem.OptionListener = i_MenuItemListener;
            }
        }

        public void InitializeBackListener(IBackOptionListener i_BackListener)
        {
            foreach (MenuItem menuItem in SubMenu)
            {
                menuItem.BackOptionListener = i_BackListener;
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

        public void ActiveMethod()
        {
            m_MethodAction.CallMethod();
        }

        public void ChooseOptionToClick(int i_SubMenuChosice)
        {
            if (i_SubMenuChosice == 0)
            {
                m_BackeWasClicked.BackClicked();
            }
            else
            {
                m_OptionWasClicked.MenuItemClicked(this);
            }
        }
    }
}
