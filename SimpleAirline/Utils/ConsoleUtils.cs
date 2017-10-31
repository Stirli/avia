using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using SimpleAirline.Data;
using static System.Console;

namespace SimpleAirline.Utils
{
    static class ConsoleUtils
    {
        public static void ShowAsMenu(params string[] strs)
        {
            int maxLen = strs.Max(s => s.Length);
            WriteLine(new string('-', maxLen + 4));
            foreach (var str in strs)
            {
                Write("+ ");
                Write(str);
                Write(new string(' ', maxLen - str.Length));
                Write(" +\n");
            }
            WriteLine(new string('-', maxLen + 4));
        }
        public static T SelectDbRow<T>(DbSet<T> db) where T : class, IModel
        {
            var list = db.ToList();
            list.Print(true);
            var i = (int)ReadDouble("Введите ID", 0);
            var carrier = db.Local.First(c => c.Id == i);
            return carrier;
        }
        public static void Print<T>(this IEnumerable<T> o, bool indexed = false) where T : IModel
        {
            WriteLine(new string('-', 80));
            foreach (var obj in o)
            {
                if (indexed)
                    Write(obj.Id + ": ");
                WriteLine(obj);
            }
            WriteLine(new string('-', 80));
        }

        public static string ReadLine(string message)
        {
            Write(message);
            Write("(или Ctrl+Z для отмены): ");
            var str = Console.ReadLine();
            if (str == null)
            {
                throw new ApplicationException("Ввод отменен");
            }
            return str;
        }

        public static double ReadDouble(string message, double min, double max = double.MaxValue)
        {
            while (true)
            {
                try
                {
                    double number = double.Parse(ReadLine(message));
                    if (number < min || number > max)
                    {
                        WriteLine("Выход за пределы диапазона {0} : {1}", min, max);
                    }
                    return number;
                }
                catch (ApplicationException)
                {
                    throw;
                }
                catch (Exception e)
                {
                    WriteLine(e.Message);
                }
            }
        }

        public static int ReadInt(string message, int min, int max = int.MaxValue)
        {
            while (true)
            {
                try
                {
                    int number = int.Parse(ReadLine(message));
                    if (number < min || number > max)
                    {
                        WriteLine("Выход за пределы диапазона {0} : {1}", min, max);
                    }
                    return number;
                }
                catch (ApplicationException)
                {
                    throw;
                }
                catch (Exception e)
                {
                    WriteLine(e.Message);
                }
            }
        }
    }
}
