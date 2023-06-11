using UnityEngine;

public class Weapon
{
    private IWeapon m_Weapon;

    public void SetStrategy(IWeapon strategy)
    {
        m_Weapon = strategy;
    }
    
    public void Fire(Transform shootPoint)
    {
        m_Weapon.Fire(shootPoint);
    }
    
    public void ReloadByButton()
    {
        m_Weapon.ReloadByButton();
    }
}