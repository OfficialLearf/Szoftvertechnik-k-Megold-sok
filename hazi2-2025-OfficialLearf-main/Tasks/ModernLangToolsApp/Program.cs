using System.ComponentModel;
using System.Xml.Serialization;

namespace ModernLangToolsApp;

public class Program
{
    public static void initCollection(JediCollection council)
    {
        council.Add(new Jedi("Anakin Skywalker", 25000));
        council.Add(new Jedi("Skywalker Anakin", 25000));
        council.Add(new Jedi("Yoda", 500));
    }
    [Description("Task2")]
    public static void CloneTest()
    {
        try
        {
            Jedi anakin = new Jedi("Anakin Skywalker", 25000);
            Jedi anakinClone = Jedi.JediClone(anakin);
            Console.WriteLine($"{anakinClone.Name},{anakinClone.MidiChlorianCount}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Hiba történt: {ex.Message}");
        }
    }
    [Description("Task3")]
    public static void JediCollectionTest()
    {
        JediCollection council = new JediCollection();
        council.change += (message) => Console.WriteLine(message);
        council.Add(new Jedi("Anakin Skywalker", 25000));
        council.Add(new Jedi("Skywalker Anakin", 25000));
        council.Remove();
        council.Remove();
    }
    [Description("Task4")]
    public static void LowerThan530Test()
    {
        JediCollection council = new JediCollection();
        initCollection(council);
        List<Jedi> LowJedis = council.FindAll530_Delegate();
        LowJedis.ForEach(j => Console.WriteLine(j.Name));
    }
    [Description("Task5")]
    public static void HigherThan1000Test()
    {
        JediCollection council = new JediCollection();
        initCollection(council);
        List<Jedi> HighJedis = council.FindAll1000_Lambda();
        HighJedis.ForEach(j => Console.WriteLine(j.Name));
    }
    [Description("Task6")]
    static void test6()
    {
        var employees = new Person[] { new Person("Joe", 20), new Person("Jill", 30) };

        ReportPrinter reportPrinter = new ReportPrinter(
            employees,
            () => Console.WriteLine("Employees"),
            () => Console.WriteLine("Total number of employees: " + employees.Length),
            person => Console.WriteLine($"Name: {person.Name} (Age: {person.Age})"
            ));

        reportPrinter.PrintReport();
    }
    [Description("Task6b")]
    static void test6b()
    {
        var employees = new Person[] { new Person("Joe", 20), new Person("Jill", 30) };

        ReportBuilder reportBuilder = new ReportBuilder(
            employees,
            stringbuilder => stringbuilder.AppendLine("Employees"),
            stringbuilder => stringbuilder.AppendLine("Total number of employees: " + employees.Length),
            person => $"Name: {person.Name} (Age: {person.Age})"
        );

        // Riport lekérése és kiírása
        Console.WriteLine(reportBuilder.GetResult());
    }

    public static void Main(string[] args)
    {
        CloneTest();
        JediCollectionTest();
        LowerThan530Test();
        HigherThan1000Test();
        test6();
        test6b();
    }
}
