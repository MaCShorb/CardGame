using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameHandlerScript : MonoBehaviour
{
    public DelegateScript handlerDelegateScript;
    public PlayerScript handlerPlayerScript;

    public GameObject temporaryMonster; //temporary (duh)

    public bool isSearchingForTarget { get; private set; }
    public Action.Targets[] searchViableTargets { get; private set; }
    GameObject chosenTarget = null;
    GameObject cardOfActionExecuting;

    // Temporary Board for Milestone 1. Fill with monster.
    public GameObject[] Board = new GameObject[1];

    public GameHandlerScript()
    {
        isSearchingForTarget = false;
    }

    // Start is called before the first frame update
    void Start()
    {
        Board[0] = temporaryMonster;
    }

    public void beginSearchForTarget(Action.Targets[] targets, GameObject cardSender)
    {
        searchViableTargets = targets;
        isSearchingForTarget = true;
        cardOfActionExecuting = cardSender;
        Debug.Log("Beginning search for target!");
    }

    public void endSearchForTarget(bool continueExecutionCardActions)
    {
        searchViableTargets = new Action.Targets[0];
        isSearchingForTarget = false;
        // =====================================================================
        // GameHandler only continues card action execution if specifically told 
        // that it should. This allows for cancelling of actions. Also reason
        // why setChosenTarget and endSearchForTarget are seperate functions.
        // =====================================================================
        if (continueExecutionCardActions)
        {
            CardScript dummyCS = cardOfActionExecuting.GetComponent<CardScript>();
            dummyCS.setTargetExecutingAction(chosenTarget);
            chosenTarget = null;
            
            // Not sent straight to delegate because card condition still has to be
            // evaluated.

            dummyCS.executeAction();
            
        }
    }

    public void sendActionToDelegate(Action action, GameObject cardSender)
    {
        handlerDelegateScript.delegateAction(action);
        cardOfActionExecuting = cardSender;
    }

    public void processActionFromDelegate(Action action)
    {
        Action actionToExecute = action;
        // =====================================================================
        // TODO: the double switch statement is bothersome. When it comes time
        // for code optimization, see if there is another way to go about this.
        // =====================================================================
        switch(actionToExecute.getChosenTarget().tag)
        {
            case "Monster":
                MonsterScript dummyMS = actionToExecute.getChosenTarget().GetComponent<MonsterScript>();
                switch(actionToExecute.getActionType())
                {
                    case Action.ActionType.damage:
                        dummyMS.takeDamage(actionToExecute.getAmount());
                        break;
                    case Action.ActionType.restore:
                        dummyMS.takeDamage(actionToExecute.getAmount());
                        break;
                    default:
                        break;
                }
                break;
            case "Deck":
                // TODO: Add in code for DeckScript
                break;
            case "Board":
                // TODO: Add in code for BoardScript
                break;
            case "Ally":
            case "Self":
                PlayerScript dummyPS = actionToExecute.getChosenTarget().GetComponent<PlayerScript>();
                switch (actionToExecute.getActionType())
                {
                    case Action.ActionType.damage:
                        dummyPS.takeDamage(actionToExecute.getAmount());
                        break;
                    case Action.ActionType.armor:
                        dummyPS.gainArmor(actionToExecute.getAmount());
                        break;
                    case Action.ActionType.restore:
                        dummyPS.restoreHealth(actionToExecute.getAmount());
                        break;
                }
                break;
            case "Card":
                //TODO: If discard, trigger discard function in deck.hand using target (the card)
                //TODO: If price_reduction, just call price reduction.
                // Multiple commands, multiple targets when cards are involved.
                break;
            default:
                Debug.Log("Default, first layer switch processActionFromDelegate");
                break;
        }
        CardScript dummyCS = cardOfActionExecuting.GetComponent<CardScript>();
        dummyCS.executeAction();
    }

    public void setChosenTarget(GameObject target)
    {
        chosenTarget = target;
        Debug.Log("Target Chosen!");
        endSearchForTarget(continueExecutionCardActions: true);
    }
    
}
