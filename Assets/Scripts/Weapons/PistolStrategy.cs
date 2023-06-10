using System.Threading.Tasks;
using UnityEngine;

public class PistolStrategy : IWeaponStrategy
{
    private const int PISTOL_DAMAGE = 10;

    private int _maxAmmo = 60;
    private int _magazineSize = 6;
    private int _currentAmmo;
    private bool _isReloading;

    public PistolStrategy(Transform pistolShootPoint)
    {
        _currentAmmo = _magazineSize;
    }

    public void Fire(Transform shootPoint)
    {
        if (_currentAmmo > 0)
        {
            RaycastHit hit;
            if (Physics.Raycast(shootPoint.position, shootPoint.forward, out hit))
            {
                if (hit.collider != null)
                {
                    EnemyController enemy = hit.collider.GetComponent<EnemyController>();
                    if (enemy != null)
                    {
                        enemy.TakeDamage(PISTOL_DAMAGE);
                        enemy.ActivateBloodParticles();
                    }
                }
            }

            _currentAmmo--;
            Debug.Log("Выстрел! Осталось патронов: " + _currentAmmo);

            if (_currentAmmo == 0)
            {
                Debug.Log("Обойма пуста. Необходима перезарядка.");
                ReloadAsync();
            }
        }
        else
        {
            Debug.Log("Обойма пуста. Необходима перезарядка.");
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
            _isReloading = false;

            Debug.Log("Обойма пистолета перезаряжена. Патронов: " + _currentAmmo);
        }
    }
    
    public void ReloadByButton()
    {
        if (!_isReloading && _currentAmmo < _magazineSize)
        {
            _isReloading = true;
            
            Debug.Log("Начинается перезарядка по нажатию кнопки...");
            ReloadAsync();
        }
    }
}