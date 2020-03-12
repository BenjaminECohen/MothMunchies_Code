using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class B_PlayerCollisionHandler : MonoBehaviour
{

    public PlayerController cont;
    private GameObject player;
    private bool playerDisabled;
    private float startTime;
    private RigidbodyConstraints fullConstrain = RigidbodyConstraints.FreezeAll;
    // Start is called before the first frame update
    void Start()
    {
        player = cont.gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if (playerDisabled)
        {
            
            if (Time.time - startTime < 5)
            {
                Debug.Log("Yeah boi we can't move");
            }
            else
            {
                Debug.Log("Times Up");
                cont.enabled = true;
                //Fully constrain all rotations and zero out all velocity and rotations
                cont.GetComponent<Rigidbody>().constraints = fullConstrain;
                resetPlayerVelocity();
                resetPlayerRotations();

                //Reset to normal player constraints
                cont.GetComponent<Rigidbody>().constraints = cont.currConstr;
                playerDisabled = false;
            }
        }

        if (Input.GetKeyDown(KeyCode.P))
        {
            resetPlayerRotations();
            resetPlayerVelocity();
        }
        
    }


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            if (cont.hover)
            {
                cont.toggleHover();
            }
            Debug.Log("Dayum son you hit the ground");
            cont.toggleFlight();
        }
        if (collision.gameObject.tag == "Wall" && !playerDisabled)
        {
            if (cont.hover)
            {
                cont.toggleHover();
            }
            Debug.Log("Oh shit thats a wall");
            cont.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
            cont.GetComponent<Rigidbody>().velocity = transform.forward * -50;
            cont.enabled = false;
            playerDisabled = true;
            startTime = Time.time;

        }
    }

    private void resetPlayerRotations()
    {
        player.transform.rotation = Quaternion.identity;
    }
    private void resetPlayerVelocity()
    {
        cont.GetComponent<Rigidbody>().velocity = Vector3.zero;
    }
}
