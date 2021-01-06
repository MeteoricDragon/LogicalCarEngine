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
        private ValveExhaust ExhaustValve;
        private ValveIntake IntakeValve;
        public override string UserFriendlyName { get => "Combustion Chamber"; }
        public CombustionChamber(Engine e) : base(e)
        {
            Engine = e;
        }
        protected override bool TryActivate(CarPart activatingPart)
        {
            if (ExhaustValve == null || IntakeValve == null)
            {
                var parts = Engine.AllParts;
                ExhaustValve = parts.Find(x => x is ValveExhaust) as ValveExhaust;
                IntakeValve = parts.Find(x => x is ValveIntake) as ValveIntake;
            }

            if (UnitsOwned >= UnitTriggerThreshold
                && !IntakeValve.IsOpen && !ExhaustValve.IsOpen)
                return base.TryActivate(activatingPart);

            return false;
        }


    }
}
