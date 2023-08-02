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

    public bool close_to_door = false;
    public bool inside_building = false;
    public GameObject which_building = null;

    public GameObject building_inside = null;
    private GameObject which_room = null;
    private GameObject building_in_collision = null;
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
        

        if(Input.GetKeyDown(KeyCode.L)) {
            shoot();
        }

        if(close_to_door){
            if(Input.GetKeyDown(KeyCode.E) & !inside_building){
                transform.position = which_building.transform.Find("EnterMarker").position;
                inside_building = true;
                building_inside = which_building;
                building_inside.GetComponent<SpriteRenderer>().enabled = false;
                building_inside.transform.Find("Room").gameObject.GetComponent<SpriteRenderer>().enabled = true;
            } else if(Input.GetKeyDown(KeyCode.E) & inside_building){
                transform.position = building_inside.transform.Find("ExitMarker").position;
                inside_building = false;
                building_inside.GetComponent<SpriteRenderer>().enabled = true;
                building_inside.transform.Find("Room").gameObject.GetComponent<SpriteRenderer>().enabled = false;
                building_inside = null;
            }

        }

        SpriteRenderer sprite = gameObject.GetComponent<SpriteRenderer>();
        
        sprite.flipX = !facing_right;

        // var enter_building = Input.GetKeyDown(KeyCode.E);
        
        // var building_collision = gameObject.GetComponent<BuildingCollision>();
        // building_collision.add_pos = add_pos;
        // building_collision.enter_building = enter_building;
        // if (building_collision.inside_building) {
        //     GameObject.Find("Building").GetComponent<SpriteRenderer>().enabled = false;
        // } else {
        //     GameObject.Find("Building").GetComponent<SpriteRenderer>().enabled = true;
        // }

        if(inside_building) {
            var room = building_inside.transform.Find("Room").gameObject;
            
            var bounds = new Bounds();
            var vec = room.GetComponent<BoxCollider2D>().bounds.center;
            vec.z = 0;
            bounds.extents = room.GetComponent<BoxCollider2D>().bounds.extents;
            bounds.center = vec;

            var new_bounds = new Bounds();
            var new_pos = GetComponent<BoxCollider2D>().bounds.center + add_pos;
            new_pos.z = 0;
            new_bounds.center = new_pos;
            new_bounds.extents = GetComponent<BoxCollider2D>().bounds.extents;
        
            if (bounds.ContainBounds(new_bounds)) {
                transform.position += add_pos;
            }
        } 
        else if (building_in_collision != null) {
            var bounds = new Bounds();
            var internal_bounds = building_in_collision.transform.Find("InternalCollider").GetComponent<BoxCollider2D>().bounds;
            var vec = internal_bounds.center;
            vec.z = 0;
            bounds.extents = internal_bounds.extents;
            bounds.center = vec;

            var new_bounds = new Bounds();
            var new_pos = GetComponent<BoxCollider2D>().bounds.center + add_pos;
            new_pos.z = 0;
            new_bounds.center = new_pos;
            new_bounds.extents = GetComponent<BoxCollider2D>().bounds.extents;
            
            if (!bounds.Intersects(new_bounds)) {
                transform.position += add_pos;
            }
        } 
        else {
                transform.position += add_pos;
        }

    }


    private void OnTriggerEnter2D(Collider2D collision){
        if(collision.gameObject.layer == LayerMask.NameToLayer("Doors")) {
            close_to_door = true;
            which_building = collision.gameObject.transform.parent.gameObject;
        } 
        if(collision.gameObject.layer == LayerMask.NameToLayer("Buildings")) {
            building_in_collision = collision.gameObject;
        }
    }

    private void OnTriggerExit2D(Collider2D collision){
        if(collision.gameObject.layer == LayerMask.NameToLayer("Doors")) {
            close_to_door = false;
            which_building = null;
        }
        if(collision.gameObject.layer == LayerMask.NameToLayer("Buildings")) {
            building_in_collision = null;
        }
    }

    private void shoot() {
        var bullet = Instantiate(bullet_prefab, transform.position, transform.rotation);
        float angle = Random.Range(0, 2*Mathf.PI);
        Vector3 direction = new Vector3(Mathf.Cos(angle), Mathf.Sin(angle), 0);
        bullet.GetComponent<BulletController>().direction = direction;
    }
}
