using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car : MonoBehaviour
{
    public bool is_active = false;
    public float speed_multiplier = 2f;
    private float speed;
    private float angle_control;
    private float length;
    public Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.freezeRotation = false;
        length = GetComponent<Collider2D>().bounds.extents.y * 2;
        
    }

    // Update is called once per frame
    void Update()
    {
        if (is_active)
        {
            speed = Input.GetAxisRaw("Vertical") * speed_multiplier;
            angle_control = - Input.GetAxisRaw("Horizontal") * Mathf.PI /4;

        } else
        {
            speed = 0;
        }
        
    }

    private void FixedUpdate()
    {
        var current_angle = (rb.rotation + 90f) * Mathf.Deg2Rad;
        rb.velocity = new Vector2(speed * Mathf.Cos(current_angle), speed * Mathf.Sin(current_angle));
        rb.angularVelocity = 1 / length * speed * Mathf.Tan(angle_control) * Mathf.Rad2Deg;
    }
}
