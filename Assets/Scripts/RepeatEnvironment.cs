using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepeatEnvironment : MonoBehaviour
{
    private Vector3 startPos = new Vector3(0, 16.78f, 39.76f);

    // Update is called once per frame
    void FixedUpdate()
    {
        if (transform.position.z < 1)
        {
            transform.position = startPos;
        }
    }
}
