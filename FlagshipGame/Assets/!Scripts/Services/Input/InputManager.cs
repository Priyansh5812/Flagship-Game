using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GenericServiceLocator;
using GenericServiceLocator.Templates;

public class InputManager : MonoBehaviour , IMonoService
{
    public float HorizontalInput 
    {
        get; private set;
    }

    public float VerticalInput
    {
        get;
        private set;
    }

    [SerializeField] private float horizontalSens, verticalSens;

    private void Awake()
    {
        RegisterService();
    }

    void Update()
    {
        MonitorInput();
    }

    private void MonitorInput() 
    {   
        HorizontalInput = Input.GetAxis("Horizontal");
        VerticalInput = Input.GetAxis("Vertical");
    } 

    private void OnDisable()
    {
        UnregisterService();
    }

    public void RegisterService()
    {
        (ServiceLocator.Instance as IProvideService).PushService(this, true);
    }
    
    public void UnregisterService()
    {
        (ServiceLocator.Instance as IProvideService).ClearService<InputManager>();
    }
}
