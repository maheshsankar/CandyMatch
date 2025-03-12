using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
namespace CandyMatch.Controllers 
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance = null;

        public delegate void GameStartEvent();
        public static GameStartEvent OnGameStart;

        public delegate void GameplayStartEvent();
        public static GameplayStartEvent OnGameplayStart;

        public delegate void GamePauseEvent();
        public static GamePauseEvent OnGamePause;

        public delegate void GameOverEvent();
        public static GameOverEvent OnGameOver;

        [SerializeField] private int selectedLevelIndex = 0;
        public static int GetSelectedLevelIndex
        {
            get
            {
                return Instance.selectedLevelIndex;
            }
        }

        private int score;
        public int GetScore => score;

        private int turnCount;
        public int GetTurnCount => turnCount;

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
        }

        private void Start()
        {
            OnGameStart?.Invoke();
        }
        public void UpdateScore(int scoreValue)
        {
            score += scoreValue;
        }

        public void UpdateTurn(int turnCount)
        {
            this.turnCount += turnCount;
        }

    }
}