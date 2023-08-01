using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    public Vector3 direction;
    public float velocity;
    public LogicManager logic;
    private float time;
    public float time_to_live = 10;

    // Start is called before the first frame update
    void Start()
    {
        logic = GameObject.FindGameObjectWithTag("Logic").GetComponent<LogicManager>();
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        if(time > time_to_live) {
            Destroy(gameObject);
        }
        transform.position += velocity * Time.deltaTime * direction;
    }

    private void OnTriggerEnter2D(Collider2D collision){
        if(collision.gameObject.layer != 3){
            logic.add_score();
            Destroy(gameObject);
        }
        
    }
}
