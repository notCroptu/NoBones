using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    private Canvas _canvas;

    void Start()
    {
        _canvas = GetComponent<Canvas>();
    }

    public void ClickPlayButton()
    {
        SceneManager.LoadScene("Miau tests");
    }

    public void ClickQuitButton()
    {
        Application.Quit();
    }

}
