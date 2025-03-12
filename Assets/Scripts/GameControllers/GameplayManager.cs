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

        public delegate void TurnEvent();
        public static TurnEvent OnTurn;

        public delegate void CardMatchingEvent();
        public static CardMatchingEvent OnCardMatch;

        public delegate void CardRemoveEvent(CardView cardView);
        public static CardRemoveEvent OnCardRemove;

        private CardView currentSelectedCard;
        private CardView previousSelectedCard;

        private void OnEnable()
        {
            GameManager.OnGameStart += OnGameStart;
            GameManager.OnGameplayStart += OnGameplayStart;
            GameManager.OnGameRestart += OnGameRestart;

            OnCardRemove += OnCardRemoveUpdates;
            OnCardSelected += OnCardSelectedUpdates;
        }

        private void OnGameStart()
        {
            GameManager.Instance.Reset();
            gridManager.ClearGrid();
        }

        private void OnGameplayStart()
        {
            InitLevel();
        }

        private void OnGameRestart() 
        {
            GameManager.Instance.Reset();
            gridManager.ClearCards();
            CustomCoroutiner.Start(StartGeneratingCards());
        }

        
        private void InitLevel()
        {
            GameLevelData gameLevelData = gameLevelController.GetSelectedGameLevelData(GameManager.Instance.GetSelectedLevelIndex);
            int randomLayoutIndex = Random.Range(0, gameLevelData.gameLayouts.Count);
            GameLayout gameLayout = gameLevelController.GetSelectedGameLayout(gameLevelData, randomLayoutIndex);

            gridManager.CreateGrid(gameLayout.rowCount, gameLayout.columnCount);
            gridManager.GenerateCardDatas(gameLayout.rowCount, gameLayout.columnCount, gameLevelData.cards);

            CustomCoroutiner.Start(StartGeneratingCards());
        }

        private IEnumerator StartGeneratingCards()
        {
            currentSelectedCard = null;
            previousSelectedCard = null;
            yield return new WaitForSeconds(0.5f);
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
                SoundManager.PlaySound(SoundManager.SoundTypes.CARD_MISMATCH);
                previousSelectedCard.HideCard();
                currentSelectedCard.HideCard();
            }
            else if(previousSelectedCard.GetCardID == currentSelectedCard.GetCardID)
            {
                GameManager.Instance.UpdateScore(10);
                OnCardMatch?.Invoke();
                SoundManager.PlaySound(SoundManager.SoundTypes.CARD_MATCH);
                previousSelectedCard.DeleteCard();
                currentSelectedCard.DeleteCard();
            }

            GameManager.Instance.UpdateTurn(1);
            OnTurn?.Invoke();
            previousSelectedCard = null;
            currentSelectedCard = null;
        }

        private void OnCardRemoveUpdates(CardView cardView) 
        {
            gridManager.DestroyCard(cardView);

            if (gridManager.GetGeneratedCards.Count == 0)
            {
                GameManager.Instance.CheckAndUpdateHigScore();
                GameManager.OnGameOver?.Invoke();
            }
        }

        private void OnDisable()
        {
            GameManager.OnGameStart -= OnGameStart;
            GameManager.OnGameplayStart -= OnGameplayStart;
            GameManager.OnGameRestart += OnGameRestart;
            OnCardRemove -= OnCardRemoveUpdates;
            OnCardSelected -= OnCardSelectedUpdates;
        }
    }
}

