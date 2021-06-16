using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Deck
{
    public List<GameObject> Hand { get; set; }
    public List<GameObject> DiscardPile;
    public List<GameObject> DrawPile;
    readonly int HAND_MAX;
    CardDatabase MyCardDatabase;

    public enum RemovalType { discard, play, burn }

    public event EventHandler<GameObject> CardAddedToHand;
    public event EventHandler<GameObject> CardRemovedFromHand;
    public event EventHandler<GameObject> CardDiscardedFromHand;
    public event EventHandler<GameObject> CardDrawnToHand;
    public Deck(PlayerScript playerScript, HistoryScript gameHistoryScript)
    {
        HAND_MAX = 8;
        Hand = new List<GameObject>();
        DiscardPile = new List<GameObject>();
        DrawPile = new List<GameObject>();

        // Create starting deck:
        MyCardDatabase = new CardDatabase(playerScript, gameHistoryScript);

        DrawPile.Insert(0, MyCardDatabase.GetObject("Platinum Gauntlet"));
        DrawPile.Insert(0, MyCardDatabase.GetObject("Lighten"));
        DrawPile.Insert(0, MyCardDatabase.GetObject("Badge of Abundance"));
        DrawPile.Insert(0, MyCardDatabase.GetObject("Binding Beam"));
        DrawPile.Insert(0, MyCardDatabase.GetObject("Refuel"));
        DrawPile.Insert(0, MyCardDatabase.GetObject ("Consuming Torch"));

        // Create CardDatabase class, feeding in parameter playerOwnerScript.
        // Add to Draw Pile every starting card.
        // Shuffle. If function calling not permitted within init, call shuffle
        // in start() function.
    }

    public void CreateStartingHand()
    {
        // Only one card for now, testing one punch card.
        ShuffleDrawPile();
        DrawCard(5);
    }

    public void DrawCard(int amount)
    {
        int drawAmount = amount;
        if (amount + Hand.Count > HAND_MAX)
        {
            drawAmount = HAND_MAX - Hand.Count;
        }
        for (int i = drawAmount; i > 0; i--)
        {
            if (DrawPile.Count == 0)
            {
                RefreshDrawPile();
            }

            Hand.Add(DrawPile[0]);
            DrawPile.RemoveAt(0);
            Hand[Hand.Count - 1].SetActive(true);
            DrawAnimation(Hand[Hand.Count - 1]);
            UpdateHand();
            OnCardDrawnToHand(Hand[Hand.Count - 1]);
        }

    }

    public void RemoveCardFromHand(GameObject card, RemovalType typeOfRemoval)
    {
        DiscardPile.Insert(0, card);
        Hand.Remove(card);
        switch(typeOfRemoval)
        {
            case RemovalType.play:
                PlayAnimation(card);
                break;
            case RemovalType.discard:
                DiscardAnimation(card);
                OnCardDiscarded(card);
                break;
            default:
                Debug.Log("NOTICE: Type of card removal is not supported.");
                break;
        }
        UpdateHand();
        card.SetActive(false);
        OnCardRemovedFromHand(card);
    }

    void ShuffleDrawPile()
    {
        
    }

    void RefreshDrawPile()
    {
        GameObject[] discardArray = new GameObject[DiscardPile.Count];
        DiscardPile.CopyTo(discardArray, 0);
        foreach (GameObject card in discardArray)
        {
            DrawPile.Add(card);
        }
        RefreshDrawPileAnimation();
        ShuffleDrawPile();
    }

    public void ReplaceCard(GameObject oldCard, GameObject newCard)
    {
        if (DrawPile.Contains(oldCard))
        {
            DrawPile[DrawPile.IndexOf(oldCard)] = newCard;
        }
        else if (DiscardPile.Contains(oldCard))
        {
            DiscardPile[DiscardPile.IndexOf(oldCard)] = newCard;
        }
        else if (Hand.Contains(oldCard))
        {
            Hand[Hand.IndexOf(oldCard)] = newCard;
        }
    }

    void DrawAnimation(GameObject card)
    {

    }

    void UpdateHand()
    {
        float X_CARD_SEPERATION = 1.95f;
        float Y_CARD_POSITION = -3.5f;
        float EVEN_CENTER_SEPERATION = X_CARD_SEPERATION / 2;
        int CARDS_FROM_CENTER = Hand.Count / 2;
        float MOVE_SPEED = 15;

        // If even, cards positioned with center empty. If odd, cards positioned
        // distance from a card in the center. 1 is odd.

        if (Hand.Count % 2 != 0) // Odd
        {
            EVEN_CENTER_SEPERATION = 0; // Don't need spread from center.
        }

        for (int cardFromCenterIndex = -CARDS_FROM_CENTER, i = 0; i < Hand.Count; cardFromCenterIndex++, i++)
        {
            if (EVEN_CENTER_SEPERATION != 0 && cardFromCenterIndex == 0)
            {
                cardFromCenterIndex++;
            }
            GameObject cardToPosition = Hand[i];
            MoveComponent dummyMC = cardToPosition.GetComponent<MoveComponent>();

            Vector2 moveToVector = new Vector2(cardFromCenterIndex * X_CARD_SEPERATION + EVEN_CENTER_SEPERATION * -Mathf.Sign(cardFromCenterIndex), Y_CARD_POSITION);
            dummyMC.moveTo(moveToVector, MOVE_SPEED);

            // ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
            // TODO: Scale cards in hand when drawing past 5.
            // ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
        }
    }

    void RefreshDrawPileAnimation()
    {

    }

    void DiscardAnimation(GameObject card)
    {

    }

    void PlayAnimation(GameObject card)
    {
        GameObject.Destroy(card);
    }

    void OnCardAddedToHand(GameObject card)
    {
        CardAddedToHand.Invoke(this, card);
    }

    void OnCardRemovedFromHand(GameObject card)
    {
        CardRemovedFromHand.Invoke(this, card);
    }

    void OnCardDiscarded(GameObject card)
    {
        CardDiscardedFromHand.Invoke(this, card);
    }

    void OnCardDrawnToHand(GameObject card)
    {
        CardDrawnToHand.Invoke(this, card);
    }

}
