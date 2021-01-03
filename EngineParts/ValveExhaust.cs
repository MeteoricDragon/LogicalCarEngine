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
        public bool IsOpen { get; set; }

        public ValveExhaust(Engine e) : base(e)
        {
        }

        protected override bool TryActivateNext(CarPart partToActivate, CarPart activatingPart)
        {
            TryDrain(UnitsToConsume); // No connected parts yet.  just expel contents
            
            return base.TryActivateNext(partToActivate, activatingPart);
        }
    }
}