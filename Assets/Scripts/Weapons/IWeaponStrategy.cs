using UnityEngine;

public interface IWeaponStrategy
{
    void Fire(Transform shootPoint);
    void ReloadByButton();
}