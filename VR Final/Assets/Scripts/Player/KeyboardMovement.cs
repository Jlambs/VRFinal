using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Thanks to:
 * https://www.youtube.com/watch?v=lYIRm4QEqro
 * https://answers.unity.com/questions/376587/how-to-treat-inputgetaxis-as-inputgetbuttondown.html
 */

public class KeyboardMovement : MonoBehaviour
{
    // Move the joystick with the camera when not VR controlled
    public GameObject joystick;

    // How fast walking/flying is (overriden if snapToGrid flag true)
    public float translationSpeed = 1f;
    // How fast mouse movement rotates camera
    public float horizontalRotationSpeed = 1f;
    public float verticalRotationSpeed = 1f;

    // Freecam allows smooth x/y/z travel based on direction the camera is facing
    // If disabled, movement in y will be constrained (physical floor)
    public bool freeCamEnabled = false;

    /*
    public bool snapToGrid = false;
    // Defined by 1x1x1 piece's unit border size
    // Remember to change this if grid size changes!!
    public float gridSize = 1f;
    bool movingLR = false;  // flags for forcing each button press to move exactly one unit
    bool movingFB = false;  // behaves more predictably than very high gravity/sensitivity on axis
    bool movingUD = false;
    */

    // Use Euler angles with roll=0 to control camera with mouse
    float yaw = 0.0f;
	float pitch = 0.0f;

    float maxPitch = 75f;
    float minPitch = -75f;

    void Start()
    {
        // Fix the joystick's position to be in front of the camera
        joystick.transform.position = transform.position + new Vector3(1, -1, 3);
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(Input.GetAxis("Horizontal"));
        //Debug.Log(Input.GetAxis("Vertical"));

        // Fix the joystick's position to be in front of the camera
        joystick.transform.position = transform.position + new Vector3(1, -1, 3);

        // Move smoothly through worldpsace
        if (true)//!snapToGrid)
        {
            // Move X
            if (Input.GetAxis("Horizontal") != 0)
            {
                transform.position += transform.right * Input.GetAxis("Horizontal") * translationSpeed;
            }

            // Move Z
            if (Input.GetAxis("Vertical") != 0)
            {
                if (freeCamEnabled)
                {
                    transform.position += transform.forward * Input.GetAxis("Vertical") * translationSpeed;
                }
                else
                {
                    transform.position += Vector3.forward * Input.GetAxis("Vertical") * translationSpeed;
                }
            }

            // Move Y
            if (Input.GetAxis("Vertical3D") != 0)
            {
                if (freeCamEnabled)
                {
                    transform.position += transform.up * Input.GetAxis("Vertical3D") * translationSpeed;
                }
                else
                {
                    transform.position += Vector3.up * Input.GetAxis("Vertical3D") * translationSpeed;
                }
            }

        }
        /*
        // Snap movement to grid
        else
        {
            // Move X
            if (Input.GetAxisRaw("Horizontal") != 0)
            {
                if (!movingFB && !movingLR && !movingUD)
                {
                    transform.position += Vector3.right * Input.GetAxisRaw("Horizontal") * gridSize;
                    movingLR = true;
                }
            }

            // Move Z
            else if (Input.GetAxisRaw("Vertical") != 0)
            {
                if (!movingFB && !movingLR && !movingUD)
                {
                    transform.position += Vector3.forward * Input.GetAxisRaw("Vertical") * gridSize;
                    movingFB = true;
                }
            }

            // Move Y
            else if (Input.GetAxisRaw("Vertical3D") != 0)
            {
                if (!movingFB && !movingLR && !movingUD)
                {
                    transform.position += Vector3.up * Input.GetAxisRaw("Vertical3D") * gridSize;
                    movingUD = true;
                }
            }

            // Reset movement flags on key release
            if (Input.GetAxisRaw("Horizontal") == 0)
            {
                movingLR = false;
            }
            if (Input.GetAxisRaw("Vertical") == 0)
            {
                movingFB = false;
            }
            if (Input.GetAxisRaw("Vertical3D") == 0)
            {
                movingUD = false;
            }
        }
        */

        // Rotate camera based on mouse movement
        yaw += Input.GetAxis("Mouse X") * horizontalRotationSpeed;
		pitch -= Input.GetAxis("Mouse Y") * verticalRotationSpeed;

        // Clamp pitch values
        if (pitch > maxPitch)
        {
            pitch = maxPitch;
        }
        else if (pitch < minPitch)
        {
            pitch = minPitch;
        }

		transform.eulerAngles = new Vector3(pitch, yaw, 0.0f);

	}
}
