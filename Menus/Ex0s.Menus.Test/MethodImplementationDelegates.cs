using System;

namespace Ex04.Menus.Test
{
    internal struct MethodImplementationDelegates
    {
        // Constants:
        private const char k_Space = ' ';
        // Constants:
        private const string k_VersionString = "Version";
        private const string k_VersionMajor = "20";
        private const string k_VersionMinor = "3";
        private const string k_VersionAfterMinor1 = "4";
        private const string k_VersionAfterMinor2 = "8920";

        // Methods:
        public static void CountSpaces_MethodAction()
        {
            int countSpaces = 0;

            Console.Write("Please enter a sentecnce: ");
            string sentence = Console.ReadLine();

            foreach (char character in sentence)
            {
                if (character == k_Space)
                {
                    countSpaces++;
                }
            }

            Console.WriteLine("The number of spaces {0}: {1}", countSpaces <= 1 ? "is" : "are", countSpaces);
        }

        public static void ShowVersion_MethodAction()
        {
            Console.WriteLine("{0}: {1}.{2}.{3}.{4}",
                k_VersionString,
                k_VersionMajor,
                k_VersionMinor,
                k_VersionAfterMinor1,
                k_VersionAfterMinor2);
        }

        public static void ShowDate_MethodAction()
        {
            Console.WriteLine("{0}/{1}/{2}", DateTime.Now.Day, DateTime.Now.Month, DateTime.Now.Year);
        }

        public static void ShowTime_MethodAction()
        {
            Console.WriteLine("{0}:{1}:{2}", DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second);
        }
    }
}
