using System;
using System.Text.RegularExpressions;

namespace third_task
{
    class Program
    {
        static void Main(string[] args)
        {
            string ruText = "Не выходи из комнаты, не совершай ошибку.";//Задаем строку для русского текста
            int rucouletters = Regex.Matches(ruText.ToLower(), @"[абвгдеёжзийклмнопрстуфхцчшщъыьэюя]", RegexOptions.IgnoreCase).Count;//Считаем количество букв для русского текста
            string engText = "Why do you insist on continuing to exist? | Sirus voice line, from PoE";
            engText=engText.Remove(engText.IndexOf('|'));//Удаляем комментарий по индексу символа"|"
            int engcouletters = Regex.Matches(engText.ToUpper(), @"[ABCDEFGHIJKLMNOPQRSTUVWXYZ]", RegexOptions.IgnoreCase).Count;
            double firstwords = 0.5;
            double y;
            double x=0.5;
            for (int i=1;i< rucouletters; ++i)
            {
                y = firstwords+1;
                x=x+y;
                ++firstwords;
            }
            double rupetrenko = x * rucouletters;
            Console.WriteLine("Количество букв в русском тексте:"+ rucouletters+"; Индекс петренко равен:"+ rupetrenko);
            firstwords = 0.5;
            double z;
             x = 0.5;
            for (int i = 1; i < engcouletters; ++i)
            {
                z = firstwords + 1;
                x = x + z;
                ++firstwords;
            }
            double engpetrenko = x * engcouletters;
            Console.WriteLine("Количество букв в английском тексте:" + engcouletters + "; Индекс петренко равен:" + engpetrenko);
            if (engpetrenko == rupetrenko) { Console.WriteLine("Совпадение найдено по индексу Петренко-Гольцмана"); }
            Console.ReadKey();
        }
    }
}
