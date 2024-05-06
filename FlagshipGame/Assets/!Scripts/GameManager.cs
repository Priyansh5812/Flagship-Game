using GenericServiceLocator;
using GenericServiceLocator.Templates;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum GameState
{   
    ST_MENU,
    ST_LVL1,
    ST_LVL2,
    ST_LVL3,
    ST_WIN,
    ST_LOST
}

public class GameManager
{
    private  static GameManager m_instance = null; 
    
    public static GameManager m_Instance
    {
        get
        { 
            if(m_instance == null)
                m_instance = new GameManager();
            return m_instance;
        }
    }

    private GameManager() 
    {
        Initialization();
        SubscribeEvents();
    }

    #region References
    private GameState m_State;
    private EventService m_EventService;
 
    #endregion

    #region Functions

    private void Initialization()
    { 
        m_State = GameState.ST_MENU;
        m_EventService = (ServiceLocator.Instance as IProvideService).PullService<EventService>(true);
    }

    private void SubscribeEvents()
    {
        if (m_EventService != null)
        {
            m_EventService.OnChangeGameState.AddListener(OnChangeGameState);
        }
    }

    private void UnSubscribeEvents()
    {
        if (m_EventService != null)
        {
            m_EventService.OnChangeGameState.RemoveListener(OnChangeGameState);
        }
    }

    private void OnChangeGameState(GameState state)
    { 
        this.m_State = state;
        Debug.LogWarning($"State Changed to {this.m_State}");
    }
    #endregion


    ~GameManager() 
    {
        UnSubscribeEvents();
        Debug.Log("Game Manager Destructed");
    }


}
