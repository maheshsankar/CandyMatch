using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

namespace CandyMatch.Controllers 
{
    public class GameOverCanvasView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI scoreValueText;
        [SerializeField] private TextMeshProUGUI highScoreValueText;

        [SerializeField] private Button restartButton;
        [SerializeField] private Button homeButton;

        public void Render() 
        {
            SetButtonListeners();
            RenderData();
        }

        private void RenderData() 
        {
            scoreValueText.text = GameManager.Instance.GetScore.ToString();
            //highScoreValueText = GameManager.Instance
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
        
        }

        private void OnHomeButtonClicked()
        {

        }
    }
}

