using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModernLangToolsApp
{
    class ReportPrinter
    {
        private readonly IEnumerable<Person> people;
        private readonly Action headerPrinter;
        private readonly Action footerPrinter;
        private readonly Action<Person> personPrinter;

        public ReportPrinter(IEnumerable<Person> people, Action headerPrinter,Action footerPrinter, Action<Person> personPrinter )
        {
            this.people = people;
            this.headerPrinter = headerPrinter;
            this.footerPrinter = footerPrinter;
            this.personPrinter = personPrinter;

        }

        public void PrintReport()
        {
            headerPrinter();
            Console.WriteLine("-----------------------------------------");
            int i = 0;
            foreach (var person in people)
            {
                Console.Write($"{++i}. ");
                personPrinter(person);
            }
            
            Console.WriteLine("--------------- Summary -----------------");
            footerPrinter();
        }
    }
}
