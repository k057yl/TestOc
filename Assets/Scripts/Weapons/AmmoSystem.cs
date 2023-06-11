using System.Threading.Tasks;
using UnityEngine;

public class AmmoSystem
{
    private int _maxAmmo;
    private int _magazineSize;
    private int _currentAmmo;
    private bool _isReloading;
    private UIBar _uiBar;

    public AmmoSystem(int maxAmmo, int magazineSize, UIBar uiBar)
    {
        _maxAmmo = maxAmmo;
        _magazineSize = magazineSize;
        _currentAmmo = _magazineSize;
        _uiBar = uiBar;
    }

    public int GetCurrentAmmo()
    {
        return _currentAmmo;
    }
    
    public int GetMaxAmmo()
    {
        return _maxAmmo;
    }
    
    public bool CanFire()
    {
        return _currentAmmo > 0 && !_isReloading;
    }

    public void Fire()
    {
        if (CanFire())
        {
            _currentAmmo--;

            Debug.Log("Осталось патронов: " + _currentAmmo);
            
            _uiBar.UpdateAmmoText(GetCurrentAmmo(), GetMaxAmmo());

            if (_currentAmmo == 0)
            {
                Debug.Log("Обойма пуста. Необходима перезарядка.");
                ReloadAsync();
            }
        }
        else
        {
            Debug.Log("Невозможно произвести выстрел. Обойма пуста или идет перезарядка.");
        }
    }

    public async void ReloadAsync()
    {
        if (!_isReloading)
        {
            _isReloading = true;

            Debug.Log("Начинается перезарядка...");
            await Task.Delay(3000);

            int ammoToAdd = Mathf.Min(_maxAmmo - _currentAmmo, _magazineSize);
            _currentAmmo += ammoToAdd;
            _maxAmmo -= ammoToAdd;
            _isReloading = false;

            Debug.Log("Обойма перезаряжена. Патронов: " + _currentAmmo);
            
            _uiBar.UpdateAmmoText(GetCurrentAmmo(), GetMaxAmmo());
        }
    }

    public void ReloadByButton()
    {
        if (!_isReloading && _currentAmmo < _magazineSize)
        {
            _isReloading = true;

            Debug.Log("Начинается перезарядка по нажатию кнопки R...");
            ReloadAsync();
        }
    }
}