using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
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

        public delegate void GameRestartEvent();
        public static GameRestartEvent OnGameRestart;

        public delegate void GamePauseEvent();
        public static GamePauseEvent OnGamePause;

        public delegate void GameOverEvent();
        public static GameOverEvent OnGameOver;

        public int selectedLevelIndex = 0;
        public int GetSelectedLevelIndex => selectedLevelIndex;

        private int score;
        public int GetScore => score;

        private int highScore;
        public int GetHighScore => highScore;

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

            Application.targetFrameRate = 60;
        }

        private void Start()
        {
            if(PlayerPrefs.HasKey("HighScore")) 
            {
                highScore = PlayerPrefs.GetInt("HighScore");
            }

            OnGameStart?.Invoke();
        }

        public void UpdateLevelIndex(int value)
        {
            selectedLevelIndex = value;
        }

        public void UpdateScore(int scoreValue)
        {
            score += scoreValue;
        }

        public void CheckAndUpdateHigScore()
        {
            if(score > highScore)
            {
                highScore = score;

                PlayerPrefs.SetInt("HighScore", highScore);
            }
        }

        public void UpdateTurn(int turnCount)
        {
            this.turnCount += turnCount;
        }

        public void Reset()
        {
            score = 0;
            turnCount = 0;
        }
    }
}