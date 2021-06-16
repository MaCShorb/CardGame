using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class RelicScript : UIActionExecutor
{
    public string Name { get; set; }
    public string Text { get; set; }
    public int Durability { get; set; }
    public string Type { get; set; }

    public event EventHandler<GameObject> RelicConditionMet;

    public RelicScript(PlayerScript MyPlayerScript)
    {
        
    }

    public void OnRelicConditionMet(object sender, EventArgs e) // Notification that relic needs to ProcessAction();
    {
        Debug.Log("Hello!");
        RelicConditionMet.Invoke(this, gameObject);
    }

    public void RelicEquipAnimation()
    {

    }

    public void SetValues(string Name, string Text, int Durability, string Type)
    {
        this.Name = Name;
        this.Text = Text;
        this.Durability = Durability;
        this.Type = Type;
    }

    // Relics either apply states, or have a condition action pair.
}
