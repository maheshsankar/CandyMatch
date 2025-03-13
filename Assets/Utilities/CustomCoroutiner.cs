using UnityEngine;
using System.Collections;

namespace CandyMatch.Utilities 
{
    public class CustomCoroutiner
    {
        private class CoroutineBehaviour : MonoBehaviour
        {

            private void OnDestroy()
            {
                instance = null;
            }
        }

        private static CoroutineBehaviour instance;
        private static CoroutineBehaviour Instance
        {
            get
            {
                if (instance == null)
                {
                    GameObject go = new GameObject("[CustomCoroutiner]");
                    go.hideFlags = HideFlags.HideAndDontSave;
                    Object.DontDestroyOnLoad(go);
                    instance = go.AddComponent<CoroutineBehaviour>();
                }
                return instance;
            }
        }

        public static Coroutine Start(IEnumerator ienumerator)
        {
            return Instance.StartCoroutine(ienumerator);
        }

        public static void Stop(Coroutine routine)
        {
            if (routine == null)
            {
                Debug.LogError("Coroutine is NULL. There is nothing to STOP!!");
            }
            else
            {
                Instance.StopCoroutine(routine);
            }
        }

        public static void Stop(IEnumerator enumerator)
        {
            if (enumerator == null)
            {
                Debug.LogError("IEnumerator is NULL. There is nothing to STOP!!");
            }
            else
            {
                Instance.StopCoroutine(enumerator);
            }
        }
    }
}

