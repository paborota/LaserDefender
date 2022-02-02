using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathfinder : MonoBehaviour
{
    private EnemySpawner _enemySpawner;
    private WaveConfigSO _waveConfig;
    private List<Transform> _waypoints;
    private int _waypointIndex;

    private void Awake()
    {
        _enemySpawner = FindObjectOfType<EnemySpawner>();
    }

    // Start is called before the first frame update
    void Start()
    {
        _waveConfig = _enemySpawner.GetCurrentWave();
        if (_waveConfig == null)
        {
            Debug.LogWarning("There were no Wave Config found when accessing the Enemy Spawner");
            return;
        }
        
        _waypoints = _waveConfig.GetWaypoints();
        if (_waypoints.Count == 0)
        {
            Debug.LogWarning("There were no waypoints found when accessing the Enemy Spawner's Wave Config");
            return;
        }
        
        transform.position = _waypoints[_waypointIndex].position;
    }
    
    void Update()
    {
        if (_waveConfig == null) return;
        FollowPath();
    }

    private void FollowPath()
    {
        if (_waypointIndex < _waypoints.Count)
        {
            Vector2 targetPosition = _waypoints[_waypointIndex].position;
            float delta = _waveConfig.GetMoveSpeed() * Time.deltaTime;
            Vector2 currentPosition = transform.position;
            
            currentPosition = Vector2.MoveTowards(currentPosition, targetPosition, delta);
            transform.position = currentPosition;
            if (currentPosition == targetPosition)
            {
                ++_waypointIndex;
            }
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
