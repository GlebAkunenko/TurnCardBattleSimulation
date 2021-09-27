using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Warrior
{
    [SerializeField] private int initiative;
    [SerializeField] private int speed;

    public Team team { get; set; }
    public int id { get; set; }
    public int Initiative { get => initiative; set => initiative = value; }
    public int Speed { get => speed; set => speed = value; }
}

public enum Team
{
    red,
    blue
}
