using System.Linq;
using UnityEngine;

namespace VanillaAddons.InsanityChanges.Hallucinations
{
    internal class StingerHallucination : Hallucination
    {
        public StingerHallucination()
        {
            Init("StingerSound", 10, 45f);
        }

        public override void InvokeHallucination()
        {
            VanillaAddonsBase.Instance.mls.LogInfo("Invoke Stinger Sound Hallucionation");

            AudioClip clip = Insanity.Stingers[Random.Range(0, Insanity.Stingers.Length - 1)];
            VanillaAddonsBase.Instance.mls.LogInfo($"Playing Hallucination {clip.name}");

            SoundManager.Instance.PlaySoundAroundLocalPlayer(clip, 1.5f);

            HallucinationManager.LocalPlayer.JumpToFearLevel(0.3f);
        }

        public override void RemoveHallucination()
        {
            VanillaAddonsBase.Instance.mls.LogInfo("Remove Stinger Sound Hallucionation Post");

        }
    }
}
