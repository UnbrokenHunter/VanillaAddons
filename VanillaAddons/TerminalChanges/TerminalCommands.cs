using BepInEx;
using HarmonyLib;
using System.Reflection;
using System.Threading.Tasks;
using TerminalApi;
using TerminalApi.Classes;
using VanillaAddons.TerminalChanges;
using VanillaAddons.TerminalChanges.Patches;
using static TerminalApi.TerminalApi;

namespace VanillaAddons.TerminalChanges
{
    public class TerminalCommands
    {
        public TerminalCommands() 
        {
            TerminalScreenPatch.Init();

            Neofetch();
            Ship();
            Time();
            Clear();
            
            new Files();
        }

        void Ship()
        {
            AddCommand("ship", new CommandInfo
            {
                Category = "other",
                Description = "Displays the amount of scrap on the ship.",
                DisplayTextSupplier = GetShipLootCount
            },
            clearPreviousText: false);


            string GetShipLootCount()
            {
                int lootCount = 0;
                int lootValue = 0;

                // Find all grabbable objects in the game
                GrabbableObject[] allObjects = UnityEngine.Object.FindObjectsOfType<GrabbableObject>();

                // Filter out the objects that are considered loot and are located on the ship
                foreach (var obj in allObjects)
                {
                    if (obj.itemProperties.isScrap && obj.isInShipRoom && obj.isInElevator)
                    {
                        lootCount++;
                        lootValue += obj.scrapValue;
                    }
                }

                return $"There are {lootCount} items on the ship, worth a total of '{lootValue}.\n";
            }
        }

        void Neofetch()
        {
            AddCommand("neofetch", new CommandInfo
            {
                Category = "other",
                Description = "Runs Neofetch",
                DisplayTextSupplier = OnNeofetchCommand
            },
            clearPreviousText: true);

            string OnNeofetchCommand()
            {
                return NeofetchText.GetNeofetch();
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