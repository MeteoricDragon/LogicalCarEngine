using LogicalEngine;
using LogicalEngine.EngineParts;
using LogicalEngine.Engines;
using System;
using System.Collections.Generic;
using System.Text;

namespace LogicalEngine.Engines
{        
    public enum CombustionStrokeCycles
    {
        Intake,
        Compression,
        Combustion,
        Exhaust,
        End
    };
    public class CombustionStrokeCycler : AbstractPart
    {
        public CombustionStrokeCycler(Engine engine) : base(engine)
        {
            Engine = engine;
        }

        public CombustionStrokeCycles StrokeCycle { get; protected set; }

        public override string UserFriendlyName => "Combustion Cycler";
        public override int UnitsMax => 0;
        public override int UnitsToGive => 0;
        public override int UnitsToConsume => 0;
        public override int UnitTriggerThreshold => 0;
        public override bool EngineCycleComplete => false;

        public void NextStroke()
        {
            var cycle = StrokeCycle;

            switch (cycle)
            {
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
            }

            if (StrokeCycle != cycle)
            {
                StrokeCycle = cycle;
                Engine.Output.ChangeCycleReport(cycle);
            }
        }

        public void ResetStrokeCycle()
        {
            StrokeCycle = CombustionStrokeCycles.Intake;
            Engine.Output.ChangeCycleReport(StrokeCycle);
        }

        public override bool TryTransferUnits(UnitContainer receiver) { return false; }
        protected override bool PreTransferReturnToEngineLoop(CarPart sender)  { return false; }
        public override bool CanBeDrainedBy(UnitContainer receiver)  { return false; }
        public override bool CanTransferTo(UnitContainer receivingPart) { return false; }
        protected override void OnActivate(object sender, EventArgs e)
        {
            var carPartSender = sender as CarPart;
            //Output.ConnectedPartsHeader(carPartSender);
            RefreshEngineStage(carPartSender);
            TriggerConnectedParts(carPartSender);
            //Output.ConnectedPartsFooter(carPartSender);
        }

        protected override bool ShouldActivate(CarPart target)
        {
            return true;
        }
    }
}
