using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public int max_health = 100;
    private int current_health;
    private GameObject healthbar;
    // Start is called before the first frame update
    void Start()
    {
        current_health = max_health;
        healthbar = transform.Find("Canvas").Find("HealthBar").gameObject;
        healthbar.GetComponent<HealthBar>().set_max(max_health);
    }

    // Update is called once per frame
    void Update()
    {
        healthbar.transform.parent.transform.rotation = Quaternion.identity;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.layer == LayerMask.NameToLayer("Bullets"))
        {
            int damage = collision.gameObject.GetComponent<Bullet>().damage;
            current_health = Mathf.Max(current_health - damage, 0);
            healthbar.GetComponent<HealthBar>().set_value(current_health);

            if (current_health == 0)
            {
                Destroy(gameObject);
            }
        }
    }
}
