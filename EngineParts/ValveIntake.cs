using LogicalEngine.Engines;
using System;
using System.Collections.Generic;
using System.Text;
using static LogicalEngine.Engines.CombustionEngine;

namespace LogicalEngine.EngineParts
{
    public class ValveIntake : FuelPart, IValve
    {
        public override string UserFriendlyName { get => "Intake Valve"; }
        public bool IsOpen { get; set; }
        public ValveIntake(Engine e) : base(e)
        {
            Engine = e;
        }

        protected override bool TryActivateNext(CarPart partToActivate, CarPart activatingPart)
        {
            if (activatingPart is Carburetor )  
            {
                if (UnitsOwned >= UnitTriggerThreshold)
                {
                    return base.TryActivateNext(partToActivate, activatingPart);
                }
            }
            return false;
        }
    }
}