using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadialMenu : MonoBehaviour
{
    public List<string> commands;
    public UnityEngine.UI.Image selectionImage;

    private int selectionIndex;
    private Vector2 mouseDirection => (Input.mousePosition - selectionImage.transform.position);


    // Update is called once per frame
    void Update()
    {
        GetClosestSegment();
        UpdateSegmentPosition();
        CheckCommandPrcess();
    }

    private void UpdateSegmentPosition()
    {
        float speed = Time.deltaTime * 8f;
        Vector2 upTarget = GetSegmentPosition(selectionIndex).normalized;
        
        selectionImage.transform.up = Vector3.Slerp(selectionImage.transform.up, upTarget, speed);
        selectionImage.transform.Rotate(Vector3.forward, GetAngleBetween() * speed);
    }

    private void CheckCommandPrcess()
    {
        if(Input.GetMouseButtonDown(0))
        {
            ProcessCommand(selectionIndex);
        }
    }

    private void ProcessCommand(int commandIndex)
    {
        Mouledoux.Components.Mediator.NotifySubscribers(commands[commandIndex]);
    }

    private float GetAngleBetween()
    {
        selectionImage.fillAmount = 1f/ (float)commands.Count;
        return selectionImage.fillAmount * 180f;
    }

    private Vector2 GetPointOnCircle(float angle)
    {
        angle %= 360f;
        angle *= Mathf.Deg2Rad;
        return new Vector2(Mathf.Sin(angle), Mathf.Cos(angle)).normalized;
    }

    private Vector2 GetSegmentPosition(int segment)
    {
        return GetPointOnCircle(segment * selectionImage.fillAmount * 360f).normalized;
    }

    private int GetClosestSegment()
    {
        float minDist = float.MaxValue;
        int closestElement = 0;

        for(int i = 0; i < commands.Count; i++)
        {
            Vector2 thisSegment = GetSegmentPosition(i);
            float thisDist = Vector2.Distance(mouseDirection.normalized, thisSegment.normalized);

            if(thisDist < minDist)
            {
                minDist = thisDist;
                closestElement = i;
            }
        }

        selectionIndex = closestElement;
        return closestElement;
    }
}
