using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatPosition
{
    private Warrior[] redPriorityArmy;
    private Warrior[] bluePriorityArmy;

    public int WarriorsCount { get; }

    private Warrior[] GetArmyByPriority(Team priority)
    {
        if (priority == Team.red)
            return redPriorityArmy;
        return bluePriorityArmy;
    }

    private Team GetPriorityByTurn(int tern)
    {
        if ((tern / WarriorsCount) % 2 == 0)
            return Team.red;
        return Team.blue;
    }

    public Warrior GetWarriorByTurn(int tern)
    {
        Team priority = GetPriorityByTurn(tern);
        return GetArmyByPriority(priority)[tern % WarriorsCount];
    }

    public int GetRoundByTurn(int turn)
    {
        return (turn - 1) / WarriorsCount + 1;
    }


    public CombatPosition(List<Warrior> allWariors)
    {
        allWariors.Sort(new TeamPrioritySort(Team.red));
        redPriorityArmy = allWariors.ToArray();

        allWariors.Sort(new TeamPrioritySort(Team.blue));
        bluePriorityArmy = allWariors.ToArray();

        WarriorsCount = allWariors.Count;
    }

}

