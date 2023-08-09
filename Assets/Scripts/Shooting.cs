using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    public GameObject bullet_prefab;
    public Transform firing_pos;
    public float bullet_force;
    // Start is called before the first frame update
    void Start()
    {
        firing_pos = transform.Find("FiringPosition");
    }

    // Update is called once per frame
    void Update()
    {
        var in_car = GetComponent<TopDown>().in_car;
        if (Input.GetKeyDown(KeyCode.Space) && !in_car)
        {
            var bullet = Instantiate(bullet_prefab, firing_pos.position, firing_pos.rotation);
            bullet.GetComponent<Rigidbody2D>().AddForce(firing_pos.up * bullet_force, ForceMode2D.Impulse);
        }
    }
}
