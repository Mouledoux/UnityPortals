using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalHandler : MonoBehaviour
{
    public PortalHandler pairPortal;
    private List<Collider> portalObjects = new List<Collider>();

     private void OnTriggerStay(Collider other)
    {
        if(other.GetComponent<Rigidbody>() == null) return;
        else if(portalObjects.Contains(other) || pairPortal.portalObjects.Contains(other)) return;
        else if(transform.InverseTransformPoint(other.transform.position).z < 0) return;

        portalObjects.Add(other);
        pairPortal.portalObjects.Add(other);

        GameObject copy = Instantiate(other.gameObject);
        copy.transform.parent = pairPortal.transform;

        //copy.GetComponent<Rigidbody>().isKinematic = true;
        //portalObjects.Add(copy.GetComponent<Collider>());

        other.gameObject.layer = 8;
        copy.layer = 8;
        
        DisableAllComponents(copy);
        StartCoroutine(MatchPortalObject(other, copy));
    }

    private void OnTriggerExit(Collider other)
    {
        if(portalObjects.Contains(other)) portalObjects.Remove(other);
    }


    IEnumerator MatchPortalObject(Collider original, GameObject copy)
    {
        Rigidbody rb = original.GetComponent<Rigidbody>();

        Vector3 relativePosition = Vector3.zero;
        Quaternion relativeRotation = Quaternion.identity;

        while(portalObjects.Contains(original))
        {
            relativePosition = transform.InverseTransformPoint(original.transform.position);
            relativePosition = Vector3.Scale(relativePosition, new Vector3(-1, 1, -1));
            copy.transform.position = pairPortal.transform.TransformPoint(relativePosition);

            relativeRotation = Quaternion.Inverse(transform.rotation) * original.transform.rotation;
            copy.transform.localRotation = relativeRotation;
            copy.transform.RotateAround(copy.transform.position, pairPortal.transform.up, 180f);
            //relativeRotation = Quaternion.Inverse(pairPortal.transform.rotation) * relativeRotation;
            // relativeRotation = transform.rotation * relativeRotation;
            // copy.transform.rotation = relativeRotation;

            yield return null;
        }

        if(relativePosition.z > 0)
        {
            //rb.velocity = pairPortal.transform.TransformDirection(transform.TransformDirection(rb.velocity));
            Vector3 vel = rb.transform.InverseTransformDirection(rb.velocity);
            original.transform.position = copy.transform.position;
            original.transform.rotation = copy.transform.rotation;
            rb.velocity = rb.transform.TransformDirection(vel);
            yield return new WaitForEndOfFrame();
        }
        
        pairPortal.portalObjects.Remove(original);
        original.gameObject.layer = 0;

        portalObjects.Remove(copy.GetComponent<Collider>());
        Destroy(copy);
    }

    void DisableAllComponents(GameObject go)
    {
        for(int i = 0; i < go.transform.childCount; i++)
        {
            DisableAllComponents(go.transform.GetChild(i).gameObject);
        }
        
        foreach(Component c in go.GetComponents(typeof(Component)))
        {
            System.Type type = c.GetType();
            if(//type != typeof(Collider) &&
                type != typeof(Transform) &&
                //type != typeof(Rigidbody) &&
                type != typeof(MeshFilter) &&
                type != typeof(MeshRenderer))
            Destroy(c);
        }
    }
}
