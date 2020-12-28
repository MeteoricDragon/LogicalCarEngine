using System;


namespace LogicalEngine
{
    class Program
    {
        static void Main(string[] args)
        {
            var engine = new ICEOverheadValveEngine();
            engine.StartEngine();
            engine.RunEngine();
        }
    }
}
