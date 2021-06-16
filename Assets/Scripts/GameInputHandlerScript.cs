using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GameInputHandlerScript : MonoBehaviour
{
    public DelegateScript handlerDelegateScript;
    List<string> ViableTargetTags;

    public GameObject temporaryMonster; //temporary (duh)

    public event EventHandler<GameObject> TargetSelected;
    public event EventHandler ActionExecuted;
    public event EventHandler NoTargetsFound;

    // Temporary Board for Milestone 1. Fill with monster.
    public GameObject[] Board = new GameObject[1];

    public GameInputHandlerScript()
    {

    }

    // Start is called before the first frame update
    void Start()
    {
        Board[0] = temporaryMonster;
    }

    List<GameObject> GetViableTargetObjects()
    {
        List<GameObject> returnList = new List<GameObject>();

        foreach (string tag in ViableTargetTags)
        {
            foreach (GameObject obj in GameObject.FindGameObjectsWithTag(tag))
            {
                returnList.Add(obj);
            }
        }
        return returnList;
    }

    public void BeginSearchForTarget(List<string> ViableTargetTags)
    {
        this.ViableTargetTags = ViableTargetTags;
        // =====================================================================
        // Subscribe to viable targets
        // =====================================================================
        if (GetViableTargetObjects().Count == 0)
        {
            OnNoTargetsFound();
        }
        else
        {
            foreach (GameObject obj in GetViableTargetObjects())
            {
                try
                {
                    ClickComponent dummyCC = obj.GetComponent<ClickComponent>();
                    dummyCC.MouseUp += HandleTargetSelection;
                }
                catch
                {
                    Debug.Log("ERROR: Attempt to access click component that does not exist. GameHandler, BeginSearchForTarget");
                }
            }
        }
     //   Debug.Log("Beginning search for target!");
    }

    void HandleTargetSelection(object sender, GameObject obj)
    {
        // =====================================================================
        // Unsubscribe from viable targets
        // =====================================================================
        foreach (GameObject objX in GetViableTargetObjects())
        {
            try
            {
                ClickComponent dummyCC = objX.GetComponent<ClickComponent>();
                dummyCC.MouseUp -= HandleTargetSelection;
            }
            catch
            {
                Debug.Log("ERROR: Attempt to access click component that does not exist. GameHandler, BeginSearchForTarget");
            }
        }
        OnTargetSelected(obj);
    }

    // Find way to remove
    public void SendActionToDelegate(Action action)
    {
        handlerDelegateScript.delegateAction(action);
    }

    public void ProcessActionFromDelegate(Action action)
    {
        action.Execute();
        OnActionExecuted(EventArgs.Empty);
    }

    public void SetTarget(GameObject Target)
    {
        Debug.Log("Target Set!");
        OnTargetSelected(Target);
    }

    protected virtual void OnTargetSelected(GameObject Target)
    {
        TargetSelected?.Invoke(this, Target);
    }

    protected virtual void OnActionExecuted(EventArgs e)
    {
        ActionExecuted?.Invoke(this, e);
    }

    protected virtual void OnNoTargetsFound()
    {
        Debug.Log("No Targets!");
        NoTargetsFound?.Invoke(this, EventArgs.Empty);
    }

}
