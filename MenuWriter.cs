using System;
using System.Collections.Generic;
using System.Text;
using LogicalEngine.Engines;
namespace LogicalEngine
{
    public class MenuWriter : IMenuWriter
    {
        private List<string> Options = new List<string>();
        private const string UnformattedMenuItem = "{0}) {1}";
        private List<string> MenuItems = new List<string>();
        private const string Item_Exit = "Exit";
        private const string Item_StartEngine = "Start Engine";
        private const string Item_CycleEngine = "Cycle Engine";
        private const string Item_CycleEngineWithPause = "Step Engine";

        private void RefreshMenuOptions(Engine engine)
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
                Options.Add(Item_CycleEngineWithPause);
            }
        }
        private void RefreshDisplayMenu()
        {
            MenuItems.Clear();
            for (int i = 0; i < Options.Count; i++)
                MenuItems.Add(String.Format(UnformattedMenuItem, i, Options[i]));
        }
        private void ShowMenu(Engine e)
        {
            RefreshMenuOptions(e);
            RefreshDisplayMenu();
            Console.WriteLine("~~~~~~~~~~~~~~~~~~~~~~~");
            foreach (string s in MenuItems)
                Console.WriteLine(s);
            Console.WriteLine("~~~~~~~~~~~~~~~~~~~~~~~");
            Console.Write("Enter Choice:");
        }
        public int PromptSelection(Engine engine)
        {
            ShowMenu(engine);
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
                    engine.RunFullCycle();
                    break;
                case Item_CycleEngineWithPause:
                    engine.RunFullCycle(cycleWithPause: true);
                    break;
                default:
                    Console.WriteLine("Failure...");
                    break;
            }
            return choice;
        }
    }
}
