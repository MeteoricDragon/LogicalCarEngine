using LogicalEngine.EngineContainers;
using LogicalEngine.Engines;
using System;
using System.Collections.Generic;
using System.Text;
using static LogicalEngine.EngineParts.CombustionChamber;

namespace LogicalEngine.EngineParts
{
    public abstract class CarPart
    {
        public event EventHandler Activate;
        public List<CarPart> ConnectedParts;
        public Battery Battery { get; set; }
        virtual public bool EngineCycleComplete { get => Engine.CycleComplete; }
        virtual public string UserFriendlyName { get => "Car Part"; }
        virtual public string UnitType { get => "Units"; }
        virtual public int UnitsMax { get => 50; }
        public int UnitsOwned { get; protected set; }
        virtual public int UnitsToGive { get => 15; }
        virtual public int UnitsToConsume { get => 5; }
        virtual public int UnitTriggerThreshold { get => 1; }

        protected bool TriggerOnlyIfTransferSuccess = true;
        public bool CanDrawFromBattery { get; set; }
        public bool CanChargeBattery { get; set; }
        // TODO: bools for fuel too?

        /// <summary>
        /// Reference to Engine that owns this part
        /// </summary>
        public Engine Engine { get; protected set; }

        public CarPart(Engine engine)
        {
            Engine = engine;
        }

        public void AssignTargetPart(List<CarPart> subscribers)
        {
            ConnectedParts = subscribers;
            foreach (CarPart p in ConnectedParts)
            {
                Activate += p.OnActivate;
            }
        }

        protected virtual void OnActivate(object sender, EventArgs e)
        {           
            var carPartSender = (sender as CarPart);
            Output.ConnectedPartsHeader(carPartSender);
            AdjustFlow();
            ActivateConnectedParts(carPartSender);
            Output.ConnectedPartsFooter(carPartSender);
        }
        protected virtual void ActivateConnectedParts(CarPart sender)
        {
            foreach (CarPart connected in sender.ConnectedParts)
            {
                //Output.TransferReportHeader(carPartSender, connected);
                var transferSuccess = TryTransferUnits(sender, connected);
                    
                if (connected.ThresholdTriggered(sender)
                    && (transferSuccess == connected.TriggerOnlyIfTransferSuccess))
                {   
                    connected.InvokeActivate();
                }
            }
        }
        protected virtual void InvokeActivate()
        {
            Activate?.Invoke(this, new EventArgs());
        }

        protected virtual bool ThresholdTriggered(CarPart activatingPart) 
        {
            return (UnitsOwned >= UnitTriggerThreshold);
        }

        protected virtual void AdjustFlow()
        {

        }

        public virtual bool TryTransferUnits(CarPart sender, CarPart receiver)
        {
            if (sender.TryDrain(UnitsToConsume))
            {
                receiver.Fill(sender.UnitsToGive);
                return true;
            }
            return false;
        }
        public virtual bool TryDrain(int drainAmount)
        {
            int amountNeeded = Math.Max(drainAmount - UnitsOwned, 0);
            if (amountNeeded > 0 && CanDrawFromBattery)
            {
                if (Battery.TryDrain(amountNeeded))
                {
                    Fill(amountNeeded);
                }
            }

            if (UnitsOwned < drainAmount)
            {
                //Output.TransferReportDrainFail(UserFriendlyName);
                return false;
            }

            Output.TransferReportDrain(this, drainAmount);
            UnitsOwned -= drainAmount;

            return true;
        }
        public virtual void Fill(int fillAmount)
        {
            Output.TransferReportFill(this, fillAmount);
            UnitsOwned = Math.Min(UnitsOwned + fillAmount, UnitsMax);
        }
    }
}