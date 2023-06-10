using UnityEngine;

public class ObjectFactory : MonoBehaviour
{
    [SerializeField] private GameObject _characterPrefab;
    [SerializeField] private GameObject _canvasPrefab;

    private GameObject _characterInstance;
    private GameObject _canvasInstance;

    private void Awake()
    {
        _characterInstance = Instantiate(_characterPrefab);
        _canvasInstance = Instantiate(_canvasPrefab);
    }
}