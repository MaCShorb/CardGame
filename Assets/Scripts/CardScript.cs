using System;
using System.Collections.Generic;
using UnityEngine;
public class CardScript : UIActionExecutor
{
    public string Name { get; private set; }
    public string Text { get; private set; }
    public int Cost { get; set; }
    public int Tier { get; private set; }
    public string Type { get; private set; }

    public Action.ActionCodeShell DiscardCode { get; set; }
    public Action.ActionCodeShell DrawCode { get; set; }
    public Action.ActionCodeShell RefreshCode { get; set; }

    public void SetValues(string name, string text, int cost, int tier, string type)
    {
        Name = name;
        Text = text;
        Cost = cost;
        Tier = tier;
        Type = type;
        DiscardCode = delegate { };
        DrawCode = delegate { };
        RefreshCode = delegate { };
        
        SetCardVisuals();
    }

    void SetCardVisuals()
    {
      /*  // =====================================================================
        // Update function for Card Sprite and visual components.
        // =====================================================================

        for (int i = 0; i < gameObject.transform.childCount; i++)
        {
            switch (gameObject.transform.GetChild(i).name)
            {
                case "CardText":
                    TextMeshPro TM_Text = gameObject.transform.GetChild(i).GetComponent<TextMeshPro>();
                    TM_Text.text = Text;
                    break;
                case "CardName":
                    TextMeshPro TM_Name = gameObject.transform.GetChild(i).GetComponent<TextMeshPro>();
                    TM_Name.text = Name;
                    break;
                case "CardCost":
                    TextMeshPro TM_Cost = gameObject.transform.GetChild(i).GetComponent<TextMeshPro>();
                    TM_Cost.text = Cost.ToString();
                    break;
                case "CardTier":
                    TextMeshPro TM_Tier = gameObject.transform.GetChild(i).GetComponent<TextMeshPro>();
                    TM_Tier.text = Tier.ToString();
                    break;
                case "CardArt":
                    Sprite art = Resources.Load<Sprite>(Name + "_Temp_Art");
                    SpriteRenderer CardArtSpriteRenderer = gameObject.transform.GetChild(i).GetComponent<SpriteRenderer>();
                    CardArtSpriteRenderer.sprite = art;
                    break;
                default:
                    Debug.Log("I don't know this child of card!");
                    break;
            }
        } */

        SpriteRenderer CardFrameSpriteRenderer = gameObject.GetComponent<SpriteRenderer>();

        switch (Type)
        {
            case "CLUNK":
                CardFrameSpriteRenderer.color = Color.red;
                break;
            case "CYCLE":
                CardFrameSpriteRenderer.color = Color.cyan;
                break;
            case "ABSORB":
                CardFrameSpriteRenderer.color = Color.green;
                break;
            case "BUFF":
                CardFrameSpriteRenderer.color = Color.yellow;
                break;
            case "START":
                CardFrameSpriteRenderer.color = Color.gray;
                break;
            case "RELIC":
            default:
                CardFrameSpriteRenderer.color = Color.black;
                break;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
       
    }

}
