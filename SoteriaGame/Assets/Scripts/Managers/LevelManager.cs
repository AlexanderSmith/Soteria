using UnityEngine;
using System.Collections;

public class LevelManager : MonoBehaviour 
{
    static LevelManager _instance;
    private string currentLevelName;

    void Awake ()
    {
       if (_instance == null)
       {
          _instance = this;
           DontDestroyOnLoad(this.gameObject);
       }
       else
       {
           Destroy(this.gameObject);
       }
    }

    static LevelManager Instance
    {
        get
        {
            if (_instance == null)
            {
                GameObject levelManagerObject = GameObject.Find("Game Director");
                _instance = levelManagerObject.GetComponent<LevelManager>();
            }
            return _instance;
        }
    }

    public static string GetCurrentLevel()
    {
        return Instance.currentLevelName;
    }

    public static void LoadMainMenu()
    {
        Instance.prv_LoadMainMenu();
    }

    private void prv_LoadMainMenu()
    {
        currentLevelName = "StartMenu";
        Application.LoadLevel(currentLevelName);
    }

    public static void LoadTestLevel()
    {
        Instance.prv_LoadTestLevel();
    }

    private void prv_LoadTestLevel()
    {
        currentLevelName = "TileEventSystem";
        Application.LoadLevel(currentLevelName);
    }

    public static void LoadNextLevel()
    {
        Instance.prv_LoadNextLevel();
    }

    private void prv_LoadNextLevel()
    {
        if(Application.levelCount == 0)
            Application.LoadLevel(Application.levelCount + 1);
        else
            Application.LoadLevel(Application.levelCount - 1);
        currentLevelName = Application.loadedLevelName;
    }
}
