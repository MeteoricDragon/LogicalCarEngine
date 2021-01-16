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
        public CarPart Reservoir { get; set; }
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
            var carPartSender = (sender as CarPart);
            Output.ConnectedPartsHeader(carPartSender);
            AdjustFlow(carPartSender);
            ActivateConnectedParts(carPartSender);
            Output.ConnectedPartsFooter(carPartSender);
        }
        protected virtual void ActivateConnectedParts(CarPart sender)
        {
            foreach (CarPart connected in sender.ConnectedParts)
            {
                bool transferSuccess = false;
                bool transferAllowed = ConditionsForTransferMet(sender);

                if (transferAllowed)
                    transferSuccess = TryTransferUnits(connected);
                    
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

        protected virtual bool ConditionsForTransferMet(CarPart activatingPart)
        {


            return true;
        }
        protected virtual bool ThresholdTriggered(CarPart activatingPart) 
        {
            return (UnitsOwned >= UnitTriggerThreshold);
        }

        protected virtual void AdjustFlow(CarPart sender)
        {

        }

        public virtual bool TryTransferUnits(CarPart receiver)
        {
            
            if (CanDrain(UnitsToConsume))
            {
                TakeFromReservoir(UnitsToConsume);
                Drain(UnitsToConsume);
                receiver.Fill(UnitsToGive);
                return true;
            }
            return false;
        }
        private bool CanDrain(int drainAmount)
        {
            return (drainAmount - UnitsOwned >= 0);
        }
        private void Drain(int drainAmount)
        {
            Output.DrainReport(this, drainAmount);
            UnitsOwned -= drainAmount;
        }
        private void TakeFromReservoir(int drainAmount)
        {
            if (Reservoir == null)
                return;

            var needed = Math.Max(drainAmount - UnitsOwned, 0);
            if (Reservoir.CanDrawOut(needed))
            {
                Reservoir.Drain(needed);
                Fill(Reservoir.UnitsToGive);
            }
            else
            {
                Output.TakeFromReservoirFailReport(Reservoir.UserFriendlyName);
            }
        }
        private bool CanDrawOut(int drawAmount)
        {
            if (Reservoir != null)
            {
                return Reservoir.CanDrain(drawAmount);
            }
            return false;
        }
        private void Fill(int fillAmount)
        {
            Output.FillReport(this, fillAmount);
            UnitsOwned = Math.Min(UnitsOwned + fillAmount, UnitsMax);
        }
    }
}