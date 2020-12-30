using LogicalEngine.Engines;
using System;
using System.Collections.Generic;
using System.Text;

namespace LogicalEngine.EngineParts
{
    public class IgnitionCoil : ElectricalPart
    {
        public override string UserFriendlyName { get => "Ignition Coil"; }
        public IgnitionCoil(Engine e) : base(e)
        {
            CanDrawFromBattery = true;
        }

        protected override bool ActivateNext(CarPart part)
        {
            if (part is Distributor && (Engine as CombustionEngine).Ignition.IgnitionSwitchOn)
                return true;
            return false;
        }
    }
}