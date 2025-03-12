using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System;
using TMPro;
using System.Linq;

namespace CandyMatch.Controllers
{
    public class MainMenuCanvasView : MonoBehaviour
    {
        [SerializeField] private GameObject panelObj;
        [SerializeField] private GameObject highScoreObj;
        [SerializeField] private TextMeshProUGUI highScoreValueText;
        [SerializeField] private Button startButton;

        [SerializeField] private ToggleGroup modeSelectionToggleGroup;
        private List<Toggle> modeSelectionToggles;

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
            SetToggles();
            DsiplayHighScore();
            startButton.transform.DOShakeScale(1f, 0.2f, 5).SetLoops(-1);
        }

        private void SetButtonListener()
        {
            startButton.onClick.RemoveAllListeners();
            startButton.onClick.AddListener(OnStartButtonClicked);
        }

        private void SetToggles()
        {
            modeSelectionToggles = modeSelectionToggleGroup.GetComponentsInChildren<Toggle>().ToList();
            GameManager.Instance.UpdateLevelIndex(GetSelectedToggleIndex());
            foreach (Toggle toggle in modeSelectionToggles) 
            {
                toggle.onValueChanged.RemoveAllListeners();
                toggle.onValueChanged.AddListener(OnToggleValueChanged);
            }
        }

        private void OnToggleValueChanged(bool value)
        {
            if(value)
            {
                GameManager.Instance.UpdateLevelIndex(GetSelectedToggleIndex());
            }
        }

        public int GetSelectedToggleIndex()
        {
            Toggle selectedToggle = modeSelectionToggleGroup.ActiveToggles().FirstOrDefault();

            if (selectedToggle != null)
            {
                return modeSelectionToggles.IndexOf(selectedToggle);
            }

            return -1;
        }

        private void DsiplayHighScore()
        {
            if(GameManager.Instance.GetHighScore > 0)
            {
                highScoreValueText.text = GameManager.Instance.GetHighScore.ToString();
                highScoreObj.SetActive(true);
            }
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

