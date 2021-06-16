using UnityEngine;
using System.Collections;

public abstract class CharacterScript: MonoBehaviour
{
    public int health { get; private set; }
    public int armor { get; private set;  }
    public int maxHealth { get; set; }

    // =========================================================================
    // TODO: The GameHandler object, when given the get set property, removes
    // it's input from the unity editor. When it comes to optimization, a 
    // solution should be found. I don't want an object's designated 
    // GameHandler to change.
    // =========================================================================

    protected internal GameInputHandlerScript GameInputHandlerScript;

    private void Start()
    {
        health = maxHealth;
        armor = 0;
    }

    public void takeDamage(int amount)
    {
        if (armor >= amount)
        {
            armor -= amount;
        } else
        {
            health -= amount - armor;
            armor = 0;
        }
        Debug.Log("I took " + amount + " damage!");
    }

    public void gainArmor(int amount)
    {
        armor += amount;
    }

    public void restoreHealth(int amount)
    {
        int missingHealth = maxHealth - health;
        if (missingHealth <= amount)
        {
            health = maxHealth;
        }
        else
        {
            health += amount;
        }
        Debug.Log("I restored " + amount + " health!");
    }
}
