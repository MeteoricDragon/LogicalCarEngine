using LogicalEngine.EngineParts;
using System;
using System.Collections.Generic;
using System.Text;

namespace LogicalEngine
{
    public static class Output
    {
        public static string Indent = "";
        public static string TransferIndent = "==";
        public static void ConnectedPartsHeader(CarPart part)
        {
            Console.WriteLine(Indent + "<" + part.UserFriendlyName + ">");
            Indent = "    " + Indent;
        }

        public static void ConnectedPartsFooter(CarPart part)
        {
            Indent = Indent[4..];
            Console.WriteLine(Indent + "</" + part.UserFriendlyName + ">");
        }

        public static void TransferReportHeader(CarPart partSender, CarPart partReceiver)
        {
            Console.WriteLine(Indent + "[" + partReceiver.UserFriendlyName + "]");
        }
        public static void TransferReportDrainFail(string name)
        {
            Console.WriteLine(Indent + TransferIndent + "ERROR: Failed to drain " + name);
        }
        public static void TransferReportDrain(CarPart p, int drainAmount)
        {
            Console.WriteLine(Indent + "--" + p.UserFriendlyName + ": " + p.UnitsOwned + " - " + drainAmount + " " + p.UnitType);
        }
        public static void TransferReportFill(CarPart p, int fillAmount)
        {
            Console.WriteLine(Indent + "++" + p.UserFriendlyName + ": " + p.UnitsOwned + " + " + fillAmount + " " + p.UnitType);

        }
    }
}
