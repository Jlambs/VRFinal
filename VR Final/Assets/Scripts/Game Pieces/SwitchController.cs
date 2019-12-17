using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Thanks to
 * https://forum.unity.com/threads/changing-a-material-at-runtime-on-a-meshrenderer.540890/
 */

public class SwitchController : MonoBehaviour
{
    public bool turn1InitiallyEnabled = true;

    public Material turnEnabledMaterial;
	public Material turnDisabledMaterial;

    Collider turn1Collider;
    Collider turn2Collider;

    MeshRenderer turn1Renderer;
    MeshRenderer turn2Renderer;

    bool turn1Enabled;

	void Start()
    {
		// Get game objects of turn 1 and turn 2
		GameObject geometry = gameObject.transform.Find("Geometry").gameObject;
		GameObject turn1 = geometry.transform.Find("Turn 1").gameObject;
		GameObject turn2 = geometry.transform.Find("Turn 2").gameObject;

        // Get mesh colliders of each turn
		turn1Collider = turn1.GetComponent<Collider>();
		turn2Collider = turn2.GetComponent<Collider>();

		// Get mesh renderers of each turn
		turn1Renderer = turn1.GetComponent<MeshRenderer>();
		turn2Renderer = turn2.GetComponent<MeshRenderer>();

        if (turn1InitiallyEnabled)
        {
            turn1Enabled = false;
            EnableTurn1();
        }
        else
        {
            turn1Enabled = true;
            EnableTurn2();
        }

	}

    public void ToggleEnabledTurn()
	{
        if (turn1Enabled)
		{
			EnableTurn2();
		}
		else
		{
			EnableTurn1();
		}
	}

    public void EnableTurn1()
	{
        if (turn1Enabled)
		{
			Debug.Log("Turn 1 already enabled!");
		}
        else
		{
			turn1Collider.enabled = true;
			turn2Collider.enabled = false;

			turn1Renderer.material = turnEnabledMaterial;
			turn2Renderer.material = turnDisabledMaterial;

			turn1Enabled = true;

            //Debug.Log("Turn 1 successfully enabled.");
		}
	}

	public void EnableTurn2()
	{
		if (!turn1Enabled)
		{
			Debug.Log("Turn 2 already enabled!");
		}
		else
		{
			turn2Collider.enabled = true;
			turn1Collider.enabled = false;

			turn2Renderer.material = turnEnabledMaterial;
			turn1Renderer.material = turnDisabledMaterial;

            turn1Enabled = false;

            //Debug.Log("Turn 2 successfully enabled.");
        }
	}
}
