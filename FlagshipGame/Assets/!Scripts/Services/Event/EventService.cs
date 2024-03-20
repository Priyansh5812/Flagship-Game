using System;
using GenericServiceLocator;
using GenericServiceLocator.Templates;
using UnityEngine;

public class EventService : INonMonoService
{
    private EventService()
    {
       
    }


    public static EventService CreateInstance(System.Type caller)
    {
        if (caller == typeof(ServiceLocator))
        {
            return new();
        }
        else 
        {
            Debug.Log("Instance creation access is forbidden for the type : " +caller);
            return null;
        }
        
    }



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
            baseEvent -= i as Action;
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
            baseEvent -= i as Action<T>;
        }
    }


}

#endregion EventController