using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using CandyMatch.Utilities;
using CandyMatch.Model;
using Random = UnityEngine.Random;

namespace CandyMatch.Controllers 
{
    public class GridManager : MonoBehaviour
    {
        [SerializeField] private GameObject cardPrefab;
        [SerializeField] private RectTransform containerRectTr;
        [SerializeField] private Image containerBGImage;

        [SerializeField] private Vector2 padding = new Vector2(20, 20);
        [SerializeField] private Vector2 spacing = new Vector2(20, 20);

        private float cardSize;
        public float GetCardSize => cardSize;
        private List<Vector2> gridPositionDatas;

        private List<CardView> generatedCards;
        public List<CardView> GetGeneratedCards => generatedCards;

        private List<Card> cardDatas;

        private void Init() 
        {
            containerBGImage.enabled = false;
            generatedCards = new List<CardView>();
        }

        /// <summary>
        /// Setting Up Grids Positions
        /// </summary>
        /// <param name="rows"></param>
        /// <param name="columns"></param>
        public void CreateGrid(int rows, int columns)
        {
            Init();

            if (containerRectTr == null)
            {
                Debug.LogError("Container not assigned");
            }

            gridPositionDatas = new List<Vector2>();

            float gridWidth = containerRectTr.rect.width - (padding.x * 2);
            float gridHeight = containerRectTr.rect.height - (padding.y * 2);

            float xSpacings = (columns - 1) * spacing.x;
            float ySpacings = (rows - 1) * spacing.y;

            float cardWidth = (gridWidth - xSpacings) / columns;
            float cardHeight = (gridHeight - ySpacings) / rows;
            cardSize = Mathf.Min(cardWidth, cardHeight);

            gridWidth = (columns * cardSize) + xSpacings;
            gridHeight = (rows * cardSize) + ySpacings;

            float startXPos = -gridWidth / 2 + (cardSize / 2);
            float startYPos = gridHeight / 2 - (cardSize / 2);

            for (int row = 0; row < rows; row++)
            {
                for (int column = 0; column < columns; column++)
                {
                    Vector2 gridPosition = new Vector2(startXPos + column * (cardSize + spacing.x), startYPos - row * (cardSize + spacing.y));
                    gridPositionDatas.Add(gridPosition);
                }
            }

            containerRectTr.sizeDelta = new Vector2(gridWidth + padding.x * 2, gridHeight + padding.y * 2);
            containerBGImage.enabled = true;
        }

        public void GenerateCardDatas(int rowCount, int columnCount, List<Sprite> cardIcons) 
        {
            cardDatas = new List<Card>();

            List<Sprite> tempCardIcons = new List<Sprite>();

            foreach (Sprite icon in cardIcons) 
            {
                tempCardIcons.Add(icon);
            }

            int requiredCardCount = (rowCount * columnCount) / 2;

            for(int i = 0; i < requiredCardCount; i++)
            {
                if(tempCardIcons.Count != 0)
                {
                    int randomIndex = Random.Range(0, tempCardIcons.Count);
                    Card card = new Card();
                    card.cardID = i;
                    card.cardIcon = tempCardIcons[randomIndex];

                    cardDatas.Add(card);
                    cardDatas.Add(card);

                    tempCardIcons.RemoveAt(randomIndex);
                }
                else
                {
                    int randomIndex = Random.Range(0, cardIcons.Count);
                    Card card = new Card();
                    card.cardID = i;
                    card.cardIcon = cardIcons[randomIndex];

                    cardDatas.Add(card);
                    cardDatas.Add(card);
                }
            }
            ShuffleGeneratedCards();
        }

        private void ShuffleGeneratedCards()
        {
            var count = cardDatas.Count;
            var last = count - 1;
            for (var i = 0; i < last; ++i)
            {
                var randomIndex = Random.Range(i, count);
                var temp = cardDatas[i];
                cardDatas[i] = cardDatas[randomIndex];
                cardDatas[randomIndex] = temp;
            }
        }

        public void GenerateCards()
        {
            for(var i = 0; i < cardDatas.Count; i++)
            {
                CreateCard(cardDatas[i], i);
            }

            CustomCoroutiner.Start(SetCardsEntryAnimation());
        }

        /// <summary>
        /// Create Individual Cards and Set the details
        /// </summary>
        /// <param name="index"></param>
        /// <param name="cardData"></param>
        private void CreateCard(Card card, int positionIndex)
        {
            CardView cardView = PrefabGenerator.Generate<CardView>(cardPrefab, containerRectTr) as CardView;
            cardView.RenderCard(card);
            cardView.GetCardRectTr.anchoredPosition = gridPositionDatas[positionIndex];
            cardView.GetCardRectTr.sizeDelta = Vector2.zero;
            generatedCards.Add(cardView);
        }

        private IEnumerator SetCardsEntryAnimation()
        {
            for (int i = 0; i < generatedCards.Count; i++)
            {
                generatedCards[i].GetCardRectTr.DOSizeDelta(Vector2.one * cardSize, 0.1f).SetEase(Ease.OutElastic);
                yield return new WaitForSeconds(0.05f);
            }
            Invoke(nameof(HideCards), 2f);
        }

        private void HideCards()
        {
            for (int i = 0; i < generatedCards.Count; i++)
            {
                generatedCards[i].HideCard();
            }
        }
    }
}
