using System;


namespace LogicalEngine
{
    class Program
    {
        static void Main(string[] args)
        {
            Output.Legend();
            var engine = new ICEOverheadValveEngine();
            engine.StartEngine();
            engine.RunEngine();
        }
    }
}
