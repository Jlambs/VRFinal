using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Thanks to:
 * https://answers.unity.com/questions/448300/get-variable-from-other-gameobjects-script-1.html
 */

public class TriggerController : MonoBehaviour
{
    // 0: ==
    // 1: >=
    // 2: color setter
    public int triggerType;

    // Object that displays color within the trigger (ring, square, circle)
    public GameObject coloredObject;

    public Color whiteColor;
    public Color redColor;
    public Color yellowColor;
    public Color blueColor;
    public Color orangeColor;
    public Color purpleColor;
    public Color greenColor;
    public Color brownColor;

    // For the purposes of the demonstration connections will be defined manually
    // In the future, AddConnection(GameObject objectToConnect) should be used.
    public GameObject connection1;
    public GameObject connection2;
    public GameObject connection3;
    public GameObject connection4;
    public GameObject connection5;
    public GameObject connection6;
    public GameObject connection7;
    public GameObject connection8;
    //int maxConnections = 8;
    List<GameObject> connectedObjects = new List<GameObject>();


    // Possible colors: white, red, blue, yellow, orange, purple, green, brown
    public string initialColor = "white";
    // Useful to store the RYB as well as let user work with string equivalents
    public int[] RYB;

    public GameObject connectionArrowPrefab;

    public string connectionArrowColor;

    GameObject newArrow;  // for creating new connections

    // Start is called before the first frame update
    void Start()
    {
        // Set own color
        SetColorFromRYB(ColorStringToRYB(initialColor));

        // Build connectionList
        InitializeConnectionList();

        // Create arrows between connected items
        InitializeConnectionArrows();
    }

    string RYBToColorString(int[] RYBvalues)
    {
        // Many ways to do this, all of them suck
        if (RYBvalues[2] == 0)
        {
            if (RYBvalues[1] == 0)
            {
                if (RYBvalues[0] == 0)
                {
                    // { 0, 0, 0 }
                    return "white";
                }
                else
                {
                    // { 1, 0, 0 }
                    return "red";
                }
            }
            else
            {
                if (RYBvalues[0] == 0)
                {
                    // { 0, 1, 0 }
                    return "yellow";
                }
                else
                {
                    // { 1, 1, 0 }
                    return "orange";
                }
            }
        }
        else
        {
            if (RYBvalues[1] == 0)
            {
                if (RYBvalues[0] == 0)
                {
                    // { 0, 0, 1 }
                    return "blue";
                }
                else
                {
                    // { 1, 0, 1 }
                    return "purple";
                }
            }
            else
            {
                if (RYBvalues[0] == 0)
                {
                    // { 0, 1, 1 }
                    return "green";
                }
                else
                {
                    // { 1, 1, 1 }
                    return "brown";
                }
            }
        }
    }

    int[] ColorStringToRYB(string color)
    {
        if (color == "white")
        {
            return new int[] { 0, 0, 0 };
        }
        else if (color == "red")
        {
            return new int[] { 1, 0, 0 };
        }
        else if (color == "yellow")
        {
            return new int[] { 0, 1, 0 };
        }
        else if (color == "blue")
        {
            return new int[] { 0, 0, 1 };
        }
        else if (color == "orange")
        {
            return new int[] { 1, 1, 0 };
        }
        else if (color == "purple")
        {
            return new int[] { 1, 0, 1 };
        }
        else if (color == "green")
        {
            return new int[] { 0, 1, 1 };
        }
        else
        {
            return new int[] { 1, 1, 1 };
        }
    }

    Color RYBToColor(int[] RYBvalues)
    {
        return ColorStringToColor(RYBToColorString(RYBvalues));
    }

    Color ColorStringToColor(string colorString)
    {
        if (colorString == "white")
        {
            return whiteColor;
        }
        else if (colorString == "red")
        {
            return redColor;
        }
        else if (colorString == "yellow")
        {
            return yellowColor;
        }
        else if (colorString == "blue")
        {
            return blueColor;
        }
        else if (colorString == "orange")
        {
            return orangeColor;
        }
        else if (colorString == "purple")
        {
            return purpleColor;
        }
        else if (colorString == "green")
        {
            return greenColor;
        }
        else
        {
            return brownColor;
        }
    }

    public void CheckCondition(int[] marbleRYB)
    {
        bool conditionPassed = true;


        //Debug.Log(RYB[0]);
        //Debug.Log(RYB[1]);
        //Debug.Log(RYB[2]);


        //Debug.Log(marbleRYB[0]);
        //Debug.Log(marbleRYB[1]);
        //Debug.Log(marbleRYB[2]);

        if (triggerType == 0)
        {
            // Is the marble *exactly* the conditions's color?
            // ==
            for (int i = 0; i < 3; i++)
            {
                if (marbleRYB[i] != RYB[i])
                {
                    conditionPassed = false;
                }
            }
        }
        else
        {
            // Does the marble *contain* the condition's color?
            // >=
            for (int i = 0; i < 3; i++)
            {
                if (marbleRYB[i] < RYB[i])
                {
                    //Debug.Log("Condition failed on iter");
                    //Debug.Log(i);
                    conditionPassed = false;
                }
            }
        }

        if (conditionPassed)
        {
            SendTrigger();
        }
    }

    void AddConnection(GameObject toConnect)
    {
        connectedObjects.Add(toConnect);
    }

    void SendTrigger()
    {
        // Color setter
        if (triggerType == 2)
        {
            foreach (GameObject connection in connectedObjects)
            {
                if (connection.tag == "Generator")
                {
                    connection.gameObject.GetComponent<GeneratorController>().SetColorFromRYB(RYB);
                }
                else if (connection.tag == "Condition")
                {
                    connection.gameObject.GetComponent<TriggerController>().SetColorFromRYB(RYB);
                }
                else if (connection.tag == "Color Setter")
                {
                    connection.gameObject.GetComponent<TriggerController>().SetColorFromRYB(RYB);
                }
            }
        }

        // Conditions
        else
        {
            foreach (GameObject connection in connectedObjects)
            {
                if (connection.tag == "Switch")
                {
                    connection.gameObject.GetComponent<SwitchController>().ToggleEnabledTurn();
                }
                else if (connection.tag == "Generator")
                {
                    connection.gameObject.GetComponent<GeneratorController>().CreateMarble();
                }
                else if (connection.tag == "Gate")
                {
                    connection.gameObject.GetComponent<GateController>().LetOneMarblePass();
                }
                else if (connection.tag == "Color Setter")
                {
                    connection.gameObject.GetComponent<TriggerController>().SendTrigger();
                }
            }
        }
    }

    public void SetColorFromRYB(int[] RYBvalues)
    {
        RYB = RYBvalues;
        coloredObject.GetComponent<MeshRenderer>().material.color = RYBToColor(RYB);
    }

    void InitializeConnectionList()
    {
        // Add non-null connections to connectedObjects
        if (connection1 != null)
        {
            AddConnection(connection1);
        }
        if (connection2 != null)
        {
            AddConnection(connection2);
        }
        if (connection3 != null)
        {
            AddConnection(connection3);
        }
        if (connection4 != null)
        {
            AddConnection(connection4);
        }
        if (connection5 != null)
        {
            AddConnection(connection5);
        }
        if (connection6 != null)
        {
            AddConnection(connection6);
        }
        if (connection7 != null)
        {
            AddConnection(connection7);
        }
        if (connection8 != null)
        {
            AddConnection(connection8);
        }
    }

    void InitializeConnectionArrows()
    {
        for (int i = 0; i < connectedObjects.Count; i++)
        {
            // Create new connection from prefab
            newArrow = Instantiate(connectionArrowPrefab);//, transform);

            // Set color of arrow
            // Future work: make this settable in-game. Probably associate triggers w/ colors, not indiv arrows?
            newArrow.GetComponent<ConnectionController>().SetColor(ColorStringToColor(connectionArrowColor));

            // Set origin to current object
            newArrow.GetComponent<ConnectionController>().origin = gameObject;

            // Set destination to object from connectedObjects
            newArrow.GetComponent<ConnectionController>().destination = connectedObjects[i];

            // Initialize arrow's size and orientation
            newArrow.GetComponent<ConnectionController>().InitializeArrow();

            // Make arrow invisible by default
            newArrow.SetActive(false);
        }
    }
}
