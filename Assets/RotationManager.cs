using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class RotationManager : MonoBehaviour
{
    // Start is called before the first frame update
    public static event Action<bool> OnChangeGravityA = delegate { };
    public static event Action<bool> OnChangeGravityB = delegate { };
    public Transform tranform;
    public GameObject player;

    void Start()
    {
        tranform = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        Flip();
    }

    void Flip()
    {
        if (Input.GetKeyDown(KeyCode.C) && !GameController.instance.IsTurning())
        {
            // Todo: dispatch action event 
            //OnChangeGravityA(true);
            player.BroadcastMessage("FlipA", true);
            Vector3 currentPosition = transform.eulerAngles;
            transform.eulerAngles = currentPosition + new Vector3(0f, 0f, -90f);
            player.BroadcastMessage("FlipA", false);
            //OnChangeGravityB(false);
        }

        else if (Input.GetKeyDown(KeyCode.Z) && !GameController.instance.IsTurning())
        {
            //OnChangeGravityA(true);
            player.BroadcastMessage("FlipB", true);
            Vector3 currentPosition = transform.eulerAngles;
            transform.eulerAngles = currentPosition + new Vector3(0f, 0f, 90f);
            player.BroadcastMessage("FlipB", false);
            //OnChangeGravityB(false);
        }


    }

}
