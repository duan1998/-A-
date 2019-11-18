using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum Actors
{
    AI,
    Player
}

public class GameManager : MonoBehaviour
{
   
        


    private static GameManager m_instance;
    public static GameManager Instance
    {
        get
        {
            return m_instance;
        }
    }
    private void Awake()
    {
        m_instance = this;
    }

    private int m_currentTurnNumber;
    public int _CurrentTurnNumber
    {
        get { return m_currentTurnNumber; }
    }

    private Actors m_currentActor;

    public DiceCtrl m_diceCtrl;
    public GameUI m_GameUI;

    public PlayerCtrl m_playerCtrl;
    public AICtrl m_aiCtrl;

    public ActionCtrl m_actionCtrl;

    public void StartGame()
    {
        m_currentTurnNumber = 0;
        NewTurn(Actors.Player);
    }


    public void NextTurn()
    {
        NewTurn((Actors)(((int)(m_currentActor)) ^ 1));
    }
    public void NewTurn(Actors actor)
    {
        m_diceCtrl.GetNewThreeDice();
        m_currentActor = actor;
        m_currentTurnNumber++;
        m_GameUI.SetActonUI(m_currentActor);

        m_actionCtrl.InitActionState();
        if (m_currentActor == Actors.Player)
        {
            m_playerCtrl.m_isCanAction = true;
            m_aiCtrl.m_isCanAction = false;
        }
        else
        {
            m_playerCtrl.m_isCanAction = false;
            m_aiCtrl.m_isCanAction = true;
        }

    }

    public void ReStart()
    {
        SceneManager.LoadScene(0, LoadSceneMode.Single);
    }

}
