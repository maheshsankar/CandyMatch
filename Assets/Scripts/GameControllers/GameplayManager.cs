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
        [SerializeField] private GameLevelController gameLevelController; //reference for game data
        [SerializeField] private GridManager gridManager; //reference for grid manager

        public delegate void SelectedCardEvent(CardView cardView);
        public static SelectedCardEvent OnCardSelected; //Delegate Event for Card Selection

        public delegate void TurnEvent();
        public static TurnEvent OnTurn; //Delegate Event for Turns

        public delegate void CardMatchingEvent();
        public static CardMatchingEvent OnCardMatch; //Delegate Event for Card Matching Event

        public delegate void CardRemoveEvent(CardView cardView);
        public static CardRemoveEvent OnCardRemove; //Delegate Event for Card removal

        private CardView currentSelectedCard;
        private CardView previousSelectedCard;

        /// <summary>
        /// Defult Unity OnEnable Method
        /// </summary>
        private void OnEnable()
        {
            GameManager.OnGameStart += OnGameStart;
            GameManager.OnGameplayStart += OnGameplayStart;
            GameManager.OnGameRestart += OnGameRestart;

            OnCardRemove += OnCardRemoveUpdates;
            OnCardSelected += OnCardSelectedUpdates;
        }

        /// <summary>
        /// Delegate Callback for Game Start Event
        /// </summary>
        private void OnGameStart()
        {
            GameManager.Instance.Reset();
            gridManager.ClearGrid();
        }

        /// <summary>
        /// Delegate Call back for Gameplay Start
        /// </summary>
        private void OnGameplayStart()
        {
            InitLevel();
        }

        /// <summary>
        /// Delegate Call back for Game Restart
        /// </summary>
        private void OnGameRestart() 
        {
            GameManager.Instance.Reset();
            gridManager.ClearCards();
            CustomCoroutiner.Start(StartGeneratingCards());
        }

        /// <summary>
        /// Inititialising Level Data and Grid Data
        /// </summary>
        private void InitLevel()
        {
            GameLevelData gameLevelData = gameLevelController.GetSelectedGameLevelData(GameManager.Instance.GetSelectedLevelIndex);
            int randomLayoutIndex = Random.Range(0, gameLevelData.gameLayouts.Count);
            GameLayout gameLayout = gameLevelController.GetSelectedGameLayout(gameLevelData, randomLayoutIndex);

            gridManager.CreateGrid(gameLayout.rowCount, gameLayout.columnCount);
            gridManager.GenerateCardDatas(gameLayout.rowCount, gameLayout.columnCount, gameLevelData.cards);

            CustomCoroutiner.Start(StartGeneratingCards());
        }

        /// <summary>
        /// Generate Cards Based on Grid Positions with delay
        /// </summary>
        /// <returns></returns>
        private IEnumerator StartGeneratingCards()
        {
            currentSelectedCard = null;
            previousSelectedCard = null;
            yield return new WaitForSeconds(0.5f);
            gridManager.GenerateCards();
        }

        /// <summary>
        /// Delegate Call back for Card Selected Event
        /// </summary>
        /// <param name="cardView"></param>
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

        /// <summary>
        /// Delegate Call back for Card Remove Event
        /// </summary>
        /// <param name="cardView"></param>
        private void OnCardRemoveUpdates(CardView cardView) 
        {
            gridManager.DestroyCard(cardView);

            if (gridManager.GetGeneratedCards.Count == 0)
            {
                GameManager.Instance.CheckAndUpdateHigScore();
                CustomCoroutiner.Start(ShowGameOverWithDelay());
            }
        }

        private IEnumerator ShowGameOverWithDelay()
        {
            yield return new WaitForSeconds(0.5f);
            GameManager.OnGameOver?.Invoke();
        }

        /// <summary>
        /// Default Unity OnDisable Method
        /// </summary>
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

