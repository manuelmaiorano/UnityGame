using UnityEngine;

public class BuildingCollision : MonoBehaviour
{ 
    
    public bool close_to_door = false;
    public bool inside_building = false;
    public GameObject which_building = null;
    private GameObject which_room = null;
    public GameObject building_in_collision = null;
    public GameObject building_inside = null;
    public Vector3 add_pos;
    public bool enter_building;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if(close_to_door){
            if(enter_building && !inside_building){
                transform.position = which_building.transform.Find("EnterMarker").position;
                building_inside = which_building;
                inside_building = true;
            } else if(!enter_building && inside_building){
                transform.position = building_inside.transform.Find("ExitMarker").position;
                inside_building = false;
            }

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
}
