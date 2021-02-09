using LogicalEngine.EngineContainers;
using LogicalEngine;
using System;
using System.Collections.Generic;
using System.Text;
using LogicalEngine.Engines;
using LogicalEngine.EngineParts;
using static LogicalEngine.EngineParts.Cylinders;
using LogicalCarEngine.Engines;

namespace LogicalEngine
{
    public class ICEOverheadValveEngine : CombustionEngine
    {
        public override bool CycleComplete { get => StrokeCycler.StrokeCycle == CombustionStrokeCycles.End; }
        public ICEOverheadValveEngine() : base()
        {
            EngineSubsystem[] systems = { new CombustionParts(this), new FuelParts(this), new PowerParts(this),
                                          new ExhaustParts(this), new AbstractParts(this)};
            Subsystems.AddRange(systems);

            DefineEngineSequence();
            AssembleEngine();
        }

        public void DefineEngineSequence() // TODO: make this method in CombustionEngine instead. 
        {
            EngineOrder.ConfigureICEOverheadValveEngine(this);
            EngineOrder.ConnectBackup(this);
        }

        protected override void AssignPartListToPart(CarPart part)
        {
            if (EngineOrder.PartChain.TryGetValue(part, out List<CarPart> Targets))
                part.AssignTargetPart(Targets);
            

        }


    }
}