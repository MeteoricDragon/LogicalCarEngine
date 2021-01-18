﻿using System;
using System.Collections.Generic;
using System.Text;

namespace LogicalEngine.EngineParts
{
    public class StarterMotor : ElectricalPart
    {
        public override string UserFriendlyName { get => "Starter Motor"; }
        public StarterMotor(Engine e) : base(e)
        {
        }

        protected override bool TriggerConditionsMet(CarPart activatingPart)
        {
            if ((activatingPart as IgnitionSwitch).StartupOn)
                return base.TriggerConditionsMet(activatingPart);
            return false;
        }
    }
}