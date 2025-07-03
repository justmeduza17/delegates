using System.Security.Cryptography.X509Certificates;

namespace CarEvents
{
    public class Car
    {
        public int CurrentSpeed { get; set; }
        public int MaxSpeed { get; set; } = 100;
        public string PetName { get; set; }
        private bool carIsDead;
        public Car() { }
        public Car(string name, int maxSp, int currSp)
        {
            CurrentSpeed = currSp;
            MaxSpeed = maxSp;
            PetName = name;
        }
        public delegate void CarEngineHandler(string msg);
        public event CarEngineHandler Exploded;
        public event CarEngineHandler AboutToBlow;
        private CarEngineHandler listOfHandlers;
        public void RegisterWithCarEngine(CarEngineHandler methodToCall)
        {
            listOfHandlers += methodToCall;
        }
        public void Accelarate(int delta)
        {
            if (carIsDead)
            {
                Exploded?.Invoke("Sorry, this car is dead...");
            }
            else
            {
                CurrentSpeed += delta;
                if (10 == (MaxSpeed - CurrentSpeed) && AboutToBlow != null)
                {
                    AboutToBlow?.Invoke("Careful buddy! Gonna blow!");
                }
                if (CurrentSpeed >= MaxSpeed)
                {
                    carIsDead = true;
                }
                else
                {
                    Console.WriteLine("Current speed = {0}", CurrentSpeed);
                }
            }
        }
        public void UnregisterWithCarEngine(CarEngineHandler methodToCall)
        {
            listOfHandlers -= methodToCall;
        }
    }
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("***** Fun with Events *****\n");

            Car cl = new Car("SlugBug", 100, 10);

            cl.AboutToBlow += new Car.CarEngineHandler(CarIsAlmostDoomed);
            cl.AboutToBlow += new Car.CarEngineHandler(CarAboutToBlow);
            Car.CarEngineHandler d = new Car.CarEngineHandler(CarExploded);
            cl.Exploded += d;

            Console.WriteLine("***** Speeding up *****\n");
            for (int i = 0; i < 6; i++)
            {
                cl.Accelarate(20);
            }

            cl.Exploded -= d;

            Console.WriteLine("***** Speeding up *****\n");
            for (int i = 0; i < 6; i++)
            {
                cl.Accelarate(20);
            }
        }

        public static void CarAboutToBlow(string msg)
        {
            Console.WriteLine(msg);
        }
        public static void CarIsAlmostDoomed(string msg)
        {
            Console.WriteLine("=> Critical message from car: {0}", msg);
        }

        public static void CarExploded(string msg)
        {
            Console.WriteLine(msg);
        }
    }
}
