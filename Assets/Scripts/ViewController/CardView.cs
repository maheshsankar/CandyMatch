using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using DG.Tweening;
using System.Collections;
using CandyMatch.Model;
using System;

namespace CandyMatch.Controllers 
{
    public class CardView : MonoBehaviour, IPointerClickHandler
    {
        [SerializeField] private Image cardBGImage;
        [SerializeField] private Image cardIcon;
        [SerializeField] private Sprite cardBackSprite;
        [SerializeField] private Sprite cardFrontSprite;
        [SerializeField] private RectTransform cardRectTr;
        public RectTransform GetCardRectTr => cardRectTr;

        private bool isCardSelected = false;
        private int cardID;
        public int GetCardID => cardID;

        public void RenderCard(Card card)
        {
            isCardSelected = true;
            cardID = card.cardID;

            cardBGImage.sprite = cardFrontSprite;
            cardIcon.sprite = card.cardIcon;
            cardIcon.enabled = true;
        }

        public void ShowCard()
        {
            isCardSelected = true;
            SoundManager.PlaySound(SoundManager.SoundTypes.CARD_FLIP);
            cardRectTr.DORotate(new Vector3(0, 90, 0), 0.2f).
                 OnComplete(() =>
                 {
                     cardBGImage.sprite = cardFrontSprite;
                     cardIcon.enabled = true;
                     cardRectTr.DORotate(new Vector3(0, 0, 0), 0.2f).
                     OnComplete(() => GameplayManager.OnCardSelected?.Invoke(this));
                 });
        }

        public void HideCard()
        {
            isCardSelected = false;
            cardRectTr.DORotate(new Vector3(0, 90, 0), 0.2f).
                 OnComplete(() =>
                 {
                     cardBGImage.sprite = cardBackSprite;
                     cardIcon.enabled = false;
                     cardRectTr.DORotate(new Vector3(0, 0, 0), 0.2f);
                 });

        }

        public void DeleteCard()
        {
            cardRectTr.DOScale(0f, 0.1f).SetEase(Ease.OutFlash).OnComplete(() => 
            {
                GameplayManager.OnCardRemove?.Invoke(this);
            });
        }
        
        public void OnPointerClick(PointerEventData eventData)
        {
            if (eventData.pointerClick.GetComponent<CardView>() == null) return;

            if(!isCardSelected)
            {
                ShowCard();
            }
                 
        }
    }
}
