using LogicalEngine.EngineParts;
using LogicalEngine.Engines;
using System;
using System.Collections.Generic;
using System.Text;

namespace LogicalEngine.EngineParts
{
    public class IgnitionSwitch : ElectricalPart
    {
        public enum IgnitionState
        {
            IgnitionState_Off,
            IgnitionState_On,
            IgnitionState_Start
        }
        public IgnitionState IgnitionSwitchState { get; protected set; }
        public bool IgnitionSwitchOn { get; protected set; }
        public bool StartupOn { get; protected set; }
        public bool IsRunning { get; protected set; }
        public override string UserFriendlyName { get => "Ignition Switch"; }
        public IgnitionSwitch(Engine e) : base(e)
        {
            CanDrawFromBattery = true;
        }
        /// <summary>
        /// This should be called in a loop to make the engine run
        /// </summary>
        public void Tick()
        {
            InvokeActivate();
        }

        public void TurnClockwise()
        {
            if (IgnitionSwitchState == IgnitionState.IgnitionState_Start)
                throw new Exception();

            IgnitionSwitchState++;

            if (!IgnitionSwitchOn)
                IgnitionSwitchOn = true;
            else if (!StartupOn)
            {
                StartupOn = true;
                Engine.IsCycling = true;
            }
        }

        public void TurnCounterClockwise()
        {
            if (IgnitionSwitchState == IgnitionState.IgnitionState_Off)
                throw new Exception();

            IgnitionSwitchState--;

            if (StartupOn)
                StartupOn = false;
            else if (IgnitionSwitchOn)
                IgnitionSwitchOn = false;

            if (IgnitionSwitchState == IgnitionState.IgnitionState_Off)
                Engine.IsCycling = false;

        }

        protected override bool ShouldActivate(CarPart target)
        {
            if ((target is StarterMotor && StartupOn)
                || (target is IgnitionCoil && IgnitionSwitchOn))
                return base.ShouldActivate(target);
            return false;
        }
    }
}