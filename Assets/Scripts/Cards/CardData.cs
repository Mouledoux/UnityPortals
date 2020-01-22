using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Cards
{
    public class CardData : ScriptableObject, ITargetableCard
    {
        public CardData() {}

        public string       cardName;
        public CardType     cardType;
        public CardAction[] cardActions;


        private int originalCardCost;  
        private int originalCardResources;
        public int  currentCardCost;
        public int  currentCardResources;


        public CardType GetCardType()
        {
            return cardType;
        }

        public void AdjustStatBy(ref int stat, int deltaStat)
        {
            stat += deltaStat;
        }
    }



    [CreateAssetMenu(fileName = "MonsterCardData", menuName = "CardData/MonsterCard")]
    public class MonsterCardData : CardData
    {
        public MonsterCardData() { cardType = CardType.MONSTER; }

        private int originalMonsterHealth;
        private int originalMonsterAttack;

        public int currentMonsterHealth;
        public int currentMonsterDamage;

    }



    public abstract class CardAction : ScriptableObject
    {
        public string actionName;
        public string actionDescription;
        public int actionCost;

        [Space]

        public ActionType actionType;
        public int actionValue;

        protected ITargetableCard[] targetCards;


        public virtual bool CanAffordAction(int resources) { return resources >= actionCost; }
        public virtual void ProcessActionCost(ref int resources) { resources -= actionCost; }
        

        public abstract void ProcessActionOnTargets(ref int resources);
    }



    public class AttackCardAction : CardAction
    {
        public override void ProcessActionOnTargets(ref int resources)
        {
            foreach(ITargetableCard tc in targetCards)
            {
                switch(tc.GetCardType())
                {
                    case CardType.MONSTER:
                        tc.AdjustStatBy(ref (tc as MonsterCardData).currentMonsterHealth, -actionValue);
                        break;

                    default:
                        return;
                };
            }
        }
    }





    public interface ITargetableCard
    {
        CardType GetCardType();
        void AdjustStatBy(ref int stat, int deltaStat);
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
        DEFEND,
        HEAL,
        BUFF,
    }
}