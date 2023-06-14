using UnityEngine;
using UnityEngine.UI;
using System;

public class Popup : MonoBehaviour
{
    [SerializeField] Button _button1;
    [SerializeField] Button _button2;
    [SerializeField] Text _messageText;
    [SerializeField] Text _scoreText;

    private Action _action;


    public void Init(Transform canvas, string message, int score, Action action) 
    {
        Time.timeScale = Constants.ZERO;
        _messageText.text = message;
        _scoreText.text = score.ToString();

        transform.SetParent(canvas);
        transform.localScale = Vector3.one;
        
        UnLockCursor();
    }

    private void OnEnable()
    {
        LockCursor();
        _button1.onClick.AddListener(() => {
            Time.timeScale = Constants.ONE;
            Destroy(gameObject);
        });
		
        _button2.onClick.AddListener(() => {
            _action();
            Time.timeScale = Constants.ONE;
            Destroy(gameObject);
        });
    }

    private void OnDisable()
    {
        LockCursor();
        _button1.onClick.RemoveAllListeners();
        _button2.onClick.RemoveAllListeners();
    }
    
    private void LockCursor()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    
    private void UnLockCursor()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
}