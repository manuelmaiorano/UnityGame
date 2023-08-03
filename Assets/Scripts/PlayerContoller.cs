using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerContoller : MonoBehaviour
{
    public float velocity = 10;
    public float horizontal;
    public float vertical;
    public float moveLimiter = 0.7f;
    public SpriteRenderer sprite;

    public Rigidbody2D rb;
    public LogicManager logic;
    public bool alive = true;
    public GameObject bullet_prefab;
    private bool facing_right = true;

    public bool close_to_door = false;
    public bool inside_building = false;
    public GameObject which_building = null;

    public GameObject building_inside = null;
    public GameObject which_room = null;

    private List<SpriteRenderer> building_sprites;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        logic = GameObject.FindGameObjectWithTag("Logic").GetComponent<LogicManager>();
        sprite = GetComponent<SpriteRenderer>();
        rb.freezeRotation = true;
        building_sprites = new List<SpriteRenderer>();
        foreach(var sprite in GameObject.FindObjectsOfType<SpriteRenderer>()){
            if(sprite.gameObject.layer == LayerMask.NameToLayer("Buildings")) {
                building_sprites.Add(sprite);
            }

        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!alive) return;
        

        if(Input.GetKeyDown(KeyCode.L)) {
            shoot();
        }

        if(close_to_door){
            if(Input.GetKeyDown(KeyCode.E) & !inside_building){
                transform.position = which_building.transform.Find("EnterMarker").position;
                inside_building = true;
                building_inside = which_building;
                building_inside.GetComponent<SpriteRenderer>().enabled = false;
                set_buildings_sprites(false);
                which_room = building_inside.transform.Find("Room").gameObject;
                which_room.GetComponent<SpriteRenderer>().enabled = true;
            } else if(Input.GetKeyDown(KeyCode.E) & inside_building){
                transform.position = building_inside.transform.Find("ExitMarker").position;
                inside_building = false;
                building_inside.GetComponent<SpriteRenderer>().enabled = true;
                set_buildings_sprites(true);
                building_inside.transform.Find("Room").gameObject.GetComponent<SpriteRenderer>().enabled = false;
                building_inside = null;
                which_room = null;
            }

        }


        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical"); 
        if(horizontal < 0) {
            facing_right = false;
        } else if(horizontal >0) {
            facing_right = true;
        }
        sprite.flipX = !facing_right;
        

    }

    private void FixedUpdate()
    {  
        if (horizontal != 0 && vertical != 0) {
            horizontal *= moveLimiter;
            vertical *= moveLimiter;
        } 
        rb.velocity = new Vector2(horizontal * velocity, vertical * velocity);
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

    private void shoot() {
        var bullet = Instantiate(bullet_prefab, transform.position, transform.rotation);
        float angle = Random.Range(0, 2*Mathf.PI);
        Vector3 direction = new Vector3(Mathf.Cos(angle), Mathf.Sin(angle), 0);
        bullet.GetComponent<BulletController>().direction = direction;
    }

    private void set_buildings_sprites(bool isactive){
        foreach(var sprite in building_sprites){
            sprite.enabled = isactive;
        }
    }

}
