
using UnityEngine;

public class YSort : MonoBehaviour
{
    [SerializeField] private Transform _marker_transform;
    // Start is called before the first frame update
    void Start()
    {
        var sprite = GetComponent<SpriteRenderer>();
        if (_marker_transform) {
            sprite.sortingOrder = transform.get_sorting_order(_marker_transform.position.y);

        } else {
            sprite.sortingOrder = transform.get_sorting_order();

        }
    }

}
