using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Action
{
    public enum ActionType { discard, damage, restore, armor, draw, play, none }
    public enum Targets { self, monsters, allies, cards, board, deck, none }

    ActionType type { get;  }
    public List<Targets> viableTargets { get; }

    GameObject inputChosenTarget { get; set; }
    Targets nonInputChosenTarget;

    public int amount { get; }

    (int targetValue, int comparisonValue, int conditionValue) conditionTuple;
    // Above tuple is used within card. It's functionality is described there.

    public Action(ActionType aType, List<Targets> aTargets, int aAmount, (int tVal, int comVal, int conVal) cTuple)
    {
        viableTargets = aTargets;
        type = aType;
        viableTargets = aTargets;
        amount = aAmount;
        conditionTuple.targetValue = cTuple.tVal;
        conditionTuple.comparisonValue = cTuple.comVal;
        conditionTuple.conditionValue = cTuple.conVal;
    }

    public (int, int, int) getCondition()
    {
        return conditionTuple;
    }

}
