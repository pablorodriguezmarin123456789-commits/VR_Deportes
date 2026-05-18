using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InGameCheckListUI : MonoBehaviour
{
    // La lista de comprobaciones
    private ChecklistItemData[] _checklistElements;
    
    // Las referencias de la interfaz
    [SerializeField] private GameObject checklistItemPrefab;
    [SerializeField] private GameObject checklistParent;

    [SerializeField] private TMP_Text failsDisplay;
    
    private int totalFails;

    public void SetUp(ChecklistItemData[] checklistElements)
    {
        _checklistElements = checklistElements;

        foreach (var checklistItemData in _checklistElements)
        {
            var uiElement =  Instantiate(checklistItemPrefab, checklistParent.transform);
            Toggle _toggle = uiElement.GetComponentInChildren<Toggle>();
            TMP_Text _text = uiElement.GetComponentInChildren<TextMeshPro>();

            _toggle.onValueChanged.AddListener((bool isOn) =>
            {
                checklistItemData.SetPlayerValue(isOn);
            });
            _text.text = checklistItemData.checklistDefinition;
        }
    }

    public void MidGameCheck()
    {
        totalFails = 0;

        foreach (var item in _checklistElements)
        {
            if (item.isNotCorrect != item.inputValue)
            {
                totalFails++;
            }
        }

        if (totalFails == 0)
        {
            // TODO: WIN
            failsDisplay.text = "¡Todo correcto!";
            // TODO: Añadir evento de sonido
            failsDisplay.color = Color.green;
        }
        else
        {
            failsDisplay.text = "Errores encontrados: " + totalFails;
            failsDisplay.color = Color.red;
        }
    }
}
