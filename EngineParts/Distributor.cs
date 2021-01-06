using LogicalEngine.Engines;
using System;
using System.Collections.Generic;
using System.Text;
using static LogicalEngine.Engines.CombustionEngine;

namespace LogicalEngine.EngineParts
{
    public class Distributor : ElectricalPart
    {
        public override string UserFriendlyName { get => "Distributor"; }

        public override int UnitTriggerThreshold { get => 5; }
        public Distributor(Engine e) : base(e)
        {
        }
        protected override bool TryActivate(CarPart activatingPart)
        {
            CombustionEngine CE = (Engine as CombustionEngine);
            if (
                (activatingPart is IgnitionCoil 
                && CE.Ignition.IgnitionSwitchOn)
                || 
                (activatingPart is CamShaft 
                && UnitsOwned >= UnitTriggerThreshold))
            {
                return base.TryActivate(activatingPart);
            }
                
            return false;
        }
    }
}