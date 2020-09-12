using Ex04.Menus.Interfaces;

namespace Ex04.Menus.Test
{
    class Program
    {
        static void Main()
        {
            MainMenu mainMenu = CreateMenuWithInterfaces();
            mainMenu.Show();
        }

        private static MainMenu CreateMenuWithInterfaces()
        {
            MainMenu menu = new MainMenu("Main Menu");
            MenuItem MenuItemVersionSpaces = new MenuItem("Version and Spaces");
            MenuItem MenuItemShowDateTime = new MenuItem("Show Date/Time");
            menu.CreateMainMenu(MenuItemVersionSpaces, MenuItemShowDateTime);

            MenuItem countSpaces = new MenuItem("Count Spaces");
            countSpaces.MethodActivator = new MethodsImplementation.CountSpacesClass();
            MenuItem showVersion = new MenuItem("Show Version");
            showVersion.MethodActivator = new MethodsImplementation.ShowVersionClass();
            menu.MenuItems[0].AddToSubMenu(countSpaces, showVersion);

            MenuItem showDate = new MenuItem("Show Date");
            showDate.MethodActivator = new MethodsImplementation.ShowDateClass();
            MenuItem showTime = new MenuItem("Show Time");
            showTime.MethodActivator = new MethodsImplementation.ShowTimeClass();
            menu.MenuItems[1].AddToSubMenu(showDate, showTime);

            return menu;
        }
    }
}
