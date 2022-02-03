using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    [SerializeField] private float shakeDuration = 1.0f;
    [SerializeField] private float shakeMagnitude = 0.5f;

    private Vector3 _initialPosition;
    
    // Start is called before the first frame update
    void Start()
    {
        _initialPosition = transform.position;
    }

    public void Play()
    {
        StartCoroutine(Shake());
    }

    private IEnumerator Shake()
    {
        var timePassed = 0.0f;
        while (timePassed < shakeDuration)
        {
            transform.position = _initialPosition + (Vector3)Random.insideUnitCircle * shakeMagnitude;
            yield return new WaitForEndOfFrame();
            timePassed += Time.deltaTime;
        }

        transform.position = _initialPosition;
    }
}
