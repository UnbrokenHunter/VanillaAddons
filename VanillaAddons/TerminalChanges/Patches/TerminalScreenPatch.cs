using HarmonyLib;

namespace VanillaAddons.TerminalChanges.Patches
{
	[HarmonyPatch(typeof(Terminal))]
    internal class TerminalScreenPatch
    {
        [HarmonyPatch("BeginUsingTerminal")]
        [HarmonyPrefix]
        public static void OpenTerminal(Terminal __instance)
        {
            TerminalNode neofetch = new TerminalNode
            {
                displayText = Neofetch.GetNeofetch(),
                clearPreviousText = true
            };
            __instance.terminalNodes.specialNodes[13] = neofetch;

            __instance.terminalUIScreen.gameObject.SetActive(true);
        }

        [HarmonyPatch("QuitTerminal")]
        [HarmonyPrefix]
        public static void CloseTerminal(Terminal __instance)
        {
            __instance.terminalUIScreen.gameObject.SetActive(false);
        }
    }
}
