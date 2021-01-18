using LogicalEngine.EngineContainers;
using LogicalEngine.EngineParts;
using System;
using System.Collections.Generic;
using System.Text;

namespace LogicalEngine.Engines
{
    public abstract class CombustionEngine : Engine
    {
        public CombustionChambers Chamber { get; protected set; }
        public IgnitionSwitch Ignition { get; protected set; }
        public bool CombustionActive { get; protected set; }
        public CombustionEngine() : base()
        {
        }
        
        public override void StartEngine()
        {
            if (Ignition == null)
                Ignition = AllParts.Find(x => x is IgnitionSwitch) as IgnitionSwitch;
            if (Chamber == null)
                Chamber = AllParts.Find(x => x is CombustionChambers) as CombustionChambers;

            while (!Ignition.StartupOn)
            {
                Ignition.TurnIgnitionClockwise();
            }

        }


        public override void TickEngine()
        {
            base.TickEngine();
            Ignition?.Tick();
        }

    }
}
