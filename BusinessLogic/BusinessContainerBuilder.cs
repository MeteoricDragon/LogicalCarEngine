using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace LogicalEngine
{
    public class BusinessContainerBuilder
    {
        public IServiceProvider Build()
        {
            var container = new ServiceCollection();

            container.AddSingleton<IEngineAssembler, EngineAssembler>();

            return container.BuildServiceProvider();
        }
    }
}
