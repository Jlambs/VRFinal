using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogMonitor : MonoBehaviour
{
    // Will be used to store a reference to the text mesh component
    private TextMesh textMesh;

    // Use this for initialization
    private void Start()
    {
        // Get the instance of the TextMesh component on this game object and store it
        textMesh = gameObject.GetComponentInChildren<TextMesh>();
    }

    // Called by Unity when this object is enabled in the scene
    private void OnEnable()
    {
        // Attach the LogMessage function as a callback for the logMessageReceived event in Unity
        Application.logMessageReceived += LogMessage;
    }

    // Called by Unity when this object is disabled in the scene
    void OnDisable()
    {
        Application.logMessageReceived -= LogMessage;
    }

    public void LogMessage(string message, string stackTrace, LogType type)
    {
        // Set the text 
        textMesh.text = message;
    }

    private void Update()
    {
        /*
        // Horizontal joystick movement
        if (Mathf.Abs(Input.GetAxis("Horizontal")) > 0)
            Debug.Log("Horizontal: " + Input.GetAxis("Horizontal"));

        // Horizontal joystick movement
        else if (Mathf.Abs(Input.GetAxis("Vertical")) > 0)
            Debug.Log("Vertical: " + Input.GetAxis("Vertical"));

        // Trigger press
        else if (Input.GetButtonDown("Fire1"))
            Debug.Log("Trigger pressed.");

        // Trigger release
        else if (Input.GetButtonDown("Fire2"))
            Debug.Log("Trigger released.");

        // X Button on controller (y on keyboard)
        else if (Input.GetButtonDown("Grab"))
            Debug.Log("X Button (KeyCode y) pressed.");

        // Trigger release
        else if (Input.GetButtonUp("Grab"))
            Debug.Log("X Button (KeyCode y) released.");

        // All other keys
        // Thanks to https://answers.unity.com/questions/17455/what-key-was-pressed.html
        else
        {
            foreach (KeyCode keycode in Enum.GetValues(typeof(KeyCode)))
            {
                if (Input.GetKeyDown(keycode))
                    Debug.Log("KeyCode " + keycode + " pressed.");
                else if (Input.GetKeyUp(keycode))
                    Debug.Log("KeyCode " + keycode + " released.");
            }
        }
        */

        // All keycodes
        foreach (KeyCode keycode in Enum.GetValues(typeof(KeyCode)))
        {
            if (Input.GetKeyDown(keycode))
                Debug.Log("KeyCode " + keycode + " pressed.");
            else if (Input.GetKeyUp(keycode))
                Debug.Log("KeyCode " + keycode + " released.");
        }

        // Mouse movement
        if (Input.GetAxis("Mouse X") != 0)
        {
            //Code for action on mouse moving left
            Debug.Log("Mouse X");
        }
        if (Input.GetAxis("Mouse Y") != 0)
        {
            //Code for action on mouse moving right
            Debug.Log("Mouse Y");
        }
    }
}
