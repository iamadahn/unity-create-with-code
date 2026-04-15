using System;
using UnityEngine;

public class DestroyOutOfBounds : MonoBehaviour
{
    private float boundZ = 11.0f;
    private float boundX = 23.0f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Math.Abs(gameObject.transform.position.x) > boundX || Math.Abs(gameObject.transform.position.z) > boundZ)
        {
            Destroy(gameObject);
        }
    }
}
