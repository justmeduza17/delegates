using System.Runtime.CompilerServices;

namespace Car
{
    internal class Car
    {
        public class Car1
        {
            public int CurrentSpeed { get; set; }
            public int MaxSpeed { get; set; } = 100;
            public string PetName { get; set; }
            private bool carIsDead;
            public Car1() { }
            public Car1(string name, int maxSp, int currSp)
            {
                CurrentSpeed = currSp;
                MaxSpeed = maxSp;
                PetName = name;
            }
            public delegate void CarEngineHandler(string msgForCaller);
            private CarEngineHandler listOfHandlers;
            public void RegisterWithCarEngine(CarEngineHandler methodToCall)
            {
                listOfHandlers += methodToCall;
            }
            public void Accelarate(int delta)
            {
                if (carIsDead)
                {
                    if (listOfHandlers != null)
                    {
                        listOfHandlers("Sorry, this car is dead...");
                    }
                }
                else
                {
                    CurrentSpeed += delta;
                    if (10 == (MaxSpeed - CurrentSpeed) && listOfHandlers != null)
                    {
                        {
                            listOfHandlers("Careful buddy! Gonna blow!");
                        }
                    }
                    if (CurrentSpeed >= MaxSpeed)
                    {
                        carIsDead = true;
                    }
                    else {
                        Console.WriteLine("Current speed = {0}", CurrentSpeed);
                    }
                }
            }
            public void UnregisterWithCarEngine(CarEngineHandler methodToCall)
            {
                listOfHandlers -= methodToCall;
            }
        }

        static void Main(string[] args)
        {
            Console.WriteLine("***** Delegates as event enablares ******\n");

            Car1 cl = new Car1("SlugBug", 100, 10);

            cl.RegisterWithCarEngine(new Car1.CarEngineHandler(OnCarEngineEvent));
            Car1.CarEngineHandler handler2 = new Car1.CarEngineHandler(OnCarEngineEvent2);
            cl.RegisterWithCarEngine(handler2);
            Console.WriteLine("***** Speeding up *****\n");
            for (int i = 0; i < 6; i++)
            {
                cl.Accelarate(20);
                cl.UnregisterWithCarEngine(handler2);
            }
            Console.WriteLine("***** Speeding up *****\n");
            for (int i = 0; i < 6; i++)
            {
                cl.Accelarate(20);
                Console.ReadLine();
            }
        }

        public static void OnCarEngineEvent(string msg)
        {
            Console.WriteLine("\n***** Message From Car Object *****");
            Console.WriteLine("=> {0}", msg);
            Console.WriteLine("***********************************\n");
        }
        public static void OnCarEngineEvent2(string msg)
        {
            Console.WriteLine("=> {0}", msg.ToUpper());
        }
    }
}
