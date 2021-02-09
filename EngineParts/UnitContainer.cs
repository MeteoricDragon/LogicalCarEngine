using System;
using System.Collections.Generic;
using System.Text;

namespace LogicalEngine.EngineParts
{
    public abstract class UnitContainer
    {
        public List<CarPart> BackupSources { get; set; }
        public UnitContainer Reservoir { get; set; }
        virtual public string UserFriendlyName { get => "Unit Container"; }
        virtual public string UnitTypeSent { get => "Units"; }
        virtual public int UnitsMax { get => 40; }
        public int UnitsOwned { get; protected set; }
        virtual public int UnitsToGive { get => 5; }
        virtual public int UnitsToConsume { get => 5; }
        virtual public int UnitTriggerThreshold { get => 1; }
        public bool CanDrawFromBattery { get; set; }
        public bool CanChargeBattery { get; set; }
        public bool CanDrawFuel { get; set; }
        public bool IsBackupSource { get; protected set; }
        public bool HasBackupSource { get; internal set; }
        public virtual bool TryTransferUnits(UnitContainer receiver)
        {
            var success = false;
            if (CanDrain(receiver) == false)
            {
                Output.TakeFromReservoirFailReport(UserFriendlyName);
                return success;
            }

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
                if (CanFill(receiver))
                    receiver.Fill(UnitsToGive);
            }

            return success;
        }
        protected virtual bool CanTransfer(UnitContainer receivingPart)
        {
            return true;
        }
        private bool HasEnoughToDrain(int drainAmount)
        {
            return (UnitsOwned - drainAmount >= 0);
        }
        protected virtual bool CanDrain(UnitContainer receiver)
        {
            return true;
        }
        private void Drain(int drainAmount)
        {
            Output.DrainReport(this, drainAmount);
            UnitsOwned -= drainAmount;
        }
        protected virtual bool CanFill(UnitContainer receiver)
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
