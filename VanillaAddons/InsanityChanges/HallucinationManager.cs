using GameNetcodeStuff;
using System.Collections.Generic;
using UnityEngine;
using VanillaAddons.InsanityChanges.Hallucinations;

namespace VanillaAddons.InsanityChanges
{
    internal class HallucinationManager : MonoBehaviour
    {
        public static HallucinationManager Instance;
        public static PlayerControllerB LocalPlayer => GameNetworkManager.Instance.localPlayerController;


        // TODO Make into a config
        private float sanityRNGTimer;
        private float sanityRNGFrequency = 40f; // Seconds between each roll for whether to have a hallucination
        private float sanityRNG = 0.45f; // Chance of hallucination per roll

        private readonly Hallucination[] hallucinations = new Hallucination[] {
            new LightsHallucination(),
            new MutedHallucination(),
            new BlindHallucination(),
            new MonsterHallucination(),
            new SFXHallucination(),
            new BreathingHallucination(),
            new DroneHallucination(),
            new StingerHallucination(),
        };

        public void Awake() 
        {
            if (Instance == null)
                Instance = this;
            else
            {
                Destroy(Instance);
                VanillaAddonsBase.Instance.mls.LogInfo("Error: Two Hallucination Managers were made");
            }
        }

        public void OnEnable()
        {
            VanillaAddonsBase.Instance.mls.LogInfo("Hallucination Manager Enabled: " + Instance.gameObject.name);
        }

        public void Update()
        {
            float insanity = LocalPlayer.insanityLevel + 0.000001f; // To prevent divide by 0?

            if (LocalPlayer.isPlayerControlled &&
                !LocalPlayer.isPlayerDead)
            {
                if (LocalPlayer.isInsideFactory)
                {
                    sanityRNGTimer += Time.deltaTime * (insanity / 10);
                    if (sanityRNGTimer > sanityRNGFrequency)
                    {
                        VanillaAddonsBase.Instance.mls.LogInfo("Insanity Level: " + insanity);
                        sanityRNGTimer = 0;
                        float rng = UnityEngine.Random.Range(0f, 1f);
                        if (rng <= sanityRNG)
                        {
                            Hallucinate(GetRandomHallucination(insanity));
                        }
                    }
                }
            }
            else
            {
                RemoveAllHallucionations();
            }
        }

        private void Hallucinate(Hallucination hallucination)
        {
            if (hallucination != null)
                hallucination.InvokeHallucination();
            else
                Debug.Log("Hallucination is Null");
        }

        private Hallucination GetRandomHallucination(float insanity)
        {
            // Create a list to hold eligible hallucinations based on the current insanity level and whether they are enabled
            var eligibleHallucinations = new List<Hallucination>();

            // Calculate the total weight of eligible hallucinations
            int totalWeight = 0;
            foreach (var hallucination in hallucinations)
            {
                if (!hallucination.IsHallucinationEnabled && insanity >= hallucination.HallucinationMinInsanity)
                {
                    eligibleHallucinations.Add(hallucination);
                    totalWeight += hallucination.HallucinationWeight;
                }
            }

            // If no hallucinations are eligible, return null or handle appropriately
            if (eligibleHallucinations.Count == 0)
            {
                Debug.Log("No Valid Hallucinations Found. Returning Null.");
                return null; // or throw an exception or return a default hallucination
            }

            // Choose a random hallucination based on the weight
            int randomValue = new System.Random().Next(0, totalWeight);
            int cumulativeWeight = 0;

            foreach (var hallucination in eligibleHallucinations)
            {
                cumulativeWeight += hallucination.HallucinationWeight;
                if (randomValue < cumulativeWeight)
                {
                    return hallucination;
                }
            }

            // Fallback in case of an unexpected error (this shouldn't happen)
            Debug.Log("No Valid Hallucinations Found. Returning Fallback.");
            return eligibleHallucinations[0];
        }
  
        public void RemoveAllHallucionations()
        {
            foreach(Hallucination hallucination in hallucinations)
            {
                hallucination.RemoveHallucination();
            }
        }
    }
}
