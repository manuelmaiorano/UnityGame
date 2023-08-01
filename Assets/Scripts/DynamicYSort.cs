using System;
using UnityEngine;

public class DynamicYSort : MonoBehaviour
{
    private int _base_sorting_order;
    [SerializeField] private SortableSprite[] _sortable_sprites;
    private float _y_offset;
    [SerializeField] private Transform _marker_transform;
    
    // Start is called before the first frame update
    void Start()
    {
        _y_offset = _marker_transform.position.y;
        
    }

    // Update is called once per frame
    void Update()
    {
        _base_sorting_order = transform.get_sorting_order(_y_offset);
        GetComponent<SpriteRenderer>().sortingOrder = _base_sorting_order;
        foreach(var sortable in _sortable_sprites){

            sortable.sprite.sortingOrder = _base_sorting_order + sortable.relative_order;
        }
    }

    [Serializable]
    public struct SortableSprite {
        public SpriteRenderer sprite;
        public int relative_order;
    }
}
