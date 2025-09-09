using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModernLangToolsApp
{
    class ReportBuilder
    {
        private readonly IEnumerable<Person> people;
        private readonly Action<StringBuilder> headerPrinter;
        private readonly Action<StringBuilder> footerPrinter;
        private readonly Func<Person, string> personPrinter;

        public ReportBuilder(IEnumerable<Person> people, Action<StringBuilder> headerPrinter, Action<StringBuilder> footerPrinter, Func<Person, string> personPrinter)
        {
            this.people = people;
            this.headerPrinter = headerPrinter;
            this.footerPrinter = footerPrinter;
            this.personPrinter = personPrinter;

        }
        public string GetResult()
        {
           
            var result = new StringBuilder();
            headerPrinter(result);
            result.AppendLine("-----------------------------------------");
            int i = 0;
            foreach (var person in people)
            {
                result.Append($"{++i}. ");
                result.AppendLine(personPrinter(person));
            }
            result.AppendLine("--------------- Summary -----------------");
            footerPrinter(result);
            return result.ToString();
            
        }
    }
}
