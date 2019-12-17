using UnityEngine;

public class JoystickController : MonoBehaviour
{
    public GameObject exampleTrack;

    void Update()
    {
        /*
        foreach (KeyCode vKey in System.Enum.GetValues(typeof(KeyCode)))
        {
            if (Input.GetKey(vKey))
            {
                Debug.Log(vKey);
            }
        }
        */
        if (Input.GetKeyDown("Fire1"))
        {
            //exampleTrack.transform.position = new Vector3(exampleTrack.transform.position.x, exampleTrack.transform.position.y, exampleTrack.transform.position.z + Input.GetAxis("X axis"));
        }
    }
}