using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private float timeBeforeInitialWave = 1.0f;
    [SerializeField] private List<WaveConfigSO> waveConfigs;
    [SerializeField] private float timeBetweenWaves;
    private WaveConfigSO _currentWave;

    [Tooltip("Whether or not the game should continually spawn these waves.")]
    [SerializeField] private bool spawningShouldLoop = true;

    [Tooltip("Used to keep the Scene Hierarchy clean by parenting the projectiles to this empty object.")]
    [SerializeField] private Transform projectileContainer;
    
    void Start()
    {
        if (waveConfigs.Count == 0)
        {
            // there are no wave configs linked notify console, return
            Debug.LogWarning("There are no Wave Configs linked to the Enemy Spawner.");
            return;
        }
        
        StartCoroutine(StartWave());
    }

    public WaveConfigSO GetCurrentWave()
    {
        return _currentWave;
    }

    private IEnumerator StartWave()
    {
        yield return new WaitForSecondsRealtime(timeBeforeInitialWave);
        StartCoroutine(SpawnEnemies());
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
                    var enemy = Instantiate(_currentWave.GetEnemyPrefab(i),
                        _currentWave.GetStartingWaypoint().position,
                        Quaternion.identity,
                        transform);
                    enemy.GetComponent<ShootingBehavior>().projectileContainer = projectileContainer;
                    // Wait before spawning next enemy
                    yield return new WaitForSecondsRealtime(_currentWave.GetRandomSpawnTime());
                }

                // Wait before next wave starts
                yield return new WaitForSecondsRealtime(timeBetweenWaves);
            }
        } while (spawningShouldLoop);
    }
}
