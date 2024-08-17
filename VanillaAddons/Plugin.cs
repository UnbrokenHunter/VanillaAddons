using BepInEx;
using BepInEx.Logging;
using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VanillaAddons
{
    [BepInPlugin(modGUID, modName, modVersion)]
    public class VanillaAddonsBase : BaseUnityPlugin
    {
        private const string modGUID = "hunter.VanillaAddons";
        private const string modName = "Vanilla Addons";
        private const string modVersion = "1.0.0.0";

        private readonly Harmony harmony = new Harmony(modGUID);

        private static VanillaAddonsBase Instance;

        internal ManualLogSource mls;



        void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
        
            mls = BepInEx.Logging.Logger.CreateLogSource(modGUID);

            mls.LogInfo("VanillaAddons has been created successfully");

            harmony.PatchAll(typeof(VanillaAddonsBase));
        }
    }
}
