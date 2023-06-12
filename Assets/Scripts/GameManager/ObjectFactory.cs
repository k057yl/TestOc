using System.Collections;
using UnityEngine;

public class ObjectFactory : MonoBehaviour
{
    [SerializeField] private GameObject _characterPrefab;
    [SerializeField] private GameObject _canvasPrefab;
    [SerializeField] private GameObject _enemyPrefab;
    [SerializeField] private Transform[] _spawnPoints;
    [SerializeField] private float _spawnInterval;

    private GameObject _characterInstance;
    private GameObject _canvasInstance;

    private void Awake()
    {
        _characterInstance = Instantiate(_characterPrefab);
        _canvasInstance = Instantiate(_canvasPrefab);
        
        StartCoroutine(SpawnEnemies());
    }
    
    private IEnumerator SpawnEnemies()
    {
        while (true)
        {
            SpawnEnemy();

            yield return new WaitForSeconds(_spawnInterval);
        }
    }

    private void SpawnEnemy()
    {
        Transform spawnPoint = _spawnPoints[Random.Range(Constants.NULL, _spawnPoints.Length)];
        Instantiate(_enemyPrefab, spawnPoint.position, spawnPoint.rotation);
    }
}