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
            Intake,
            Compression,
            Combustion,
            Exhaust
        };
        public CombustionStrokeCycles StrokeCycle { get; protected set; }

        public override string UserFriendlyName { get => "Combustion Chambers"; }
        public CombustionChambers(Engine e) : base(e)
        {
            Engine = e;
        }

        private void NextStroke()
        {
            var cycle = StrokeCycle;

            switch (cycle)
            {
                case CombustionStrokeCycles.Exhaust:
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

        protected override void AdjustEngineStage(CarPart sender)
        {
            bool inCombustion = StrokeCycle == CombustionStrokeCycles.Combustion;
            
            if ((sender is SparkPlugs && inCombustion) // ready to ignite then exhaust

                || (sender is SparkPlugs == false && !inCombustion)) // not igniting or ready to ignite
            {
                NextStroke();
                // TODO: implement counteracting force from counterbalanced pistons
                // that takes place of this command.

                // TODO: Get a better fix than this for staying on Exhaust for end 4 stroke cycle
                if (StrokeCycle != CombustionStrokeCycles.Exhaust) 
                    NextStroke(); 
            }
        }
        protected override bool ShouldAdjustEngineStage(CarPart sender)
        {
            return true;
        }
        protected override bool ShouldActivate(CarPart activatingPart)
        {
            return (StrokeCycle == CombustionStrokeCycles.Combustion
                && activatingPart is SparkPlugs 
                && base.ShouldActivate(activatingPart));
        }
        protected override bool TransferConditionsMet(CarPart transferringPart)
        {
            if ((StrokeCycle == CombustionStrokeCycles.Combustion 
                && transferringPart is SparkPlugs) 
                ||
                (StrokeCycle == CombustionStrokeCycles.Intake
                && transferringPart is ValveIntake ))
                return true;
            return false;
        }
        protected override bool CanFill(CarPart givingPart)
        {
            if (givingPart is SparkPlugs)
                return false;
            return true;
        }
    }
}
