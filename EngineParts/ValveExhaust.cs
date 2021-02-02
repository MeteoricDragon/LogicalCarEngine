using LogicalEngine.EngineParts;
using LogicalEngine.Engines;
using System;
using System.Collections.Generic;
using System.Text;
using static LogicalEngine.EngineParts.CombustionChambers;

namespace LogicalEngine.EngineParts
{
    public class ValveExhaust : FuelPart, IValve
    {
        public override string UserFriendlyName { get => "Exhaust Valve"; }
        public bool IsOpen { 
            get {
                return ((Engine as CombustionEngine).Chamber.StrokeCycle == CombustionStrokeCycles.Exhaust);
            } 
        }

        public ValveExhaust(Engine e) : base(e)
        {
            Engine = e;
        }
        protected override bool ShouldActivate(CarPart target)
        {
            if (IsOpen)
            {
                return base.ShouldActivate(target);
            }
            return false;
        }
        protected override bool CanTransfer(UnitContainer target)
        {
            if (IsOpen)
            {
                return base.CanTransfer(target);
            }
            return false;
        }
    }
}