using UnityEngine;

public class Weapon
{
    private IWeapon _weapon;

    public void SetStrategy(IWeapon strategy)
    {
        _weapon = strategy;
    }
    
    public void Fire(Transform shootPoint)
    {
        _weapon.Fire(shootPoint);
    }
    
    public void ReloadByButton()
    {
        _weapon.ReloadByButton();
    }
    
    public bool GetIsReloading()
    {
        return _weapon.GetIsReloading();
    }
}