using System.Collections;
using System.Linq;
using UnityEngine;
using VanillaAddons.Utility;

namespace VanillaAddons.InsanityChanges.Hallucinations
{
    public class BlindHallucination : Hallucination
    {

        private float _blindTime = 5f;
        private bool _isBlind;

        public BlindHallucination()
        {
            Init("Blind", 1, 49f);

            _isBlind = false;

            On.HUDManager.SetScreenFilters += HUDManager_SetScreenFilters;
        }

        public override void InvokeHallucination()
        {
            VanillaAddonsBase.Instance.mls.LogInfo("Invoke Blind Hallucionation");

            CoroutineRunner.StartCoroutine(BlindTimer());

            IsHallucinationEnabled = true;
            _isBlind = true; // Will allow effect to be triggered every frame

            AudioClip clip = Insanity.PlayerHallucinationSounds.FirstOrDefault(x => x.name == "JumpScare");
            SoundManager.Instance.PlaySoundAroundLocalPlayer(clip, 1.7f);

            SoundManager.Instance.SetEchoFilter(true); 
            SoundManager.Instance.earsRingingTimer = 1;

            HallucinationManager.LocalPlayer.JumpToFearLevel(1f);
        }

        private IEnumerator BlindTimer()
        {
            yield return new WaitForSeconds(_blindTime);
            RemoveHallucination();
            HUDManager.Instance.flashFilter = 1f;
        }

        public override void RemoveHallucination()
        {
            VanillaAddonsBase.Instance.mls.LogInfo("Remove Blind Hallucionation Post");

            IsHallucinationEnabled = false;
            _isBlind = false;

            Object.FindObjectsOfType<HUDManager>()[0].sinkingUnderAnimator.SetBool("cover", _isBlind);

            SoundManager.Instance.SetEchoFilter(false);
        }

        void HUDManager_SetScreenFilters(On.HUDManager.orig_SetScreenFilters orig,
            HUDManager self)
        {
            orig(self);

            if (IsHallucinationEnabled)
            {
                VanillaAddonsBase.Instance.mls.LogInfo("Set Screen Filter Patch Blind Hallucination");
                Object.FindObjectsOfType<HUDManager>()[0].sinkingUnderAnimator.SetBool("cover", _isBlind);
            }
        }
    }
}
