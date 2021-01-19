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

        protected override void AdjustFlow(CarPart sender)
        {
            bool inCombustion = StrokeCycle == CombustionStrokeCycles.Combustion;
            
            if ((sender is SparkPlugs && inCombustion) // ready to ignite then exhaust

                || (sender is SparkPlugs == false && !inCombustion)) // not igniting or ready to ignite
            {
                NextStroke();
                // TODO: implement counteracting force from counterbalanced pistons
                // that takes place of this command.
                NextStroke(); 
            }
        }
        protected override bool TriggerConditionsMet(CarPart activatingPart)
        {
            var baseCond = base.TriggerConditionsMet(activatingPart);

            return baseCond;
        }
        protected override bool TransferConditionsMet(CarPart activatingPart)
        {
            var Stroke = (Engine as CombustionEngine).Chamber.StrokeCycle;
            if ((Stroke == CombustionStrokeCycles.Combustion 
                && activatingPart is SparkPlugs) 
                ||
                ( Stroke == CombustionStrokeCycles.Intake
                && activatingPart is ValveIntake ))
                return true;
            return false;
        }
    }
}
