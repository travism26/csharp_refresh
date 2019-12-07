using System;

namespace csharp.oop_examples
{

    public class A
    {
        // full on blueprint to create objects
    }

    public abstract class B
    {
        // create base functionallity and attributes to allow inheriting class' 
        // to implement their own behavior and attributes by themselves they cant
        // be instantiated. it can only be inherited.

        /*
        Basic idea: There is some "Common" functionallity between class', inherite it to
        keep code DRY. also have functionallity "Abstract" so subclass' can write their own
        behavor.
        */

        /* 
        Commen pitfalls i seen only if youre going to give all methods abstract just use
        an interface if there is no common "Stuff" required, interface are less resource intensive. 
        */
    }

    public interface C
    {
        /*
            A contract each class MUST follow. 
        */
    }

    // Create some example of each above descriptions
    public abstract class Car
    {
        public virtual void StartCar()
        {
            Console.WriteLine("Car Starts");
        }

        public void ShutOff()
        {
            Console.WriteLine("Car Shuts off");
        }

        public abstract String Transmission { get; }
    }

    public class runnable {

        public static void carExample()
        {
            Car travsCah = new ClassicMustang();
            travsCah.StartCar();
            travsCah.ShutOff();
            Console.WriteLine("Transmission: "+travsCah.Transmission);
        }
    }
}