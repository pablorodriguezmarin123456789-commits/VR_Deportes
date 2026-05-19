using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_MainMenu : MonoBehaviour
{

    [SerializeField] private GameObject settingsMenu;
    [SerializeField] private Toggle teleport;
    [SerializeField] private Toggle turn;
    [SerializeField] private Toggle tunneling;
    [SerializeField] private Toggle learning;
    [SerializeField] private Slider errorsAmount;
    [SerializeField] private TextMeshProUGUI counter;
    [SerializeField] private InputField seed;
    [SerializeField] private Toggle randomSeed;


    public void SwapScene(int index)
    {
        SystemSceneManager.Singleton.LoadScene(index);
    }
    public void SettingsStateSwap()
    {
        settingsMenu.SetActive(!settingsMenu.activeInHierarchy);
    }

    public void Exit()
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
        if (learning)
        { 
            errorsAmount.gameObject.SetActive(false);
        }
        else
        {
            errorsAmount.gameObject.SetActive(true);
        }

    }
    public void ErrorsAmount()
    {
        ChecklistSystem.Singleton.errosAmount = (int)errorsAmount.value;
        counter.text = errorsAmount.value.ToString();
    }

    public void SetUpSeed()
    {

        ChecklistSystem.Singleton.useRandomSeed = randomSeed.isOn;
        ChecklistSystem.Singleton.inputSeed = seed.text;
    }
}
