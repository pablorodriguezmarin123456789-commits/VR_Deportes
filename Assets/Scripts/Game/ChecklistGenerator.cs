using UnityEngine;

public class ChecklistGenerator : MonoBehaviour
{
    [SerializeField] private ChecklistElement[] checklistElements;

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

        for (int i = 0; i < checklistElements.Length; i++)
        {
            int randomOutput = isolatedRandom.Next(0, 10);

            // TRUE = It will be alright, FALSE = it will be wrong
            // TODO: Add cap to wrong and right elements.
            if (randomOutput <= 4)
            {
                checklistElements[i].DisableIncorrectObjects();
                checklistElements[i].isCorrect = true;
            }
            else
            {
                checklistElements[i].DisableCorrectObjectAndChooseWrong();
                checklistElements[i].isCorrect = false;
            }
        }
    }
}
