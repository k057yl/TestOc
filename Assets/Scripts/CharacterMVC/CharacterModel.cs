using System;

public class CharacterModel
{
    public event Action<int> HealthChanged;
    private int _health = Constants.ONE_HUNDRED;
    public int Health
    {
        get { return _health; }
        set
        {
            _health = value;
            HealthChanged?.Invoke(_health);
        }
    }
    
    public event Action<int> KilledChanged;

    private int _killed = Constants.ZERO;
    public int Killed
    {
        get { return _killed; }
        set
        {
            _killed = value;
            KilledChanged?.Invoke(_killed);
        }
    }

    public void TakeDamage(int damageAmount)
    {
        Health -= damageAmount;
        
        if (Health <= Constants.ZERO)
        {
            
        }
    }

    public void EnemyKills()
    {
        Killed++;
    }
}