using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_MainMenu : MonoBehaviour
{

    [SerializeField] private GameObject settingsMenu;
    [SerializeField] private Toggle teleport;
    [SerializeField] private Toggle learning;
    [SerializeField] private Slider errorsAmount;
    [SerializeField] private TextMeshProUGUI counter;


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

    public void LearningOn()
    {
        GameSettings.Singleton.Learning = learning.isOn;
    }
    public void ErrorsAmount()
    {
        GameSettings.Singleton.errorscount = (int)errorsAmount.value;
        counter.text = errorsAmount.value.ToString();
    }
}
