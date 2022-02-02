using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private List<WaveConfigSO> waveConfigs;
    [SerializeField] private float timeBetweenWaves;
    private WaveConfigSO _currentWave;

    private bool _spawningShouldLoop;
    
    void Start()
    {
        if (waveConfigs.Count == 0)
        {
            // there are no wave configs linked notify console, return
            Debug.LogWarning("There are no Wave Configs linked to the Enemy Spawner.");
            return;
        }

        _spawningShouldLoop = true;
        StartCoroutine(SpawnEnemies());
    }

    public WaveConfigSO GetCurrentWave()
    {
        return _currentWave;
    }

    private IEnumerator SpawnEnemies()
    {
        do
        {
            foreach (var waveConfig in waveConfigs)
            {
                _currentWave = waveConfig;
                for (int i = 0; i < _currentWave.GetEnemyCount(); ++i)
                {
                    Instantiate(_currentWave.GetEnemyPrefab(i),
                        _currentWave.GetStartingWaypoint().position,
                        Quaternion.identity,
                        transform);
                    // Wait before spawning next enemy
                    yield return new WaitForSecondsRealtime(_currentWave.GetRandomSpawnTime());
                }

                // Wait before next wave starts
                yield return new WaitForSecondsRealtime(timeBetweenWaves);
            }
        } while (_spawningShouldLoop);
    }
}
