using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
namespace CandyMatch.Controllers 
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance = null;

        [SerializeField] private int selectedLevelIndex = 0;
        public static int GetSelectedLevelIndex
        {
            get
            {
                return Instance.selectedLevelIndex;
            }
        }

        public void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else if (Instance != this)
            {
                Destroy(gameObject);
            }
            DontDestroyOnLoad(gameObject);
        }



    }
}