using LogicalEngine.EngineContainers;
using LogicalEngine.EngineParts;
using System;
using System.Collections.Generic;
using System.Text;

namespace LogicalEngine.Engines
{
    public abstract class CombustionEngine : Engine
    {
        public enum CombustionStrokeCycle
        {
            Intake,
            Compression,
            Combustion,
            Exhaust
        };
        public CombustionStrokeCycle ScheduledStrokeCycle { get; protected set; }
        public IgnitionSwitch Ignition { get; protected set; }
        public bool CombustionActive { get; protected set; }
        public CombustionEngine() : base()
        {
        }
        
        public override void StartEngine()
        {
            if (Ignition == null)
                Ignition = Subsystems.Find(x => x is PowerParts)
                .Parts.Find(x => x is IgnitionSwitch) as IgnitionSwitch;

            while (!Ignition.StartupOn)
            {
                Ignition.TurnIgnitionClockwise();
            }

        }

        public void StrokeCycleChange(CombustionStrokeCycle cycle)
        {
            ScheduledStrokeCycle = cycle;
            Console.WriteLine("^&^&^&^ Changed cycle to " + cycle);
        }

        public override void TickEngine()
        {
            base.TickEngine();
            Ignition?.Tick();
        }

    }
}
