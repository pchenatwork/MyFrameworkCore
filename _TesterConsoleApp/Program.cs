using Application.JobServices;
using System;

namespace _TesterConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            JobRunner.RunJob("Job1", "Hello World");
            JobRunner.RunJob("Job2", "Ni Hao");
            Console.ReadKey();

        }
    }
}
