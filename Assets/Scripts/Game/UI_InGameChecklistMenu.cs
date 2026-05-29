using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_InGameChecklistMenu : MonoBehaviour
{
    [Header("Checklist")]
    [SerializeField] private GameObject checklistItemPrefab;
    [SerializeField] private GameObject checkListPanel;
    [SerializeField] private Transform checklistParent;
    [SerializeField] private TMP_Text failsDisplay;

    [Header("Settings Menu")]
    [SerializeField] private GameObject settingsMenu;
    [SerializeField] private GameObject panelMenuIngame;

    [Header("Locomotion Settings")]
    [SerializeField] private Toggle teleport;
    [SerializeField] private Toggle turn;
    [SerializeField] private Toggle tunneling;

    private List<ChecklistItemData> checklistElements;
    private int totalFails;
    public bool OnMainMenu;
    public static UI_InGameChecklistMenu Singleton;


    private void Awake()
    {
        if (Singleton == null)
        {
            Singleton = this;

        }
        else
        {
            Destroy(gameObject);
        }
    }
    private void Start()
    {
        SyncSettings();
        panelMenuIngame.SetActive(false);
        if (settingsMenu != null)
            settingsMenu.SetActive(false);
    }

    private void Update()
    {
        if (OnMainMenu)
        {
            panelMenuIngame.SetActive(false);
        }
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
        checkListPanel.SetActive(false);
        settingsMenu.SetActive(!settingsMenu.activeInHierarchy);
    }

    public void OpenCanvasList()
    {
        if(!OnMainMenu)
            panelMenuIngame.SetActive(!panelMenuIngame.activeInHierarchy);
    }

    public void CheckListSwapState()
    {
        settingsMenu.SetActive(false);
        checkListPanel.SetActive(!checkListPanel.activeInHierarchy);
    }

    public void Continue()
    {
        checkListPanel.SetActive(false);
    }

    public void GoToMainMenu()
    {
        OnMainMenu = true;
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


    private void SyncSettings()
    {
        teleport.isOn = InputManager.Singleton.teleport;
        turn.isOn = InputManager.Singleton.turner;
        tunneling.isOn = InputManager.Singleton.tunneling;
    }
}