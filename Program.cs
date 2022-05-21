using System;
using System.Linq;
using System.Collections.Generic; 

namespace MultiThreadedApp
{
    class Program
    {
        private List<NumberAverager> processedData = new List<NumberAverager>();
        public List<NumberAverager> ProcessedData { get { return this.processedData; } set { this.processedData = value;  } }

        static void Main(string[] args)
        {
            Program program = new Program();
            List<NumberAverager> processedAverager = new(); 
            ProgramHelpers prg = new(5); 
            Random rand = new Random();
            prg.ThresholdReached += program.Prg_ThresholdReached; 
            int i = 0; 
            while(i < 100)
            {
                prg.AddValueToQueue(ValueGenerator.CreateValue(5, rand)); 
                i++; 
            }
            Console.WriteLine(); 
            Console.WriteLine("Averages:");
            foreach(NumberAverager nAvg in program.ProcessedData)
            {
                Console.WriteLine(nAvg.Average.ToString());
            } 
        }
        private void Prg_ThresholdReached(object? sender, ThresholdReachedEventArgs e)
        {
            ProcessedData.Add(new NumberAverager(e.Data)); 
        }

    }
    public static class ValueGenerator
    {
        public static double CreateValue(int millisecondDelay, Random rnd)
        {
            Thread.Sleep(millisecondDelay);
            double value = rnd.NextDouble(); 
            Console.WriteLine(value.ToString());
            return value; 
        }
    }
}