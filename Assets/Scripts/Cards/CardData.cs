using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Cards
{
    [CreateAssetMenu(fileName = "BaseCardData", menuName = "CardData/BaseCard", order = 1)]
    public class CardData : ScriptableObject
    {
        public CardData() {}

        public string          cardName;
        public int             cardCost;
        public CardType        cardType;
        public CardAction[]    cardActions;
    }


    [CreateAssetMenu(fileName = "MonsterCardData", menuName = "CardData/MonsterCard")]
    public class MonsterCardData : CardData
    {
        public MonsterCardData() { cardType = CardType.MONSTER; }

        private int originalMonsterHealth;
        private int originalMonsterAttack;

    }

    [System.Serializable]
    public class CardAction
    {
        public string actionName;
        public string actionDescription;
        public string actionCost;

        [Space]

        public ActionType actionType;
        public int actionValue;

        protected ITargetableCard[] targetCards;
        public virtual void ProcessActionOnTargets() {}
    }



    public enum CardType
    {
        VOID,
        HERO,       // Player
        MONSTER,
        SPELL,
    }

    public enum ActionType
    {
        VOID,
        MOVE,
        ATTACK,
        HEAL,
        BUFF,
    }


    public interface ITargetableCard
    {
        CardType GetCardType();
    }
}
