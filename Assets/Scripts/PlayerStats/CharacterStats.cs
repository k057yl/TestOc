using System;

public class CharacterStats
{
    private UIBar _uiBar;
    
    private int _enemyKills;
    private int _foundKeys;

    public int EnemyKills//
    {
        get { return _enemyKills; }
        set { _enemyKills = value; }
    }

    public CharacterStats(UIBar uiBar)
    {
        _uiBar = uiBar;
    }
    //public Action OnEnemyKilled;

    public void SetEnemyKilled()
    {
        EnemyKills++;
    }
}
