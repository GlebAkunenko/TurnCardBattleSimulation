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
        return "�������� " + (Warrior.team == Team.red ? "�" : "�") + (Warrior.id + 1).ToString();
    }

    private string GetCharacteristicsText()
    {
        return string.Format("����������: {0}, ��������: {1}", Warrior.Initiative, Warrior.Speed);
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
        roundTextUI.text = "����� �" + round.ToString();
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


    // ������ (�������) ���� ����� ���������� � ������� WarriorCell, ������������� ������� WarriorCell - ��� ��� ������� ���� ���������
    public static void SortCells()
    {   
        Cells.Sort((a, b) => b.transform.position.y.CompareTo(a.transform.position.y));
    }


}
