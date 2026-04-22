using UnityEngine;

public class UI_MainMenu : MonoBehaviour
{

    [SerializeField] private GameObject settingsMenu;
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
}
