namespace Cards
{
    public class AttackCardAction : CardAction
    {
        public override bool ProcessActionCost()
        {
            throw new System.NotImplementedException();
        }

        public override void ProcessActionOnTargets()
        {
            foreach(ITargetableCard tc in targetCards)
            {
                switch(tc.GetCardType())
                {
                    case CardType.MONSTER:
                        tc.AdjustStatBy(ref (tc as MonsterCardData).currentMonsterHealth, actionValue);
                        break;

                    default:
                        return;
                };

                ProcessActionCost();
            }
        }
    }
}
