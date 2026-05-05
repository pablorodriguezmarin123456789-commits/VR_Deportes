using UnityEngine;

public class ChecklistSystem : MonoBehaviour
{
    [SerializeField] private ChecklistItemData[] checklistElements;

    [SerializeField] private string inputSeed = "";
    [SerializeField] private bool useRandomSeed = true;

    private int _numericSeed;

    // MAKE SURE GENERATE CHECKLIST GOES BEFORE GENERATE MAP
    
    private void GenerateChecklist()
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

            // TRUE = It will be alright, FALSE = it will be wrong
            // TODO: Add cap to wrong and right elements.
            if (randomOutput <= 4)
            {
                checklistElement.CorrectObjectActivation();
                checklistElement.trueValue = true;
            }
            else
            {
                checklistElement.IncorrectObjectActivation();
                checklistElement.trueValue = false;
            }
        }
    }
    
    // CHECK THE CHECKLIST

    public void CheckChecklist()
    {
        foreach (var item in checklistElements)
        {
            if (item.trueValue && item.trueValue == item.inputValue)
            {
                item.itemResult = ChecklistItemData.Results.CorrectSafe;
            }
            if (!item.trueValue && item.trueValue == item.inputValue)
            {
                item.itemResult = ChecklistItemData.Results.CorrectSpotted;
            }
            if (item.trueValue && item.trueValue != item.inputValue)
            {
                item.itemResult = ChecklistItemData.Results.WrongFalseAlarm;
            }
            if (!item.trueValue && item.trueValue != item.inputValue)
            {
                item.itemResult = ChecklistItemData.Results.WrongMissed;
            }
        }
    }
}
