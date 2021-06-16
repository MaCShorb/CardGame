using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerScript : CharacterScript
{
    public int Mana { get; set; }
    public Deck MyDeck { get; private set; }
    public GameInputHandlerScript MyGameInputHandlerScript;
    public HistoryScript MyHistoryScript;
    public GameBoardScript MyGameBoardScript;
    public RelicScript MyRelicScript;

    // Start is called before the first frame update
    void Start()
    {
        Mana = 1000;
        MyDeck = new Deck(this, MyHistoryScript);
        MyDeck.CardAddedToHand += HandleCardAddedToHand;
        MyDeck.CardRemovedFromHand += HandleCardRemovedFromHand;
        MyDeck.CardDiscardedFromHand += HandleCardDiscardedFromHand;
        MyDeck.CreateStartingHand();
        SubscribeMouseDownHand();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SubscribeMouseDownHand()
    {
        foreach (GameObject card in MyDeck.Hand)
        {
            ClickComponent dummyClickComponent = card.GetComponent<ClickComponent>();
            dummyClickComponent.MouseUp += HandleCardMouseUp;
            dummyClickComponent.MouseEnter += HandleCardMouseEnter;
            dummyClickComponent.MouseExit += HandleCardMouseExit;
        }
    }

    public void UnsubscribeMouseDownHand()      
    {
        foreach (GameObject card in MyDeck.Hand)
        {
            ClickComponent dummyClickComponent = card.GetComponent<ClickComponent>();
            dummyClickComponent.MouseUp -= HandleCardMouseUp;
            dummyClickComponent.MouseEnter -= HandleCardMouseEnter;
            dummyClickComponent.MouseExit -= HandleCardMouseExit;
        }
    }

    public void HandleCardMouseUp(object sender, GameObject card) // Click that indicates card is being played.
    {
        CardScript dummyCardScript = card.GetComponent<CardScript>();
        if (dummyCardScript.Cost <= Mana)
        {
            Mana -= dummyCardScript.Cost;
            UnsubscribeMouseDownHand();
            dummyCardScript.ProcessAction();
        }
    }

    public void HandleCardMouseEnter(object sender, GameObject card)
    {
        float SCALE_AMOUNT = 1.1f;
        float SCALE_SPEED = 1f;

        MoveComponent dummyMC = card.GetComponent<MoveComponent>();
        dummyMC.scaleTo(SCALE_AMOUNT, SCALE_SPEED);
    }

    public void HandleCardMouseExit(object sender, GameObject card)
    {
        float SCALE_SPEED = 1f;

        MoveComponent dummyMC = card.GetComponent<MoveComponent>();
        dummyMC.scaleToOriginal(SCALE_SPEED);
    }

    public void SubscribeToUIExecutorActionEvents(UIActionExecutor UIAE)
    {
        UIAE.ActionNeedsTarget += HandleUIActionExecutorNeedsTarget;
        UIAE.PassAction += HandleUIActionExecutorPassAction;
        UIAE.ActionFoundTarget += HandleUIActionExecutorFoundTarget;
        UIAE.FinishedExecuting += HandleCardFinishedExecuting;
    }

    public void UnsubscribeToUIExecutorActionEvents(UIActionExecutor UIAE)
    {
        UIAE.ActionNeedsTarget -= HandleUIActionExecutorNeedsTarget;
        UIAE.PassAction -= HandleUIActionExecutorPassAction;
        UIAE.ActionFoundTarget -= HandleUIActionExecutorFoundTarget;
        UIAE.FinishedExecuting -= HandleCardFinishedExecuting;
    }

    public void HandleCardAddedToHand(object sender, GameObject card)
    {
        SubscribeToUIExecutorActionEvents(card.GetComponent<UIActionExecutor>());
    }

    public void HandleCardDrawnToHand(object sender, GameObject card)
    {
        HandleCardAddedToHand(this, card);
    }

    public void HandleCardRemovedFromHand(object sender, GameObject card)
    {
        ClickComponent dummyCC = card.GetComponent<ClickComponent>();
        dummyCC.MouseUp -= HandleCardMouseUp;

        UnsubscribeToUIExecutorActionEvents(card.GetComponent<UIActionExecutor>());
    }
    
    public void HandleUIActionExecutorNeedsTarget(object sender, GameObject card)
    {
        UIActionExecutor UIAE = card.GetComponent<UIActionExecutor>();
        MyGameInputHandlerScript.TargetSelected += UIAE.HandleTargetSelection;
        MyGameInputHandlerScript.BeginSearchForTarget(UIAE.GetActionFirstInLine().ViableTargetTags);
    }

    public void HandleUIActionExecutorFoundTarget(object sender, GameObject card)
    {
        UIActionExecutor UIAE = card.GetComponent<UIActionExecutor>();
        MyGameInputHandlerScript.TargetSelected -= UIAE.HandleTargetSelection;
    }

    public void HandleUIActionExecutorPassAction(object sender, Action action)
    {
        MyGameInputHandlerScript.SendActionToDelegate(action);
    }

    public void HandleCardFinishedExecuting(object swender, GameObject card)
    {
        MyDeck.RemoveCardFromHand(card, Deck.RemovalType.play);
        SubscribeMouseDownHand();
    }

    public void HandleCardDiscardedFromHand(object sender, GameObject card)
    {
        CardScript dummmyCardScript = card.GetComponent<CardScript>();
        dummmyCardScript.DiscardCode.Invoke(null);
    }

    public void EquipRelic(GameObject relic)
    {
        MyRelicScript = relic.GetComponent<RelicScript>();
        MyRelicScript.gameObject.SetActive(true);
        MyRelicScript.RelicEquipAnimation();

        SubscribeToUIExecutorActionEvents(MyRelicScript);
    }

    public void DiscardRelic()
    {
        UnsubscribeToUIExecutorActionEvents(MyRelicScript);

        MyRelicScript.gameObject.SetActive(false);
    }

    public void HandleRelicConditionMet(object sender, GameObject relic)
    {
        MyRelicScript.ProcessAction();
    }
}
