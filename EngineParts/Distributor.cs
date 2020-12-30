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
        protected override bool ActivateNext(CarPart activator)
        {
            if (activator is CamShaft && UnitsOwned >= UnitTriggerThreshold)
                return true;
            return false;
        }
    }
}