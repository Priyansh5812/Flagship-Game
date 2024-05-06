using System;
using GenericServiceLocator;
using GenericServiceLocator.Templates;
using UnityEngine;

public class EventService : INonMonoService
{
    public EventService() 
    {
        OnPlayerDetected = new EventController();
        OnPlayerUnDetected = new EventController();
        OnDamagePlayer = new EventController<int>();
        OnChangeGameState = new EventController<GameState>();
        OnStartGame = new EventController();
    }


    #region Events

    public EventController<int> OnDamagePlayer;   // Event for Player recieving damage.
    public EventController<GameState> OnChangeGameState;
    
    public EventController // Used in radial trigger events 
        OnPlayerDetected,
        OnPlayerUnDetected;

    public EventController OnStartGame; // Can also be used for restart

    #endregion
}





#region EventController

public class EventController
{
    public event Action baseEvent;
    public void AddListener(Action Listener) => baseEvent += Listener;
    public void RemoveListener(Action Listerner) => baseEvent -= Listerner;
    public void InvokeEvent() => baseEvent?.Invoke();

    public void ClearListeners()
    {   
        foreach (var i in baseEvent.GetInvocationList())
        {
            baseEvent -= (i as Action);
        }
    }

}


public class EventController<T>
{
    public event Action<T> baseEvent;
    public void AddListener(Action<T> Listener) => baseEvent += Listener;
    public void RemoveListener(Action<T> Listerner) => baseEvent -= Listerner;
    public void InvokeEvent(T val) => baseEvent?.Invoke(val);
    public void ClearListeners()
    {
        foreach (var i in baseEvent.GetInvocationList())
        {
            baseEvent -= (i as Action<T>);
        }
    }


}

#endregion EventController