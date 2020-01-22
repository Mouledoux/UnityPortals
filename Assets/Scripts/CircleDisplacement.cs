using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class CircleDisplacement : MonoBehaviour
{
    [Range(0, 360)]
    public float angleMax;
    
    [Range(-360, 360)]
    public float angleOffset;

    public float radius = 2;
    public float radiusMod = 0f;

    public bool faceInward;

    void Update()
    {
        PositionChildren();
    }

    void PositionChildren()
    {
        for(int i = 0; i < transform.childCount; i++)
        {
            float angle = (((float)i + 1f) / ((float)transform.childCount + 1f)) * angleMax;
            angle += angleOffset;
            angle *= Mathf.Deg2Rad;
            float x = Mathf.Cos(angle) * (radius + (radiusMod * i));
            float y = Mathf.Sin(angle) * (radius + (radiusMod * i));

            transform.GetChild(i).transform.localPosition = new Vector3(x, 0, y);

            if(faceInward)
                transform.GetChild(i).LookAt(transform.position);
        }
    }
}
