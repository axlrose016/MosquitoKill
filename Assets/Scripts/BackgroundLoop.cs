using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundLoop : MonoBehaviour
{
    public float speed = .5f;
    Vector3 startPos;
 
    void Start()
    {
        startPos = transform.position;
    }
    void Update()
    {
        transform.Translate(((new Vector3(-1, 0, 0)) * speed * Time.deltaTime));
        
        if (transform.position.x < -19.34315)
            transform.position = startPos;
    }
}
