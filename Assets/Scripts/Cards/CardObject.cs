using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cards
{
    public class CardObject : MonoBehaviour
    {
        public readonly CardData cardData;

        private CardZoneType _currentZone;
        public CardZoneType currentZone
        {
            get { return _currentZone; }

            private set
            {
                if(_currentZone == value) return;

                this.onCardMove?.Invoke(value);
                _currentZone = value;
            }
        }

        public System.Action<CardZoneType> onCardMove;
    }
}
