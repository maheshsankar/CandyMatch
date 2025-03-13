using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CandyMatch.Utilities 
{
    public class PrefabGenerator
    {
        public static Component Generate<T>(string prefabPath, Transform parent = null) where T : Component
        {
            GameObject obj = MonoBehaviour.Instantiate(Resources.Load(prefabPath)) as GameObject;
            if (parent != null) obj.transform.SetParent(parent, false);
            obj.transform.localScale = Vector3.one;
            obj.transform.rotation = Quaternion.identity;
            return obj.GetComponent<T>();
        }

        public static Component Generate<T>(GameObject prefabObj, Transform parent = null) where T : Component
        {
            GameObject obj = MonoBehaviour.Instantiate(prefabObj) as GameObject;
            if (parent != null) obj.transform.SetParent(parent, false);
            obj.transform.localScale = Vector3.one;
            obj.transform.rotation = Quaternion.identity;
            return obj.GetComponent<T>();
        }
    }
}

