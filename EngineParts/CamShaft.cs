using LogicalEngine.Engines;
using System;
using System.Collections.Generic;
using System.Text;
using static LogicalEngine.Engines.CombustionEngine;

namespace LogicalEngine.EngineParts
{
    public class CamShaft : MechanicalPart
    {
        public override string UserFriendlyName { get => "Camshaft"; }
        public CamShaft(Engine e) : base(e)
        {
        }

        protected override bool TryActivateNext(CarPart partToActivate, CarPart activatingPart)
        {
            if (partToActivate is IValve)
            {
                SetValveOpenClose(partToActivate as IValve);
                return false;
            }
                
            return base.TryActivateNext(partToActivate, activatingPart);
        }

        private void SetValveOpenClose(IValve valve)
        {           
            var CE = (Engine as CombustionEngine);

            if (CE.ScheduledStrokeCycle == CombustionStrokeCycle.Intake)
                valve.IsOpen = (valve is ValveIntake);
            else if (CE.ScheduledStrokeCycle == CombustionStrokeCycle.Compression)
                valve.IsOpen = false;
            else if (CE.ScheduledStrokeCycle == CombustionStrokeCycle.Combustion)
                valve.IsOpen = false;
            else
                valve.IsOpen = (valve is ValveExhaust);

            Console.WriteLine("@#@#@# " + (valve as CarPart).UserFriendlyName + (valve.IsOpen ? "Open" : "Closed"));
        }

        // if camshaft is activating valves, shouldn't transfer momentum.  
        // if I transfer momentum, when units get added in the valve, it will report as fuel being added.
        // That would work for fuel pump sending fuel, but not for camshaft opening valve
    }
}