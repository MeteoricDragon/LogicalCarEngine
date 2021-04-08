﻿using LogicalEngine.EngineContainers;
using LogicalEngine.EngineParts;
using LogicalEngine.Engines;
using System;
using System.Collections.Generic;
using System.Text;

namespace LogicalEngine
{
    public abstract class Engine
    {
        public int CycleCount = 0;
        public abstract bool CycleComplete { get; }
        public abstract bool IsCycling { get; set; }
        protected EngineOperationOrder EngineOrder;
        public List<EngineSubsystem> Subsystems { get; protected set; }
        public IOutput Output { get; private set; }
        public List<CarPart> AllParts
        {
            get {
                var parts = new List<CarPart>();
                foreach (var x in Subsystems)
                {
                    parts.AddRange(x.Parts);
                }
                return parts;
            }
        }

        public virtual void RunFullCycle(bool cycleWithPause = false)
        {

            if (CycleComplete)
                Output.EngineCycleCount(++CycleCount);
        }



        public abstract void StartEngine();


        public virtual void StopEngine()
        {

        }

        public Engine()
        {
            Subsystems = new List<EngineSubsystem>();
            EngineOrder = new EngineOperationOrder();
        }

        protected void AssembleEngine() 
        { 
            foreach (EngineSubsystem s in Subsystems)
            {
                foreach (CarPart p in s.Parts)
                {
                    AssignPartListToPart(p);
                }
            }
        }

        protected abstract void AssignPartListToPart(CarPart p);

        public void SetServiceRefs(IOutput output)
        {
            Output = output;
            foreach (UnitContainer u in AllParts)
            {
                u.Output = output;
            }
        }
    }
}