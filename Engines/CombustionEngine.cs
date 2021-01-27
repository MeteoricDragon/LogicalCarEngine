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
        public Crankshaft Crankshaft { get; protected set; }
        public bool CombustionActive { get; protected set; }
        public CombustionEngine() : base()
        {
        }
        
        public void MakeSurePartRefsAreSet()
        {
            if (Ignition == null)
                Ignition = AllParts.Find(x => x is IgnitionSwitch) as IgnitionSwitch;
            if (Chamber == null)
                Chamber = AllParts.Find(x => x is CombustionChambers) as CombustionChambers;
            if (Crankshaft == null)
                Crankshaft = AllParts.Find(x => x is Crankshaft) as Crankshaft;
        }
        public override void StartEngine()
        {
            MakeSurePartRefsAreSet();
            while (!Ignition.StartupOn)
            {
                Ignition.TurnIgnitionClockwise();
            }

        }


        public override void TickEngine()
        {
            MakeSurePartRefsAreSet();
            if (Crankshaft == null)
                Crankshaft = AllParts.Find(x => x is Crankshaft) as Crankshaft;
            base.TickEngine();

            bool firstRun = !CycleComplete;

            if (CycleComplete)
                Chamber.ResetStrokeCycle();

            if (firstRun)
                Ignition?.Tick();
            else
                Crankshaft?.Tick();
        }

    }
}
