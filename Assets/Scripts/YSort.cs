
using UnityEngine;

public class YSort : MonoBehaviour
{
    public int sort_order;
    // Start is called before the first frame update
    void Start()
    {
        var _marker_transform = transform.Find("SortMarker");
        if (_marker_transform) {
            sort_order = -(int)(_marker_transform.position.y *100);

        } else {
            sort_order = -(int)(transform.position.y * 100);

        }

        GetComponent<SpriteRenderer>().sortingOrder = sort_order;
    }

}
