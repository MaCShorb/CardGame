using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public abstract class UIActionExecutor : MonoBehaviour
{
    public List<Action> ActionList;
    List<Action> SET_ACTIONLIST;
    public event EventHandler<GameObject> ActionNeedsTarget;
    public event EventHandler<Action> PassAction;
    public event EventHandler<GameObject> ActionFoundTarget;
    public event EventHandler<GameObject> FinishedExecuting;

    string SET_TAG;


    private void Awake()
    {
        SET_TAG = gameObject.tag;
        SET_ACTIONLIST = new List<Action>(ActionList);
    }

    public void ProcessAction()
    {
        if (ActionList.Count != 0)
        {
            Action ActionExecuting = ActionList[0];
            gameObject.tag = "EXECUTING";
            if (ActionExecuting.NeedsTarget)
            {
                OnActionNeedsTarget();
            }
            else
            {
                OnPassAction(ActionExecuting);
                ActionList.RemoveAt(0);
                ProcessAction();
            }
        }
        else
        {
            gameObject.tag = SET_TAG;
            OnFinishedExecuting();
            ActionList = new List<Action>(SET_ACTIONLIST);
        }
    }

        public void HandleTargetSelection(object sender, GameObject target)
        {
            ActionList[0].SetTarget(target);
            OnActionFoundTarget();
            ProcessAction();
        }

        public Action GetActionFirstInLine()
        {
            return ActionList[0];
        }

        public void OnActionNeedsTarget()
        {
            ActionNeedsTarget.Invoke(this, gameObject);
        }

        public void OnPassAction(Action action)
        {
            PassAction.Invoke(this, action);
        }

        public void OnActionFoundTarget()
        {
            ActionFoundTarget.Invoke(this, gameObject);
        }

        public void OnFinishedExecuting()
        {
            FinishedExecuting.Invoke(this, gameObject);
        }
}
