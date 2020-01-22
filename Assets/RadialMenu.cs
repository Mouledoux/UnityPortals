using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadialMenu : MonoBehaviour
{
    public UnityEngine.UI.Image selectionImage;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 mouseDirection = Input.mousePosition - selectionImage.transform.position;
        float angleBetween = selectionImage.fillAmount * 180f;
        
        selectionImage.transform.up = mouseDirection.normalized;
        selectionImage.transform.Rotate(Vector3.forward, angleBetween);
    }
}
