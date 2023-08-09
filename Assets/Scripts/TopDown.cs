using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopDown : MonoBehaviour
{
    private Rigidbody2D rb;
    private Vector2 movement;
    public float speed = 5f;
    public Camera cam;
    private Vector2 mousepos;
    private SpriteRenderer spriteRenderer;
    private Collider2D collider;
    public bool in_car = false;
    private bool close_to_car = false;
    public GameObject current_car = null;
    public int max_health = 100;
    private int current_health;
    private GameObject healthbar;
    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        rb.freezeRotation = true;
        spriteRenderer = GetComponent<SpriteRenderer>();
        collider = GetComponent<Collider2D>();
        current_health = max_health;
        healthbar = transform.Find("Canvas").Find("HealthBar").gameObject;
        healthbar.GetComponent<HealthBar>().set_max(max_health);
    }

    // Update is called once per frame
    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
        mousepos = cam.ScreenToWorldPoint(Input.mousePosition);
        if(Input.GetKeyDown(KeyCode.L))
        {
            if (in_car)
            {
                rb.isKinematic = false;
                collider.enabled = true;
                spriteRenderer.enabled = true;
                in_car = false;
                current_car.GetComponent<Car>().is_active = false;

            }
            else if(close_to_car)
            {
                rb.isKinematic = true;
                collider.enabled = false;
                spriteRenderer.enabled = false;
                in_car = true;
                transform.position = current_car.transform.Find("DismountPoint").position;
                current_car.GetComponent<Car>().is_active = true;
            }
        }

        if(in_car)
        {
            transform.position = current_car.transform.position;
        }
        
   

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Enemies"))
        {
            int damage = collision.gameObject.GetComponent<Monster>().damage;
            float force = collision.gameObject.GetComponent<Monster>().force;
            current_health = Mathf.Max(current_health - damage, 0);
            var force_impulse = (transform.position - collision.gameObject.transform.position).normalized * force;
            rb.AddForce(force_impulse, ForceMode2D.Force);
            healthbar.GetComponent<HealthBar>().set_value(current_health);

            if (current_health == 0)
            {
                Destroy(gameObject);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.layer == LayerMask.NameToLayer("Cars"))
        {
            current_car = collision.gameObject.transform.parent.gameObject;
            close_to_car = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Cars"))
        {
            close_to_car = false;
        }
    }

    private void FixedUpdate()
    {
        if(!in_car)
        {
            var look_dir = mousepos - rb.position;
            var angle = Mathf.Atan2(look_dir.y, look_dir.x) * Mathf.Rad2Deg - 90f;
            rb.velocity = movement.normalized * speed;
            rb.rotation = angle;

        }
        
    }
}
