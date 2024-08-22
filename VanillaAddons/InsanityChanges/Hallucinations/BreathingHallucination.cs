using System.Linq;
using UnityEngine;

namespace VanillaAddons.InsanityChanges.Hallucinations
{
    internal class BreathingHallucination : Hallucination
    {
        public BreathingHallucination()
        {
            Init("BreathingSound", 5, 35f);
        }

        public override void InvokeHallucination()
        {
            VanillaAddonsBase.Instance.mls.LogInfo("Invoke Breathing Sound Hallucionation");

            AudioClip clip = Insanity.PlayerHallucinationSounds.Where(x => x.name != "JumpScare")
                .ToArray()[Random.Range(0, Insanity.LCGameSFX.Length - 2)];
            VanillaAddonsBase.Instance.mls.LogInfo($"Playing Hallucination {clip.name}");

            SoundManager.Instance.PlaySoundAroundLocalPlayer(clip, 0.8f);

            HallucinationManager.LocalPlayer.JumpToFearLevel(0.2f);
        }

        public override void RemoveHallucination()
        {
            VanillaAddonsBase.Instance.mls.LogInfo("Remove Breathing Sound Hallucionation Post");

        }
    }
}
