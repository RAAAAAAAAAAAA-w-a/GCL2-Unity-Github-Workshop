using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void NewGame()
    {
        SceneManager.LoadScene("Opening Scene");
    }

    public void ContinueGame()
    {
        SceneManager.LoadScene("Opening Scene");
    }

    public void Credits()
    {
        SceneManager.LoadScene("Credits");
    }


}
