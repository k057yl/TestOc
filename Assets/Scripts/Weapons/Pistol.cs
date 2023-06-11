using UnityEngine;

public class Pistol : IWeapon
{
    private const int PISTOL_DAMAGE = 10;

    private AmmoSystem _ammoSystem;
    private UIBar _uiBar;

    public Pistol(Transform pistolShootPoint, UIBar uiBar)
    {
        _ammoSystem = new AmmoSystem(60, 6, uiBar);
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
                        enemy.TakeDamage(PISTOL_DAMAGE);
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