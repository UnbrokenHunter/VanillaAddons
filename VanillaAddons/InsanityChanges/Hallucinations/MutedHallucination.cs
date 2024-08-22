using UnityEngine;

namespace VanillaAddons.InsanityChanges.Hallucinations
{
    public class MutedHallucination : Hallucination
    {
        public MutedHallucination()
        {
            Init("Muted", 5, 40f);

            On.EntranceTeleport.TeleportPlayer += EntranceTeleport_TeleportPlayer;
        }

        public override void InvokeHallucination()
        {
            VanillaAddonsBase.Instance.mls.LogInfo("Invoke Muted Hallucionation");

            IsHallucinationEnabled = true;

            // Assuming there's a method or property in the voice chat module to mute the mic
            StartOfRound.Instance.voiceChatModule.IsMuted = true;

            // Log the current state for debugging purposes
            Debug.Log("Microphone is now " + (true ? "muted" : "unmuted"));

            SoundManager.Instance.SetEchoFilter(true);
        }

        public override void RemoveHallucination()
        {
            VanillaAddonsBase.Instance.mls.LogInfo("Remove Muted Hallucionation Post");

            IsHallucinationEnabled = false;

            // Assuming there's a method or property in the voice chat module to mute the mic
            StartOfRound.Instance.voiceChatModule.IsMuted = false;

            SoundManager.Instance.SetEchoFilter(false);

            // Log the current state for debugging purposes
            Debug.Log("Microphone is now " + (false ? "muted" : "unmuted"));
        }

        void EntranceTeleport_TeleportPlayer(On.EntranceTeleport.orig_TeleportPlayer orig,
            EntranceTeleport self)
        {
            orig(self);

            VanillaAddonsBase.Instance.mls.LogInfo("Remove Muted Hallucionation Pre");

            if (!self.isEntranceToBuilding)
            {
                RemoveHallucination();
            }
        }
    }
}
