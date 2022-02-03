using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundScroller : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 1.0f;

    private Vector2 _offset;
    private Material _material;
    
    // Start is called before the first frame update
    void Awake()
    {
        _material = GetComponent<SpriteRenderer>().material;
    }

    // Update is called once per frame
    void Update()
    {
        _offset.y += moveSpeed * Time.deltaTime;
        _material.mainTextureOffset = _offset;
    }
}
