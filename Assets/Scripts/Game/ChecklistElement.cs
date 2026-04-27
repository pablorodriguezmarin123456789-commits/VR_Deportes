using UnityEngine;

public class ChecklistElement : ScriptableObject
{
    // Scriptable object para cada objeto de la checklist
    // Versión correcta, e incorrectas con el texto asociado
    
    private GameObject _correctObject;
    private GameObject[] _incorrectObjects;
    private string _checklistText;

    // Maybe save the value in the SO
    public bool isCorrect;
    
    public void DisableIncorrectObjects()
    {
        foreach (GameObject go in _incorrectObjects)
        {
            go.SetActive(false);
        }
        _correctObject.SetActive(true);
    }

    public void DisableCorrectObjectAndChooseWrong()
    {
        _correctObject.SetActive(false);
        
        if(_incorrectObjects.Length !> 1)
        {
            _incorrectObjects[0].SetActive(true);
        }
        else
        {
            DisableIncorrectObjects();
            _correctObject.SetActive(false);
            int randomNum = Random.Range(0, _incorrectObjects.Length);
            _incorrectObjects[randomNum].SetActive(true);
        }
    }
}
