using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{
    private GameObject hero;
    private Transform hero_transform;
    private Rigidbody2D rb;
    public float ang_velocity = 2;
    public float velocity = 2;
    public int damage = 10;
    public int force = 100;
    // Start is called before the first frame update
    void Start()
    {
        hero = GameObject.Find("Hero");
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void FixedUpdate()
    {
        var vec = (hero.transform.position - transform.position).normalized;
        rb.angularVelocity = -Vector2.SignedAngle(vec, transform.up)* ang_velocity;
        rb.velocity = vec * velocity;
    }


}
