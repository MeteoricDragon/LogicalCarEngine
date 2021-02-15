using LogicalEngine.Engines;
using LogicalEngine.EngineParts;
using System;
using System.Collections.Generic;
using System.Text;
using static LogicalEngine.EngineParts.Cylinders;

namespace LogicalEngine
{
    public static class Output
    {
        private static string Indent = "";
        private const int IndentStep = 3;
        private const string Prefix_Transfer = "==";
        private const string Prefix_Drain = "--";
        private const string Prefix_Fill = "++";
        private const string Prefix_ChangeStroke = "^^";
        private const string Prefix_CycleCount = "**";
        public static void ConnectedPartsHeader(UnitContainer part)
        {
            Console.WriteLine(Indent + "<" + part.UserFriendlyName + ">");
            Indent = new string(' ', IndentStep) + Indent;
        }

        public static void ConnectedPartsFooter(UnitContainer part)
        {
            Indent = Indent[IndentStep..];
            Console.WriteLine(Indent + "</" + part.UserFriendlyName + ">");
        }

        public static void TransferHeader(UnitContainer partSender, CarPart partReceiver)
        {
            Console.WriteLine(Indent + "[" + partReceiver.UserFriendlyName + "]");
        }
        public static void TakeFromReservoirFailReport(string name)
        {
            Console.WriteLine(Indent + Prefix_Transfer + name + " EMPTY!");
        }
        public static void DrainReport(UnitContainer p, int drainAmount)
        {
            Console.WriteLine(Indent + Prefix_Drain + p.UserFriendlyName + ": " + p.UnitsOwned + " - " + drainAmount + " = " + (p.UnitsOwned - drainAmount) + " " + p.UnitTypeSent);
        }
        public static void FillReport(UnitContainer p, int fillAmount)
        {
            Console.WriteLine(Indent + Prefix_Fill + p.UserFriendlyName + ": " + p.UnitsOwned + " + " + fillAmount + " = " + (p.UnitsOwned + fillAmount) + " " + p.UnitTypeSent);

        }
        public static void ChangeCycleReport(CombustionStrokeCycles cycle)
        {
            Console.WriteLine(Indent + Prefix_ChangeStroke + "Stroke: " + cycle + "===============");
        }

        public static void EngineCycleCount(int cycles)
        {
            Console.WriteLine(Indent + Prefix_CycleCount + "# Cycles: " + cycles);
        }

        public static void Legend()
        {
            var output = "<PartName>  -> beginning of PartName being triggered \n"
                       + "</PartName> -> end of PartName being triggered. \n"
                       + "indentation -> things that happen within the triggering of the part\n"
                       + "--PartName  -> this line indicates units being subtracted from PartName\n"
                       + "++PartName  -> this line indicates units being added to PartName\n"
                       + "^^Stroke:   -> indicates a change in stroke cycle";

            Console.WriteLine(output);
        }
    }
}
