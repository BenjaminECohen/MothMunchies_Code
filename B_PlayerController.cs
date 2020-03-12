using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class B_PlayerController : MonoBehaviour
{
    public float turnSpeed = 50;
    public float maxForwardFlightSpeed = 5;
    public float maxForwardCrawlSpeed = 5;
    public float maxUpSpeed = 5;
    public float maxDownSpeed = 3;
    public float maxBackwardsSpeed = 3;
    [HideInInspector]
    public bool hover = false;
    private bool jumping = false;
    private Vector3 currVelocity;
    private bool flightEnabled = true;
    [HideInInspector]
    public RigidbodyConstraints currConstr = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
    


    // Start is called before the first frame update
    void Start()
    {
        currVelocity = this.GetComponent<Rigidbody>().velocity;
        
    }

    // Update is called once per frame
    void Update()
    {

        //Movement
        bool isGrounded = Physics.Raycast(transform.position, transform.up * -1, 1.0f);
        Debug.DrawRay(transform.position, transform.up * -5);
        if (isGrounded)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                toggleFlight();
            }
        }
        
        

        if (flightEnabled)
        {
            if (Input.GetKey(KeyCode.Space))
            {
                jumping = true;
                this.GetComponent<Rigidbody>().velocity = transform.up * maxUpSpeed;
                //this.GetComponent<Rigidbody>().velocity = new Vector3(currVelocity.x, 5);

            }
            else
            {
                jumping = false;
                this.GetComponent<Rigidbody>().velocity = (-1 * transform.up) * maxDownSpeed;
            }
            if (Input.GetKeyDown(KeyCode.LeftShift))
            {
                toggleHover();
            }

            if (Input.GetKey(KeyCode.W))
            {
                if (jumping)
                {
                    this.GetComponent<Rigidbody>().velocity = (transform.up + transform.forward) * maxForwardFlightSpeed;
                }
                else
                {
                    this.GetComponent<Rigidbody>().velocity = (transform.forward) * maxForwardFlightSpeed;
                }

            }
            else
            {
                if (jumping)
                {
                    this.GetComponent<Rigidbody>().velocity = (transform.up + transform.forward) * (maxForwardFlightSpeed / 2.0f);
                }
                else
                {
                    this.GetComponent<Rigidbody>().velocity = ((-1 * transform.up) + transform.forward) * maxDownSpeed;
                }
            }
            /*
            else if (Input.GetKey(KeyCode.S))
            {
                //this.GetComponent<Rigidbody>().velocity = new Vector3(currVelocity.x, currVelocity.y, -5);
                this.GetComponent<Rigidbody>().velocity = (-1 * transform.forward) * 3;
            }*/
            if (Input.GetKey(KeyCode.A))
            {
                this.transform.Rotate(Vector3.up * Time.deltaTime * turnSpeed * -1);
            }
            if (Input.GetKey(KeyCode.D))
            {
                this.transform.Rotate(Vector3.up * Time.deltaTime * turnSpeed);
            }
        }
        else //Flight not enabled
        {
            if (Input.GetKey(KeyCode.W))
            {
                
                this.GetComponent<Rigidbody>().velocity = (transform.forward) * maxForwardCrawlSpeed;
            }
            else if (Input.GetKey(KeyCode.S))
            {
                this.GetComponent<Rigidbody>().velocity = (-1 * transform.forward) * maxBackwardsSpeed;
            }
            if (Input.GetKey(KeyCode.A))
            {
                this.transform.Rotate(Vector3.up * Time.deltaTime * turnSpeed * -1);
            }
            if (Input.GetKey(KeyCode.D))
            {
                this.transform.Rotate(Vector3.up * Time.deltaTime * turnSpeed);
            }
        }
        currVelocity = this.GetComponent<Rigidbody>().velocity;
        //Debug.DrawLine(currVelocity, currVelocity * 5);
        
    }


    public void toggleHover()
    {
        if (hover)
        {
            this.GetComponent<Rigidbody>().constraints = currConstr;
            this.GetComponent<Rigidbody>().useGravity = true;
            hover = false;
        }
        else
        {
            this.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePositionY | currConstr;
            this.GetComponent<Rigidbody>().useGravity = false;
            hover = true;
        }
    }

    public void toggleFlight()
    {
        if (flightEnabled)
        {
            flightEnabled = false;
        }
        else
        {
            flightEnabled = true;
        }
    }


    


}
