using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

namespace CandyMatch.Controllers 
{
    public class GameOverCanvasView : MonoBehaviour
    {
        [SerializeField] private GameObject panelObj;
        [SerializeField] private TextMeshProUGUI scoreValueText;
        [SerializeField] private TextMeshProUGUI highScoreValueText;

        [SerializeField] private Button restartButton;
        [SerializeField] private Button homeButton;

        /// <summary>
        /// Default Unity OnEnable Method
        /// </summary>
        private void OnEnable()
        {
            GameManager.OnGameOver += OnGameOver;
        }

        /// <summary>
        /// Default Unity OnDisable Method
        /// </summary>
        private void OnDisable()
        {
            GameManager.OnGameOver -= OnGameOver;
        }

        /// <summary>
        /// Delegate call back for Game Over
        /// </summary>
        private void OnGameOver()
        {
            panelObj.SetActive(true);
            SetButtonListeners();
            RenderData();
            SoundManager.PlaySound(SoundManager.SoundTypes.GAMEOVER);
        }

        /// <summary>
        /// Displaying the game over data
        /// </summary>
        private void RenderData() 
        {
            scoreValueText.text = GameManager.Instance.GetScore.ToString();
            highScoreValueText.text = GameManager.Instance.GetHighScore.ToString();
        }

        /// <summary>
        /// Setting all button listeners
        /// </summary>
        private void SetButtonListeners()
        {
            restartButton.onClick.RemoveAllListeners();
            restartButton.onClick.AddListener(OnRestartButtonClicked);

            homeButton.onClick.RemoveAllListeners();
            homeButton.onClick.AddListener(OnHomeButtonClicked);
        }

        /// <summary>
        /// Restart Button Click
        /// </summary>
        private void OnRestartButtonClicked() 
        {
            panelObj.SetActive(false);
            GameManager.OnGameRestart?.Invoke();
        }
        /// <summary>
        /// Home Button Click
        /// </summary>
        private void OnHomeButtonClicked()
        {
            panelObj.SetActive(false);
            GameManager.OnGameStart?.Invoke();
        }
    }
}

