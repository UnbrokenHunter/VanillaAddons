using System.Linq;
using UnityEngine;

namespace VanillaAddons.InsanityChanges.Hallucinations
{
    internal class MonsterHallucination : Hallucination
    {
        public MonsterHallucination()
        {
            Init("MonsterSound", 20, 30f);
        }

        public override void InvokeHallucination()
        {
            VanillaAddonsBase.Instance.mls.LogInfo("Invoke Monster Sound Hallucionation");

            AudioClip[] possible = Insanity.LCGameSFX.Where(x =>
                x.name.Equals("DeathShriek") ||
                x.name.Equals("Found1") ||
                x.name.Equals("LongRoar1") ||
                x.name.Equals("LongRoar2") ||
                x.name.Equals("LongRoar3") ||
                x.name.Equals("MineTrigger") ||
                x.name.Equals("monsterNoise") ||
                x.name.Equals("Spring1")
            ).ToArray();
            AudioClip clip = possible[Random.Range(0, possible.Length - 1)];
            VanillaAddonsBase.Instance.mls.LogInfo($"Playing Hallucination {clip.name}");

            SoundManager.Instance.PlaySoundAroundLocalPlayer(clip, 1.7f);

            HallucinationManager.LocalPlayer.JumpToFearLevel(0.7f);
        }

        public override void RemoveHallucination()
        {
            VanillaAddonsBase.Instance.mls.LogInfo("Remove Monster Sound Hallucionation Post");

        }
    }
}
