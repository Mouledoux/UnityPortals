using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorChanging : MonoBehaviour
{
    Mouledoux.Components.Mediator.Subscriptions subscriptions =
        new Mouledoux.Components.Mediator.Subscriptions();


    Renderer myRenderer;
    void Start()
    {
        myRenderer = GetComponent<Renderer>();

        subscriptions.Subscribe("Red", (object[] x) => transform.localScale = Vector3.zero);
        subscriptions.Subscribe("green", (object[] x) => transform.localScale = Vector3.one);
        subscriptions.Subscribe("blue", (object[] x) => transform.localScale = Vector3.one * 2);
        
    }
}
