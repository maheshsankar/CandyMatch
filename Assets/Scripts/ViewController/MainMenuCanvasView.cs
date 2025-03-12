using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System;

namespace CandyMatch.Controllers
{
    public class MainMenuCanvasView : MonoBehaviour
    {
        [SerializeField] private GameObject panelObj;
        [SerializeField] private Button startButton;

        private void OnEnable()
        {
            GameManager.OnGameStart += OnGameStart;
        }

        private void OnDisable()
        {
            GameManager.OnGameStart -= OnGameStart;
        }

        private void OnGameStart()
        {
            panelObj.SetActive(true);
            SetButtonListener();

            startButton.transform.DOShakeScale(1f, 0.2f, 5).SetLoops(-1);
        }

        private void SetButtonListener()
        {
            startButton.onClick.RemoveAllListeners();
            startButton.onClick.AddListener(OnStartButtonClicked);
        }

        private void OnStartButtonClicked() 
        {
            GameManager.OnGameplayStart?.Invoke();
            Disable();
        }

        public void Disable()
        {
            DOTween.Kill(startButton.transform);
            panelObj.SetActive(false);
        }
    }
}

