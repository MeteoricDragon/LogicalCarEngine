using LogicalEngine.EngineContainers;
using LogicalEngine.Engines;
using System;
using System.Collections.Generic;
using System.Text;
using static LogicalEngine.EngineParts.CombustionChambers;

namespace LogicalEngine.EngineParts
{
    public abstract class CarPart
    {
        public event EventHandler Activate;
        public List<CarPart> ConnectedParts;
        public CarPart Reservoir { get; set; }
        virtual public bool EngineCycleComplete { get => Engine.CycleComplete; }
        virtual public string UserFriendlyName { get => "Car Part"; }
        virtual public string UnitType { get => "Units"; }
        virtual public int UnitsMax { get => 50; }
        public int UnitsOwned { get; protected set; }
        virtual public int UnitsToGive { get => 15; }
        virtual public int UnitsToConsume { get => 5; }
        virtual public int UnitTriggerThreshold { get => 1; }
        public bool CanDrawFromBattery { get; set; }
        public bool CanChargeBattery { get; set; }
        public bool CanDrawFuel { get; set; }

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
            var carPartSender = sender as CarPart;
            Output.ConnectedPartsHeader(carPartSender);
            ActivateConnectedParts(carPartSender);
            Output.ConnectedPartsFooter(carPartSender);
        }
        protected virtual void ActivateConnectedParts(CarPart sender)
        {
            foreach (CarPart connected in sender.ConnectedParts)
            {
                bool transferSuccess = false;
                bool transferAllowed = connected.TransferConditionsMet(sender);

                if (transferAllowed)
                    transferSuccess = sender.TryTransferUnits(connected);

                if (transferSuccess && connected.ShouldDoTrigger(sender))
                {
                    connected.AdjustFlow(sender);
                    connected.InvokeActivate();
                }
            }
        }
        protected virtual void InvokeActivate()
        {
            Activate?.Invoke(this, new EventArgs());
        }
        
        protected virtual bool TransferConditionsMet(CarPart transferingPart)
        {
            return true;
        }

        protected virtual bool ShouldDoTrigger(CarPart activatingPart) 
        {
            return (UnitsOwned >= UnitTriggerThreshold);
        }

        protected virtual void AdjustFlow(CarPart sender)
        {

        }

        public virtual bool TryTransferUnits(CarPart receiver)
        {
            var success = false;
            if (CanDrain() == false)
                return success;

            if ((CanDrawFromBattery || CanDrawFuel)
                && Reservoir.HasEnoughToDrain(UnitsToConsume)
                && !HasEnoughToDrain(UnitsToConsume))
            {
                Reservoir.Drain(UnitsToConsume);
                Fill(Reservoir.UnitsToGive);
            }

            if (HasEnoughToDrain(UnitsToConsume))
            {
                success = true;
                Drain(UnitsToConsume);
                if (receiver.CanFill(this))
                    receiver.Fill(UnitsToGive);
            }

            return success;
        }
        private bool HasEnoughToDrain(int drainAmount)
        {
            return (UnitsOwned - drainAmount >= 0);
        }
        protected virtual bool CanDrain()
        {
            return true;
        }
        private void Drain(int drainAmount)
        {
            Output.DrainReport(this, drainAmount);
            UnitsOwned -= drainAmount;
        }
        protected virtual bool CanFill(CarPart givingPart)
        {
            return true;
        }
        private void Fill(int fillAmount)
        {
            Output.FillReport(this, fillAmount);
            UnitsOwned = Math.Min(UnitsOwned + fillAmount, UnitsMax);
        }
    }
}