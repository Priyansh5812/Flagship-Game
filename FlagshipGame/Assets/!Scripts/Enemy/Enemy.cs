using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GenericServiceLocator;
using GenericServiceLocator.Templates;
using System;


#region Interfaces
public interface IDamagable
{
    public void GetDamage(int damage);
}
public interface IYieldProp
{
    public GameObject GetProp();
}
#endregion



/// <summary>
/// This is a driver class which is responsible for executing required functions
/// </summary>

[RequireComponent(typeof(EnemyMovement))]
public class Enemy : MonoBehaviour, IDamagable, IYieldProp
{

    [SerializeField] private SO_Enemy ReadOnly_Obj; 
    [SerializeField] private event Action moveDel;
    [Header("Component References")]
    [SerializeField] private EnemyMovement m_EnemyMovement;
    [SerializeField] private EnemyStats m_EnemyStats;

    #region Events
    [SerializeField] private event Action<int> e_ReceiveDamage;
    [SerializeField] public event Action onDestroy;
    #endregion

    

    private void Awake()
    {
        
        if (this.TryGetComponent<EnemyMovement>(out m_EnemyMovement))
        {
            moveDel += m_EnemyMovement.Move; // Basic Translation
            switch (ReadOnly_Obj.type)
            {
                case EnemyType.BAT:
                    moveDel += m_EnemyMovement.Apply_SinVariations;
                    break;
                case EnemyType.BEE:
                    //TODO
                    break;
                case EnemyType.DRAGON_RIDER:
                    //TODO
                    break;
            
            }
        }

        if (m_EnemyStats == null)
            m_EnemyStats = new EnemyStats(ReadOnly_Obj);
        else
            m_EnemyStats.ReviseStats(ReadOnly_Obj); // This part will come when Object Pooling is done


        
    }

    private void OnEnable()
    {
        InitializationEvents();
    }


    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        moveDel.Invoke(); // Movement for a paticular 

        if (Input.GetKeyDown(KeyCode.Space))
        {
            IDamagable i_ref;
            Player.main.TryGetComponent<IDamagable>(out i_ref);
            m_EnemyStats.ProvideDamage(i_ref);
        }
    }

    public void GetDamage(int damage)
    {
        m_EnemyStats.RecieveDamage(damage);
        if (m_EnemyStats.Health == 0)
        {   
            
            // OnDestroyEnemy
        }
    }

    public GameObject GetProp()
    {
        return null;
    }


    private void OnDisable()
    {
        DeinitializationEvents();
    }

    private void OnDestroy()
    {
        onDestroy.Invoke();
    }


    private void InitializationEvents()
    {
        e_ReceiveDamage += m_EnemyStats.RecieveDamage;

    }
    private void DeinitializationEvents()
    {
        e_ReceiveDamage -= m_EnemyStats.RecieveDamage;
    }


    #region Non Mono Classes

    private class EnemyStats
    {
        public int Health;
        public int Damage;

        public EnemyStats(SO_Enemy obj)
        { 
            this.Health = obj.Health;
            this.Damage = obj.Damage;
        }

        // Re-initialization Purposes
        public void ReviseStats(SO_Enemy obj)
        {
            this.Health = obj.Health;
            this.Damage = obj.Damage;
        }

        public void RecieveDamage(int d_Amount)
        {
            Health -= d_Amount;
            if(Health < 0)
                Health = 0;
        }

        public void ProvideDamage(IDamagable obj)
        { 
            obj.GetDamage(Damage);
        }

    }

    #endregion


}


