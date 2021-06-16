using System;
using System.Collections;
using UnityEngine;
using System.Collections.Generic;

public static class CardDatabase : UIActionExecutorDatabase
{


    /*
    public RelicDatabase MyRelicDatabase;

    public CardDatabase(PlayerScript playerScript, HistoryScript gameHistoryScript) : base (playerScript, gameHistoryScript, Resources.Load<GameObject>("Card"))
    {
        MyRelicDatabase = new RelicDatabase(playerScript, gameHistoryScript);
    }

    protected override void ParseCSV()
    {
        List<Dictionary<string, object>> data = CSVReader.Read("CardDatabase_CSV");

        for (var i = 0; i < data.Count; i++)
        {
            string Name = data[i]["CardName"].ToString();
            string Text = data[i]["CardText"].ToString();
            int Cost = int.Parse(data[i]["CardCost"].ToString());
            int Tier = int.Parse(data[i]["CardTier"].ToString());
            string Type = data[i]["CardType"].ToString();

            CreateDatabaseEntry(Name.ToUpper());
            CardScript dummyCardScript = DatabaseList[Name.ToUpper()].GetComponent<CardScript>();
            dummyCardScript.SetValues(Name, Text, Cost, Tier, Type);
        }
    }

    protected override void LikenSecondToFirstValuesForGetObject(GameObject first, GameObject second)
    {
        CardScript firstCardScript = first.GetComponent<CardScript>();
        CardScript secondCardScript = second.GetComponent<CardScript>();

        secondCardScript.SetValues(firstCardScript.Name, firstCardScript.Text, firstCardScript.Cost, firstCardScript.Tier, firstCardScript.Type);
        secondCardScript.DiscardCode = firstCardScript.DiscardCode;
        secondCardScript.DrawCode = firstCardScript.DrawCode;
        secondCardScript.RefreshCode = firstCardScript.RefreshCode;
    }

    protected override void EstablishDatabaseValues()
    {
        // =====================================================================
        // Punch Card
        // =====================================================================
        Action.ActionCodeShell PunchCode1 = delegate (GameObject target)
        {
            int DAMAGE_AMOUNT = 1;
            GetCharScript(target).takeDamage(DAMAGE_AMOUNT);
            MyHistoryScript.AddEvent(HistoryScript.EventType.Damage, target, DAMAGE_AMOUNT);
        };

        Action PunchAction1 = new Action(PunchCode1, new List<String> { "MONSTER", "ALLY", "SELF" });
        SetUIActionExecutorActions("Punch", new List<Action> { PunchAction1 });
        // =====================================================================
        // Refuel Card
        // =====================================================================
        Action.ActionCodeShell RefuelCode1 = delegate
        {
            int DRAW_AMOUNT = 1;
            MyPlayerScript.MyDeck.DrawCard(DRAW_AMOUNT);
            MyHistoryScript.AddEvent(HistoryScript.EventType.Draw, null, DRAW_AMOUNT);
        };

        Action RefuelAction1 = new Action(RefuelCode1);
        SetUIActionExecutorActions("Refuel", new List<Action> { RefuelAction1 });
        // =====================================================================
        // Stall Card
        // =====================================================================
        Action.ActionCodeShell StallCode1 = delegate
        {
            MyHistoryScript.AddEvent(HistoryScript.EventType.None, null, 0);
        };

        Action StallAction1 = new Action(StallCode1);
        SetUIActionExecutorActions("Stall", new List<Action> { StallAction1 });

        // =====================================================================
        // Bandage Card
        // =====================================================================
        Action.ActionCodeShell BandageCode1 = delegate (GameObject target)
        {
            int RESTORE_AMOUNT = 1;
            GetCharScript(target).restoreHealth(RESTORE_AMOUNT);
            MyHistoryScript.AddEvent(HistoryScript.EventType.Restore, target, RESTORE_AMOUNT);
        };

        Action BandageAction1 = new Action(BandageCode1, new List<String> { "MONSTER", "ALLY", "SELF" });
        SetUIActionExecutorActions("Bandage", new List<Action> { BandageAction1 });
        // =====================================================================
        // Lighten Card
        // =====================================================================
        Action.ActionCodeShell TossCode1 = delegate (GameObject target)
        {
            MyPlayerScript.MyDeck.RemoveCardFromHand(target, Deck.RemovalType.discard);
            MyHistoryScript.AddEvent(HistoryScript.EventType.Discard, target, 1);
        };

        Action TossAction1 = new Action(TossCode1, new List<String> { "CARD" });
        SetUIActionExecutorActions("Lighten", new List<Action> { TossAction1 });
        // =====================================================================
        // Brass Knuckles Card
        // =====================================================================
        Action.ActionCodeShell BrassKnucklesCode1 = delegate (GameObject target)
        {
            int DAMAGE_AMOUNT = 2;
            GetCharScript(target).takeDamage(DAMAGE_AMOUNT);
            MyHistoryScript.AddEvent(HistoryScript.EventType.Damage, target, DAMAGE_AMOUNT);
        };

        Action BrassKnucklesAction1 = new Action(BrassKnucklesCode1, new List<String> { "MONSTER", "ALLY", "SELF" });
        SetUIActionExecutorActions("Brass Knuckles", new List<Action> { BrassKnucklesAction1 });
        // =====================================================================
        // Double-Edged Dagger
        // =====================================================================
        Action.ActionCodeShell DoubleEdgedDaggerCode1 = delegate (GameObject target)
        {
            int DAMAGE_AMOUNT = 3;
            GetCharScript(target).takeDamage(DAMAGE_AMOUNT);
            MyHistoryScript.AddEvent(HistoryScript.EventType.Damage, target, DAMAGE_AMOUNT);
            MyPlayerScript.takeDamage(DAMAGE_AMOUNT);
            MyHistoryScript.AddEvent(HistoryScript.EventType.Damage, MyPlayerScript.gameObject, DAMAGE_AMOUNT);
        };

        Action DoubleEdgedDaggerAction1 = new Action(DoubleEdgedDaggerCode1, new List<String> { "MONSTER", "ALLY", "SELF" });
        SetUIActionExecutorActions("Double-Edged Dagger", new List<Action> { DoubleEdgedDaggerAction1 });
        // =====================================================================
        // Spiked Uppercut
        // =====================================================================
        Action.ActionCodeShell SpikedUppercutCode1 = delegate (GameObject target)
        {
            int DAMAGE_AMOUNT = 4;
            GetCharScript(target).takeDamage(DAMAGE_AMOUNT);
            MyHistoryScript.AddEvent(HistoryScript.EventType.Damage, target, DAMAGE_AMOUNT);
        };

        Action SpikedUppercutAction1 = new Action(SpikedUppercutCode1, new List<String> { "MONSTER", "ALLY", "SELF" });
        SetUIActionExecutorActions("Spiked Uppercut", new List<Action> { SpikedUppercutAction1 });
        // =====================================================================
        // Double-Edged Longsword
        // =====================================================================
        Action.ActionCodeShell DoubleEdgedLongswordCode1 = delegate (GameObject target)
        {
            int DAMAGE_AMOUNT = 5;
            GetCharScript(target).takeDamage(DAMAGE_AMOUNT);
            MyHistoryScript.AddEvent(HistoryScript.EventType.Damage, target, DAMAGE_AMOUNT);
            MyPlayerScript.takeDamage(DAMAGE_AMOUNT);
            MyHistoryScript.AddEvent(HistoryScript.EventType.Damage, MyPlayerScript.gameObject, DAMAGE_AMOUNT);
        };

        Action DoubleEdgedLongswordAction1 = new Action(SpikedUppercutCode1, new List<String> { "MONSTER", "ALLY", "SELF" });
        SetUIActionExecutorActions("Double-Edged Longsword", new List<Action> { DoubleEdgedDaggerAction1 });
        // =====================================================================
        // Spirit Share
        // =====================================================================
        Action.ActionCodeShell SpiritShareCode1 = delegate (GameObject target)
        {
            int DAMAGE_AMOUNT = 7;
            GetCharScript(target).takeDamage(DAMAGE_AMOUNT);
            MyHistoryScript.AddEvent(HistoryScript.EventType.Damage, target, DAMAGE_AMOUNT);
            MyPlayerScript.takeDamage(DAMAGE_AMOUNT);
            MyHistoryScript.AddEvent(HistoryScript.EventType.Damage, MyPlayerScript.gameObject, DAMAGE_AMOUNT);
        };

        Action SpiritShareAction1 = new Action(SpiritShareCode1, new List<string> { "MONSTER", "ALLY", "SELF" });
        SetUIActionExecutorActions("Spirit Share", new List<Action> { SpiritShareAction1 });
        // =====================================================================
        // Platinum Gauntlet
        // =====================================================================
        Action.ActionCodeShell PlatinumGauntletCode1 = delegate (GameObject target)
        {
            int DAMAGE_AMOUNT = 5;
            GetCharScript(target).takeDamage(DAMAGE_AMOUNT);
            MyHistoryScript.AddEvent(HistoryScript.EventType.Damage, target, DAMAGE_AMOUNT);
        };

        Action PlatinumGauntletAction1 = new Action(PlatinumGauntletCode1, new List<string> { "MONSTER", "ALLY", "SELF" });
        SetUIActionExecutorActions("Platinum Gauntlet", new List<Action> { PlatinumGauntletAction1 });

        // =====================================================================
        // Potion of Revigoration
        // =====================================================================
        Action.ActionCodeShell RevigorationCode1 = delegate
        {
            int DRAW_AMOUNT = 4;
            MyPlayerScript.MyDeck.DrawCard(DRAW_AMOUNT);
            MyHistoryScript.AddEvent(HistoryScript.EventType.Draw, null, DRAW_AMOUNT);
        };

        Action RevigorationAction1 = new Action(RevigorationCode1);
        SetUIActionExecutorActions("Potion of Revigoration", new List<Action> { RevigorationAction1 });
        // =====================================================================
        // Consuming Torch
        // =====================================================================
        Action.ActionCodeShell ConsumingTorchCode1 = delegate (GameObject target)
        {
            MyPlayerScript.MyDeck.RemoveCardFromHand(target, Deck.RemovalType.discard);
            MyHistoryScript.AddEvent(HistoryScript.EventType.Discard, target, 1);
        };

        Action ConsumingTorchAction1 = new Action(ConsumingTorchCode1, new List<string> { "CARD" });
        SetUIActionExecutorActions("Consuming Torch", new List<Action> { ConsumingTorchAction1, ConsumingTorchAction1, ConsumingTorchAction1 });
        // =====================================================================
        // Throw the Lot!
        // =====================================================================
        Action.ActionCodeShell LotCode1 = delegate (GameObject target)
        {
            int DAMAGE_AMOUNT = MyPlayerScript.MyDeck.Hand.Count;
            GetCharScript(target).takeDamage(DAMAGE_AMOUNT);
            MyHistoryScript.AddEvent(HistoryScript.EventType.Damage, target, DAMAGE_AMOUNT);
        };
        Action LotAction1 = new Action(LotCode1, new List<string> { "MONSTER", "ALLY", "SELF" });
        SetUIActionExecutorActions("Throw the Lot!", new List<Action> { LotAction1 });
        // =====================================================================
        // Shield Up!
        // =====================================================================
        Action.ActionCodeShell ShieldUpCode1 = delegate
        {
            int ARMOR_AMOUNT = 5;
            MyPlayerScript.gainArmor(ARMOR_AMOUNT);
            MyHistoryScript.AddEvent(HistoryScript.EventType.Armor, MyPlayerScript.gameObject, ARMOR_AMOUNT);
        };

        Action ShieldUpAction1 = new Action(ShieldUpCode1);
        SetUIActionExecutorActions("Shield Up!", new List<Action> { ShieldUpAction1 });
        // =====================================================================
        // Safety First
        // =====================================================================
        Action.ActionCodeShell SafetyFirstCode1 = delegate (GameObject target)
        {
            int ARMOR_AMOUNT = 3;
            GetCharScript(target).gainArmor(ARMOR_AMOUNT);
            MyHistoryScript.AddEvent(HistoryScript.EventType.Armor, target, ARMOR_AMOUNT);
        };

        Action SafetyFirstAction1 = new Action(SafetyFirstCode1, new List<string> { "ALLY" });
        SetUIActionExecutorActions("Safety First", new List<Action> { SafetyFirstAction1 });
        // =====================================================================
        // Bash
        // =====================================================================
        Action.ActionCodeShell BaseCode1 = delegate (GameObject target)
        {
            int ARMOR_CONDITIONAL_AMOUNT = 5;
            int DAMAGE_AMOUNT = 4;
            int ARMOR_AMOUNT = 3;

            if (MyPlayerScript.armor >= ARMOR_CONDITIONAL_AMOUNT)
            {
                GetCharScript(target).takeDamage(DAMAGE_AMOUNT);
                MyHistoryScript.AddEvent(HistoryScript.EventType.Damage, target, DAMAGE_AMOUNT);
            }
            MyPlayerScript.gainArmor(ARMOR_AMOUNT);
            MyHistoryScript.AddEvent(HistoryScript.EventType.Armor, MyPlayerScript.gameObject, ARMOR_AMOUNT);
        };

        Action BaseAction1 = new Action(SafetyFirstCode1, new List<string> { "MONSTER", "ALLY", "SELF" });
        SetUIActionExecutorActions("Bash", new List<Action> { BaseAction1 });
        // =====================================================================
        // Implement Alloy
        // =====================================================================
        Action.ActionCodeShell ImplementAlloyCode1 = delegate
        {
            int ARMOR_MULTIPLICATION_AMOUNT = 3;
            int TOTAL_ARMOR_GAIN = MyPlayerScript.armor * ARMOR_MULTIPLICATION_AMOUNT - MyPlayerScript.armor;
            MyPlayerScript.gainArmor(TOTAL_ARMOR_GAIN);
            MyHistoryScript.AddEvent(HistoryScript.EventType.Armor, MyPlayerScript.gameObject, TOTAL_ARMOR_GAIN);
        };

        Action ImplementAlloyAction1 = new Action(ImplementAlloyCode1);
        SetUIActionExecutorActions("Implement Alloy", new List<Action> { ImplementAlloyAction1 });
        // =====================================================================
        // Medic's Bond
        // =====================================================================
        Action.ActionCodeShell MedicsBondCode1 = delegate (GameObject target)
        {
            int RESTORE_AMOUNT = 4;
            GetCharScript(target).restoreHealth(RESTORE_AMOUNT);
            MyHistoryScript.AddEvent(HistoryScript.EventType.Restore, target, RESTORE_AMOUNT);
            MyPlayerScript.restoreHealth(RESTORE_AMOUNT);
            MyHistoryScript.AddEvent(HistoryScript.EventType.Restore, MyPlayerScript.gameObject, RESTORE_AMOUNT);
        };

        Action MedicsBondAction1 = new Action(ImplementAlloyCode1, new List<string> { "ALLY" });
        SetUIActionExecutorActions("Medic's Bond", new List<Action> { MedicsBondAction1 });
        // =====================================================================
        // Binding Beam
        // =====================================================================
        Action.ActionCodeShell BindingBeamCode1 = delegate (GameObject target)
        {
            int RESTORE_AMOUNT = 6;
            GetCharScript(target).restoreHealth(RESTORE_AMOUNT);
            MyHistoryScript.AddEvent(HistoryScript.EventType.Restore, target, RESTORE_AMOUNT);
        };

        Action BindingBeamAction1 = new Action(BindingBeamCode1, new List<string> { "MONSTER", "ALLY", "SELF" });
        SetUIActionExecutorActions("Binding Beam", new List<Action> { BindingBeamAction1 });
        // =====================================================================
        // Luther's Renewal
        // =====================================================================
        Action.ActionCodeShell LutherRenewalCode1 = delegate (GameObject target)
        {
            CharacterScript targetCharScript = GetCharScript(target);
            int RESTORE_AMOUNT_TARGET = targetCharScript.maxHealth - targetCharScript.health;
            int RESTORE_AMOUNT_SELF = MyPlayerScript.maxHealth - MyPlayerScript.health;
            targetCharScript.restoreHealth(RESTORE_AMOUNT_TARGET);
            MyHistoryScript.AddEvent(HistoryScript.EventType.Restore, target, RESTORE_AMOUNT_TARGET);
            MyPlayerScript.restoreHealth(RESTORE_AMOUNT_SELF);
            MyHistoryScript.AddEvent(HistoryScript.EventType.Restore, MyPlayerScript.gameObject, RESTORE_AMOUNT_SELF);
        };

        Action LutherRenewalAction1 = new Action(LutherRenewalCode1, new List<string> { "ALLY" });
        SetUIActionExecutorActions("Luther's Renewal", new List<Action> { LutherRenewalAction1 });
        // =====================================================================
        // Sick Note
        // =====================================================================
        Action.ActionCodeShell SickNoteCode1 = delegate
        {
            int DRAW_AMOUNT = 3;
            foreach (HistoryScript.Event e in MyHistoryScript.GetCurrentTurn().TurnEvents)
            {
                if (e.Type == HistoryScript.EventType.Damage && e.Target == MyPlayerScript.gameObject && e.Amount > 0)
                {
                    MyPlayerScript.MyDeck.DrawCard(DRAW_AMOUNT);
                    MyHistoryScript.AddEvent(HistoryScript.EventType.Draw, null, DRAW_AMOUNT);
                }
            }
        };

        Action SickNoteAction1 = new Action(SickNoteCode1);
        SetUIActionExecutorActions("Sick Note", new List<Action> { SickNoteAction1 });
        // =====================================================================
        // Explosive Cry
        // =====================================================================
        Action.ActionCodeShell ExplosiveCryCode1 = delegate
        {
            int DAMAGE_AMOUNT = 3;
            foreach (HistoryScript.Event e in MyHistoryScript.GetCurrentTurn().TurnEvents)
            {
                if (e.Type == HistoryScript.EventType.Damage && e.Target == MyPlayerScript.gameObject && e.Amount > 0)
                {
                    MyPlayerScript.MyGameBoardScript.ExecuteDelegateOnAllMonsters(delegate (GameObject target)
                    {
                        GetCharScript(target).takeDamage(DAMAGE_AMOUNT);
                        MyHistoryScript.AddEvent(HistoryScript.EventType.Damage, target, DAMAGE_AMOUNT);
                    });
                }
            }
        };

        Action ExplosiveCryAction1 = new Action(ExplosiveCryCode1);
        SetUIActionExecutorActions("Explosive Cry", new List<Action> { ExplosiveCryAction1 });
        // =====================================================================
        // Last Resort
        // =====================================================================
        Action.ActionCodeShell LastResortCode1 = delegate
        {
            int DAMAGE_AMOUNT = 5;
            if (MyPlayerScript.MyDeck.Hand.Count == 1)
            {
                MyPlayerScript.MyGameBoardScript.ExecuteDelegateOnAllMonsters(delegate (GameObject target)
                {
                    GetCharScript(target).takeDamage(DAMAGE_AMOUNT);
                    MyHistoryScript.AddEvent(HistoryScript.EventType.Damage, target, DAMAGE_AMOUNT);
                });
            }
        };

        Action LastResortAction1 = new Action(LastResortCode1);
        SetUIActionExecutorActions("Last Resort", new List<Action> { LastResortAction1 });
        // =====================================================================
        // Portable Plates
        // =====================================================================
        Action.ActionCodeShell PortablePlatesCode1 = delegate
        {
            int ARMOR_AMOUNT = 2;
            MyPlayerScript.MyGameBoardScript.ExecuteDelegateOnAllPlayers(delegate (GameObject target)
            {
                GetCharScript(target).gainArmor(ARMOR_AMOUNT);
                MyHistoryScript.AddEvent(HistoryScript.EventType.Armor, target, ARMOR_AMOUNT);
            });

        };

        Action PortablePlatesAction1 = new Action(PortablePlatesCode1);
        SetUIActionExecutorActions("Portable Plates", new List<Action> { PortablePlatesAction1 });
        // =====================================================================
        // Experimental Voodoology
        // =====================================================================
        Action.ActionCodeShell VoodoologyCode1 = delegate
        {
            int ARMOR_AMOUNT = 6;
            int DAMAGE_AMOUNT = 6;

            MyPlayerScript.MyGameBoardScript.ExecuteDelegateOnAllPlayers(delegate (GameObject target)
            {
                if (target != MyPlayerScript.gameObject)
                {
                    GetCharScript(target).gainArmor(ARMOR_AMOUNT);
                    MyHistoryScript.AddEvent(HistoryScript.EventType.Armor, target, ARMOR_AMOUNT);
                }
            });
            MyPlayerScript.takeDamage(DAMAGE_AMOUNT);
            MyHistoryScript.AddEvent(HistoryScript.EventType.Damage, MyPlayerScript.gameObject, DAMAGE_AMOUNT);
        };

        Action VoodoologyAction1 = new Action(VoodoologyCode1);
        SetUIActionExecutorActions("Experimental Voodoology", new List<Action> { VoodoologyAction1 });
        // =====================================================================
        // Pep Talk
        // =====================================================================
        Action.ActionCodeShell PepTalkCode1 = delegate
        {
            int RESTORE_AMOUNT = 2;

            MyPlayerScript.MyGameBoardScript.ExecuteDelegateOnAllPlayers(delegate (GameObject target)
            {
                GetCharScript(target).restoreHealth(RESTORE_AMOUNT);
                MyHistoryScript.AddEvent(HistoryScript.EventType.Restore, target, RESTORE_AMOUNT);
            });
        };

        Action PepTalkAction1 = new Action(PepTalkCode1);
        SetUIActionExecutorActions("Pep Talk", new List<Action> { PepTalkAction1 });
        // =====================================================================
        // Group Therapy
        // =====================================================================
        Action.ActionCodeShell GroupTherapyCode1 = delegate
        {
            int RESTORE_AMOUNT = 4;

            MyPlayerScript.MyGameBoardScript.ExecuteDelegateOnAllPlayers(delegate (GameObject target)
            {
                GetCharScript(target).restoreHealth(RESTORE_AMOUNT);
                MyHistoryScript.AddEvent(HistoryScript.EventType.Restore, target, RESTORE_AMOUNT);
            });
        };

        Action GroupTherapyAction1 = new Action(GroupTherapyCode1);
        SetUIActionExecutorActions("Group Therapy", new List<Action> { GroupTherapyAction1 });
        // =====================================================================
        // Ticking Tuber
        // =====================================================================
        Action.ActionCodeShell TickingTuberCode1 = delegate
        {
            int DAMAGE_AMOUNT = 8;
            MonsterScript monster = MyPlayerScript.MyGameBoardScript.GetRandomMonster();
            monster.takeDamage(DAMAGE_AMOUNT);
            MyHistoryScript.AddEvent(HistoryScript.EventType.Damage, monster.gameObject, DAMAGE_AMOUNT);
        };

        SetUIActionExecutorActions("Ticking Tuber", new List<Action> { });
        GetCardScript(DatabaseList["TICKING TUBER"]).DiscardCode = TickingTuberCode1;
        // =====================================================================
        // Confuzzle
        // =====================================================================
        Action.ActionCodeShell ConfuzzleCode1 = delegate
        {
            GameObject[] placeHoldArray = new GameObject[MyPlayerScript.MyDeck.DrawPile.Count];
            MyPlayerScript.MyDeck.DrawPile.CopyTo(placeHoldArray);
            MyPlayerScript.MyDeck.DrawPile = MyPlayerScript.MyDeck.DiscardPile;
            MyPlayerScript.MyDeck.DiscardPile = new List<GameObject>(placeHoldArray);
            MyHistoryScript.AddEvent(HistoryScript.EventType.DeckManipulaton, null, 0);
        };

        Action ConfuzzleAction1 = new Action(ConfuzzleCode1);
        SetUIActionExecutorActions("Confuzzle", new List<Action> { ConfuzzleAction1 });
        // =====================================================================
        // Hoarder's Medallion
        // =====================================================================
        Action.ActionCodeShell HoarderCode1 = delegate (GameObject target)
        {
            int DAMAGE_AMOUNT = 10;
            GetCharScript(target).takeDamage(DAMAGE_AMOUNT);
            MyHistoryScript.AddEvent(HistoryScript.EventType.Damage, target, DAMAGE_AMOUNT);
        };

        Action HoarderAction1 = new Action(HoarderCode1, new List<string> { "MONSTER", "ALLY", "SELF" });
        SetUIActionExecutorActions("Hoarder's Medallion", new List<Action> { HoarderAction1 });

        Action.ActionCodeShell HoarderRefresh = delegate (GameObject card)
        {
            GetCardScript(DatabaseList["HOARDER'S MEDALLION"]).Cost = MyPlayerScript.MyDeck.Hand.Count;
        };
        // =====================================================================
        // Rummage
        // =====================================================================
        Action.ActionCodeShell RummageCode1 = delegate
        {
            int DRAW_AMOUNT = 3;
            int ARMOR_AMOUNT = 3;
            for (int i = 0; i < DRAW_AMOUNT; i++)
            {
                if (GetCardScript(MyPlayerScript.MyDeck.DrawPile[0]).Type == "ABSORB")
                {
                    MyPlayerScript.gainArmor(ARMOR_AMOUNT);
                    MyHistoryScript.AddEvent(HistoryScript.EventType.Armor, MyPlayerScript.gameObject, ARMOR_AMOUNT);
                }
                MyPlayerScript.MyDeck.DrawCard(1);
                MyHistoryScript.AddEvent(HistoryScript.EventType.Draw, null, 1);
            }
        };

        Action RummageAction1 = new Action(RummageCode1);
        SetUIActionExecutorActions("Rummage", new List<Action> { RummageAction1 });
        // =====================================================================
        // Warrior's Sacrifice
        // =====================================================================
        Action.ActionCodeShell WarriorSacrificeCode1 = delegate
        {
            int DAMAGE_AMOUNT = 2;
            int SELF_DAMAGE_OCCURENCE_AMOUNT = 2;
            int count = 0;
            foreach (HistoryScript.Event e in MyHistoryScript.GetCurrentTurn().TurnEvents)
            {
                if (e.Type == HistoryScript.EventType.Damage && e.Target == MyPlayerScript.gameObject && e.Amount > 0)
                {
                    count++;
                }
                if (count >= SELF_DAMAGE_OCCURENCE_AMOUNT)
                {

                    MyPlayerScript.MyGameBoardScript.ExecuteDelegateOnAllMonsters(delegate (GameObject target)
                    {
                        CharacterScript dummyCharScript = target.GetComponent<CharacterScript>();
                        dummyCharScript.takeDamage(DAMAGE_AMOUNT);
                        MyHistoryScript.AddEvent(HistoryScript.EventType.Damage, target, DAMAGE_AMOUNT);
                    });
                }
            }
        };

        Action WarriorSacrificeAction1 = new Action(WarriorSacrificeCode1);
        SetUIActionExecutorActions("Warrior's Sacrifice", WarriorSacrificeAction1);
        // =====================================================================
        // Left Handed Traitor
        // =====================================================================
        Action.ActionCodeShell LeftHandTraitorCode1 = delegate
        {
            int DAMAGE_AMOUNT = 1;
            int SELF_DAMAGE_OCCURENCE_AMOUNT = 3;

            for (int i = 0; i < SELF_DAMAGE_OCCURENCE_AMOUNT; i++)
            {
                MyPlayerScript.takeDamage(DAMAGE_AMOUNT);
                MyHistoryScript.AddEvent(HistoryScript.EventType.Damage, MyPlayerScript.gameObject, DAMAGE_AMOUNT);
            }
        };

        Action LeftHandTraitorAction1 = new Action(LeftHandTraitorCode1);
        SetUIActionExecutorActions("Left Handed Traitor", LeftHandTraitorAction1);
        // =====================================================================
        // Badge of Abundance
        // =====================================================================
        Action.ActionCodeShell BadgeOfAbundanceCode1 = delegate
        {
            MyPlayerScript.EquipRelic(MyRelicDatabase.GetObject("Badge of Abundance"));
            // ~~~~~~~~~~
            // TODO: Need to add HistoryScript Provisions for relic equiping.
            //       HistoryScript in general needs fine tuning to track which
            //       which cards are played and stuff.
            // ~~~~~~~~~~
        };
        Action BadgeOfAbundanceAction1 = new Action(BadgeOfAbundanceCode1);
        SetUIActionExecutorActions("Badge of Abundance", BadgeOfAbundanceAction1);


    }
    */
}
