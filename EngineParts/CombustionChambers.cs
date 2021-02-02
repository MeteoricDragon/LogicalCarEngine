using LogicalEngine;
using LogicalEngine.EngineParts;
using LogicalEngine.Engines;
using System;
using System.Collections.Generic;
using System.Text;
using static LogicalEngine.Engines.CombustionEngine;

namespace LogicalEngine.EngineParts
{
    public class CombustionChambers : FuelPart
    {
        // TODO: consume all fuel when changing from Combustion to exhaust and after transfer to pistons.
        public enum CombustionStrokeCycles
        {
            Start,
            Intake,
            Compression,
            Combustion,
            Exhaust,
            End
        };
        public CombustionStrokeCycles StrokeCycle { get; protected set; }

        public override string UserFriendlyName { get => "Combustion Chambers"; }
        public CombustionChambers(Engine e) : base(e)
        {
            Engine = e;
        }

        public void NextStroke()
        {
            var cycle = StrokeCycle;

            switch (cycle)
            {
                case CombustionStrokeCycles.Start:
                    cycle = CombustionStrokeCycles.Intake;
                    break;
                case CombustionStrokeCycles.Intake:
                    cycle = CombustionStrokeCycles.Compression;
                    break;
                case CombustionStrokeCycles.Compression:
                    cycle = CombustionStrokeCycles.Combustion;
                    break;
                case CombustionStrokeCycles.Combustion:
                    cycle = CombustionStrokeCycles.Exhaust;
                    break;
                case CombustionStrokeCycles.Exhaust:
                    cycle = CombustionStrokeCycles.End;
                    break;
                case CombustionStrokeCycles.End:
                    cycle = CombustionStrokeCycles.Start;
                    break;
            }

            if (StrokeCycle != cycle)
            {
                StrokeCycle = cycle;
                Output.ChangeCycleReport(cycle);
            }
        }

        public void ResetStrokeCycle()
        {
            StrokeCycle = CombustionStrokeCycles.Intake;
        }

        protected override bool ShouldActivate(CarPart target)
        {
            return (target is ValveExhaust exhaust && exhaust.IsOpen) 
                || (target is Pistons && base.ShouldActivate(target));
        }
        protected override bool CanTransfer(UnitContainer receiver)
        {
            if ((receiver is ValveExhaust exhaust && exhaust.IsOpen) 
                || (receiver is Pistons))
                return true;
            return false;
        }
        protected override bool CanFill(UnitContainer receiver)
        {
            if ((receiver is ValveExhaust exhaust && exhaust.IsOpen)
                || (receiver is Pistons))
                return true;
            return false;
        }
    }
}
