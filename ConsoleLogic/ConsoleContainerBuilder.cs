using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace LogicalEngine
{
    public class ConsoleContainerBuilder
    {
        public IServiceProvider Build()
        {
            var container = new ServiceCollection();

            container.AddSingleton<IOutput, Output>();
            container.AddSingleton<IMenuWriter, MenuWriter>();

            return container.BuildServiceProvider();
        }
    }
}
