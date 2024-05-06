using GenericServiceLocator;
using GenericServiceLocator.Templates;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// This class will contain the player component itself and the metadata of the player like... life.
/// With its Nested class named 'PlayerStats'
/// </summary>
/// 
[RequireComponent(typeof(PlayerMovement))]
public class Player : MonoBehaviour , IDamagable
{
    public static Transform main;
    private EventService m_EventService;
    [SerializeField] private PlayerStats m_PlayerStats;


    private void Awake()
    {
        Physics.gravity = Vector3.zero;
        m_EventService = (ServiceLocator.Instance as IProvideService).PullService<EventService>(true);

    }

    private void OnEnable()
    {
        main = this.transform;
        m_PlayerStats = new();
        m_EventService.OnDamagePlayer.AddListener(m_PlayerStats.GetDamage);

    }
    void Start()
    {

        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            m_EventService.OnStartGame.InvokeEvent();
        }
    }

    public void GetDamage(int damage)
    {
        m_EventService.OnDamagePlayer.InvokeEvent(damage);
    }



    [System.Serializable]
    private class PlayerStats 
    {

        public float Health;
        

        public PlayerStats()
        {
            this.Health = 100;
        }

        public void GetDamage(int d_Amount)
        {
            Health -= d_Amount;
            if (Health < 0)
                Health = 0;
        }
    }

}
