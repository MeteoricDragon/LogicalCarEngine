using LogicalEngine.EngineContainers;
using LogicalEngine.Engines;
using System;
using System.Collections.Generic;
using System.Text;

namespace LogicalEngine.EngineParts
{
    public class SparkPlugs : ElectricalPart
    {
        private ValveExhaust ExhaustValve;
        private ValveIntake IntakeValve;
        public override int UnitsToGive { get => 0; } // nothing gets added to the fuel
        public override string UserFriendlyName { get => "Spark Plugs"; }
        public SparkPlugs(Engine e) : base(e)
        {          
        }

        protected override bool TryActivateNext(CarPart partToActivate, CarPart activatingPart)
        {
            if (ExhaustValve == null || IntakeValve == null)
            {
                var parts = Engine.AllParts;
                ExhaustValve = parts.Find(x => x is ValveExhaust) as ValveExhaust;
                IntakeValve = parts.Find(x => x is ValveIntake) as ValveIntake;
            }

            if (partToActivate.UnitsOwned >= UnitTriggerThreshold
                && !IntakeValve.IsOpen && !ExhaustValve.IsOpen)
                return base.TryActivateNext(partToActivate, activatingPart);
            // Should spark plug activate combustion chamber only when both valves closed?
            return false;
        }
    }
}