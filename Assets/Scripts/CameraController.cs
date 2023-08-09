using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float velocity = 5;
    private GameObject hero;
    // Start is called before the first frame update
    void Start()
    {
        hero = GameObject.Find("Hero");
    }

    // Update is called once per frame
    void Update()
    {
        var in_car = hero.GetComponent<TopDown>().in_car;
        if(in_car)
        {
            transform.rotation = hero.GetComponent<TopDown>().current_car.transform.rotation;
        } else
        {
            transform.rotation = Quaternion.identity;
        }
        Vector3 player_position = hero.transform.position;
        Vector3 error = player_position - transform.position;
        error.z = 0;

        
        transform.position += error * velocity * Time.deltaTime;
        
    }
}
