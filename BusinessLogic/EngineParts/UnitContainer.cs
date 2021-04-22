using System;
using System.Collections.Generic;
using System.Text;

namespace LogicalEngine.EngineParts
{
    public abstract class UnitContainer
    {
        public List<CarPart> BackupSources { get; set; }
        virtual public string UserFriendlyName { get => "Unit Container"; }
        virtual public string UnitTypeSent { get => "Units"; }
        virtual public int UnitsMax { get => 40; }
        public int UnitsOwned { get; protected set; }
        virtual public int UnitsToGive { get => 5; }
        virtual public int UnitsToConsume { get => 5; }
        virtual public int UnitTriggerThreshold { get => 1; }
        public bool CanDrawFromBattery { get; set; }
        public bool CanChargeBattery { get; set; }
        public bool CanDrawFromFuelTank { get; set; }

        public virtual bool TryTransferUnits(UnitContainer receiver)
        {
            if (CanBeDrainedBy(receiver) == false)
            {
                Engine.Output?.TakeFromReservoirFailReport(UserFriendlyName);
                return false;
            }

            var transferrable = CanTransferTo(receiver);
            if (transferrable 
                 && !HasEnoughToDrain(UnitsToConsume)
                )
            {
                foreach (CarPart source in BackupSources)
                {
                    var foundUnits = source.TryTransferUnits(this);
                    if (foundUnits)
                        break;
                }
            }

            var drainable = CanBeDrainedBy(receiver);
            var success = drainable
                && HasEnoughToDrain(UnitsToConsume);
            if (success)
            {
                Drain(UnitsToConsume);
                if (transferrable)
                    receiver.Fill(UnitsToGive);
            }

            return success;
        }
        public virtual bool CanTransferTo(UnitContainer receivingPart)
        {
            return true;
        }

        private bool HasEnoughToDrain(int drainAmount)
        {
            return (UnitsOwned - drainAmount >= 0);
        }
        public virtual bool CanBeDrainedBy(UnitContainer receiver)
        {
            return true;
        }

        private void Drain(int drainAmount)
        {
            Engine.Output?.DrainReport(this, drainAmount);
            UnitsOwned -= drainAmount;
        }
        private void Fill(int fillAmount)
        {
            Engine.Output?.FillReport(this, fillAmount);
            UnitsOwned = Math.Min(UnitsOwned + fillAmount, UnitsMax);
        }
    }
}
