using System.Linq;
using UnityEngine;

namespace VanillaAddons.InsanityChanges.Hallucinations
{
    internal class DroneHallucination : Hallucination
    {
        public DroneHallucination()
        {
            Init("DroneSound", 10, 25f);
        }

        public override void InvokeHallucination()
        {
            VanillaAddonsBase.Instance.mls.LogInfo("Invoke Drone Sound Hallucionation");

            AudioClip clip = Insanity.Drones[Random.Range(0, Insanity.Drones.Length - 1)];
            VanillaAddonsBase.Instance.mls.LogInfo($"Playing Hallucination {clip.name}");

            SoundManager.Instance.PlaySoundAroundLocalPlayer(clip, 1.7f);

            HallucinationManager.LocalPlayer.JumpToFearLevel(0.4f);
        }

        public override void RemoveHallucination()
        {
            VanillaAddonsBase.Instance.mls.LogInfo("Remove Drone Sound Hallucionation Post");

        }
    }
}
