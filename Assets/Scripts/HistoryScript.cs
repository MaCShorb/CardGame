using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HistoryScript: MonoBehaviour
{
    public enum EventType
    {
        Damage,
        Restore,
        Draw,
        Discard,
        None,
        Armor,
        DeckManipulaton
    }

    public class Event
    {
        public EventType Type { get; set; }
        public GameObject Target { get; set; }
        public int Amount { get; set; }

        public Event(EventType Type, GameObject Target, int Amount)
        {
            this.Type = Type;
            this.Target = Target;
            this.Amount = Amount;
        }
    }

    public class Turn
    {
        public enum Type
        {
            Player,
            Monster
        }

        public Type TurnType;
        public GameObject TurnTaker { get; set; }
        public List<Event> TurnEvents { get; private set; }

        public Turn(Turn.Type TurnType, GameObject TurnTaker)
        {
            this.TurnType = TurnType;
            this.TurnTaker = TurnTaker;
            this.TurnEvents = TurnEvents;
        }

        public void AddEvent(Event e)
        {
            TurnEvents.Add(e);
        }
    }

    private List<Turn> GameHistory;

    private void Awake()
    {
        GameHistory = new List<Turn>();
        // Add first turn. Get information needed for TurnType, TurnTaker from GameTurnHandler.
    }

    public void AddEvent(EventType Type, GameObject Target, int Amount)
    {
       // GameHistory[GameHistory.Count - 1].AddEvent(new Event(Type, Target, Amount));
    }

    public void EndTurn()
    {
        // Add new turn to GameHistory, getting information for next neccessary turn from GameTurnHandler.
    }

    public Turn GetLastTurn(GameObject ofTurnTaker)
    {
        for (int i = GameHistory.Count - 2; i >= 0; i--)
        {
            if (GameHistory[i].TurnTaker == ofTurnTaker)
            {
                return GameHistory[i];
            }
        }
        return GameHistory[0];
    }

    public Turn GetCurrentTurn()
    {
        return GameHistory[GameHistory.Count - 1];
    }

}
 