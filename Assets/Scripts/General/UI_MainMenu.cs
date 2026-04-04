using UnityEngine;

public class UI_MainMenu : MonoBehaviour
{
    SystemSceneManager manager;

    [SerializeField] private GameObject settingsMenu;
    public void SwapScene(int index)
    {
        manager.LoadScene(index);
    }
    public void SettingsStateSwap()
    {
        settingsMenu.SetActive(!settingsMenu);
    }

    public void Exit()
    {
        Application.Quit();
    }
}
