using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameSystem.Views
{
    public class DeckView : MonoBehaviour
    {
        private List<CardView> _cards = new List<CardView>();

        private int _maxCardsInScene = 5;
        void Awake()
        {
            _cards.AddRange(FindObjectsOfType<CardView>());

            int counter = 0;
            foreach (CardView card in _cards)
            {
                if (counter < _maxCardsInScene)
                {
                    counter++;
                }
                else
                {
                    card.gameObject.SetActive(false);
                }
            }
        }


        public void DeactivateCard(CardView card)
        {
            if (_cards.Contains(card))
            {
                _cards.Remove(card);
                card.gameObject.SetActive(false);

                if (_cards.Count >= 0)
                {
                    for (int i = 0; i <= _cards.Count - 1; i++)
                    {
                        if (_cards[i].isActiveAndEnabled == false)
                        {
                            _cards[i].gameObject.SetActive(true);
                            break;
                        }
                    }
                }
            }
        }
    }
}
