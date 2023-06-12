using System;
using UnityEngine;
using UnityEngine.UI;

public class UIBar : MonoBehaviour
{
    [SerializeField] private Text _ammoText;
    [SerializeField] private Text _totalAmmoText;
    [SerializeField] private Text _killedText;

    private int _killedEnemy;

    public int Killed
    {
        get { return _killedEnemy; }
        set { _killedEnemy = value; }
    }

    public static Action OnKilled;

    private void Start()
    {
        OnKilled += SetKilledText;
    }

    private void OnDestroy()
    {
        OnKilled += SetKilledText;
    }

    public void UpdateAmmoText(int currentAmmo, int maxAmmo)
    {
        _ammoText.text = currentAmmo.ToString();
        _totalAmmoText.text = maxAmmo.ToString();
    }
    
    public void SetKilledText()
    {
        Killed++;
        _killedText.text = _killedEnemy.ToString();
    }
}