using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AcceleratorController : MonoBehaviour
{
	public float accelerationFactor = 1.5f;

    public void AccelerateMarble(GameObject marble)
	{
		marble.GetComponent<Rigidbody>().velocity *= accelerationFactor;
        // Check for max/min speed?
	}
}
