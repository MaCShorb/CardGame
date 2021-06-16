using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class RelicDatabase : UIActionExecutorDatabase
{
    public RelicDatabase(PlayerScript playerScript, HistoryScript gameHistoryScript) : base(playerScript, gameHistoryScript, Resources.Load<GameObject>("Relic"))
    {

    }

    protected override void LikenSecondToFirstValuesForGetObject(GameObject first, GameObject second)
    {
        RelicScript FirstRelic = first.GetComponent<RelicScript>();
        RelicScript SecondRelic = second.GetComponent<RelicScript>();
        SecondRelic.SetValues(FirstRelic.Name, FirstRelic.Text, FirstRelic.Durability, FirstRelic.Type);
    }

    protected override void ParseCSV()
    {
        List<Dictionary<string, object>> data = CSVReader.Read("RelicDatabase_CSV");
        for (var i = 0; i < data.Count; i++)
        {
            string Name = data[i]["RelicName"].ToString();
            string Text = data[i]["RelicText"].ToString();
            int Durability = int.Parse(data[i]["RelicDurability"].ToString());
            string Type = data[i]["RelicType"].ToString();

            CreateDatabaseEntry(Name.ToUpper());
            RelicScript dummyRelicScript = DatabaseList[Name.ToUpper()].GetComponent<RelicScript>();
            dummyRelicScript.SetValues(Name, Text, Durability, Type);
        }
    }

    protected override void EstablishDatabaseValues()
    { 
        // Current method of relic action triggering is observer pattern linked
        // to HistoryScript singleton instance. Is there a better way to do this?
        // How do I account for non-relic triggers when it's not your turn?

        // =====================================================================
        // Staff of Projection // Condition: Self-Damage
        // =====================================================================
        Action.ActionCodeShell StaffOfProjectionCode1 = delegate (GameObject target)
        {
            int DAMAGE_AMOUNT = 2;
            GetCharScript(target).takeDamage(DAMAGE_AMOUNT);
            MyHistoryScript.AddEvent(HistoryScript.EventType.Damage, target, DAMAGE_AMOUNT);
        };

        Action StaffOfProjectionAction1 = new Action(StaffOfProjectionCode1, CharacterTagList);
        SetUIActionExecutorActions("Staff of Projection", StaffOfProjectionAction1);

        // =====================================================================
        // Carnivorous Satchel // Condition: Discard
        // =====================================================================
        Action.ActionCodeShell CarnivorousSatchelCode1 = delegate (GameObject target)
        {
            int DAMAGE_AMOUNT = 1;
            GetCharScript(target).takeDamage(DAMAGE_AMOUNT);
            MyHistoryScript.AddEvent(HistoryScript.EventType.Damage, target, DAMAGE_AMOUNT);
        };

        Action CarnivorousSatchelAction1 = new Action(CarnivorousSatchelCode1, CharacterTagList);
        SetUIActionExecutorActions("Carnivorous Satchel", CarnivorousSatchelAction1);

        // =====================================================================
        // Badge of Abundance // Condition: Draw
        // =====================================================================
        Action.ActionCodeShell BadgeOfAbundanceCode1 = delegate (GameObject target)
        {
            int DAMAGE_AMOUNT = 2;
            GetCharScript(target).takeDamage(DAMAGE_AMOUNT);
            MyHistoryScript.AddEvent(HistoryScript.EventType.Damage, target, DAMAGE_AMOUNT);
        };

        Action BadgeOfAbundanceAction1 = new Action(BadgeOfAbundanceCode1, CharacterTagList);
        SetUIActionExecutorActions("Badge of Abundance", BadgeOfAbundanceAction1);

        // =====================================================================
        // Staff of the Void // Condition: Discard
        // =====================================================================
        Action.ActionCodeShell StaffOfTheVoidCode1 = delegate (GameObject target)
        {
            int DAMAGE_AMOUNT = 4;
            GetCharScript(target).takeDamage(DAMAGE_AMOUNT);
            MyHistoryScript.AddEvent(HistoryScript.EventType.Damage, target, DAMAGE_AMOUNT);
	    };

        Action StaffOfTheVoidAction1 = new Action(StaffOfTheVoidCode1, CharacterTagList);
        SetUIActionExecutorActions("Staff of the Void", StaffOfTheVoidAction1);

        // =====================================================================
        // Curious Crystal // Condition: Draw
        // =====================================================================
        Action.ActionCodeShell CuriousCrystalCode1 = delegate (GameObject target)
        {
            int DAMAGE_AMOUNT = 8;
            GetCharScript(target).takeDamage(DAMAGE_AMOUNT);
            MyHistoryScript.AddEvent(HistoryScript.EventType.Damage, target, DAMAGE_AMOUNT);
        };

        Action CuriousCrystalAction1 = new Action(CuriousCrystalCode1, CharacterTagList);
        SetUIActionExecutorActions("Curious Crystal", CuriousCrystalAction1);

    }
}
