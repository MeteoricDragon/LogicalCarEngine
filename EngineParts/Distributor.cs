using LogicalEngine.Engines;
using System;
using System.Collections.Generic;
using System.Text;

namespace LogicalEngine.EngineParts
{
    public class Distributor : ElectricalPart
    {
        public override string UserFriendlyName { get => "Distributor"; }
        
        public Distributor(Engine e) : base(e)
        {
        }
        protected override bool TryActivateNext(CarPart partToActivate, CarPart activatingPart)
        {
            if (activatingPart is Distributor && (Engine as CombustionEngine).Ignition.IgnitionSwitchOn
                || activatingPart is CamShaft && UnitsOwned >= UnitTriggerThreshold)
                return base.TryActivateNext(partToActivate, activatingPart);
            return false;
        }
    }
}