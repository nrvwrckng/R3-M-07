using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movemint : MonoBehaviour
{
    public Rigidbody2D mcrigidbody;
    public float jumpstrength = 5; 
    public float movementspeed = 2;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) 
        {
            mcrigidbody.velocity = Vector2.up * jumpstrength;
        }
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            mcrigidbody.velocity = Vector2.left * movementspeed;
        }
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            mcrigidbody.velocity = Vector2.right * movementspeed;
        }
    }
}
