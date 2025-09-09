using Lab_Extensibility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Lab_Extensibility.InputReaders
{
    class CsvInputReader : IInputReader
    {
        // Constructor to set the file path
        public CsvInputReader(string filePath)
        {
            _filePath = filePath;
        }
        // File path to read from
        // This is a private field, so it can only be accessed within this class
    
        private readonly string _filePath;

        public List<Person> Read() {
        // We will add Person objects to this list
        List<Person> persons = new();

        // Open open input file and process (source: https://www.briandunning.com/sample-data, with some post processing)
        using (StreamReader reader = new (_filePath))
        {
            Console.WriteLine($"File has been opened ({_filePath})");

            // Process the file
            string line;
            while ((line = reader.ReadLine()) != null)
            {
                // Split rows into columns - no need to understand regex here
                System.Text.RegularExpressions.MatchCollection columns =
                    new System.Text.RegularExpressions.Regex("((?<=\")[^\"]*(?=\"(,|$)+)|(?<=,|^)[^,\"]*(?=,|$))").Matches(line);

              persons.Add(new Person(firstName: columns[0].Value, lastName: columns[1].Value,
             companyName: columns[2].Value, address: columns[3].Value, city: columns[4].Value, state: columns[6].Value,
             age: columns[10].Value, weight: columns[11].Value, decease: columns[12].Value));
            }
        }

        return persons;
        }
    }
}
