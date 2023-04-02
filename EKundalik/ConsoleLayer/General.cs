// --------------------------------------------------------
// Copyright (c) Coalition of Good-Hearted Engineers
// --------------------------------------------------------

using System;
using System.Linq;
using Tynamix.ObjectFiller;

namespace EKundalik.ConsoleLayer
{
    public class General
    {
        public static Filler<T> CreateObjectFiller<T>() where T : class
        {
            var filler = new Filler<T>();

            filler.Setup().OnType<DateTime>()
                .Use(new DateTimeRange(DateTime.UnixEpoch).GetValue);

            return filler;
        }

        public static void Pause()
        {
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }

        public static int PrintCrudOptions(string name)
        {
            Console.Clear();
            Console.Write($"1.Create {name}\n2.Select " +
                $"{name}\n3.Update {name}\n4.Delete {name}\n" +
                $"5.Select All {name}\n6.Add random {name}\n7.Back\n\n" +
                $"choose option: ");

            string choose = Console.ReadLine();
            int choice;
            int.TryParse(choose, out choice);

            return choice;
        }

        public static void SelectAll<T>(IQueryable<T> list)
        {
            foreach (var item in list)
            {
                PrintObjectProperties<T>(item);
                Console.WriteLine();
            }
        }

        public static void PrintObjectProperties<T>(T obj)
        {
            if (obj is null) return;

            Console.Clear();
            Console.WriteLine($"Type: {typeof(T)}");

            foreach (var prop in typeof(T).GetProperties())
            {
                dynamic propValue = prop.GetValue(obj);
                Console.WriteLine($"{prop.Name}: {propValue}");
            }

            Console.WriteLine();
            Pause();
        }
    }
}
