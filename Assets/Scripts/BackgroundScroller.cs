using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundScroller : MonoBehaviour
{
    [Tooltip("Vertical scrolling speed of the background image.")]
    [SerializeField] private float moveSpeed = 1.0f;
    private float _newMoveSpeed;
    public float LerpSpeed { get; set; }
    private float _defaultMoveSpeed;
    
    
    private Vector2 _offset;
    private Material _material;
    
    // Start is called before the first frame update
    void Awake()
    {
        _material = GetComponent<SpriteRenderer>().material;
        _defaultMoveSpeed = moveSpeed;
        _newMoveSpeed = moveSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        // _offset.y += moveSpeed * Time.deltaTime;
        // _material.mainTextureOffset = _offset;

        _offset.y += GetMoveSpeedInterp() * Time.deltaTime;
        _material.mainTextureOffset = _offset;
    }

    private float GetMoveSpeedInterp()
    {
        moveSpeed = Mathf.Lerp(moveSpeed, _newMoveSpeed, LerpSpeed * Time.deltaTime);
        return moveSpeed;
    }

    public void ResetScrollSpeed()
    {
        _newMoveSpeed = _defaultMoveSpeed;
    }

    public void LerpScrollSpeed(ref float multiplier)
    {
        _newMoveSpeed = moveSpeed * multiplier;
    }

    public void AlterScrollSpeed(ref float multiplier)
    {
        moveSpeed *= multiplier;
        _newMoveSpeed = moveSpeed;
    }
}
