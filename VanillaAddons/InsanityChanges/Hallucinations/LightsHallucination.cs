using UnityEngine;

namespace VanillaAddons.InsanityChanges.Hallucinations
{
    public class LightsHallucination : Hallucination
    {
        public LightsHallucination() 
        {
            Init("Lights", 5, 40f);

            On.EntranceTeleport.TeleportPlayer += EntranceTeleport_TeleportPlayer;
        }

        public override void InvokeHallucination()
        {
            VanillaAddonsBase.Instance.mls.LogInfo("Invoke Lights Hallucionation");

            IsHallucinationEnabled = true;

            foreach (Animator light in RoundManager.Instance.allPoweredLightsAnimators)
            {
                if (light != null)
                    light.SetBool("on", false);

                else
                    VanillaAddonsBase.Instance.mls.LogInfo("Light Animator Was Null");
            }

            HallucinationManager.LocalPlayer.JumpToFearLevel(1f);
        }

        public override void RemoveHallucination()
        {
            VanillaAddonsBase.Instance.mls.LogInfo("Remove Lights Hallucionation Post");

            IsHallucinationEnabled = false;
            foreach (Animator light in RoundManager.Instance.allPoweredLightsAnimators)
            {
                light.SetBool("on", true);
            }
        }

        void EntranceTeleport_TeleportPlayer(On.EntranceTeleport.orig_TeleportPlayer orig,
            EntranceTeleport self)
        {
            orig(self);

            VanillaAddonsBase.Instance.mls.LogInfo("Remove Lights Hallucionation Pre");

            if (!self.isEntranceToBuilding)
            {
                RemoveHallucination();
            }
        }
    }
}
