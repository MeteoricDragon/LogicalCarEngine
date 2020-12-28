using LogicalEngine.EngineContainers;
using System;
using System.Collections.Generic;
using System.Text;

namespace LogicalEngine.EngineParts
{
    public abstract class CarPart
    {
        public event EventHandler Activate;
        public List<CarPart> ConnectedParts;
        public Battery Battery { get; set; }
        virtual public string UserFriendlyName { get => "Car Part"; }
        virtual public string UnitType { get => "Units"; }
        virtual public int UnitsMax { get => 15; }
        public int UnitsOwned { get; protected set; }
        virtual public int UnitsToGive { get => 5; }
        virtual public int UnitsToConsume { get => 5; }

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
            Console.WriteLine("in " + UserFriendlyName);
            var carPartSender = (sender as CarPart);

            foreach (CarPart part in carPartSender.ConnectedParts)
            {
                if (part.TryDrain(UnitsToConsume))
                {
                    Fill(UnitsToGive);
                    Console.WriteLine("Battery: ( " + Engine.Subsystems.Find(x => x is PowerParts).Parts.Find(x => x is Battery).UnitsOwned + " )");

                }

                if (ActivateNext(part))
                {
                    InvokeActivate(part, e); 
                }
            } 
        }
        protected virtual void InvokeActivate(CarPart part, EventArgs e)
        {
            Activate?.Invoke(part, e);
        }

        protected virtual bool ActivateNext(CarPart partToActivate) 
        {
            return true; // TODO: make specific cases for true/false 
        }

        public virtual bool TryDrain(int drainAmount)
        {
            int amountNeeded = Math.Max(drainAmount - UnitsOwned, 0);
            if (amountNeeded > 0 && CanDrawFromBattery)
            {
                if (Battery.TryDrain(amountNeeded))
                    Fill(amountNeeded);
            }

            if (UnitsOwned < drainAmount)
            {
                return false;
            }
            
            UnitsOwned -= drainAmount;
            return true;
        }

        public virtual void Fill(int fillAmount)
        {
            UnitsOwned = Math.Min(UnitsOwned + fillAmount, UnitsMax);
        }
    }
}