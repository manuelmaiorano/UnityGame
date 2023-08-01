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
        float step = velocity * Time.deltaTime;
        transform.position += versor * step;
        
    }

    private void OnTriggerEnter2D(Collider2D collision){
        if(collision.gameObject.layer == 3){
            logic.game_over();
        }
        if(collision.gameObject.layer == 6){
            Destroy(gameObject);
        }
        

    }

    
}
