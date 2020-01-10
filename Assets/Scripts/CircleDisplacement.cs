using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleDisplacement : MonoBehaviour
{
    [Range(0, 360)]
    public float angleMax;
    
    [Range(-360, 360)]
    public float angleOffset;

    public float radius = 2;
    void Update()
    {
        for(int i = 0; i < transform.childCount; i++)
        {
            float angle = ((float)i / (float)transform.childCount) * angleMax;
            angle += angleOffset;
            angle *= Mathf.Deg2Rad;
            float x = Mathf.Cos(angle) * radius;
            float y = Mathf.Sin(angle) * radius;

            transform.GetChild(i).transform.localPosition = new Vector3(x, 0, y);
        }
    }
}
