using LogicalEngine.EngineContainers;
using LogicalEngine;
using System;
using System.Collections.Generic;
using System.Text;
using LogicalEngine.Engines;
using LogicalEngine.EngineParts;
using static LogicalEngine.EngineParts.Cylinders;

namespace LogicalEngine
{
    public class ICEOverheadValveEngine : CombustionEngine
    {
        public override bool CycleComplete { get => StrokeCycler.StrokeCycle == CombustionStrokeCycles.End; }
        public ICEOverheadValveEngine(IOutput Output)
        {
            EngineSubsystem[] systems = { new CombustionParts(this), new FuelParts(this), new PowerParts(this),
                                          new ExhaustParts(this), new AbstractParts(this)};
            Subsystems.AddRange(systems);

            DefineEngineSequence();
            AssembleEngine();
            MakeSurePartRefsAreSet();
            SetServiceRefs(Output);
        }

        public void DefineEngineSequence() // TODO: make this method in CombustionEngine instead. 
        {
            EngineAssembler.ConfigureICEOverheadValveEngine(this);
            EngineAssembler.ConnectBackup(this);
        }

        protected override void AssignPartListToPart(CarPart part)
        {
            if (EngineAssembler.PartChain.TryGetValue(part, out List<CarPart> Targets))
                part.AssignTargetPart(Targets);
            

        }


    }
}