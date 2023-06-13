using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class Popup : MonoBehaviour
{
    [SerializeField] Button _button1;
    [SerializeField] Button _button2;
    [SerializeField] Text _button1Text;
    [SerializeField] Text _button2Text;

    private Action _action;


    public void Init(Transform canvas, string btn1txt, int btn2txt, Action action) 
    {
        Time.timeScale = Constants.ZERO;
        _button1Text.text = btn1txt;
        _button2Text.text = btn2txt.ToString();
		
        transform.SetParent(canvas);
        transform.localScale = Vector3.one;
    }

    private void OnEnable()
    {
        _button1.onClick.AddListener(() => {
            Time.timeScale = Constants.ONE;
            GameObject.Destroy(this.gameObject);
        });
		
        _button2.onClick.AddListener(() => {
            _action();
            Time.timeScale = Constants.ONE;
            GameObject.Destroy(this.gameObject);
        });
    }

    private void OnDisable()
    {
        _button1.onClick.RemoveAllListeners();
        _button2.onClick.RemoveAllListeners();
    }
}