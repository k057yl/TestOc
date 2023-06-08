using System.Collections;
using System.Threading.Tasks;
using UnityEngine;

public class WeaponReloader : MonoBehaviour
{
    //private NewInput _newInput;
    public float reloadTime = 2f;
    public int maxAmmo = 30;
    private int currentAmmo;
    private bool isReloading = false;

    void Start()
    {
        currentAmmo = maxAmmo;
        isReloading = false;
    }

    void Update()
    {
        if (_newInput.Gameplay.Reloaded.triggered)
        {
            Reload();
        }
    }

    void Shoot()
    {
        if (currentAmmo > 0)
        {
            // Выполнение выстрела
            currentAmmo--;
        }
        else
        {
            Reload();
        }
    }

    async void Reload()
    {
        if (isReloading)
            return;

        isReloading = true;
        
        await Task.Delay((int)(reloadTime * 1000));

        currentAmmo = maxAmmo;
        isReloading = false;
    }
}