using LogicalEngine;
using LogicalEngine.EngineParts;
using LogicalEngine.Engines;
using System;
using System.Collections.Generic;
using System.Text;
using static LogicalEngine.Engines.CombustionEngine;

namespace LogicalEngine.EngineParts
{
    public class CombustionChamber : FuelPart
    {
        public override string UserFriendlyName { get => "Combustion Chamber"; }
        public CombustionChamber(Engine e) : base(e)
        {
            Engine = e;
        }
        protected override bool TryActivateNext(CarPart partToActivate, CarPart activatingPart)
        {
            if (activatingPart is ValveIntake)
            {
                if (UnitsOwned >= UnitTriggerThreshold)
                {
                    (Engine as CombustionEngine).StrokeCycleChange(CombustionStrokeCycle.Compression);
                }
                return false;
            }

            return base.TryActivateNext(partToActivate, activatingPart);
        }
    }
}
