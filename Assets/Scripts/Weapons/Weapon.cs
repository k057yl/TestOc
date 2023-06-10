using UnityEngine;

public class Weapon
{
    private IWeaponStrategy _weaponStrategy;
    
    public void SetStrategy(IWeaponStrategy strategy)
    {
        _weaponStrategy = strategy;
    }
    
    public void Fire(Transform shootPoint)
    {
        _weaponStrategy.Fire(shootPoint);
    }
    
    public void Reload()
    {
        _weaponStrategy.ReloadByButton();
    }
    /*
    public void Fire(Transform shootPoint)
    {
        _weaponStrategy.Fire(shootPoint);
    }
    */
}
