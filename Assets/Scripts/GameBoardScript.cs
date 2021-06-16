using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameBoardScript : MonoBehaviour
{
    public PlayerScript Player1Script;
    public PlayerScript Player2Script;
    public PlayerScript Player3Script;
    public PlayerScript Player4Script;

    public int CurrentCharacterTurn { get; set; }

    public List<PlayerScript> PlayerBoard { get; set; }
    public List<GameObject> MonsterBoard { get; set; }

    private void Awake()
    {
        PlayerBoard = new List<PlayerScript>();
        PlayerBoard.Add(Player1Script);
        PlayerBoard.Add(Player2Script);
        PlayerBoard.Add(Player3Script);
        PlayerBoard.Add(Player4Script);

        MonsterBoard = new List<GameObject>();
    }

    public delegate void Del(GameObject target);

    public void ExecuteDelegateOnAllPlayers(Del Delegate)
    {
        foreach (PlayerScript player in PlayerBoard)
        {
            Delegate.Invoke(player.gameObject);
        }
    }

    public void ExecuteDelegateOnAllMonsters(Del Delegate)
    {
        foreach (GameObject monster in MonsterBoard)
        {
            Delegate.Invoke(monster);
        }
    }

    public MonsterScript GetRandomMonster()
    {
        int RandomIndex = UnityEngine.Random.Range(1, MonsterBoard.Count);
        return MonsterBoard[RandomIndex].GetComponent<MonsterScript>();
    }
}
