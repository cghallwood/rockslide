using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowRock : MonoBehaviour
{
    private Rigidbody rockRb;
    public float forceSpeed;
    // Start is called before the first frame update
    void Start()
    {
        rockRb = GetComponent<Rigidbody>();
        rockRb.AddRelativeForce(Vector3.back * forceSpeed, ForceMode.Impulse);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
