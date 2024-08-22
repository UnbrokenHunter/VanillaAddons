using System.Linq;
using UnityEngine;

namespace VanillaAddons.InsanityChanges.Hallucinations
{
    internal class SFXHallucination : Hallucination
    {
        public SFXHallucination()
        {
            Init("SFXSound", 50, 1f);
        }

        public override void InvokeHallucination()
        {
            VanillaAddonsBase.Instance.mls.LogInfo("Invoke SFX Sound Hallucionation");

            AudioClip[] possible = Insanity.LCGameSFX.Where(x =>
                x.name.Equals("DoorClose1") ||
                x.name.Equals("DoorClose2") ||
                x.name.Equals("DoorOpen1") ||
                x.name.Equals("DoorOpen2") ||
                x.name.Equals("GhostDevicePing") ||
                x.name.Equals("MineTrigger") ||
                x.name.Equals("PipeBurstInitialSound1") ||
                x.name.Equals("TurretSeePlayer")
            ).ToArray();
            AudioClip clip = possible[Random.Range(0, possible.Length - 1)];
            VanillaAddonsBase.Instance.mls.LogInfo($"Playing Hallucination {clip.name}");

            SoundManager.Instance.PlaySoundAroundLocalPlayer(clip, 1.7f);
        }

        public override void RemoveHallucination()
        {
            VanillaAddonsBase.Instance.mls.LogInfo("Remove SFX Sound Hallucionation Post");

        }
    }
}
