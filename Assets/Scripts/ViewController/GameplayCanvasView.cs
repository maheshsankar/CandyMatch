using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace CandyMatch.Controllers
{
    public class GameplayCanvasView : MonoBehaviour
    {
        [SerializeField] private GameObject panelObj;

        [SerializeField] private TextMeshProUGUI scoreValueText;
        [SerializeField] private TextMeshProUGUI turnValueText;

        private void OnEnable()
        {
            GameManager.OnGameplayStart += OnGameplayStart;

            GameplayManager.OnCardMatch += OnCardMatchUpdates;
            GameplayManager.OnTurn += OnTurnUpdates;
        }

        private void OnGameplayStart()
        {
            panelObj.SetActive(true);
        }

        private void OnTurnUpdates()
        {
            turnValueText.text = GameManager.Instance.GetTurnCount.ToString();
        }

        private void OnCardMatchUpdates()
        {
            scoreValueText.text = GameManager.Instance.GetScore.ToString();
        }

        private void OnDisable()
        {
            GameManager.OnGameplayStart -= OnGameplayStart;

            GameplayManager.OnCardMatch -= OnCardMatchUpdates;
            GameplayManager.OnTurn -= OnTurnUpdates;
        }
    }
}

