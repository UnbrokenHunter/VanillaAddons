
namespace VanillaAddons.TerminalChanges.Patches
{
    internal class TerminalScreenPatch
    {

        internal static void Init()
        {
            On.Terminal.BeginUsingTerminal += Terminal_BeginUsingTerminal;
            On.Terminal.QuitTerminal += Terminal_QuitTerminal;
        }

        public static void Terminal_BeginUsingTerminal(On.Terminal.orig_BeginUsingTerminal orig,
            Terminal self)
        {
            TerminalNode neofetch = new TerminalNode
            {
                displayText = NeofetchText.GetNeofetch(),
                clearPreviousText = true
            };
            self.terminalNodes.specialNodes[13] = neofetch;

            self.terminalUIScreen.gameObject.SetActive(true);

            orig(self);
        }

        public static void Terminal_QuitTerminal(On.Terminal.orig_QuitTerminal orig,
            Terminal self, bool syncTerminalInUse = true)
        {
            self.terminalUIScreen.gameObject.SetActive(false);

            orig(self, syncTerminalInUse);
        }
    }
}
