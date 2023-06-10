using System;
using UnityEngine;
using UnityEngine.UI;

public class UIBar : MonoBehaviour
{
    [SerializeField] private Text _ammoText;
    [SerializeField] private Text _totalAmmoText;

    public void UpdateAmmoText(int currentAmmo, int totalAmmo)
    {
        _ammoText.text = currentAmmo.ToString();
        _totalAmmoText.text = totalAmmo.ToString();
    }

    private void Update()
    {
        _ammoText.text.ToString();
    }
}
