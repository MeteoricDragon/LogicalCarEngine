using LogicalEngine.Engines;
using System;
using System.Collections.Generic;
using System.Text;
using static LogicalEngine.EngineParts.CombustionChamber;

namespace LogicalEngine.EngineParts
{
    public class ValveIntake : FuelPart, IValve
    {
        public override string UserFriendlyName { get => "Intake Valve"; }
        public bool IsOpen
        {
            get
            {
                return ((Engine as CombustionEngine).Chamber.StrokeCycle == CombustionStrokeCycle.Intake);
            }
        }
        public ValveIntake(Engine e) : base(e)
        {
            Engine = e;
        }

        protected override bool ThresholdTriggered(CarPart activatingPart)
        {
            if (activatingPart is Carburetor )  
            {
                return base.ThresholdTriggered(activatingPart);
            }
            return false;
        }
    }
}