using GameNetcodeStuff;
using TerminalApi.Classes;
using UnityEngine;
using UnityEngine.Events;
using static TerminalApi.TerminalApi;

namespace VanillaAddons.TerminalChanges
{
    public class ShipCommands
    {
        public ShipCommands() 
        {
            Door();
            Lights();
            Teleporter();
            InverseTeleporter();
        }

        void Door()
        {
            AddCommand("door", new CommandInfo
            {
                Category = "Ship",
                Description = "Opens or closes the ship door.",
                DisplayTextSupplier = OnDoorCommand
            },
            clearPreviousText: false);

            string OnDoorCommand()
            {
                HangarShipDoor door = Object.FindObjectOfType<HangarShipDoor>();
                if (door != null && !door.overheated)
                {
                    if (door.doorPower == 1f)
                    {
                        door.triggerScript.onInteract.Invoke(GameNetworkManager.Instance.localPlayerController);
                    }
                    else
                    {
                        door.triggerScript.onStopInteract.Invoke(GameNetworkManager.Instance.localPlayerController);
                    }
                    return "Toggling door...\n";
                }
                return "Door not found or cannot be toggled.\n";
            }
        }

        void Lights()
        {
            AddCommand("lights", new CommandInfo
            {
                Category = "Ship",
                Description = "Toggles the lights on or off.",
                DisplayTextSupplier = OnLightsCommand
            },
            clearPreviousText: false);

            string OnLightsCommand()
            {
                InteractTrigger[] triggers = Object.FindObjectsOfType<InteractTrigger>();
                foreach (var trigger in triggers)
                {
                    if (trigger.name == "LightSwitch")
                    {
                        trigger.onInteract.Invoke(GameNetworkManager.Instance.localPlayerController);
                        return "Toggling lights...\n";
                    }
                }
                return "Light switch not found.\n";
            }
        }

        void InverseTeleporter()
        {
            AddCommand("itp", new CommandInfo
            {
                Category = "Ship",
                Description = "Activates the Inverse Teleporter.",
                DisplayTextSupplier = OnInverseTeleporterCommand
            },
            clearPreviousText: false);

            string OnInverseTeleporterCommand()
            {
                ShipTeleporter[] teleporters = Object.FindObjectsOfType<ShipTeleporter>();
                foreach (var teleporter in teleporters)
                {
                    if (teleporter.buttonTrigger.interactable && teleporter.isInverseTeleporter)
                    {
                        teleporter.buttonTrigger.onInteract.Invoke(GameNetworkManager.Instance.localPlayerController);
                        return "Inverse teleporting...\n";
                    }
                }
                return "Inverse teleportation failed. No valid inverse teleporter found.\n";
            }
        }

        void Teleporter()
        {
            AddCommand("tp", new CommandInfo
            {
                Category = "Ship",
                Description = "Teleports selected player back to the ship.",
                DisplayTextSupplier = OnTeleporterCommand
            },
            clearPreviousText: false);

            string OnTeleporterCommand()
            {
                ShipTeleporter[] teleporters = Object.FindObjectsOfType<ShipTeleporter>();
                foreach (var teleporter in teleporters)
                {
                    if (teleporter.buttonTrigger.interactable && !teleporter.isInverseTeleporter)
                    {
                        teleporter.buttonTrigger.onInteract.Invoke(GameNetworkManager.Instance.localPlayerController);
                        return "Teleporting...\n";
                    }
                }
                return "Teleportation failed. No valid teleporter found.\n";
            }
        }
    }
}
