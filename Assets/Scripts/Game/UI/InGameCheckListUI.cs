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
    private Toggle _toggle;
    private TMP_Text _text;

    public void SetUp(ChecklistItemData[] checklistElements)
    {
        _checklistElements = checklistElements;

        foreach (ChecklistItemData checklistItemData in _checklistElements)
        {
            GameObject uiElement =  Instantiate(checklistItemPrefab, checklistParent.transform);
            _toggle = uiElement.GetComponent<Toggle>();
            _text = uiElement.GetComponent<TextMeshPro>();

            _toggle.onValueChanged = checklistItemData.SetPlayerValue(_toggle.isOn);
            _text.text = checklistItemData.checklistDefinition;
        }
    }
}
