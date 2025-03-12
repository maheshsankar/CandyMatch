using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace CandyMatch.Controllers
{
    public class GameplayCanvasView : MonoBehaviour
    {
        [SerializeField] private GameObject panelObj;

        [SerializeField] private TextMeshProUGUI scoreValueText;
        [SerializeField] private TextMeshProUGUI turnValueText;

        [SerializeField] private Button pauseButton;

        /// <summary>
        /// Default Unity OnEnable Method
        /// </summary>
        private void OnEnable()
        {
            GameManager.OnGameStart += OnGameStart;
            GameManager.OnGameplayStart += OnGameplayStart;
            GameManager.OnGameRestart += OnGameplayStart;

            GameplayManager.OnCardMatch += OnCardMatchUpdates;
            GameplayManager.OnTurn += OnTurnUpdates;
        }

        /// <summary>
        /// Delegate Call back for Game Start
        /// </summary>
        private void OnGameStart()
        {
            panelObj.SetActive(false);
        }

        /// <summary>
        /// Delegate call back for gameplay start
        /// </summary>
        private void OnGameplayStart()
        {
            panelObj.SetActive(true);
            SetButtonListener();
            scoreValueText.text = "0";
            turnValueText.text = "0";
        }

        /// <summary>
        /// Delegate call back for turn updates
        /// </summary>
        private void OnTurnUpdates()
        {
            turnValueText.text = GameManager.Instance.GetTurnCount.ToString();
        }

        /// <summary>
        /// Delegate Call back for card matching 
        /// </summary>
        private void OnCardMatchUpdates()
        {
            scoreValueText.text = GameManager.Instance.GetScore.ToString();
        }

        /// <summary>
        /// Setting all button listeners
        /// </summary>
        private void SetButtonListener()
        {
            pauseButton.onClick.RemoveAllListeners();
            pauseButton.onClick.AddListener(OnPauseButtonClicked);
        }

        /// <summary>
        /// Pause button Clicked
        /// </summary>
        private void OnPauseButtonClicked()
        {
            Time.timeScale = 0;
            GameManager.OnGamePause?.Invoke();
        }

        /// <summary>
        /// Default Unity OnDisable Method
        /// </summary>
        private void OnDisable()
        {
            GameManager.OnGameStart -= OnGameStart;
            GameManager.OnGameplayStart -= OnGameplayStart;
            GameManager.OnGameRestart -= OnGameplayStart;

            GameplayManager.OnCardMatch -= OnCardMatchUpdates;
            GameplayManager.OnTurn -= OnTurnUpdates;
        }
    }
}

