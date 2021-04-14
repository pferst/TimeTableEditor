using System;
using System.Threading;


namespace TimetableEditor
{
    class Program
    {
        static void Main()
        {
            WelcomeMessage();
            Console.WriteLine("Is your file is in Downloads? Y/N");
            if (Console.ReadKey().KeyChar.ToString().ToLower() == "n")
            {
                Console.WriteLine("\nPlease move it there, others options in future realeses");
                Thread.Sleep(5000);
                Environment.Exit(0);
            }
            else
            {
                Console.Write("\nFile name with you timetable (.ics): ");
                string fileName = Console.ReadLine();
                while (!Plan.FileExists(fileName))
                {
                    Console.WriteLine("Sorry, but tht file not exists :( - check downloads directory");
                    Console.Write("\nFile name with you timetable (.ics): ");
                    fileName = Console.ReadLine();
                }
                Plan newTimetable = new Plan(fileName); ;
                string theEnd;
                do
                {
                    Console.Write("Short name of subject to delete: ");
                    string sn = Console.ReadLine();
                    Console.Write("TYPE of lessons from given subejct to delete: ");
                    string tos = Console.ReadLine();
                    newTimetable.RemoveSubject(sn + " " + tos);
                    JustStars();
                    Console.WriteLine("Do you want to delete more subjects? Y/N");
                    theEnd = Console.ReadKey().KeyChar.ToString().ToLower();
                    Console.WriteLine();

                } while (theEnd == "y");
                newTimetable = null;
            }
        }
        private static void WelcomeMessage()
        {
            string[] rules = new string[]
            {
                "Default directory for program is Downloads on you computer",
                "You have to write correct name of file without file extension - otherwise it won't work",
                "Write correct short name of subject you want to delete",
                "2 separate instructions will appear for subject name and type",
                "If you want remove all type of lessons from given subject use [Enter]",
                "Specific type from given subject - just write name like \"lab\" \"ćw\" etc."
            };
            JustStars();
            Console.WriteLine("\tWelcome user");
            Console.WriteLine("\nInstructions:");
            for (int i = 0; i < rules.Length; i++)
            {
                Console.WriteLine("{0}. {1}", i + 1, rules[i]);
            }
            JustStars();
            Console.Write("\n");
        }
        private static void JustStars()
        {
            for (int i = 0; i < 30; i++)
            {
                Console.Write("*");
            }
            Console.WriteLine();
        }
    }
}
