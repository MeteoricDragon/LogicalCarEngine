using System;
using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;

namespace LogicalEngine
{
    class Program
    {
        public static readonly IServiceProvider Container = new ContainerBuilder().Build();
        static void Main(string[] args)
        {
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
