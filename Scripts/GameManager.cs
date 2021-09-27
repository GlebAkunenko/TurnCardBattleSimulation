using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Color redTeamBackgroundColor;
    [SerializeField] private Color blueTeamBackgroundColor;
    [SerializeField] private Warrior[] redTeam;
    [SerializeField] private Warrior[] blueTeam;

    private List<Warrior> warriors = new List<Warrior>();
    private Warrior targerWarrior;  // who may be killed
    private int turn = 0;
    
    private IEnumerator Start()
    {
        WarriorCell.RedTeamBackgroundColor = redTeamBackgroundColor;
        WarriorCell.BlueTeamBackgroundColor = blueTeamBackgroundColor;

        // так как всего две команды, думаю обобщение логики ниже - усложнение
        for(int i = 0; i < redTeam.Length; i++) {
            redTeam[i].id = i;
            redTeam[i].team = Team.red;
            warriors.Add(redTeam[i]);
        }
        for (int i = 0; i < blueTeam.Length; i++) {
            blueTeam[i].id = i;
            blueTeam[i].team = Team.blue;
            warriors.Add(blueTeam[i]);
        }

        // так как transform табличек контролируется Vertical Layout Group, то во время первого кадра все таблички "схлопнуты" и имеют одну координату Y
        // из-за этого сортировка табличек проводится некоректно
        yield return new WaitForEndOfFrame();

        WarriorCell.SortCells();
        UpdateTable();
    }

    private void UpdateTable()
    {
        CombatPosition combatPosition = new CombatPosition(warriors);
        int warriorIndex = 0;

        bool[] visualizedRounds = new bool[1024];

        for (int i = 0; i < WarriorCell.Cells.Count; i++) {

            int round = combatPosition.GetRoundByTurn(warriorIndex + turn + 1);
            if (!visualizedRounds[round]) {
                visualizedRounds[round] = true;
                WarriorCell.Cells[i].DrawRoundPlate(round);
            }
            else {
                WarriorCell.Cells[i].Warrior = combatPosition.GetWarriorByTurn(warriorIndex + turn);
                WarriorCell.Cells[i].Turn = warriorIndex + 1 + turn;
                warriorIndex++;
            }
            
            if (warriorIndex > 20)
                WarriorCell.Cells[i].DrawEmptyPlate();

        }
        targerWarrior = combatPosition.GetWarriorByTurn(1 + turn);
    }


    public void SkipButtle()
    {
        turn++;
        UpdateTable();
    }

    public void KillEnemy()
    {
        turn++;
        if (warriors.Count > 1)
            warriors.Remove(targerWarrior);
        else
            Debug.LogWarning("only one warriors exist");
        UpdateTable();
    }
}
