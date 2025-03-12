using CandyMatch.Controllers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CandyMatch.Utilities;
using DG.Tweening;
using CandyMatch.LevelEditor;
using CandyMatch.Model;

namespace CandyMatch.Controllers
{
    public class GameplayManager : MonoBehaviour
    {
        [SerializeField] private GameLevelController gameLevelController;
        [SerializeField] private GridManager gridManager;

        public delegate void SelectedCardEvent(CardView cardView);
        public static SelectedCardEvent OnCardSelected;

        private CardView currentSelectedCard;
        private CardView previousSelectedCard;

        private void Start()
        {
            CustomCoroutiner.Start(InitLevel());

            OnCardSelected += OnCardSelectedUpdates;
        }
        
        private IEnumerator InitLevel()
        {
            GameLevelData gameLevelData = gameLevelController.GetSelectedGameLevelData(GameManager.GetSelectedLevelIndex);
            int randomLayoutIndex = Random.Range(0, gameLevelData.gameLayouts.Count);
            GameLayout gameLayout = gameLevelController.GetSelectedGameLayout(gameLevelData, randomLayoutIndex);

            gridManager.CreateGrid(gameLayout.rowCount, gameLayout.columnCount);
            gridManager.GenerateCardDatas(gameLayout.rowCount, gameLayout.columnCount, gameLevelData.cardIcons);
            yield return new WaitForSeconds(1);

            gridManager.GenerateCards();
        }

        private void OnCardSelectedUpdates(CardView cardView)
        {
            currentSelectedCard = cardView;

            if (previousSelectedCard == null)
            {
                previousSelectedCard = currentSelectedCard;
                return;
            }
            
            if(previousSelectedCard.GetCardID != currentSelectedCard.GetCardID) 
            {
                previousSelectedCard.HideCard();
                currentSelectedCard.HideCard();
            }
            else if(previousSelectedCard.GetCardID == currentSelectedCard.GetCardID)
            {
                previousSelectedCard.DeleteCard(() =>
                {
                    gridManager.GetGeneratedCards.Remove(previousSelectedCard);
                    Destroy(previousSelectedCard);
                });

                currentSelectedCard.DeleteCard(() =>
                {
                    gridManager.GetGeneratedCards.Remove(previousSelectedCard);
                    Destroy(currentSelectedCard);
                });
            }
            previousSelectedCard = null;
            currentSelectedCard = null;
        }

        private void OnDestroy()
        {
            OnCardSelected -= OnCardSelectedUpdates;
        }

        private void OnDisable()
        {
            OnCardSelected -= OnCardSelectedUpdates;
        }
    }
}

