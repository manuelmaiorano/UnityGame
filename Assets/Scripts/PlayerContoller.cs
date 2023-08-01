using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerContoller : MonoBehaviour
{
    public float velocity = 10;
    public LogicManager logic;
    public bool alive = true;
    public GameObject bullet_prefab;
    private bool facing_right = true;
    // Start is called before the first frame update
    void Start()
    {
        logic = GameObject.FindGameObjectWithTag("Logic").GetComponent<LogicManager>();
        GetComponent<Rigidbody2D>().freezeRotation = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (!alive) return;
        Vector3 add_pos = new Vector3(0, 0, 0);
        float step = velocity * Time.deltaTime;
        if(Input.GetKey(KeyCode.W)){
            add_pos.y += step;
        }
        if(Input.GetKey(KeyCode.A)){
            add_pos.x -= step;
            facing_right = false;
        }
        if(Input.GetKey(KeyCode.S)){
            add_pos.y -= step;
        }
        if(Input.GetKey(KeyCode.D)){
            add_pos.x += step;
            facing_right = true;
        }
        transform.position += add_pos;

        if(Input.GetKeyDown(KeyCode.L)) {
            shoot();
        }

        SpriteRenderer sprite = gameObject.GetComponent<SpriteRenderer>();
        if(facing_right){
            sprite.flipX = false;
        } else {
            sprite.flipX = true;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision){
        if(collision.gameObject.layer == 7) {
            logic.game_over();
            alive = false;
        }

    }

    private void shoot() {
        var bullet = Instantiate(bullet_prefab, transform.position, transform.rotation);
        float angle = Random.Range(0, 2*Mathf.PI);
        Vector3 direction = new Vector3(Mathf.Cos(angle), Mathf.Sin(angle), 0);
        bullet.GetComponent<BulletController>().direction = direction;
    }
}
