
namespace VanillaAddons.InsanityChanges
{
    public abstract class Hallucination
    {
        public string HallucinationName;

        public int HallucinationWeight;

        public float HallucinationMinInsanity;

        public bool IsHallucinationEnabled;

        public void Init(string hallucinationName, int hallucinationWeight, float hallucinationMinInsanity)
        {
            HallucinationName = hallucinationName;
            HallucinationWeight = hallucinationWeight;
            HallucinationMinInsanity = hallucinationMinInsanity;
        }

        public abstract void InvokeHallucination();

        public abstract void RemoveHallucination();

    }
}
