using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_InGameChecklistMenu : MonoBehaviour
{
    [Header("Checklist")]
    [SerializeField] private GameObject checklistItemPrefab;
    [SerializeField] private Transform checklistParent;
    [SerializeField] private TMP_Text failsDisplay;

    [Header("Settings Menu")]
    [SerializeField] private GameObject settingsMenu;

    [Header("Locomotion Settings")]
    [SerializeField] private Toggle teleport;
    [SerializeField] private Toggle turn;
    [SerializeField] private Toggle tunneling;
    [SerializeField] private Toggle learning;
    [SerializeField] private Slider errorsAmount;
    [SerializeField] private TextMeshProUGUI counter;
    [SerializeField] private InputField seed;
    [SerializeField] private Toggle randomSeed;

    private List<ChecklistItemData> checklistElements;
    private int totalFails;

    private void Start()
    {
        SetUpChecklist();
        SyncSettings();

        if (settingsMenu != null)
            settingsMenu.SetActive(false);
    }

    public void SetUpChecklist()
    {
        checklistElements = ChecklistSystem.Singleton.GetChecklistElements();

        foreach (Transform child in checklistParent)
        {
            Destroy(child.gameObject);
        }

        foreach (var checklistItemData in checklistElements)
        {
            GameObject uiElement = Instantiate(checklistItemPrefab, checklistParent);

            Toggle itemToggle = uiElement.GetComponentInChildren<Toggle>();
            TMP_Text itemText = uiElement.GetComponentInChildren<TMP_Text>();

            if (itemToggle != null)
            {
                itemToggle.isOn = checklistItemData.inputValue;

                itemToggle.onValueChanged.AddListener((bool isOn) =>
                {
                    checklistItemData.SetPlayerValue(isOn);
                });
            }

            if (itemText != null)
            {
                itemText.text = checklistItemData.checklistDefinition;
            }
        }
    }

    public void MidGameCheck()
    {
        totalFails = 0;

        foreach (var item in checklistElements)
        {
            if (item.isNotCorrect != item.inputValue)
            {
                totalFails++;
            }
        }

        if (totalFails == 0)
        {
            failsDisplay.text = "¡Todo correcto!";
            failsDisplay.color = Color.green;
        }
        else
        {
            failsDisplay.text = "Errores encontrados: " + totalFails;
            failsDisplay.color = Color.red;
        }
    }

    public void SettingsStateSwap()
    {
        settingsMenu.SetActive(!settingsMenu.activeInHierarchy);
    }

    public void Continue()
    {
        gameObject.SetActive(false);
    }

    public void GoToMainMenu()
    {
        SystemSceneManager.Singleton.LoadScene(1);
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void TeleportOn()
    {
        InputManager.Singleton.teleport = teleport.isOn;
    }

    public void TurnOn()
    {
        InputManager.Singleton.turner = turn.isOn;
    }

    public void TunnelingOn()
    {
        InputManager.Singleton.tunneling = tunneling.isOn;
    }

    public void LearningOn()
    {
        ChecklistSystem.Singleton.learning = learning.isOn;

        errorsAmount.gameObject.SetActive(!learning.isOn);
        seed.gameObject.SetActive(!learning.isOn);
    }

    public void ErrorsAmount()
    {
        ChecklistSystem.Singleton.errosAmount = (int)errorsAmount.value;
        counter.text = errorsAmount.value.ToString();
    }


    private void SyncSettings()
    {
        teleport.isOn = InputManager.Singleton.teleport;
        turn.isOn = InputManager.Singleton.turner;
        tunneling.isOn = InputManager.Singleton.tunneling;

        learning.isOn = ChecklistSystem.Singleton.learning;
        errorsAmount.value = ChecklistSystem.Singleton.errosAmount;
        counter.text = errorsAmount.value.ToString();

        seed.text = ChecklistSystem.Singleton.inputSeed;
        randomSeed.isOn = ChecklistSystem.Singleton.useRandomSeed;

        LearningOn();
    }
}