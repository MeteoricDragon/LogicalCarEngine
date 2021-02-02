﻿using LogicalEngine.EngineContainers;
using LogicalEngine.Engines;
using System;
using System.Collections.Generic;
using System.Text;
using static LogicalEngine.EngineParts.CombustionChambers;

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

        private void OnActivate(object sender, EventArgs e)
        {           
            var carPartSender = sender as CarPart;
            Output.ConnectedPartsHeader(carPartSender);
            TriggerConnectedParts(carPartSender);
            Output.ConnectedPartsFooter(carPartSender);
        }
        private void TriggerConnectedParts(CarPart sender)
        {
            foreach (CarPart connected in sender.ConnectedParts)
            {
                bool transferSuccess = false;
                bool transferAllowed = CanTransfer(connected);
                bool shouldAdjust = ShouldChangeEngineStage(connected);
                bool shouldActivate = false;
                bool backToEngine = BackToEngineLoop(sender);

                if (transferAllowed)
                    transferSuccess = TryTransferUnits(connected);

                if ( !backToEngine )
                    shouldActivate = ShouldActivate(connected);

                if (shouldAdjust)
                    ChangeEngineStage(connected);

                if (transferSuccess && shouldActivate)
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
            return (target.UnitsOwned >= target.UnitTriggerThreshold);
        }

        protected virtual bool ShouldChangeEngineStage(CarPart sender)
        {
            return false;
        }
        protected virtual void ChangeEngineStage(CarPart sender) { }
        protected virtual bool BackToEngineLoop(CarPart sender)
        {
            return false;
        }

    }
}