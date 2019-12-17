using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Thanks to:
 * https://answers.unity.com/questions/682317/preventing-rigidbody-sleeping-altogether.html
 */

public class MarbleController : MonoBehaviour
{
    public Color whiteColor;
    public Color redColor;
    public Color yellowColor;
    public Color blueColor;
    public Color orangeColor;
    public Color purpleColor;
    public Color greenColor;
    public Color brownColor;

    // Possible colors: white, red, blue, yellow, orange, purple, green, brown
    public string initialColor = "white";
    // Useful to store the RYB as well as let user work with string equivalents
    public int[] RYB;

    Rigidbody rigidBody;

    // Start is called before the first frame update
    void Start()
    {
        // Set color
        SetColorFromRYB(ColorStringToRYB(initialColor));

        // Get rigidbody so we can set it to never sleep
        // This is so collisions will keep being detected even after movement has stopped
        //rigidBody = GetComponent<Rigidbody>();
        //rigidBody.sleepThreshold = 0f;
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

    public void AddColor(int[] colorRYB)
    {
        for (int i = 0; i < 3; i++)
        {

            RYB[i] += colorRYB[i];

            if (RYB[i] > 1)
            {
                RYB[i] = 1;
            }
        }

        SetColorFromRYB(RYB);
    }

    public void SubtractColor(int[] colorRYB)
    {
        for (int i = 0; i < 3; i++)
        {
            RYB[i] -= colorRYB[i];

            if (RYB[i] < 0)
            {
                RYB[i] = 0;
            }
        }

        SetColorFromRYB(RYB);
    }

    void SetColorFromRYB(int[] RYBvalues)
    {
        RYB = RYBvalues;
        //Debug.Log("New color");
        //Debug.Log(RYB[0]);
        //Debug.Log(RYB[1]);
        //Debug.Log(RYB[2]);
        gameObject.GetComponent<MeshRenderer>().material.color = RYBToColor(RYBvalues);
    }
}
