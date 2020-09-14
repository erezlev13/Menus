using System;
using Ex04.Menus;

namespace Ex04.Menus.Test
{
    class Program
    {
        static void Main()
        {
            Interfaces.MainMenu mainMenuInterfaces = CreateMenuWithInterfaces();
            mainMenuInterfaces.Show();

            Console.WriteLine("Switch to menu builder with delegates");
            Console.Clear();

            Delegates.MainMenu mainMenuDelegates = CreateMenuWithDelegates();
            mainMenuDelegates.Show();
        }

        private static Interfaces.MainMenu CreateMenuWithInterfaces()
        {
            Interfaces.MainMenu menu = new Interfaces.MainMenu("Main Menu");
            Interfaces.MenuItem MenuItemVersionSpaces = new Interfaces.MenuItem("Version and Spaces");
            Interfaces.MenuItem MenuItemShowDateTime = new Interfaces.MenuItem("Show Date/Time");
            menu.CreateMainMenu(MenuItemVersionSpaces, MenuItemShowDateTime);

            Interfaces.MenuItem countSpaces = new Interfaces.MenuItem("Count Spaces");
            countSpaces.MethodActivator = new MethodsImplementationInterfaces.CountSpacesClass();
            Interfaces.MenuItem showVersion = new Interfaces.MenuItem("Show Version");
            showVersion.MethodActivator = new MethodsImplementationInterfaces.ShowVersionClass();
            menu.MenuItems[0].AddToSubMenu(countSpaces, showVersion);

            Interfaces.MenuItem showDate = new Interfaces.MenuItem("Show Date");
            showDate.MethodActivator = new MethodsImplementationInterfaces.ShowDateClass();
            Interfaces.MenuItem showTime = new Interfaces.MenuItem("Show Time");
            showTime.MethodActivator = new MethodsImplementationInterfaces.ShowTimeClass();
            menu.MenuItems[1].AddToSubMenu(showDate, showTime);

            return menu;
        }

        private static Delegates.MainMenu CreateMenuWithDelegates()
        {
            Delegates.MainMenu menu = new Delegates.MainMenu("Main Menu");
            Delegates.MenuItem MenuItemVersionSpaces = new Delegates.MenuItem("Version and Spaces");
            Delegates.MenuItem MenuItemShowDateTime = new Delegates.MenuItem("Show Date/Time");
            menu.CreateMainMenu(MenuItemVersionSpaces, MenuItemShowDateTime);

            Delegates.MenuItem countSpaces = new Delegates.MenuItem("Count Spaces");
            countSpaces.MethodWasActivated += MethodImplementationDelegates.CountSpaces_MethodAction;
            Delegates.MenuItem showVersion = new Delegates.MenuItem("Show Version");
            showVersion.MethodWasActivated += MethodImplementationDelegates.ShowVersion_MethodAction;
            menu.MenuItems[0].AddToSubMenu(countSpaces, showVersion);

            Delegates.MenuItem showDate = new Delegates.MenuItem("Show Date");
            showDate.MethodWasActivated += MethodImplementationDelegates.ShowDate_MethodAction;
            Delegates.MenuItem showTime = new Delegates.MenuItem("Show Time");
            showTime.MethodWasActivated += MethodImplementationDelegates.ShowTime_MethodAction;
            menu.MenuItems[1].AddToSubMenu(showDate, showTime);

            return menu;
        }
    }
}
