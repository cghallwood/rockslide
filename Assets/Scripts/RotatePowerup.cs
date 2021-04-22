using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatePowerup : MonoBehaviour
{
    public float rotationSpeed = 7;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.Rotate(Vector3.up, rotationSpeed);
    }
}
