using System;

public class PlayerStats
{
    public event Action Changed;

    public string Name { get; set; }
    
    public int DamageDone
    {
        get => damageDone;
        set
        {
            damageDone = value;
            Changed?.Invoke();
        }
    }

    private int damageDone;

    public PlayerStats(string name)
    {
        Name = name;
    }
}
