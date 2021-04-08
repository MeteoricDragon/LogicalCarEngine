using LogicalEngine.Engines;
using System;
using System.Collections.Generic;
using System.Text;
using static LogicalEngine.EngineParts.Cylinders;
using static LogicalEngine.Engines.CombustionEngine;

namespace LogicalEngine.EngineParts
{
    public class CamShaft : MechanicalPart
    {
        public override string UserFriendlyName { get => "Camshaft"; }
        public CamShaft(Engine e) : base(e)
        {
        }

        protected override void RefreshEngineStage(CarPart target)
        {
            ToggleValves();
        }
        private void ToggleValves()
        {
            var CE = Engine as CombustionEngine;
            var parts = CE.AllParts;
            var ExhaustValve = parts.Find(x => x is ValveExhaust) as IValve;
            var IntakeValve = parts.Find(x => x is ValveIntake) as IValve;
            
            switch (CE.StrokeCycler.StrokeCycle)
            {
                case CombustionStrokeCycles.Intake:
                    IntakeValve.IsOpen = true;
                    ExhaustValve.IsOpen = false;
                    break;
                case CombustionStrokeCycles.Compression:
                    IntakeValve.IsOpen = false;
                    ExhaustValve.IsOpen = false;
                    break;
                case CombustionStrokeCycles.Combustion:
                    IntakeValve.IsOpen = false;
                    ExhaustValve.IsOpen = false;
                    break;
                case CombustionStrokeCycles.Exhaust:
                    IntakeValve.IsOpen = false;
                    ExhaustValve.IsOpen = true;
                    break;
            }
        }
        protected override bool ShouldActivate(CarPart target)
        {
            var CE = (Engine as CombustionEngine);
            var stroke = CE.StrokeCycler.StrokeCycle;
            return (
                (target is FuelPump && stroke == CombustionStrokeCycles.Intake) && base.ShouldActivate(target))
                || (target is Distributor && stroke == CombustionStrokeCycles.Combustion)
                || (target is CombustionStrokeCycler && (stroke == CombustionStrokeCycles.Compression || stroke == CombustionStrokeCycles.Exhaust)) 
                ;
                
        }
        public override bool CanTransferTo(UnitContainer receiver)
        {
            if (receiver is FuelPump)
                return true;
            return false;
        }
        public override bool CanBeDrainedBy(UnitContainer receiver)
        {
            if (receiver is FuelPump)
                return true;
            return false;
        }
    }
}