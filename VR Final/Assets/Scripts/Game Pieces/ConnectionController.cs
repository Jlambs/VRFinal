using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Thanks to:
 * https://docs.unity3d.com/ScriptReference/Vector3.Distance.html
 * https://docs.unity3d.com/ScriptReference/Transform.LookAt.html
 */

public class ConnectionController : MonoBehaviour
{
    public GameObject body;
    public GameObject head;

    public float thickness = 0.2f;

    public GameObject origin;
    public GameObject destination;

    public bool skipInitialization = true;

    float length;

    void Start()
    {
        if (!skipInitialization)
        {
            InitializeArrow();
        }
    }

    public void InitializeArrow()
    {
        SetHeadSize();
        SetBodySize();
        OrientArrow();
    }

    void SetHeadSize()
    {
        // Set width/height of head
        head.transform.localScale = new Vector3(2 * thickness, 2 * thickness, 2 * thickness);
    }

    void SetBodySize()
    {
        // Set width/height of body
        length = Vector3.Distance(origin.transform.position, destination.transform.position);
        body.transform.localScale = new Vector3(thickness, length, thickness);
    }

    void OrientArrow()
    {
        // Reposition head+body so base of arrow is at origin of the connection
        body.transform.localPosition = new Vector3(0, 0, length);
        head.transform.localPosition = new Vector3(0, 0, length);

        // Place origin of arrow at origin object
        transform.position = origin.transform.position;

        // Orient arrow origin->destination
        transform.LookAt(destination.transform.position);
    }

    public void SetColor(Color color)
    {
        head.GetComponent<MeshRenderer>().material.color = color;
        body.GetComponent<MeshRenderer>().material.color = color;
    }

}
