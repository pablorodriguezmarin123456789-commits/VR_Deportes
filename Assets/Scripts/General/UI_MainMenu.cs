using UnityEngine;
using UnityEngine.UI;

public class UI_MainMenu : MonoBehaviour
{

    [SerializeField] private GameObject settingsMenu;
    [SerializeField] private Toggle teleport;
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
}
