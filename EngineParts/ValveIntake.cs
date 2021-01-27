using LogicalEngine.Engines;
using System;
using System.Collections.Generic;
using System.Text;
using static LogicalEngine.EngineParts.CombustionChambers;

namespace LogicalEngine.EngineParts
{
    public class ValveIntake : FuelPart, IValve
    {
        public override string UserFriendlyName { get => "Intake Valve"; }
        public bool IsOpen
        {
            get
            {
                return ((Engine as CombustionEngine).Chamber.StrokeCycle == CombustionStrokeCycles.Intake);
            }
        }
        public ValveIntake(Engine e) : base(e)
        {
            Engine = e;
        }

        protected override bool ShouldActivate(CarPart activatingPart)
        {
            if (activatingPart is Carburetor && IsOpen)  
            {
                return base.ShouldActivate(activatingPart);
            }
            return false;
        }
        protected override bool CanFill(CarPart givingPart)
        {
            return IsOpen;
        }
    }
}