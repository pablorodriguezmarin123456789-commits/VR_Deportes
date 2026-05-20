using System.Collections.Generic;
using UnityEngine;

public class ChecklistSystem : MonoBehaviour
{
    public static ChecklistSystem Singleton;


    [SerializeField] private List<ChecklistItemData> checklistElements;

    [SerializeField] public bool learning;
    [SerializeField] public string inputSeed = "";
    [SerializeField] public bool useRandomSeed = true;

    public int errosAmount;
    private int erroscount;

    private int _numericSeed;

    public bool generatingChecklist;
    private void Awake()
    {
        if(Singleton == null)
        {
            Singleton = this;

        }
        else
        {
            Destroy(gameObject);
        }
    }
    // MAKE SURE GENERATE CHECKLIST GOES BEFORE GENERATE MAP


    public List<ChecklistItemData> GetChecklistElements()
    {
        return checklistElements;
    }
    public void GenerateChecklist()
    {
        if (useRandomSeed || string.IsNullOrEmpty(inputSeed))
        {
            // Computer's internal clock
            _numericSeed = System.Environment.TickCount;
            
            inputSeed = _numericSeed.ToString();
        }
        else
        {
            _numericSeed = inputSeed.GetHashCode();
        }
        
        System.Random isolatedRandom = new System.Random(_numericSeed);

        foreach (var checklistElement in checklistElements)
        {
            int randomOutput = isolatedRandom.Next(0, 10);

            // TRUE = IT IS WRONG, FALSE = IT IS RIGHT
            // TODO: Add cap to wrong and right elements.
            if (randomOutput <= 4 || erroscount < errosAmount || learning)
            {
                checklistElement.CorrectObjectActivation();
                checklistElement.isNotCorrect = false;
            }
            else
            {
                checklistElement.IncorrectObjectActivation();
                checklistElement.isNotCorrect = true;
                erroscount++;
            }
        }
    }

    public void AddElementToList(ChecklistItemData elemento)
    {
        checklistElements.Add(elemento);
    }
    public void ClearList()
    {
        checklistElements.Clear();
    }
    // CHECK THE CHECKLIST

    public List<ChecklistItemData> CheckChecklist()
    {
        foreach (var item in checklistElements)
        {
            if (item.isNotCorrect)
            {
                if (item.isNotCorrect == item.inputValue)
                    item.itemResult = ChecklistItemData.Results.CorrectSpotted;
                if (item.isNotCorrect != item.inputValue)
                    item.itemResult = ChecklistItemData.Results.WrongMissed;
            }
            if (!item.isNotCorrect)
            {
                if (item.isNotCorrect == item.inputValue)
                    item.itemResult = ChecklistItemData.Results.CorrectSafe;
                if (item.isNotCorrect != item.inputValue)
                    item.itemResult = ChecklistItemData.Results.WrongFalseAlarm;
            }
        }
        
        return checklistElements;
    }
}
