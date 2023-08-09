using System;
using UnityEngine;

public class DynamicYSort : MonoBehaviour
{
    public int base_sorting_order;
    [SerializeField] private SortableSprite[] _sortable_sprites;
    public float yoffset;
    
    // Start is called before the first frame update
    void Start()
    {
        yoffset = transform.Find("SortMarker").localPosition.y;
    }

    // Update is called once per frame
    void Update()
    {
        base_sorting_order = transform.get_sorting_order(yoffset);
        foreach(var sortable in _sortable_sprites){

            sortable.sprite.sortingOrder = base_sorting_order + sortable.relative_order;
        }
    }

    [Serializable]
    public struct SortableSprite {
        public SpriteRenderer sprite;
        public int relative_order;
    }
}
