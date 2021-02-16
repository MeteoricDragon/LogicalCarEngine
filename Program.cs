using LogicalEngine.Engines;
using System;
using System.Collections.Generic;

namespace LogicalEngine
{
    class Program
    {
        private static List<string> Options = new List<string>();
        private const string UnformattedMenuItem = "{0}) {1}";
        private static List<string> MenuItems = new List<string>();
        private const string Item_Exit = "Exit";
        private const string Item_StartEngine = "Start Engine";
        private const string Item_CycleEngine = "Cycle Engine";
        static void Main(string[] args)
        {
            Output.Legend();
            
            var engine = new ICEOverheadValveEngine();

            var selection = -1;
            while (selection != 0)
            {
                RefreshMenuOptions(engine);
                RefreshDisplayMenu();
                ShowMenu();
                selection = PromptSelection(engine);
            }

        }
        static void RefreshMenuOptions(Engine engine)
        {
            Options.Clear();
            Options.Add(Item_Exit);

            if (!engine.IsCycling)
            { 
                Options.Add(Item_StartEngine);
            }
            if (engine is CombustionEngine cE && cE.Ignition.IgnitionSwitchOn)
            {
                Options.Add(Item_CycleEngine);
            }
        }
        static void RefreshDisplayMenu()
        {
            MenuItems.Clear();
            for (int i = 0; i < Options.Count; i++)
                MenuItems.Add(String.Format(UnformattedMenuItem, i, Options[i]));
        }
        static void ShowMenu()
        {
            foreach (string s in MenuItems)
                Console.WriteLine(s);
            Console.WriteLine("Enter Choice:");
        }
        static int PromptSelection(Engine engine)
        {
            var isNumeric = int.TryParse(Console.ReadLine(), out int choice);

            string option = "";
            if (isNumeric && choice < Options.Count && choice >= 0)
            {
                option = Options[choice];                    
            }
            else
            {
                choice = -1;
            }
            
            switch (option)
            {
                case Item_Exit:
                    Console.WriteLine("Exiting...");
                    break;
                case Item_StartEngine:
                    engine.StartEngine();
                    break;
                case Item_CycleEngine:
                    engine.RunEngine();
                    break;
                default:
                    Console.WriteLine("Failure...");
                    break;
            }
            return choice;
        }
    }
}
