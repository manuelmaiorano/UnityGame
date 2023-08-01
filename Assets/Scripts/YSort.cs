
using UnityEngine;

public class YSort : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        var sprite = GetComponent<SpriteRenderer>();
        sprite.sortingOrder = transform.get_sorting_order();
    }

}
