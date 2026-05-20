using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EndGameCheckListUI : MonoBehaviour
{

    // Checklist options
    [SerializeField] private GameObject checklistParent;

    // Bien marcado|No Hay error
    [SerializeField] private GameObject correctSafe;

    // Bien marcado/Hay error...
    [SerializeField] private GameObject correctSpotted;
    [SerializeField] private GameObject wrongFalseAlarm;
    [SerializeField] private GameObject wrongMissed;

    private void OnEnable()
    {

    }

    private void OnDisable()
    {

    }

    private void BuildEndGameUI()
    {
        List<ChecklistItemData> gradedElements = ChecklistSystem.Singleton.CheckChecklist();
        foreach (var check in gradedElements)
        {
            GameObject spawnedPrefab = null;

            // 1. Decide WHICH prefab to spawn based on the enum
            switch (check.itemResult)
            {
                case ChecklistItemData.Results.CorrectSafe:
                    spawnedPrefab = Instantiate(correctSafe, checklistParent.transform);
                    break;
                case ChecklistItemData.Results.CorrectSpotted:
                    spawnedPrefab = Instantiate(correctSpotted, checklistParent.transform);
                    break;
                case ChecklistItemData.Results.WrongFalseAlarm:
                    spawnedPrefab = Instantiate(wrongFalseAlarm, checklistParent.transform);
                    break;
                case ChecklistItemData.Results.WrongMissed:
                    spawnedPrefab = Instantiate(wrongMissed, checklistParent.transform);
                    break;
            }

            // 2. Give the spawned prefab the correct text!
            if (spawnedPrefab != null)
            {
                // Grab the TMP component from the prefab we just created
                TMP_Text itemText = spawnedPrefab.GetComponentInChildren<TextMeshProUGUI>();

                if (itemText != null)
                {
                    // Set the text so the player knows what item this refers to
                    itemText.text = check.checklistDefinition;
                }
            }
        }
    }
}
