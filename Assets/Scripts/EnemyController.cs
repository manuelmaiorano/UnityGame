using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float velocity = 1;
    public LogicManager logic;

    // Start is called before the first frame update
    void Start()
    {
        logic = GameObject.FindGameObjectWithTag("Logic").GetComponent<LogicManager>();
        GetComponent<Rigidbody2D>().freezeRotation = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(!logic.alive) return;
        Vector3 player_position = GameObject.Find("Player").transform.position;
        Vector3 diff = player_position - transform.position;
        Vector3 versor = diff/diff.magnitude;
        float step;
        //transform.position += versor * step;
        if (diff.magnitude > 0.1) {
            step = velocity * Time.deltaTime;
        } else {
            step = 0;
        }
        var add_pos = versor * step;
        var enter_building = GameObject.Find("Player").GetComponent<PlayerContoller>().inside_building !=
                                gameObject.GetComponent<BuildingCollision>().inside_building;
                            
        gameObject.GetComponent<BuildingCollision>().add_pos = add_pos;
        gameObject.GetComponent<BuildingCollision>().enter_building = enter_building;
        
    }

    
}
