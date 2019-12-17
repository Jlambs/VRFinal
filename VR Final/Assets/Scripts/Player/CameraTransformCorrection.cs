using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTransformCorrection : MonoBehaviour
{
    public GameObject mainCamera;
    public GameObject cameraProxy;
    //public GameObject playerObject;
    public float lerpSpeed = 0.02f; // speed at which to interpolate between the proxy and main camera

    public bool useKeyboardMovement = false;

    // Start is called before the first frame update
    void Start()
    {
        // Set camera proxy's position equal to camera parent's
        cameraProxy.transform.position = transform.position;
        cameraProxy.transform.rotation = transform.rotation;

        if (useKeyboardMovement)
        {
			cameraProxy.GetComponent<KeyboardMovement>().enabled = true;
			cameraProxy.GetComponent<QTMObject>().enabled = false;
        }
        else
		{
			//cameraProxy.GetComponent<KeyboardMovement>().enabled = false;
			cameraProxy.GetComponent<QTMObject>().enabled = true;
		}
    }

    // Update is called once per frame
    void Update()
    {
        if (useKeyboardMovement)
        {
            // Skip linear interpolation if using mouse/keyboard
            transform.rotation = cameraProxy.transform.rotation;

            // Copy the z position to the camera object to set head at correct height
            //transform.position = new Vector3(transform.position.x, transform.position.y, cameraProxy.transform.position.z);
            // Copy the x/y position to the player object to set position of all player-related objects
            //playerObject.transform.position = new Vector3(cameraProxy.transform.position.x, cameraProxy.transform.position.y, playerObject.transform.position.z);

            // Rotate player object to new Y rotation (body can rotate, head can only look up/down)
            //playerObject.transform.rotation = new Vector3();
        }
        else
        {
            // Calculate the difference in rotation between the proxy rotation and current main camera rotation
            Quaternion correction = cameraProxy.transform.rotation * Quaternion.Inverse(mainCamera.transform.localRotation);
            // Set the rotation value of this camera to be a portion of the offset, over time this will correct slowly
            transform.rotation = Quaternion.Lerp(transform.rotation, correction, lerpSpeed);

            // Set player object's position
            //playerObject.transform.position = new Vector3(cameraProxy.transform.position.x, cameraProxy.transform.position.y, playerObject.transform.position.z);
            // Set head at correct height within playerobject
            //transform.position = new Vector3(transform.position.x, transform.position.y, cameraProxy.transform.position.z);
        }

        // Set position to equal camera proxy
        transform.position = cameraProxy.transform.position;

    }
}
