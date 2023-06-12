using UnityEngine;

public class Pistol : IWeapon
{
    private AmmoSystem _ammoSystem;
    private UIBar _uiBar;

    public Pistol(Transform pistolShootPoint, UIBar uiBar)
    {
        _ammoSystem = new AmmoSystem(Constants.START_AMMO, Constants.MAGAZINE_SIZE, uiBar);
        _uiBar = uiBar;
    }

    public void Fire(Transform shootPoint)
    {
        if (_ammoSystem.CanFire())
        {
            _ammoSystem.Fire();

            RaycastHit hit;
            if (Physics.Raycast(shootPoint.position, shootPoint.forward, out hit))
            {
                if (hit.collider != null)
                {
                    EnemyController enemy = hit.collider.GetComponent<EnemyController>();
                    if (enemy != null)
                    {
                        enemy.TakeDamage(Constants.PISTOL_DAMAGE);
                        enemy.ActivateBloodParticles();
                    }
                }
            }
        }
        else
        {
            Debug.Log("Невозможно произвести выстрел. Обойма пуста или идет перезарядка.");
        }
    }

    public void ReloadByButton()
    {
        _ammoSystem.ReloadByButton();
    }
}