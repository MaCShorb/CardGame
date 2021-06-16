using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public abstract class UIActionExecutorDatabase
{
    protected Dictionary<string, GameObject> DatabaseList;
    protected GameObject Prefab;
    protected PlayerScript MyPlayerScript;
    protected HistoryScript MyHistoryScript;
    protected List<string> CharacterTagList;

    public UIActionExecutorDatabase(PlayerScript playerScript, HistoryScript gameHistoryScript, GameObject prefab)
    {
        MyPlayerScript = playerScript;
        MyHistoryScript = gameHistoryScript;
        DatabaseList = new Dictionary<string, GameObject>();
        Prefab = prefab;

        CharacterTagList = new List<string> { "MONSTER", "ALLY", "SELF" };

        ParseCSV();
        EstablishDatabaseValues();
    }

    public void CreateDatabaseEntry(string forKey)
    {
        DatabaseList.Add(forKey, GameObject.Instantiate(Prefab));
    }

    public CharacterScript GetCharScript(GameObject obj)
    {
        return obj.GetComponent<CharacterScript>();
    }

    public CardScript GetCardScript(GameObject obj)
    {
        return obj.GetComponent<CardScript>();
    }

    public RelicScript GetRelicScript(GameObject obj)
    {
        return obj.GetComponent<RelicScript>();
    }


    public UIActionExecutor GetUIActionExecutorScript (GameObject obj)
    {
        return obj.GetComponent<UIActionExecutor>();
    }

    public void SetUIActionExecutorActions(string forKey, List<Action> actionList)
    {
        GetUIActionExecutorScript(DatabaseList[forKey.ToUpper()]).ActionList = actionList;
    }

    public void SetUIActionExecutorActions(string forKey, Action action)
    {
        SetUIActionExecutorActions(forKey, new List<Action> { action });
    }

    public GameObject GetObject(string forName)
    {
        GameObject databaseObj = DatabaseList[forName.ToUpper()];
        UIActionExecutor databaseObjUIExec = databaseObj.GetComponent<UIActionExecutor>();
        GameObject returnObj = GameObject.Instantiate(databaseObj);
        UIActionExecutor returnObjUIExec = returnObj.GetComponent<UIActionExecutor>();

        List<Action> returnObjUIActions = new List<Action>();
        foreach (Action action in databaseObjUIExec.ActionList)
        {
            Action newAction = new Action(action);
            returnObjUIActions.Add(newAction);
        }
        returnObjUIExec.ActionList = returnObjUIActions;

        LikenSecondToFirstValuesForGetObject(databaseObj, returnObj);
        return returnObj;
    }


    public GameObject GetDatabaseEntry(string forKey)
    {
        return DatabaseList[forKey.ToUpper()];
    }

    protected abstract void LikenSecondToFirstValuesForGetObject(GameObject first, GameObject second);

    protected abstract void ParseCSV();

    protected abstract void EstablishDatabaseValues();
}

