using System;
using Ex04.Menus.Interfaces;
// using Ex04.Menus.Delegates;

namespace Ex04.Menus.Test
{
    internal struct MethodsImplementation
    {
        internal struct CountSpacesClass : IMethodActivator
        {
            // Constants:
            private const char k_Space = ' ';

            // Methods:
            void IMethodActivator.CallMethod()
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
        }

        internal struct ShowVersionClass : IMethodActivator
        {
            // Constants:
            private const string k_VersionString = "Version";
            private const string k_VersionMajor = "20";
            private const string k_VersionMinor = "3";
            private const string k_VersionAfterMinor1 = "4";
            private const string k_VersionAfterMinor2 = "8920";

            // Methods:
            void IMethodActivator.CallMethod()
            {
                Console.WriteLine("{0}: {1}.{2}.{3}.{4}",
                    k_VersionString,
                    k_VersionMajor,
                    k_VersionMinor,
                    k_VersionAfterMinor1,
                    k_VersionAfterMinor2);
            }

        }

        internal struct ShowDateClass : IMethodActivator
        {
            // Methods:
            void IMethodActivator.CallMethod()
            {
                Console.WriteLine("{0}/{1}/{2}", DateTime.Now.Day, DateTime.Now.Month, DateTime.Now.Year);
            }
        }

        internal struct ShowTimeClass : IMethodActivator
        {
            // Methods:
            void IMethodActivator.CallMethod()
            {
                Console.WriteLine("{0}:{1}:{2}", DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second);
            }
        }
    }
}
