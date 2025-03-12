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

        private void OnEnable()
        {
            GameManager.OnGamePause += OnGamePause;
        }

        private void OnDisable()
        {
            GameManager.OnGamePause -= OnGamePause;
        }

        private void OnGamePause()
        {
            panelObj.SetActive(true);
            SetButtonListeners();
        }

        private void SetButtonListeners()
        {
            restartButton.onClick.RemoveAllListeners();
            restartButton.onClick.AddListener(OnRestartClicked);

            homeButton.onClick.RemoveAllListeners();
            homeButton.onClick.AddListener(OnHomeButtonClicked);

            closeButton.onClick.RemoveAllListeners();
            closeButton.onClick.AddListener(OnCloseButtonClicked);
        }

        private void OnRestartClicked()
        {
            Time.timeScale = 1;
            panelObj.SetActive(false);
            GameManager.OnGameRestart?.Invoke();
        }

        private void OnHomeButtonClicked()
        {
            Time.timeScale = 1;
            panelObj.SetActive(false);
            GameManager.OnGameStart?.Invoke();
        }

        private void OnCloseButtonClicked()
        {
            Time.timeScale = 1;
            panelObj.SetActive(false);
        }
    }
}

