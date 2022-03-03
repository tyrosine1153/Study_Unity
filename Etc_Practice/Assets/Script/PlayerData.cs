using System;

public enum PlayerClass
{
    Warrior,
    Mage,
    Archer
}

[Serializable]
public class PlayerData
{
    public int hp;
    public string name;
    public int level;
    public PlayerClass playerClass;
}