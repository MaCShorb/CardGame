using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickComponent: MonoBehaviour
{
    public bool objectIsTargetable;
    G_InputHandlerScript objectInputHandlerScript;

    public ClickComponent()
    {
       
    }
    void Start()
    {
        // Set up objectGameHandlerScript through determining what script to call getGameHandler from.
        switch (gameObject.tag)
        {
            case "Monster":
                MonsterScript dummyMS = gameObject.GetComponent<MonsterScript>();
                objectInputHandlerScript = dummyMS.GameHandler.GetComponent<G_InputHandlerScript>();
                break;
            case "Ally":
            case "Self":
                PlayerScript dummyPS = gameObject.GetComponent<PlayerScript>();
                objectInputHandlerScript = dummyPS.GameHandler.GetComponent<G_InputHandlerScript>();
                break;
            case "Card":
                CardScript dummyCS = gameObject.GetComponent<CardScript>();
                objectInputHandlerScript = dummyCS.GameHandler.GetComponent<G_InputHandlerScript>();
                break;
            default:
                break;
        }
    }
    void OnMouseDown()
    {
        if (objectInputHandlerScript.isSearchingForTarget)
        {
            if(isObjectViableTarget())
            {
                objectInputHandlerScript.setChosenTarget(gameObject);
            }
        }
        else
        {
            switch(gameObject.tag)
            {
                case "Card":
                    CardScript dummyCS = gameObject.GetComponent<CardScript>();
                    dummyCS.attemptPlay();
                    break;
                default:
                    break;
            }
        }
    }

    bool isObjectViableTarget()
    {
        foreach (Action.Targets viableTarget in objectInputHandlerScript.searchViableTargets)
        { // ===================================================================
          // TODO: I really don't like having two switch statements, when 
          // there's a strong possibility I only need one. Check into this when
          // it's optimization time.
          // ===================================================================
            switch (gameObject.tag)
            {
                case "Monster":
                    if (viableTarget == Action.Targets.monsters)
                    {
                        return true;
                    }
                    break;
                case "Ally":
                    if (viableTarget == Action.Targets.allies)
                    {
                        return true;
                    }
                    break;
                case "Card":
                    if (viableTarget == Action.Targets.cards)
                    {
                        return true;
                    }
                    break;
                case "Self":
                    if (viableTarget == Action.Targets.self)
                    {
                        return true;
                    }
                    break;
                default:
                    return false;
            }
        }
        return false;
    }
}
