using LogicalEngine.Engines;
using LogicalEngine.EngineContainers;
using LogicalEngine.EngineParts;
using System;
using System.Collections.Generic;
using System.Text;

namespace LogicalEngine.Engines
{
    public abstract class CombustionEngine : Engine
    {
        public CombustionStrokeCycler StrokeCycler;
        public Cylinders Chamber { get; protected set; }
        public IgnitionSwitch Ignition { get; protected set; }
        public Crankshaft Crankshaft { get; protected set; }
        public bool CombustionActive { get; protected set; }

        public override bool IsCycling { get; set; }

        public CombustionEngine() : base()
        {
            MakeSurePartRefsAreSet();
        }
        
        public void MakeSurePartRefsAreSet()
        {
            if (StrokeCycler == null)
                StrokeCycler = AllParts.Find(x => x is CombustionStrokeCycler) as CombustionStrokeCycler;
            if (Ignition == null)
                Ignition = AllParts.Find(x => x is IgnitionSwitch) as IgnitionSwitch;
            if (Chamber == null)
                Chamber = AllParts.Find(x => x is Cylinders) as Cylinders;
            if (Crankshaft == null)
                Crankshaft = AllParts.Find(x => x is Crankshaft) as Crankshaft;
        }

        public override void StartEngine()
        {
            while (!Ignition.StartupOn)
            {
                Ignition.TurnClockwise();
            }
            Ignition.Tick();
            Ignition.TurnCounterClockwise();
        }


        public override void RunFullCycle(bool cycleWithPause = false)
        {

            StrokeCycler.ResetStrokeCycle();

            Crankshaft?.Tick(cycleWithPause: cycleWithPause);
        }

    }
}
