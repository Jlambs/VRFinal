using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrierController : MonoBehaviour
{
    public void OnCollisionEnter(Collision collision)
	{
        if (collision.gameObject.tag == "Passing Gate")
		{
            //Debug.Log("Entry collision ignored at gate.");
			Physics.IgnoreCollision(collision.collider, gameObject.GetComponent<Collider>());
		}
	}

    public void OnCollisionStay(Collision collision)
    {
        Debug.Log(collision.gameObject.tag);
        if (collision.gameObject.tag == "Passing Gate")
        {
            //Debug.Log("Collision ignored at gate.");
            Physics.IgnoreCollision(collision.collider, gameObject.GetComponent<Collider>());
        }
    }
}
