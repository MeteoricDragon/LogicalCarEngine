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
            InvokeActivate(this, new EventArgs());
        }

        public void TurnIgnitionClockwise()
        {
            if (IgnitionSwitchState == IgnitionState.IgnitionState_Start)
                throw new Exception();

            IgnitionSwitchState++;

            if (!IgnitionSwitchOn)
                IgnitionSwitchOn = true;
            else if (!StartupOn)
                StartupOn = true;
        }

        public void TurnIgnitionCounterClockwise()
        {
            if (IgnitionSwitchState == IgnitionState.IgnitionState_Off)
                throw new Exception();

            IgnitionSwitchState--;

            if (StartupOn)
                StartupOn = false;
            else if (IgnitionSwitchOn)
                IgnitionSwitchOn = false;
        }

        protected override bool ActivateNext(CarPart part)
        {
            if (part is IgnitionCoil && IgnitionSwitchOn)
                return true;
            else if (part is StarterMotor && StartupOn)
                return true;
            return false;
        }

        protected override void InvokeActivate(CarPart part, EventArgs e)
        {
            base.InvokeActivate(part, e);
        }


    }
}