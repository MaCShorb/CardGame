using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeckScript : MonoBehaviour
{
    LinkedList<GameObject> Hand;
    LinkedList<GameObject> DiscardPile;
    LinkedList<GameObject> DrawPile;
    readonly int HAND_MAX;

    public DeckScript()
    {
        HAND_MAX = 8;
        Hand = new LinkedList<GameObject>();
        DiscardPile = new LinkedList<GameObject>();
        DrawPile = new LinkedList<GameObject>();
    }

    void DrawCard(int amount)
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

            Hand.AddLast(DrawPile.First);
            DrawPile.RemoveFirst();
            DrawAnimation(Hand.Last.Value);
            UpdateHand();
        }
    }

    public void RemoveCardFromHand(GameObject card, Action.ActionType typeOfRemoval)
    {
        // Make sure this actually works! I'm not sure how GameObject comparison
        // works and all that.
        Hand.Remove(card);
        switch(typeOfRemoval)
        {
            case Action.ActionType.play:
                PlayAnimation(card);
                break;
            case Action.ActionType.discard:
                DiscardAnimation(card);
                break;
            default:
                Debug.Log("Card Removal Type not supported!");
                break;
        }
        UpdateHand();
        DiscardPile.AddFirst(card);
    }

    void ShuffleDrawPile()
    {
        // ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
        // TODO: This function has a TON of for loops. When it's time for
        // optimization, see if less for loops can be used, or if there is an
        // easier way altogether to shuffle a linked list.
        // ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

        GameObject[] drawPileArray = new GameObject[DrawPile.Count];
        DrawPile.CopyTo(drawPileArray, 0);

        // Holds Positions that are drawn out at random.
        List<int> indexHolderList = new List<int>();
        for (int i = 0; i < DrawPile.Count; i++)
        {
            indexHolderList[i] = i;
        }

        // Shuffles array of ints, which will be used to shuffle order of drawPileArray
        List<int> shuffledIndexHolderList = new List<int>();
        for (int i = 0; i < DrawPile.Count; i++)
        {
            int randomIndex = Random.Range(0, indexHolderList.Count);
            shuffledIndexHolderList[i] = indexHolderList[randomIndex];
            indexHolderList.RemoveAt(randomIndex);
        }

        // Create shuffled drawPile Array
        GameObject[] shuffledDrawPileArray = new GameObject[DrawPile.Count];
        for (int i = 0; i < DrawPile.Count; i++)
        {
            shuffledDrawPileArray[i] = drawPileArray[shuffledIndexHolderList[i]];
        }

        // Move shuffled drawPile Array into DrawPile. DrawPile is now shuffled!
        DrawPile = new LinkedList<GameObject>(shuffledDrawPileArray);
    }

    void RefreshDrawPile()
    {
        GameObject[] discardArray = new GameObject[DiscardPile.Count];
        DiscardPile.CopyTo(discardArray, 0);
        foreach (GameObject card in discardArray)
        {
            DrawPile.AddLast(card);
        }
        RefreshDrawPileAnimation();
        ShuffleDrawPile();
    }

    public void ReplaceCard(GameObject oldCard, GameObject newCard)
    {
        if (DrawPile.Contains(oldCard))
        {
            DrawPile.Find(oldCard).Value = newCard;
        }
        else if (DiscardPile.Contains(oldCard))
        {
            DiscardPile.Find(oldCard).Value = newCard;
        }
        else if (Hand.Contains(oldCard))
        {
            Hand.Find(oldCard).Value = newCard;
        }
    }

    public void ProcessActionFromGameHandler(Action action)
    {
        switch(action.getActionType())
        {
            case Action.ActionType.discard:
                RemoveCardFromHand(action.getChosenTarget(), typeOfRemoval: action.getActionType());
                break;
            case Action.ActionType.draw:
                DrawCard(action.getAmount());
                break;
            default:
                Debug.Log("Deck cannot process that action type!");
                break;
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
        float MOVE_SPEED = 0;

        LinkedList<GameObject> mutableHandForPositioning = Hand;

        // If even, cards positioned with center empty. If odd, cards positioned
        // distance from a card in the center. 1 is odd.

        if (Hand.Count % 2 != 0) // Odd
        {
            EVEN_CENTER_SEPERATION = 0; // Don't need spread from center.
        }

        for (int cardFromCenterIndex = -CARDS_FROM_CENTER, i = 0; i < Hand.Count; cardFromCenterIndex++, i++)
        {
            GameObject workedWithCard = mutableHandForPositioning.First.Value;
            MoveComponent dummyMC = workedWithCard.GetComponent<MoveComponent>();

            Vector2 moveToVector = new Vector2(cardFromCenterIndex * X_CARD_SEPERATION + EVEN_CENTER_SEPERATION * Mathf.Sign(cardFromCenterIndex), Y_CARD_POSITION);
            dummyMC.moveTo(moveToVector, MOVE_SPEED);

            mutableHandForPositioning.RemoveFirst();
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
        Destroy(card);
    }

}
