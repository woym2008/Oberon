using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameLevelManager
{
    private static GameLevelManager Instance = null;
    //-------------------------------------------------
    private int m_CurLevel;

    private string m_SceneNameBase = "Scene/BattleScene/BattleField_";
    //-------------------------------------------------
    public static GameLevelManager getInstance()
    {
        if(Instance == null)
        {
            Instance = new GameLevelManager();
        }

        return Instance;
    }

    public GameLevelManager()
    {
        m_CurLevel = 0;
    }

    public void StartBegin()
    {
        m_CurLevel = 0;
        string levelname = m_SceneNameBase + m_CurLevel;
        SceneManager.LoadScene(levelname);
    }

    public void ToNextLevel(int nextLevel)
    {
        m_CurLevel = nextLevel;

        string levelname = m_SceneNameBase + m_CurLevel;
        SceneManager.LoadScene(levelname);
    }

    public void RestLevel()
    {
        string levelname = m_SceneNameBase + m_CurLevel;
        SceneManager.LoadScene(levelname);
    }
}
