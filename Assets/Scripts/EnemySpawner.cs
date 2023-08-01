using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject ememy_prefab;
    public float time = 0;
    public float spawn_interval = 2;
    public float radius = 10;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        if(time >= spawn_interval) {
            time = 0;
            GameObject player = GameObject.Find("Player");
            if(!player.GetComponent<PlayerContoller>().alive) return;
            Vector3 player_position = player.transform.position;
            float angle = Random.Range(0, 2*Mathf.PI);
            Vector3 vec = new Vector3(radius*Mathf.Cos(angle), radius*Mathf.Sin(angle), 0);
            Vector3 spawn_position = player_position + vec;
            Instantiate(ememy_prefab, spawn_position, transform.rotation);
        }

    }
}
