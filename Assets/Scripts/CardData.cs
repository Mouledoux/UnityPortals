using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Cards
{
    [CreateAssetMenu(fileName = "CardData")]
    public class CardData : ScriptableObject
    {
        public readonly string cardName;
    }
}
