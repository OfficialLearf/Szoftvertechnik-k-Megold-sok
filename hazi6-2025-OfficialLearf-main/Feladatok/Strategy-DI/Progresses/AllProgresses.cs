using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Lab_Extensibility.Progresses
{
    public static class AllProgresses
    {
        public static void Simple(int count, int index)
        {
            Console.WriteLine($"{index + 1}. person processed");
        }

        public static void Percent(int count, int index)
        {
            int percentage = (int)((double)(index + 1) / count * 100);

            Console.Write($"\rProcessing: {percentage} %");

            if (index == count - 1)
                Console.WriteLine();
        }
    }
}
