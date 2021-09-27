using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WarriorCell : MonoBehaviour
{ 
    public static List<WarriorCell> Cells { get; set; } = new List<WarriorCell>();
    public static Color RedTeamBackgroundColor { get; set; } = Color.red;
    public static Color BlueTeamBackgroundColor { get; set; } = Color.blue;

    [SerializeField] private Image colorBackground;
    [SerializeField] private Text turnTextUI;
    [SerializeField] private Text nameTextUI;
    [SerializeField] private Text characteristicsTextUI;
    [SerializeField] private Text roundTextUI;
    [SerializeField] private GameObject warriorPlate;
    [SerializeField] private GameObject roundPlate;
    [SerializeField] private GameObject emptyPlate;

    private Warrior warrior;
    private int turn;

    private string GetNameText()
    {
        return "Существо " + (Warrior.team == Team.red ? "К" : "С") + (Warrior.id + 1).ToString();
    }

    private string GetCharacteristicsText()
    {
        return string.Format("Инициатива: {0}, Скорость: {1}", Warrior.Initiative, Warrior.Speed);
    }

    private void Awake()
    {
        Cells.Add(this);
    }

    public void DrawRoundPlate(int round)
    {
        Warrior = null;
        warriorPlate.SetActive(false);
        emptyPlate.SetActive(false);
        roundPlate.SetActive(true);
        roundTextUI.text = "Раунд №" + round.ToString();
    }

    public void DrawEmptyPlate()
    {
        Warrior = null;
        warriorPlate.SetActive(false);
        roundPlate.SetActive(false);
    }

    public Warrior Warrior
    {
        get => warrior;
        set
        {
            warrior = value;
            if (warrior == null)
                return;

            emptyPlate.SetActive(false);
            roundPlate.SetActive(false);
            warriorPlate.SetActive(true);

            nameTextUI.text = GetNameText();
            characteristicsTextUI.text = GetCharacteristicsText();
            colorBackground.color = (warrior.team == Team.red ? RedTeamBackgroundColor : BlueTeamBackgroundColor);
        }
    }

    public int Turn 
    {
        get => turn;
        set
        {
            turn = value;
            turnTextUI.text = turn.ToString();
        }
    }


    // первый (нулевой) воин будет находиться в нулевом WarriorCell, следовательно нулевой WarriorCell - это тот который выше остальных
    public static void SortCells()
    {   
        Cells.Sort((a, b) => b.transform.position.y.CompareTo(a.transform.position.y));
    }


}
