using LogicalEngine.EngineParts;
using LogicalEngine.Engines;
using System;
using System.Collections.Generic;
using System.Text;
using static LogicalEngine.EngineParts.CombustionChambers;

namespace LogicalEngine.EngineParts
{
    public class ValveExhaust : CarPart, IValve
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
        protected override bool ShouldActivate(CarPart activatingPart)
        {
            if (IsOpen)
            {
                return base.ShouldActivate(activatingPart);
            }
            return false;
        }
        protected override bool TransferConditionsMet(CarPart transferingPart)
        {
            if ((Engine as CombustionEngine).Chamber.StrokeCycle == CombustionStrokeCycles.Exhaust)
            {
                return base.TransferConditionsMet(transferingPart);
            }
            return false;
        }
    }
}