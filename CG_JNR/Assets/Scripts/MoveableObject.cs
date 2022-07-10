using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveableObject : MonoBehaviour
{
    [SerializeField] Transform holdArea;
    private GameObject heldObj;
    private Rigidbody heldObjRB;

    [SerializeField] private float pickupRange = 5.0f;
    [SerializeField] private float pickupForce = 150.0f;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if(heldObj == null)
            {
                RaycastHit hit;
                if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward),out hit, pickupRange))
                {
                    Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);
                    Debug.Log("Did Hit");
                    PickupObject(hit.transform.gameObject);
                }
            }
            else
            {
                Debug.Log("Nothing Hit");
                DropObject();
            }
        }
        if (heldObj != null)
        {
            MoveObject();
        }
    }

    void MoveObject()
    {
        if(Vector3.Distance(heldObj.transform.position, holdArea.position) > 0.1f)
        {
            Vector3 moveDirection = (holdArea.position - heldObj.transform.position);
            heldObjRB.AddForce(moveDirection * pickupForce);
        }
    }
    void PickupObject(GameObject pickObj)
    {
        if (pickObj.GetComponent<Rigidbody>())
        {
            heldObjRB = pickObj.GetComponent<Rigidbody>();
            heldObjRB.useGravity = false;
            heldObjRB.drag = 10;
            heldObjRB.constraints = RigidbodyConstraints.FreezeRotation;

            heldObjRB.transform.parent = holdArea;
            heldObj = pickObj;
            Debug.Log("Pickup");
        }
    }

    void DropObject()
    {
        heldObjRB.useGravity = true;
        heldObjRB.drag = 1;
        heldObjRB.constraints = RigidbodyConstraints.FreezeRotation;

        heldObjRB.transform.parent = null;
        heldObj = null;
    }
}
