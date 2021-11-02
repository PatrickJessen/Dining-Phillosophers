using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Dining_Phillosophers
{
    class Phillosopher
    {
        public event EventHandler OnEating;
        static bool[] Forks = new bool[5];
        public int Number { get; set; }
        private bool isEating;
        public bool IsEating
        {
            get { return isEating; }
            set
            {
                if (isEating != value)
                {
                    isEating = value;
                    FireOnEatingEvent();
                }
            }
        }
        private Random rand = new Random();

        public Phillosopher(int number)
        {
            this.Number = number;
        }

        public void Update()
        {
            while (true)
            {
                // Checks if the number is 4 (last number) so i dont get out of bounds error in fork array, not happy with this, but it works..
                if (Number == 4)
                {
                    Eat(Number, 0);
                }
                else
                {
                    Eat(Number, Number + 1);
                }
                Thread.Sleep(rand.Next(1000, 2000));
            }
        }

        private void Eat(int number, int number2)
        {
            // checks if the forks next to the phillosopher is not in use, if its not we go in a lock the forks
            if (!Forks[number] && !Forks[number2])
            {
                Monitor.Enter(this);
                try
                {
                    Forks[number] = true;
                    Forks[number2] = true;
                    IsEating = true;
                }
                finally
                {
                    Monitor.PulseAll(this);
                }
            }
            else
            {
                Forks[number] = false;
                Forks[number2] = false;
                IsEating = false;
            }
        }

        //Fires event when a phillosopher is eating
        private void FireOnEatingEvent()
        {
            EventHandler handler = OnEating;
            if (handler != null)
            {
                OnEating(Number, EventArgs.Empty);
            }
        }
    }
}
