using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Lab_Extensibility.AnonymizerAlgorithms;
using Lab_Extensibility.InputReaders;
using Lab_Extensibility.Progresses;
using Lab_Extensibility.ResultWriters;

namespace Lab_Extensibility;

public class NullProgress : IProgress
{
    public void Report(int total, int current)
    {
       
    }
}

public class Anonymizer
{
    // Some variables for statistics
    private int _personCount;
    private int _trimmedPersonCount;

    private readonly string _inputFileName;
    private readonly IInputReader _inputReader;
    private readonly IResultWriter _resultWriter;
    private readonly Action<int, int>? _progress;
    private readonly IAnonymizerAlgorithm _anonymizerAlgorithm;

    public Anonymizer(string inputFileName, IAnonymizerAlgorithm anonymizerAlgorithm,
        IResultWriter resultWriter, IInputReader inputReader, Action<int, int>? progress = null)
    {
        ArgumentException.ThrowIfNullOrEmpty(inputFileName);
        ArgumentNullException.ThrowIfNull(anonymizerAlgorithm);

        _inputFileName = inputFileName;
        _anonymizerAlgorithm = anonymizerAlgorithm;
        _progress = progress;
        _resultWriter = resultWriter ?? throw new ArgumentNullException(nameof(resultWriter));
        _inputReader = inputReader ?? throw new ArgumentNullException(nameof(inputReader));
    }

    public void Run()
    {
        Console.WriteLine("App started");
        List<Person> persons = _inputReader.Read();
        _personCount = persons.Count;
        persons = TrimCityNames(persons);

        List<Person> anonymizedPersons = new();
        for (var i = 0; i < persons.Count; i++)
        {
            anonymizedPersons.Add(_anonymizerAlgorithm.Anonymize(persons[i]));
            _progress?.Invoke(persons.Count, i);
        }

        _resultWriter.Write(anonymizedPersons);
        PrintSummary();
    }



    private List<Person> TrimCityNames(List<Person> persons)
    {
        // Cleanup data 1: trim whitespaces and other unneeded characters (_ and #) from beginning and end of city names
        // As Person objects are immutable, let's create new Person objects with trimmed city names and add to new list.
        List<Person> trimmedPersons = new();
        foreach (var person in persons)
        {
            var trimmedCity = person.City.Trim().Trim('_', '#');
            if (trimmedCity != person.City)
                ++_trimmedPersonCount;
            trimmedPersons.Add(new Person(person.FirstName, person.LastName, person.CompanyName,
                person.Address, trimmedCity, person.State, person.Age, person.Weight, person.Decease));
        }
        return trimmedPersons;
    }

    private void PrintSummary()
    {
        // Print summary/statistics
        Console.WriteLine($"Summary - Anonymizer ({_anonymizerAlgorithm.GetAnonymizerDescription()}): Persons: {_personCount}, trimmed: {_trimmedPersonCount}");
    }

}