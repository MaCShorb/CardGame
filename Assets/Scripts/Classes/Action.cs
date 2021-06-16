using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Action
{
    public bool NeedsTarget { get; private set; }
    public GameObject Target { get; set; }
    public List<string> ViableTargetTags { get; }
    public delegate void ActionCodeShell(GameObject Target);
    ActionCodeShell ActionDelegate;

    // Do more research into how Delegates work. A bit confused, but I think I
    // have it down enough to make it work at least for now.

    public Action(ActionCodeShell ActionDelegate, bool IsUITargeted, List<string> ViableTargetTags)
    {
        NeedsTarget = true;
        this.ActionDelegate = ActionDelegate;
        this.ViableTargetTags = ViableTargetTags;
    }

    public Action(ActionCodeShell ActionDelegate)
    {
        NeedsTarget = false;
        this.ActionDelegate = ActionDelegate;
        ViableTargetTags = new List<string>();
    }

    public Action(ActionCodeShell ActionDelegate, List<string> ViableTargetTags)
    {
        NeedsTarget = true;
        this.ActionDelegate = ActionDelegate;
        this.ViableTargetTags = ViableTargetTags;
    }

    public Action(Action action)
    {
        NeedsTarget = action.NeedsTarget;
        ActionDelegate = action.ActionDelegate;
        ViableTargetTags = action.ViableTargetTags;
    }

    public void Execute()
    {
        ActionDelegate.Invoke(Target);
    }

    public void SetTarget(GameObject target)
    {
        Target = target;
        NeedsTarget = false;
    }

}
