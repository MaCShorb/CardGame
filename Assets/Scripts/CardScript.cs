using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardScript : MonoBehaviour
{
    int cardCost;
    Sprite cardSprite;
    string cardText;
    string cardName;

    public GameObject ownerPlayer;


    List<Action> CardActions;

    public GameObject GameHandler;

    public CardScript(int cost, string name)
    {
        cardCost = cost;
        cardText = "Hello!";
        cardName = name;
    }

    // Start is called before the first frame update
    void Start()
    {
        // Following code only for testing purposes!
        CardActions = new List<Action>(); 
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool evaluateActionCondition((int targetValue, int comparisonValue, int conditionValue) condition)
    {
        // Determines if actionCondition alllows for execution. Not much explanation needed.
        switch (condition.comparisonValue)
        {
            case 0:
                if (condition.targetValue == condition.conditionValue)
                {
                    return true;
                }
                break;
            case 1:
                if (condition.targetValue > condition.conditionValue)
                {
                    return true;
                }
                break;
            case -1:
                if (condition.targetValue < condition.conditionValue)
                {
                    return true;
                }
                break;   
            default:
                Debug.Log("ERROR: Improper condition.comparisonValue handed to Card.evaluateActionCondition!");
                break;
        }
        return false;
    }

    public void executeAction()
    {
            if (CardActions.Count != 0)
            {
                Action actionExecuting = CardActions[0];
                G_InputHandlerScript dummyGHS = GameHandler.GetComponent<G_InputHandlerScript>();
                // Is action player targeted?
                if (actionExecuting.getViableTargets().Count != 0 & actionExecuting.getChosenTarget() == null)
                {
                    dummyGHS.beginSearchForTarget(actionExecuting.getViableTargets().ToArray(), gameObject);
                }
                else
                {
                    CardActions.RemoveAt(0);
                    if (evaluateActionCondition(actionExecuting.getCondition()))
                    {
                        PlayerScript dummyPS = ownerPlayer.GetComponent<PlayerScript>();
                        dummyPS.mana -= cardCost;
                        dummyGHS.sendActionToDelegate(actionExecuting, gameObject);
                    }
                }
            }
    }

    public Action getExecutingAction()
    {
        return CardActions[0];
    }

    public void setTargetExecutingAction(GameObject target)
    {
        CardActions[0].setChosenTarget(target);
    }

    public void attemptPlay()
    {
        PlayerScript dummyPS = ownerPlayer.GetComponent<PlayerScript>();
        if (cardCost <= dummyPS.mana)
        {
            executeAction();
        }
    }
}
