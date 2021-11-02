using System;
using System.Threading;

namespace Dining_Phillosophers
{
    class Program
    {
        static Phillosopher[] phillos = new Phillosopher[5];
        static Thread[] threads = new Thread[5];
        static Phillosopher tempPhil;
        static void Main(string[] args)
        {
            InitPhillosophers();
        }

        private static void Program_OnEating(object sender, EventArgs e)
        {
            if (phillos[(int)sender].IsEating)
                Console.WriteLine($"Phillosopher {sender} is eating!");
            else
                Console.WriteLine($"Phillosopher {sender} is waiting!");
        }

        static void InitPhillosophers()
        {
            for (int i = 0; i < 5; i++)
            {
                phillos[i] = new Phillosopher(i);
                threads[i] = new Thread(phillos[i].Update);
                phillos[i].OnEating += Program_OnEating;
                threads[i].Name = phillos[i].Number.ToString();
                threads[i].Start();
            }

        }
    }
}
