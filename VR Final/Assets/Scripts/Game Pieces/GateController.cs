using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GateController : MonoBehaviour
{
    //public bool isInput = false;
    //public int inputLength;

    public Material openMaterial;
    public Material closedMaterial;

    public GameObject barrierFacade;
    public GameObject barrier;

    public float minimumTimeBetweenMarbles = 1.5f;
    float timeSinceLastMarble = 0f;

    public int initialMarblesAllowed = 0;

    bool isOpen = false;

    bool waitingForMarble = false;
    GameObject nextMarbleToPass = null;
    int marblesToPass = 0;

    // Start is called before the first frame update
    void Start()
    {
        // Let marbles through initially, usually to start input
        for (int i = 0; i < initialMarblesAllowed; i++)
        {
            LetOneMarblePass();
        }
    }

    private void Update()
    {

        if (isOpen || (timeSinceLastMarble > 0 && timeSinceLastMarble < minimumTimeBetweenMarbles))
        {
            timeSinceLastMarble += Time.deltaTime;
            //Debug.Log(timeSinceLastMarble);
        }
        else if (marblesToPass > 0 && !waitingForMarble && !isOpen)
        {
            //Debug.Log("Trying to pass marble with Update.");
            TryToPassMarble();
        }
        
    }

    public void LetOneMarblePass()
    {
        //Debug.Log("Gate triggered, marble requested to pass.");
        marblesToPass += 1;
        //Debug.Log(marblesToPass);
        TryToPassMarble();
    }

    void TryToPassMarble()
    {
        if (nextMarbleToPass is null)
        {
            //Debug.Log("No marbles ready to pass yet.");
            waitingForMarble = true;
        }
        else if (isOpen)
        {
            //Debug.Log("Gate already passing marble.");
        }
        else
        {
            //Debug.Log("Marble found and gate closed, passing now...");
            marblesToPass -= 1;
            //Debug.Log(marblesToPass);
            AllowNextMarbleToPass();
        }
    }

    void Open()
    {
        if (isOpen)
        {
            Debug.Log("Gate already opened!");
        }
        else
        {
            barrierFacade.GetComponent<MeshRenderer>().material = openMaterial;
            isOpen = true;
        }
        timeSinceLastMarble = 0f;
    }

    void Close()
    {
        if (!isOpen)
        {
            Debug.Log("Gate already closed!");
        }
        else
        {
            barrierFacade.GetComponent<MeshRenderer>().material = closedMaterial;
            isOpen = false;
        }
    }

    public void SetNextMarble(GameObject marble)
    { 
        nextMarbleToPass = marble;
        waitingForMarble = false;

        //Debug.Log("Marble detected at gate barrier.");
        //Debug.Log(marble.name);
        //Debug.Log("Set to pass when gate opens.");
    }

    void AllowNextMarbleToPass()
    {
        //Debug.Log("Marble beginning to pass.");
        Open();
        nextMarbleToPass.tag = "Passing Gate";
        Rigidbody rb = nextMarbleToPass.GetComponent<Rigidbody>();
        if (rb.IsSleeping())
        {
            rb.WakeUp();

        }
    }

    public void SetFinishedPassing()
    {
        //Debug.Log("Marble finished passing.");
        Close();
        nextMarbleToPass.tag = "Untagged";
        nextMarbleToPass = null;
    }
}
