using System;
using System.Collections.Generic;

using Microsoft.Extensions.DependencyInjection;

namespace LogicalEngine
{
    class Program
    {
        static void Main(string[] args)
        {
            IServiceProvider Container = new ConsoleContainerBuilder().Build();

            // Not a concrete implementation.  calls for an interface
            var OutputService = Container.GetService<IOutput>();
            var MenuWriter = Container.GetService<IMenuWriter>();

            OutputService.Legend();

            var engine = new ICEOverheadValveEngine(OutputService);

            var selection = -1;
            while (selection != 0)
            {
                selection = MenuWriter.PromptSelection(engine);
            }
        }

    }
}
