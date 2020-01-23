using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cards
{
    public class CardActionHandler : MonoBehaviour
    {
        public CardObject activeCard;

        private void Update()
        {
            RaycastHit rayHit;
            bool rayDoesHit = Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out rayHit);

            if(Input.GetMouseButtonDown(0) && rayDoesHit)
            {
                CardObject targetCard = rayHit.transform.GetComponent<CardObject>();

                if(activeCard == null)
                {
                    activeCard = targetCard;
                }
                else
                {
                    ProcessCardAction(activeCard.cardActions[ActionPriority.PRIMARY], targetCard);
                    activeCard = null;
                }
            }

            if(Input.GetMouseButtonDown(1) && rayDoesHit)
            {
                CardObject targetCard = rayHit.transform.GetComponent<CardObject>();

                if(activeCard == null)
                {
                    activeCard = targetCard;
                }
                else
                {
                    ProcessCardAction(activeCard.cardActions[ActionPriority.SECONDARY], targetCard);
                    activeCard = null;
                }
            }
        }

        public void ProcessCardAction(CardAction cardAction, CardObject targetCard)
        {
            cardAction.targetCard = targetCard;
            cardAction.ProcessActionOnTargets();
        }
    }
}
