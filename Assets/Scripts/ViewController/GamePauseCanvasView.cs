using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace CandyMatch.Controllers
{
    public class GamePauseCanvasView : MonoBehaviour
    {
        [SerializeField] private GameObject panelObj;
        [SerializeField] private Button restartButton;
        [SerializeField] private Button homeButton;
        [SerializeField] private Button closeButton;

        /// <summary>
        /// Default Unity OnEnable Method
        /// </summary>
        private void OnEnable()
        {
            GameManager.OnGamePause += OnGamePause;
        }

        /// <summary>
        /// Default Unity OnDisable Method
        /// </summary>
        private void OnDisable()
        {
            GameManager.OnGamePause -= OnGamePause;
        }

        /// <summary>
        /// Delegate Call back for Game Pause
        /// </summary>
        private void OnGamePause()
        {
            panelObj.SetActive(true);
            SetButtonListeners();
        }

        /// <summary>
        /// Setting up all button listeners
        /// </summary>
        private void SetButtonListeners()
        {
            restartButton.onClick.RemoveAllListeners();
            restartButton.onClick.AddListener(OnRestartClicked);

            homeButton.onClick.RemoveAllListeners();
            homeButton.onClick.AddListener(OnHomeButtonClicked);

            closeButton.onClick.RemoveAllListeners();
            closeButton.onClick.AddListener(OnCloseButtonClicked);
        }

        /// <summary>
        /// Restart Button Clicked
        /// </summary>
        private void OnRestartClicked()
        {
            Time.timeScale = 1;
            panelObj.SetActive(false);
            GameManager.OnGameRestart?.Invoke();
        }

        /// <summary>
        /// Home Button Clicked
        /// </summary>
        private void OnHomeButtonClicked()
        {
            Time.timeScale = 1;
            panelObj.SetActive(false);
            GameManager.OnGameStart?.Invoke();
        }

        /// <summary>
        /// Home Button Clicked
        /// </summary>
        private void OnCloseButtonClicked()
        {
            Time.timeScale = 1;
            panelObj.SetActive(false);
        }
    }
}

