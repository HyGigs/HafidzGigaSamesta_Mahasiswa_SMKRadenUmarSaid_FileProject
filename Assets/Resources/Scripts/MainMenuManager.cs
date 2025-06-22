using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    public void On_SceneA_Pressed()
    {
        SceneManager.LoadSceneAsync(1);
    }

    public void On_SceneB_Pressed()
    {
        SceneManager.LoadSceneAsync(2);
    }

    public void On_SceneC_Pressed()
    {
        SceneManager.LoadSceneAsync(3);
    }

    public void On_SceneD_Pressed()
    {
        SceneManager.LoadSceneAsync(4);
    }

    public void On_SceneE_Pressed()
    {
        SceneManager.LoadSceneAsync(5);
    }

    public void On_SceneF_Pressed()
    {
        SceneManager.LoadSceneAsync(6);
    }

    public void On_SceneG_Pressed()
    {
        SceneManager.LoadSceneAsync(7);
    }

    public void On_SceneH_Pressed()
    {
        SceneManager.LoadSceneAsync(8);
    }

    public void OnQuitPressed()
    {
        Application.Quit();
    }
}
