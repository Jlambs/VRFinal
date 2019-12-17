using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    // Define camera's parent (this will be what is actually moving)
    public GameObject cameraParent;

    // Define speed of movement
    public float speedFactor = 2;

    // Start is called before the first frame update
    void Start()
    {

    }

    void Update()
    {

        // When we have a horizontal value
        if (Input.GetAxis("Horizontal") != 0)
        {
            // Move the attached parent based on the right vector of this object multiplied by the horizontal axis value
            cameraParent.transform.position += transform.right * Input.GetAxis("Horizontal") * speedFactor;
        }

        // When we have a horizontal value
        else if (Input.GetAxis("Vertical") != 0)
        {
            // Move the attached parent based on the right vector of this object multiplied by the horizontal axis value
            cameraParent.transform.position += transform.forward * Input.GetAxis("Vertical") * speedFactor;
        }

    }
}
