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

        public Phillosopher(int number)
        {
            this.Number = number;
        }

        public void Update()
        {
            Random rand = new Random();
            while (true)
            {
                // Checks if the number is 4 (last number) so i dont get out of bounds error in fork array, not happy with this, but it works..
                if (Number == 4)
                {
                    if (!Forks[Number] && !Forks[0])
                    {
                        Monitor.Enter(this);
                        try
                        {
                            Forks[Number] = true;
                            Forks[0] = true;
                            IsEating = true;
                        }
                        finally
                        {
                            Monitor.PulseAll(this);
                        }
                    }
                    else
                    {
                        Forks[Number] = false;
                        Forks[0] = false;
                        IsEating = false;
                    }
                }
                // checks if the forks next to the phillosopher is not in use, if its not we go in a lock the forks
                else if (!Forks[Number] && !Forks[Number + 1])
                {
                    Monitor.Enter(this);
                    try
                    {
                        Forks[Number] = true;
                        Forks[Number + 1] = true;
                        IsEating = true;
                    }
                    finally
                    {
                        Monitor.PulseAll(this);
                    }
                }
                else
                {
                    Forks[Number] = false;
                    Forks[Number + 1] = false;
                    IsEating = false;
                }
                Thread.Sleep(rand.Next(1000, 2000));
            }
        }

        private void Eat()
        {

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
