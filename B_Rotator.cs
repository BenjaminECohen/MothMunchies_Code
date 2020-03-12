using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class B_Rotator : MonoBehaviour
{
    public float speed = 150;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        this.gameObject.transform.Rotate(new Vector3(0, Time.deltaTime * speed, 0));
    }
}
