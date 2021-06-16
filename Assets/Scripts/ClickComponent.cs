using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[RequireComponent(typeof(BoxCollider2D))]
public class ClickComponent: MonoBehaviour
{
    public event EventHandler<GameObject> MouseUp;
    public event EventHandler<GameObject> MouseEnter;
    public event EventHandler<GameObject> MouseExit;
    public ClickComponent()
    {
       
    }
    void Start()
    {
      
    }
    void OnMouseUp()
    {
        MouseUp?.Invoke(this, gameObject);
    }
    void OnMouseEnter()
    {
        MouseEnter.Invoke(this, gameObject);
    }
    void OnMouseExit()
    {
        MouseExit.Invoke(this, gameObject);
    }
}
