using UnityEngine;

public class ChecklistSystem : MonoBehaviour
{
    public static ChecklistSystem Singleton;


    [SerializeField] private ChecklistItemData[] checklistElements;

    [SerializeField] private string inputSeed = "";
    [SerializeField] private bool useRandomSeed = true;

    public int errosAmount;

    private int _numericSeed;


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
            if (randomOutput <= 4)
            {
                checklistElement.CorrectObjectActivation();
                checklistElement.isNotCorrect = false;
            }
            else
            {
                checklistElement.IncorrectObjectActivation();
                checklistElement.isNotCorrect = true;
            }
        }
    }
    
    // CHECK THE CHECKLIST

    public ChecklistItemData[] CheckChecklist()
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
