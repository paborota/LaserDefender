using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5.0f;
    private Vector2 _rawInput;

    private Vector2 _minBounds;
    private Vector2 _maxBounds;
    [Header("Camera Bounds Position Padding")]
    [SerializeField] private float paddingLeft = 0.1f;
    [SerializeField] private float paddingRight = 0.1f;
    [SerializeField] private float paddingTop = 0.1f;
    [SerializeField] private float paddingBottom = 0.1f;

    private ShootingBehavior _shootingBehavior;

    private void Start()
    {
        IniBounds();
        _shootingBehavior = GetComponent<ShootingBehavior>();
    }

    // Update is called once per frame
    private void Update()
    {
        Move();
    }

    private void IniBounds()
    {
        var mainCamera = Camera.main;
        if (mainCamera == null) return;
        
        _minBounds = mainCamera.ViewportToWorldPoint(new Vector2());
        _maxBounds = mainCamera.ViewportToWorldPoint(new Vector2(1.0f, 1.0f));
    }

    private void Move()
    {
        Vector2 delta = _rawInput* moveSpeed * Time.deltaTime;
        var newPos = new Vector2();
        var currentTransform = transform.position;
        newPos.x = Mathf.Clamp(currentTransform.x + delta.x, _minBounds.x + paddingLeft, _maxBounds.x - paddingRight);
        newPos.y = Mathf.Clamp(currentTransform.y + delta.y, _minBounds.y + paddingBottom, _maxBounds.y - paddingTop);
        transform.position = newPos;
    }

    private void OnFire(InputValue value)
    {
        // Set is firing in our shooting behavior
        if (value.isPressed)
        {
            _shootingBehavior.StartFiring();
        }
        else
        {
            _shootingBehavior.StopFiring();
        }
    }

    private void OnMove(InputValue value)
    {
        _rawInput = value.Get<Vector2>();
        Debug.Log(_rawInput);
    }
}
