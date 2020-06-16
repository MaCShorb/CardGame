using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CardDatabase
{
    class CardShell
    {
        public string name { get; set; }
        public string text { get; set; }
        public int cost { get; set; }
        public int tier { get; set; }
        List<Action> actions;

        public CardShell()
        {
            name = "Null";
            text = "";
            cost = 0;
            tier = 0;
            actions = new List<Action>();
        }

        public CardShell(string name, string text, int cost, int tier)
        {
            this.name = name;
            this.text = text;
            this.cost = cost;
            this.tier = tier;
            actions = new List<Action>();
        }

        public CardShell(string name, string text, int cost, int tier, List<Action> actions)
        {
            this.name = name;
            this.text = text;
            this.cost = cost;
            this.tier = tier;
            this.actions = actions;
            
        }

        public void AddAction(Action action)
        {
            actions.Add(action);
        }
    }

    Dictionary<string, CardShell> CardList;

    Dictionary<string, Action.ActionType> dictionaryStringToActionType;
    Dictionary<string, Action.Targets> dictionaryStringToTargets;

    enum readableValues
    {
        CardName, CardText, CardCost,
        CardTier, ActionType, ViableTargets,
        Amount, ConditionTuple, ChosenTarget,
        none
    }
    public CardDatabase() // I can live with this. Don't hate it :)
    {
        // ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
        // TODO: Find way to iterate through Action enums so that I don't have
        // to add to the dictionary line by line.
        // ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
        dictionaryStringToActionType.Add("DISCARD", Action.ActionType.discard);
        dictionaryStringToActionType.Add("DAMAGE", Action.ActionType.damage);
        dictionaryStringToActionType.Add("RESTORE", Action.ActionType.restore);
        dictionaryStringToActionType.Add("ARMOR", Action.ActionType.armor);
        dictionaryStringToActionType.Add("DRAW", Action.ActionType.draw);
        dictionaryStringToActionType.Add("PLAY", Action.ActionType.play);
        dictionaryStringToActionType.Add("NONE", Action.ActionType.none);

        dictionaryStringToTargets.Add("SELF", Action.Targets.self);
        dictionaryStringToTargets.Add("MONSTERS", Action.Targets.monsters);
        dictionaryStringToTargets.Add("ALLIES", Action.Targets.allies);
        dictionaryStringToTargets.Add("CARDS", Action.Targets.cards);
        dictionaryStringToTargets.Add("BOARD", Action.Targets.board);
        dictionaryStringToTargets.Add("DECK", Action.Targets.deck);
        dictionaryStringToTargets.Add("NONE", Action.Targets.none);

        // Decode lines from a file into dictionary that stores all cards.

        string[] linesToDecode = System.IO.File.ReadAllLines("PLACEHOLDER");

        readableValues valueReadingIn = readableValues.none;




        CardShell databaseCardShell = new CardShell();

        foreach (string line in linesToDecode)
        {
            switch(valueReadingIn)
            {
                case readableValues.none:
                    break;
                case readableValues.CardName:
                    databaseCardShell.name = line;
                    break;
                case readableValues.CardText:
                    databaseCardShell.text = line;
                    break;
                case readableValues.CardCost:
                    databaseCardShell.cost = int.Parse(line);
                    break;
                case readableValues.CardTier:
                    databaseCardShell.tier = int.Parse(line);
                    break;
                case readableValues.ActionType:

                    break;
                case readableValues.ViableTargets:
                    break;
                case readableValues.Amount:
                    break;
                case readableValues.ConditionTuple:
                    break;
                case readableValues.ChosenTarget:
                    break;
                default:
                    break;
            }
        }
    }
}