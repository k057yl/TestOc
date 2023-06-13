using System.Collections;
using UnityEngine;

public class ObjectFactory : MonoBehaviour
{
    [SerializeField] private GameObject _characterPrefab;
    [SerializeField] private GameObject _enemyPrefab;
    [SerializeField] private Transform[] _spawnPoints;
    [SerializeField] private float _spawnInterval;
    
    private CharController _char;
    private EnemyController _enemyController;
    
    private void Awake()
    {
        _char = Instantiate(_characterPrefab).GetComponent<CharController>();

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
        Transform spawnPoint = _spawnPoints[Random.Range(Constants.ZERO, _spawnPoints.Length)];
        _enemyController = Instantiate(_enemyPrefab, spawnPoint.position, spawnPoint.rotation).GetComponent<EnemyController>();
        _enemyController.Initialization(_char);
    }
}