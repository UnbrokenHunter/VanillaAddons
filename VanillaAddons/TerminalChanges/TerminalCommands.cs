using BepInEx;
using HarmonyLib;
using System.Reflection;
using System.Threading.Tasks;
using TerminalApi;
using TerminalApi.Classes;
using VanillaAddons.TerminalChanges;
using static TerminalApi.TerminalApi;

namespace VanillaAddons.TerminalChanges
{
    public class TerminalCommands
    {
        public TerminalCommands() 
        {
            Home();
            Time();
            Clear();
        }

        void Home()
        {
            AddCommand("home", new CommandInfo
            {
                Category = "other",
                Description = "Runs Neofetch",
                DisplayTextSupplier = OnHomeCommand
            },
            clearPreviousText: true);

            string OnHomeCommand()
            {
                return Neofetch.GetNeofetch();
            }
        }

        void Clear()
        {
            AddCommand("clear", new CommandInfo
            {
                Category = "other",
                Description = "Clears the screen.",
                DisplayTextSupplier = OnClearCommand
            },
            clearPreviousText: true);

            AddCommand("cls", OnClearCommand(), clearPreviousText: true);

            string OnClearCommand()
            {
                return "";
            }
        }

        void Time()
        {
            AddCommand("time", new CommandInfo
            {
                Category = "other",
                Description = "Displays the current time.",
                DisplayTextSupplier = OnTimeCommand
            },
            "check",
            clearPreviousText:false);


            string OnTimeCommand()
            {
                return !StartOfRound.Instance.currentLevel.planetHasTime || !StartOfRound.Instance.shipDoorsEnabled ? "You're not on a moon. There is no time here.\n" : $"The time is currently {HUDManager.Instance.clockNumber.text.Replace('\n', ' ')}.\n";
            }
        }
    }
}