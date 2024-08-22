using BepInEx;
using System.IO;
using UnityEngine;

namespace VanillaAddons.InsanityChanges
{
    internal class Insanity 
    {
        internal static GameObject SanityModObject;

        private static string DataFolder => Path.GetFullPath(Paths.PluginPath + "/VanillaAddons");
        internal static AudioClip[] AuditoryHallucinations { get; set; }
        internal static AudioClip[] Stingers { get; set; }
        internal static AudioClip[] PlayerHallucinationSounds { get; set; }
        internal static AudioClip[] LCGameSFX { get; set; }
        internal static AudioClip[] Drones { get; set; }

        public Insanity()
        {
            GameObject sanityObject = new GameObject("Sanity Mod");
            sanityObject.AddComponent<HallucinationManager>().enabled = false;
            SanityModObject = sanityObject;
            SanityModObject.hideFlags = HideFlags.HideAndDontSave;

            On.StartOfRound.StartGame += StartOfRound_StartGame;
            On.StartOfRound.ShipLeave += StartOfRound_ShipLeave;

            LoadSounds();
        }

        private void StartOfRound_StartGame(On.StartOfRound.orig_StartGame orig, StartOfRound self)
        {
            orig(self);

            VanillaAddonsBase.Instance.mls.LogInfo("Insanity Hook StartGame");

            SanityModObject.GetComponent<HallucinationManager>().enabled = true;
        }

        private void StartOfRound_ShipLeave(On.StartOfRound.orig_ShipLeave orig, StartOfRound self)
        {
            orig(self);

            SanityModObject.GetComponent<HallucinationManager>().RemoveAllHallucionations();
            SanityModObject.GetComponent<HallucinationManager>().enabled = true;
        }

        private static void LoadSounds()
        {
            string sfxBundle = Path.Combine(DataFolder, "soundresources_sfx");
            string ambientBundle = Path.Combine(DataFolder, "soundresources_stingers");
            string fakePlayerBundle = Path.Combine(DataFolder, "soundresources_hallucination");
            string droneBundle = Path.Combine(DataFolder, "soundresources_drones");
            string lcGameBundle = Path.Combine(DataFolder, "soundresources_lc");

            AssetBundle sfx = AssetBundle.LoadFromFile(sfxBundle);
            AssetBundle ambience = AssetBundle.LoadFromFile(ambientBundle);
            AssetBundle fakePlayer = AssetBundle.LoadFromFile(fakePlayerBundle);
            AssetBundle drone = AssetBundle.LoadFromFile(droneBundle);
            AssetBundle lcGame = AssetBundle.LoadFromFile(lcGameBundle);
            if (sfx is null || ambience is null | fakePlayer is null || droneBundle is null || lcGameBundle is null)
            {
                VanillaAddonsBase.Instance.mls.LogError("Failed to load audio assets!");
                return;
            }
            AuditoryHallucinations = sfx.LoadAllAssets<AudioClip>();
            Stingers = ambience.LoadAllAssets<AudioClip>();
            PlayerHallucinationSounds = fakePlayer.LoadAllAssets<AudioClip>();
            Drones = drone.LoadAllAssets<AudioClip>();
            LCGameSFX = lcGame.LoadAllAssets<AudioClip>();
        }
    }
}

