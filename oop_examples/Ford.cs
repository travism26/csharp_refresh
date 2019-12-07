using System;

namespace csharp.oop_examples
{
    public class ClassicMustang : Car
    {
        public String CarName = "";
        public override String Transmission => "Manual";

        public override void StartCar()
        {
            Console.WriteLine("Engine Roars");
        }
    }

    public class SomeTruck : Car
    {
        public override String Transmission
        {
            get
            {
                String TransmissionType = "Automatic";
                return TransmissionType;
            }
        }
    }
}