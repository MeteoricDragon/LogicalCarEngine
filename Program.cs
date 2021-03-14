using System;
using System.Collections.Generic;

namespace LogicalEngine
{
    class Program
    {
        static void Main(string[] args)
        {
            Output.Legend();
            
            var engine = new ICEOverheadValveEngine();

            var selection = -1;
            while (selection != 0)
            {
                selection = MenuWriter.PromptSelection(engine);
            }

        }

    }
}
