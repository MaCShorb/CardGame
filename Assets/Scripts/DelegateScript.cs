using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DelegateScript : MonoBehaviour
{
    public GameObject player1_G_InputHandler;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void delegateAction(Action action)
    {
        // TODO: Code sent to gamehandlers of each player, differentiated between
        // each according to their screen, animation requirements, etc.
        G_InputHandlerScript dummyG_IHS = player1_G_InputHandler.GetComponent<G_InputHandlerScript>();
        dummyG_IHS.processActionFromDelegate(action);
    }

}
