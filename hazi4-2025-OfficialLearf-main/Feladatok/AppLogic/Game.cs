using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace MultiThreadedApp.AppLogic;


class Game
{
    public const int StartLinePosition = 150;
    public const int DepoPosition = 300;
    public const int FinishLinePosition = 450;

    private ManualResetEvent raceStart = new ManualResetEvent(false);
    private AutoResetEvent depoStart = new AutoResetEvent(false);
    private readonly object logLock = new object();
    private readonly List<int> stepLog = new List<int>();
    private object depoLock = new object();
    private bool raceStarted = false;
    private int bikesInDepo = 0;
    private volatile bool hasWinner = false;
    private readonly object winnerLock = new object();

    private Action<Bike> bikeStateChangedCallback;

    public List<Bike> Bikes { get; } = new List<Bike>();

    /// <summary>
    /// Verseny előkészítése (biciklik létrehozása és felsorakoztatása
    /// a startvonalhoz)
    /// </summary>
    public void PrepareRace(Action<Bike> onBikeStateChanged = null)
    {
        bikeStateChangedCallback = onBikeStateChanged;

        lock (logLock)
        {
            stepLog.Clear();
        }

        raceStart.Reset();
        depoStart.Reset();
        raceStarted = false;
        bikesInDepo = 0;
        Bikes.Clear();
        hasWinner = false;

        CreateBike();
        CreateBike();
        CreateBike();
    }


    private void LogStep(int step)
    {
        lock (logLock)
        {
            stepLog.Add(step);
        }
    }

    /// <summary>
    /// Elindítja a bicikliket a startvonalról.
    /// </summary>
    public void StartBikes()
    {
        if (!raceStarted)
        {
            raceStarted = true;
            raceStart.Set();
        }
    }

    /// <summary>
    /// Elindítja a következő biciklit a depóból (mindig csak egyet)
    /// </summary>
    public void StartNextBikeFromDepo()
    {
        if (bikesInDepo > 0)
        {
            depoStart.Set();
        }
    }

    private void CreateBike()
    {
        var bike = new Bike(Bikes.Count);
        Bikes.Add(bike);
        var thread = new Thread(BikeThreadFunction);
        thread.IsBackground = true;
        thread.Start(bike);
    }

    private void BikeThreadFunction(object bikeAsObject)
    {
        Bike bike = (Bike)bikeAsObject;


        while (bike.Position <= StartLinePosition)
        {
            int step = bike.Step();
            LogStep(step);
            NotifyBikeStateChanged(bike);
            Thread.Sleep(100);
        }

        
        raceStart.WaitOne();

   
        while (bike.Position <= DepoPosition)
        {
            int step = bike.Step();
            LogStep(step);
            NotifyBikeStateChanged(bike);
            Thread.Sleep(100);
        }

   
        lock (depoLock)
        {
            bikesInDepo++;
            NotifyBikeStateChanged(bike);
        }

       
        depoStart.WaitOne();

        // Leave depo
        lock (depoLock)
        {
            bikesInDepo--;
            NotifyBikeStateChanged(bike);
        }

     
        while (bike.Position <= FinishLinePosition)
        {
            int step = bike.Step();
            LogStep(step);
            NotifyBikeStateChanged(bike);
            Thread.Sleep(100);
        }

      
        lock (winnerLock)
        {
            if (!hasWinner)
            {
                bike.SetAsWinner();
                hasWinner = true;
                NotifyBikeStateChanged(bike);
            }
        }
    }

    private void NotifyBikeStateChanged(Bike bike)
    {
        bikeStateChangedCallback?.Invoke(bike);
    }
}

