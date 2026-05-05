using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class ChecklistItemData
{
    // System.Serializable para que pueda existir en la escena
    
    // Objects
    [SerializeField] private GameObject correctObject;
    [SerializeField] private GameObject[] incorrectObjects;
    
    // Values
    [HideInInspector] public bool trueValue;
    public bool inputValue;
    
    // Checklist Text
    public string checklistDefinition;
    
    // For the correction
    public enum Results{CorrectSafe, CorrectSpotted, WrongFalseAlarm, WrongMissed}
    [HideInInspector] public Results itemResult;
    
    // UI FUNCTIONS
    public Toggle.ToggleEvent SetPlayerValue(bool isChecked)
    {
        inputValue = isChecked;
        return null;
    }
    
    // ACTIVATION FUNCTIONS
    public void CorrectObjectActivation()
    {
        foreach (var go in incorrectObjects)
        {
            go.SetActive(false);
        }
        correctObject.SetActive(true);
    }

    public void IncorrectObjectActivation()
    {
        correctObject.SetActive(false);
        
        if(incorrectObjects.Length <= 1)
        {
            incorrectObjects[0].SetActive(true);
        }
        else
        {
            foreach (var go in incorrectObjects)
            {
                go.SetActive(false);
            }
            int randomNum = Random.Range(0, incorrectObjects.Length);
            incorrectObjects[randomNum].SetActive(true);
        }
    }
    
}
