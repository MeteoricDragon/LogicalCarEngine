using LogicalEngine.EngineParts;
using System;
using System.Collections.Generic;
using System.Text;
using static LogicalEngine.EngineParts.CombustionChamber;
using static LogicalEngine.Engines.CombustionEngine;

namespace LogicalEngine
{
    public static class Output
    {
        public static string Indent = "";
        public static string TransferIndent = "==";
        public static void ConnectedPartsHeader(CarPart part)
        {
            Console.WriteLine(Indent + "<" + part.UserFriendlyName + ">");
            Indent = "  " + Indent;
        }

        public static void ConnectedPartsFooter(CarPart part)
        {
            Indent = Indent[2..];
            Console.WriteLine(Indent + "</" + part.UserFriendlyName + ">");
        }

        public static void TransferHeader(CarPart partSender, CarPart partReceiver)
        {
            Console.WriteLine(Indent + "[" + partReceiver.UserFriendlyName + "]");
        }
        public static void TakeFromReservoirFailReport(string name)
        {
            Console.WriteLine(Indent + TransferIndent + "Insufficient units in " + name);
        }
        public static void DrainReport(CarPart p, int drainAmount)
        {
            Console.WriteLine(Indent + "--" + p.UserFriendlyName + ": " + p.UnitsOwned + " - " + drainAmount + " " + p.UnitType);
        }
        public static void FillReport(CarPart p, int fillAmount)
        {
            Console.WriteLine(Indent + "++" + p.UserFriendlyName + ": " + p.UnitsOwned + " + " + fillAmount + " " + p.UnitType);

        }
        public static void ChangeCycleReport(CombustionStrokeCycle cycle)
        {
            Console.WriteLine(Indent + "^^" + "Changed cycle to " + cycle);
        }
    }
}
