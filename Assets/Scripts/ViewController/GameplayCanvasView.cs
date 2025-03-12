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

        private void OnEnable()
        {
            GameManager.OnGameStart += OnGameStart;
            GameManager.OnGameplayStart += OnGameplayStart;
            GameManager.OnGameRestart += OnGameplayStart;

            GameplayManager.OnCardMatch += OnCardMatchUpdates;
            GameplayManager.OnTurn += OnTurnUpdates;
        }

        private void OnGameStart()
        {
            panelObj.SetActive(false);
        }

        private void OnGameplayStart()
        {
            panelObj.SetActive(true);
            SetButtonListener();
            scoreValueText.text = "0";
            turnValueText.text = "0";
        }

        private void OnTurnUpdates()
        {
            turnValueText.text = GameManager.Instance.GetTurnCount.ToString();
        }

        private void OnCardMatchUpdates()
        {
            scoreValueText.text = GameManager.Instance.GetScore.ToString();
        }

        private void SetButtonListener()
        {
            pauseButton.onClick.RemoveAllListeners();
            pauseButton.onClick.AddListener(OnPauseButtonClicked);
        }

        private void OnPauseButtonClicked()
        {
            Time.timeScale = 0;
            GameManager.OnGamePause?.Invoke();
        }

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

