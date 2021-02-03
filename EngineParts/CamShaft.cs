using LogicalEngine.Engines;
using System;
using System.Collections.Generic;
using System.Text;
using static LogicalEngine.EngineParts.CombustionChambers;
using static LogicalEngine.Engines.CombustionEngine;

namespace LogicalEngine.EngineParts
{
    public class CamShaft : MechanicalPart
    {
        public override string UserFriendlyName { get => "Camshaft"; }
        public CamShaft(Engine e) : base(e)
        {
        }

        protected override bool ShouldChangeEngineStage(CarPart target)
        {
            var CE = (Engine as CombustionEngine);
            var stroke = CE.Chamber.StrokeCycle;
            return (
                (target is FuelPump && stroke == CombustionStrokeCycles.Start))
                || (target is Distributor && stroke == CombustionStrokeCycles.Compression)
                || (target is ValveIntake && stroke == CombustionStrokeCycles.Intake)
                || (target is ValveExhaust && stroke == CombustionStrokeCycles.Combustion)
                || (stroke == CombustionStrokeCycles.Exhaust);
        }

        protected override void ChangeEngineStage(CarPart target)
        {
            (Engine as CombustionEngine).Chamber.NextStroke();
        }
        protected override bool ShouldActivate(CarPart target)
        {
            // TODO: Properly trigger parts based on engine cycle.  Engine cycle is
            // changing through each cycle but no triggers seem to happen in each part
            // connected to camshaft.
            var CE = (Engine as CombustionEngine);
            var stroke = CE.Chamber.StrokeCycle;
            return (
                (target is FuelPump && stroke == CombustionStrokeCycles.Intake) && base.ShouldActivate(target))
                || (target is ValveIntake && stroke == CombustionStrokeCycles.Compression) 
                || (target is Distributor && stroke == CombustionStrokeCycles.Combustion)
                || (target is ValveExhaust && stroke == CombustionStrokeCycles.Exhaust);
                
        }
        protected override bool CanTransfer(UnitContainer receiver)
        {
            if (receiver is FuelPump)
                return true;
            return false;
        }
        protected override bool CanDrain(UnitContainer receiver)
        {
            if (receiver is FuelPump)
                return true;
            return false;
        }
        protected override bool CanFill(UnitContainer receiver)
        {
            if (receiver is FuelPump)
                return true;
            return false;
        }
    }
}