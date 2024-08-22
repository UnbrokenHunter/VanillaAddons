using System.Collections;
using UnityEngine;

namespace VanillaAddons.Utility
{
    public static class CoroutineRunner
    {
        private class CoroutineRunnerBehaviour : MonoBehaviour { }

        private static CoroutineRunnerBehaviour _runner;

        private static CoroutineRunnerBehaviour Runner
        {
            get
            {
                if (_runner == null)
                {
                    GameObject obj = new GameObject("CoroutineRunner");
                    _runner = obj.AddComponent<CoroutineRunnerBehaviour>();
                    GameObject.DontDestroyOnLoad(obj);
                }
                return _runner;
            }
        }

        public static Coroutine StartCoroutine(IEnumerator coroutine)
        {
            return Runner.StartCoroutine(coroutine);
        }

        public static void StopCoroutine(Coroutine coroutine)
        {
            Runner.StopCoroutine(coroutine);
        }

        public static void StopCoroutine(IEnumerator coroutine)
        {
            Runner.StopCoroutine(coroutine);
        }
    }
}
