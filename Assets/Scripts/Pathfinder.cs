using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathfinder : MonoBehaviour
{
    [SerializeField] private WaveConfigSO waveConfig;
    private List<Transform> _waypoints;
    private int _waypointIndex = 0;

    // Start is called before the first frame update
    void Start()
    {
        _waypoints = waveConfig.GetWaypoints();
        transform.position = _waypoints[_waypointIndex].position;
    }

    // Update is called once per frame
    void Update()
    {
        FollowPath();
    }

    private void FollowPath()
    {
        if (_waypointIndex < _waypoints.Count)
        {
            Vector2 targetPosition = _waypoints[_waypointIndex].position;
            float delta = waveConfig.GetMoveSpeed() * Time.deltaTime;
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
