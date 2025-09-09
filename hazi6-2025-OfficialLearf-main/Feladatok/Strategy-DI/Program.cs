using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.NetworkInformation;
using Lab_Extensibility.AnonymizerAlgorithms;
using Lab_Extensibility.InputReaders;
using Lab_Extensibility.Progresses;
using Lab_Extensibility.ResultWriters;

namespace Lab_Extensibility;

static class Program
{
    static void Main(string[] args)
    {
         string inputPath = "us-500.csv";
         string outputPath1 = "us-500-processed1.txt";
         string outputPath2 = "us-500-processed2.txt";
         string outputPath3 = "us-500-processed3.txt";
         string outputPath4 = "us-500-processed4.txt";
        /*Anonymizer p1 = new(inputPath,
            new NameMaskingAnonymizerAlgorithm("***"),
            new SimpleProgress(),
            new CsvResultWriter(outputPath1),
            new CsvInputReader(inputPath));
        p1.Run();

        Console.WriteLine("--------------------");

        Anonymizer p2 = new(inputPath,
            new NameMaskingAnonymizerAlgorithm("***"),
            new PercentProgress(),
             new CsvResultWriter(outputPath2),
            new CsvInputReader(inputPath)
           );
        p2.Run();

        Console.WriteLine("--------------------");

        Anonymizer p3 = new(inputPath,
            new AgeAnonymizerAlgorithm(20),
            new SimpleProgress(),
             new CsvResultWriter(outputPath3),
            new CsvInputReader(inputPath))
           ;
        p3.Run();*/

        Anonymizer p4 = new(inputPath,
            new NameMaskingAnonymizerAlgorithm("***"),
            new CsvResultWriter(outputPath4),
            new CsvInputReader(inputPath),
            (count, index) => Console.WriteLine($"{index + 1}. person processed"));
        p4.Run();

        Anonymizer p5 = new(inputPath,
            new NameMaskingAnonymizerAlgorithm("***"),
            new CsvResultWriter(outputPath4),
            new CsvInputReader(inputPath),
            AllProgresses.Simple);
        p5.Run();
        Anonymizer p6 = new(inputPath,
            new NameMaskingAnonymizerAlgorithm("***"),
            new CsvResultWriter(outputPath4),
            new CsvInputReader(inputPath),
            AllProgresses.Percent);
        p6.Run();

        Anonymizer p7 = new(inputPath,
                new NameMaskingAnonymizerAlgorithm("***"),
                new CsvResultWriter(outputPath4),
                new CsvInputReader(inputPath),
                null);
        p7.Run();

        Console.ReadKey();
    }
}