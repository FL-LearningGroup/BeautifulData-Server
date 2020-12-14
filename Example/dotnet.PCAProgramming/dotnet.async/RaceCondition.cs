using System;
using System.Collections.Generic;
using System.Text;

namespace dotnet.async
{
    public class RaceCondition
    {
        private int _counter = 0;
        private int _counterLock = 0;
        public int Counter { get => _counter; }
        public int CounterLock { get => _counterLock; }

        public void Next()
        {
            _counter++;
        }

        public void NextLock()
        {
            lock(this)
            {
                _counterLock++;
            }
        }
    }
    /**Test********************************************/
    /*
        RaceCondition raceCondition = new RaceCondition();
        Action<object> action = (object objec) => {
            for (int i = 1; i <= 10000; i++)
            {
                raceCondition.Next();
            }
            for (int i = 1; i <= 10000; i++)
            {
                raceCondition.NextLock();
            }
        };
        Task oneTask = new Task(action, "one task");
        Task twoTask = new Task(action, "two task");
        oneTask.Start();
        twoTask.Start();
        Task.WaitAll(new Task[] { oneTask,twoTask });
        Console.WriteLine("One task status: {0}, Two task status: {1}", oneTask.Status, twoTask.Status);
        Console.WriteLine("Race condition counter: {0}, counterlock: {1}", raceCondition.Counter, raceCondition.CounterLock);
    */
    /**Result: ********************************************
        One task status: RanToCompletion, Two task status: RanToCompletion
        Race condition counter: 14164, counterlock: 20000
    */

}
