using System.Collections.Generic;

public class TeamPrioritySort : Comparer<Warrior>
{
    private Team priorityTeam;

    public override int Compare(Warrior x, Warrior y)
    {
        int tmp = y.Initiative.CompareTo(x.Initiative);
        if (tmp != 0)
            return tmp;

        tmp = y.Speed.CompareTo(x.Speed);
        if (tmp != 0)
            return tmp;

        if (y.team != x.team) {
            if (y.team == priorityTeam)
                return 1;
            return -1;
        }

        return x.id.CompareTo(y.id);
    }

    public TeamPrioritySort(Team priority)
    {
        priorityTeam = priority;
    }
}



