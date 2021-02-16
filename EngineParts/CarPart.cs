using LogicalEngine.EngineContainers;
using LogicalEngine.Engines;
using System;
using System.Collections.Generic;
using System.Text;
using static LogicalEngine.EngineParts.Cylinders;

namespace LogicalEngine.EngineParts
{
    public abstract class CarPart : UnitContainer
    {
        public event EventHandler Activate;
        public List<CarPart> ConnectedParts { get; set; }

        virtual public bool EngineCycleComplete { get => Engine.CycleComplete; }

        /// <summary>
        /// Reference to Engine that owns this part
        /// </summary>
        public Engine Engine { get; protected set; }


        public CarPart(Engine engine)
        {
            Engine = engine;
            BackupSources = new List<CarPart>();
        }

        public void AssignTargetPart(List<CarPart> subscribers)
        {
            ConnectedParts = subscribers;
            Activate += OnActivate;
        }

        protected virtual void OnActivate(object sender, EventArgs e)
        {           
            var carPartSender = sender as CarPart;
            Output.ConnectedPartsHeader(carPartSender);
            RefreshEngineStage(carPartSender);
            TriggerConnectedParts(carPartSender);
            Output.ConnectedPartsFooter(carPartSender);
        }
        protected void TriggerConnectedParts(CarPart sender)
        {
            foreach (CarPart connected in sender.ConnectedParts)
            {
                if (PreTransferReturnToEngineLoop(connected))
                    return;
                
                if (CanTransferTo(connected))
                    TryTransferUnits(connected);

                if (PostTransferReturnToEngineLoop(connected))
                    return;

                if (ShouldActivate(connected))
                {
                    connected.InvokeActivate();
                }
            }
        }
        protected void InvokeActivate()
        {
            Activate?.Invoke(this, new EventArgs());
        }
        
        protected virtual bool ShouldActivate(CarPart target) 
        {
            return target.IsAtUnitThreshold(target);
        }

        public bool IsAtUnitThreshold(CarPart target)
        {
            return (target.UnitsOwned >= target.UnitTriggerThreshold);
        }

        protected virtual void RefreshEngineStage(CarPart sender) { }
        protected virtual bool PreTransferReturnToEngineLoop(CarPart sender)
        {
            return false;
        }
        protected virtual bool PostTransferReturnToEngineLoop(CarPart sender)
        {
            return false;
        }

    }
}