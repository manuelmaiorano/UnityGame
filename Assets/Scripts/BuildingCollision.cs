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
            var internal_bounds = building_in_collision.transform.Find("InternalCollider")
                                                        .GetComponent<BoxCollider2D>().bounds;
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
}
