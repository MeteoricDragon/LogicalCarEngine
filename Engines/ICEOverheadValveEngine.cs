using LogicalEngine.EngineContainers;
using LogicalEngine;
using System;
using System.Collections.Generic;
using System.Text;
using LogicalEngine.Engines;
using LogicalEngine.EngineParts;
using static LogicalEngine.EngineParts.CombustionChambers;

namespace LogicalEngine
{
    public class ICEOverheadValveEngine : CombustionEngine
    {
        public override bool CycleComplete { get => Chamber.StrokeCycle == CombustionStrokeCycles.Exhaust; }
        public ICEOverheadValveEngine() : base()
        {
            EngineSubsystem[] systems = { new CombustionParts(this), new FuelParts(this), new PowerParts(this)};
            Subsystems.AddRange(systems);

            DefineEngineSequence();
            AssembleEngine();
        }

        public void DefineEngineSequence() // TODO: make this method in CombustionEngine instead. 
        {
            EngineOrder.ConfigureICEOverheadValveEngine(this);
            EngineOrder.ConnectBattery(this);
        }

        protected override void AssignPartListToPart(CarPart part)
        {
            if (EngineOrder.PartChain.TryGetValue(part, out List<CarPart> Targets))
                part.AssignTargetPart(Targets);
            

        }


    }
}