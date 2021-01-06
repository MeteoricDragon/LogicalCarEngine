using LogicalEngine.EngineParts;
using LogicalEngine.Engines;
using System;
using System.Collections.Generic;
using System.Text;
using static LogicalEngine.Engines.CombustionEngine;

namespace LogicalEngine.EngineParts
{
    public class ValveExhaust : CarPart, IValve
    {
        public override string UserFriendlyName { get => "Exhaust Valve"; }
        public bool IsOpen { 
            get {
                return ((Engine as CombustionEngine).ScheduledStrokeCycle == CombustionStrokeCycle.Exhaust);
            } 
        }

        public ValveExhaust(Engine e) : base(e)
        {
            Engine = e;
        }

        protected override bool TryActivate( CarPart activatingPart)
        {
            TryDrain(UnitsToConsume); // connected parts for this are outside scope of program.  just drain
            
            return base.TryActivate(activatingPart);
        }
    }
}