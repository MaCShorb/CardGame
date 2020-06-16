using UnityEngine;
using System.Collections;

public class Character: MonoBehaviour
{
    internal int health { get; private set; }
    internal int armor { get; private set;  }
    internal int maxHealth { get; private set; }

    // =========================================================================
    // TODO: The GameHandler object, when given the get set property, removes
    // it's input from the unity editor. When it comes to optimization, a 
    // solution should be found. I don't want an object's designated 
    // GameHandler to change.
    // =========================================================================

    public GameObject GameHandler;

    public Character(int maxHealth)
    {
        this.maxHealth = maxHealth;
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
        Debug.Log("I took damage!");
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

    }
}
