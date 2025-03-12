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

        private void OnEnable()
        {
            GameManager.OnGameOver += OnGameOver;
        }

        private void OnDisable()
        {
            GameManager.OnGameOver -= OnGameOver;
        }

        private void OnGameOver()
        {
            panelObj.SetActive(true);
            SetButtonListeners();
            RenderData();
            SoundManager.PlaySound(SoundManager.SoundTypes.GAMEOVER);
        }

        private void RenderData() 
        {
            scoreValueText.text = GameManager.Instance.GetScore.ToString();
            highScoreValueText.text = GameManager.Instance.GetHighScore.ToString();
        }

        private void SetButtonListeners()
        {
            restartButton.onClick.RemoveAllListeners();
            restartButton.onClick.AddListener(OnRestartButtonClicked);

            homeButton.onClick.RemoveAllListeners();
            homeButton.onClick.AddListener(OnHomeButtonClicked);
        }

        private void OnRestartButtonClicked() 
        {
            panelObj.SetActive(false);
            GameManager.OnGameRestart?.Invoke();
        }

        private void OnHomeButtonClicked()
        {
            panelObj.SetActive(false);
            GameManager.OnGameStart?.Invoke();
        }
    }
}

