using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
namespace CandyMatch.Controllers 
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance = null; //Creating Global Instance

        public delegate void GameStartEvent();
        public static GameStartEvent OnGameStart; //Delegate Event for Game Start

        public delegate void GameplayStartEvent();
        public static GameplayStartEvent OnGameplayStart; //Delegate Event for Gameplay Start

        public delegate void GameRestartEvent();
        public static GameRestartEvent OnGameRestart; //Delegate Event for Game Restart

        public delegate void GamePauseEvent();
        public static GamePauseEvent OnGamePause; //Delegate Event for Game Pause

        public delegate void GameOverEvent();
        public static GameOverEvent OnGameOver; //Delegate Event for Game Over

        public int selectedLevelIndex = 0;
        public int GetSelectedLevelIndex => selectedLevelIndex;

        private int score;
        public int GetScore => score;

        private int highScore;
        public int GetHighScore => highScore;

        private int turnCount;
        public int GetTurnCount => turnCount;

        /// <summary>
        /// Default Unity Awake Method
        /// </summary>
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

        /// <summary>
        /// Default Unity Start Method
        /// </summary>
        private void Start()
        {
            if(PlayerPrefs.HasKey("HighScore")) 
            {
                highScore = PlayerPrefs.GetInt("HighScore");
            }

            OnGameStart?.Invoke();
        }

        /// <summary>
        /// Update Game Modes from Toggle Selection
        /// </summary>
        /// <param name="value"></param>
        public void UpdateLevelIndex(int value)
        {
            selectedLevelIndex = value;
        }

        /// <summary>
        /// Updates Score on Every matching cards
        /// </summary>
        /// <param name="scoreValue"></param>
        public void UpdateScore(int scoreValue)
        {
            score += scoreValue;
        }

        /// <summary>
        /// Update high score
        /// </summary>
        public void CheckAndUpdateHigScore()
        {
            if(score > highScore)
            {
                highScore = score;

                PlayerPrefs.SetInt("HighScore", highScore);
            }
        }

        /// <summary>
        /// Update turn value
        /// </summary>
        /// <param name="turnCount"></param>
        public void UpdateTurn(int turnCount)
        {
            this.turnCount += turnCount;
        }

        /// <summary>
        /// Reset values
        /// </summary>
        public void Reset()
        {
            score = 0;
            turnCount = 0;
        }
    }
}