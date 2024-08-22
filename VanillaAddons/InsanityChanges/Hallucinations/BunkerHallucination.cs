using System.Linq;
using UnityEngine;

namespace VanillaAddons.InsanityChanges.Hallucinations
{
    internal class BunkerHallucination : Hallucination
    {
        public BunkerHallucination()
        {
            Init("BunkerSound", 30, 25f);
        }

        public override void InvokeHallucination()
        {
            VanillaAddonsBase.Instance.mls.LogInfo("Invoke Bunker Sound Hallucionation");

            AudioClip clip = Insanity.AuditoryHallucinations[Random.Range(0, Insanity.AuditoryHallucinations.Length - 1)];
            VanillaAddonsBase.Instance.mls.LogInfo($"Playing Hallucination {clip.name}");

            SoundManager.Instance.PlaySoundAroundLocalPlayer(clip, 1.7f);
        }

        public override void RemoveHallucination()
        {
            VanillaAddonsBase.Instance.mls.LogInfo("Remove Bunker Sound Hallucionation Post");

        }
    }
}
