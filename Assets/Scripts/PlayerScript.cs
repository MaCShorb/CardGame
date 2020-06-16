using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : Character
{
    public int mana { get; set; }

    DeckScript Deck;

    public PlayerScript() : base(10)
    {
        mana = 3;
        Deck = new DeckScript();
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

}
