﻿using BepInEx;
using BepInEx.Logging;
using HarmonyLib;
using System.Reflection;
using VanillaAddons.TerminalChanges;

namespace VanillaAddons
{
    [BepInPlugin(PluginInfo.MOD_GUID, PluginInfo.MOD_Name, PluginInfo.MOD_Version)]
    [BepInDependency("atomic.terminalapi", "1.5.0")]
    public class VanillaAddonsBase : BaseUnityPlugin
    {
        private readonly Harmony harmony = new Harmony(PluginInfo.MOD_GUID);

        public static VanillaAddonsBase Instance;
         
        internal ManualLogSource mls;


        void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
        
            mls = BepInEx.Logging.Logger.CreateLogSource(PluginInfo.MOD_GUID);

            mls.LogInfo("VanillaAddons has been created successfully");

            Harmony.CreateAndPatchAll(Assembly.GetExecutingAssembly());

            new TerminalCommands();
        }
    }
}
 