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

        /// <summary>
        /// Default Unity OnEnable Method
        /// </summary>
        private void OnEnable()
        {
            GameManager.OnGameStart += OnGameStart;
        }

        /// <summary>
        /// Default Unity OnDisable Method
        /// </summary>
        private void OnDisable()
        {
            GameManager.OnGameStart -= OnGameStart;
        }

        /// <summary>
        /// Delegate call back for Game Start
        /// </summary>
        private void OnGameStart()
        {
            panelObj.SetActive(true);
            SetButtonListener();
            SetToggles();
            DsiplayHighScore();
            startButton.transform.DOShakeScale(1f, 0.2f, 5).SetLoops(-1);
        }

        /// <summary>
        /// Setting all button listeners
        /// </summary>
        private void SetButtonListener()
        {
            startButton.onClick.RemoveAllListeners();
            startButton.onClick.AddListener(OnStartButtonClicked);
        }

        /// <summary>
        /// Setting up toggles and togglegroup
        /// </summary>
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

        /// <summary>
        /// Event Listener call back for on toggle value change
        /// </summary>
        /// <param name="value"></param>
        private void OnToggleValueChanged(bool value)
        {
            if(value)
            {
                GameManager.Instance.UpdateLevelIndex(GetSelectedToggleIndex());
            }
        }

        /// <summary>
        /// Getting the selected toggle index
        /// </summary>
        /// <returns></returns>
        public int GetSelectedToggleIndex()
        {
            Toggle selectedToggle = modeSelectionToggleGroup.ActiveToggles().FirstOrDefault();

            if (selectedToggle != null)
            {
                return modeSelectionToggles.IndexOf(selectedToggle);
            }

            return -1;
        }

        /// <summary>
        /// Display high Score
        /// </summary>
        private void DsiplayHighScore()
        {
            if(GameManager.Instance.GetHighScore > 0)
            {
                highScoreValueText.text = GameManager.Instance.GetHighScore.ToString();
                highScoreObj.SetActive(true);
            }
        }

        /// <summary>
        /// Game Start button clicked
        /// </summary>
        private void OnStartButtonClicked() 
        {
            GameManager.OnGameplayStart?.Invoke();
            Disable();
        }

        /// <summary>
        /// Disable panel object
        /// </summary>
        public void Disable()
        {
            DOTween.Kill(startButton.transform);
            panelObj.SetActive(false);
        }
    }
}

