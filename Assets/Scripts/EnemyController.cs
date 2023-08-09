using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float velocity = 1;
    public float horizontal;
    public float vertical;
    public SpriteRenderer sprite;
    private bool facing_right = true;
    public Rigidbody2D rb;
    public LogicManager logic;
    public GameObject player;
    public bool close_to_door = false;
    public bool inside_building = false;
    public GameObject which_building = null;

    public GameObject building_inside = null;
    private GameObject which_room = null;

    // Start is called before the first frame update
    void Start()
    {
        logic = GameObject.FindGameObjectWithTag("Logic").GetComponent<LogicManager>();
        GetComponent<Rigidbody2D>().freezeRotation = true;
        player = GameObject.Find("Player");
        rb = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if(!logic.alive) return;
        
        Vector3 player_position = player.transform.position;
        Vector3 diff = player_position - transform.position;
        Vector3 versor = diff/diff.magnitude;
        
        if (diff.magnitude < 0.1) {
            horizontal = 0;
            vertical = 0;
        } else {
            horizontal = versor.x;
            vertical = versor.y;
        }

        var is_player_inside = player.GetComponent<PlayerContoller>().inside_building;
        if(close_to_door){
            if(is_player_inside & !inside_building){
                transform.position = which_building.transform.Find("EnterMarker").position;
                inside_building = true;
                building_inside = which_building;
                building_inside.GetComponent<SpriteRenderer>().enabled = false;
                which_room = building_inside.transform.parent.Find("Room").gameObject;
                which_room.GetComponent<SpriteRenderer>().enabled = true;
            } else if(!is_player_inside & inside_building){
                transform.position = building_inside.transform.Find("ExitMarker").position;
                inside_building = false;
                building_inside.GetComponent<SpriteRenderer>().enabled = true;
                which_room.gameObject.GetComponent<SpriteRenderer>().enabled = false;
                building_inside = null;
                which_room = null;
            }

        }
        sprite.enabled = which_room == player.GetComponent<PlayerContoller>().which_room;

        if(horizontal < 0) {
            facing_right = false;
        } else if(horizontal >0) {
            facing_right = true;
        }
        sprite.flipX = !facing_right;
        rb.velocity = new Vector2(horizontal * velocity, vertical * velocity);
    }

    private void FixedUpdate()
    {  
        
    }

   private void OnTriggerEnter2D(Collider2D collision){
        if(collision.gameObject.layer == LayerMask.NameToLayer("Doors")) {
            close_to_door = true;
            which_building = collision.gameObject.transform.parent.gameObject;
        } 
    }

    private void OnTriggerExit2D(Collider2D collision){
        if(collision.gameObject.layer == LayerMask.NameToLayer("Doors")) {
            close_to_door = false;
            which_building = null;
        }
    }
    
}
