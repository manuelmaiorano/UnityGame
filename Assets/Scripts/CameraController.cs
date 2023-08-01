using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float velocity = 5;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 player_position = GameObject.Find("Player").transform.position;
        Vector3 error = player_position - transform.position;
        error.z = 0;

        
        transform.position += error * velocity * Time.deltaTime;
        
    }
}
