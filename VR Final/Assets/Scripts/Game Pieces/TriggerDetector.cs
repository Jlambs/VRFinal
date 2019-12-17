using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This function is used to pass trigger information to parent object(s)

public class TriggerDetector : MonoBehaviour
{
    GameObject parentObject;  // object the collision will be emitted to

    private void Start()
    {
        parentObject = gameObject.transform.parent.gameObject;
    }

    private void OnTriggerEnter(Collider collision)
	{
        if (parentObject.tag == "Condition")
        {
            parentObject.GetComponent<TriggerController>().CheckCondition(collision.gameObject.GetComponent<MarbleController>().RYB);
        }
        else if (parentObject.tag == "Color Setter")
        {
            parentObject.GetComponent<TriggerController>().SetColorFromRYB(collision.gameObject.GetComponent<MarbleController>().RYB);
        }
        else if (parentObject.tag == "Gate")
        {
            parentObject.GetComponent<GateController>().SetNextMarble(collision.gameObject);
        }
        else if (parentObject.tag == "Operator")
        {
            parentObject.GetComponent<AdderController>().OperateOnMarble(collision.gameObject);
        }
        else if (parentObject.tag == "Accelerator")
        {
            parentObject.GetComponent<AcceleratorController>().AccelerateMarble(collision.gameObject);
        }
    }

    // Gate might have marbles stacked against it that it's waiting for
    private void OnTriggerStay(Collider collision)
    {
        if (parentObject.tag == "Gate")
        {
            parentObject.GetComponent<GateController>().SetNextMarble(collision.gameObject);
        }
    }

    // Gate requires knowing when the marble leaves the trigger plane area
    private void OnTriggerExit(Collider collision)
    {
        if (parentObject.tag == "Gate")
        {
            //Debug.Log("Marble finished passing through gate barrier.");
            //Debug.Log(collision.gameObject.name);
            parentObject.GetComponent<GateController>().SetFinishedPassing();
        }
    }
}
