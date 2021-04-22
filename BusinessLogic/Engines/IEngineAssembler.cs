using LogicalEngine.EngineParts;
using System.Collections.Generic;

namespace LogicalEngine
{
    public interface IEngineAssembler
    {
        Dictionary<CarPart, List<CarPart>> PartChain { get; }

        void ConfigureICEOverheadValveEngine(ICEOverheadValveEngine engine);
        void ConnectBackup(Engine engine);
    }
}