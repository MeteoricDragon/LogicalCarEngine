using LogicalEngine.Engines;
using System;
using System.Collections.Generic;
using System.Text;
using static LogicalEngine.Engines.CombustionEngine;

namespace LogicalEngine.EngineParts
{
    public class ValveIntake : CarPart
    {
        public override string UserFriendlyName { get => "Intake Valve"; }
        public ValveIntake(Engine e) : base(e)
        {
            Engine = e;
        }
        protected override bool TryActivateNext(CarPart partToActivate, CarPart activatingPart)
        {
            var CE = (Engine as CombustionEngine);
            if (activatingPart is CamShaft && CE.StrokeCycle == CombustionStrokeCycle.Exhaust)
                CE.StrokeCycleChange(CombustionStrokeCycle.Intake);

            // what keeps the stage at intake until it's ready for compression???  
            // Fuel presence in combustion chamber?  Might need to add a combustion chamber part.

            // if activating part is camshaft
            // change intake valve state to open when stroke cycle is intake.
            // Change intake valve state to closed when stroke cycle is compression

            if ((activatingPart is Carburetor || activatingPart is CamShaft)
                && UnitsOwned >= UnitTriggerThreshold)
                return base.TryActivateNext(partToActivate, activatingPart);
            return false;
        }
    }
}