using LogicalEngine.EngineParts;
using System;
using System.Collections.Generic;
using System.Text;
using static LogicalEngine.EngineParts.CombustionChambers;
using static LogicalEngine.Engines.CombustionEngine;

namespace LogicalEngine
{
    public static class Output
    {
        public static string Indent = "";
        public const int IndentStep = 3;
        public const string Prefix_Transfer = "==";
        public const string Prefix_Drain = "--";
        public const string Prefix_Fill = "++";
        public const string Prefix_ChangeStroke = "^^";
        public const string Prefix_CycleCount = "**";
        public static void ConnectedPartsHeader(CarPart part)
        {
            Console.WriteLine(Indent + "<" + part.UserFriendlyName + ">");
            Indent = new string(' ', IndentStep) + Indent;
        }

        public static void ConnectedPartsFooter(CarPart part)
        {
            Indent = Indent[IndentStep..];
            Console.WriteLine(Indent + "</" + part.UserFriendlyName + ">");
        }

        public static void TransferHeader(CarPart partSender, CarPart partReceiver)
        {
            Console.WriteLine(Indent + "[" + partReceiver.UserFriendlyName + "]");
        }
        public static void TakeFromReservoirFailReport(string name)
        {
            Console.WriteLine(Indent + Prefix_Transfer + name + " EMPTY!");
        }
        public static void DrainReport(CarPart p, int drainAmount)
        {
            Console.WriteLine(Indent + Prefix_Drain + p.UserFriendlyName + ": " + p.UnitsOwned + " - " + drainAmount + " = " + (p.UnitsOwned - drainAmount) + " " + p.UnitType);
        }
        public static void FillReport(CarPart p, int fillAmount)
        {
            Console.WriteLine(Indent + Prefix_Fill + p.UserFriendlyName + ": " + p.UnitsOwned + " + " + fillAmount + " = " + (p.UnitsOwned + fillAmount) + " " + p.UnitType);

        }
        public static void ChangeCycleReport(CombustionStrokeCycles cycle)
        {
            Console.WriteLine(Indent + Prefix_ChangeStroke + "Stroke: " + cycle);
        }

        public static void EngineCycleCount(int cycles)
        {
            Console.WriteLine(Indent + Prefix_CycleCount + "# Cycles: " + cycles);
        }

        public static void Legend()
        {
            // print a legend

        }
    }
}
