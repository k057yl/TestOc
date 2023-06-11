using System;
using UnityEngine;
using UnityEngine.UI;

public class UIBar : MonoBehaviour
{
    [SerializeField] private Text _ammoText;
    [SerializeField] private Text _totalAmmoText;
    [SerializeField] private Text _killedText;
    
    public void UpdateAmmoText(int currentAmmo, int maxAmmo)
    {
        _ammoText.text = currentAmmo.ToString();
        _totalAmmoText.text = maxAmmo.ToString();
    }
    
    public void UpdateKilledText(int killed)
    {
        _killedText.text = killed.ToString();
    }
}