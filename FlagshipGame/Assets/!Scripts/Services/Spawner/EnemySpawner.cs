using UnityEngine;
using GenericServiceLocator;
using GenericServiceLocator.Templates;
using System.Collections.Generic;
using System;
using System.Collections;
public class EnemySpawner : MonoBehaviour , IMonoService
{
    // Start is called before the first frame update
    [Header("Spawn System")]
    [SerializeField] private GameObject Bat_Prefab;
    [SerializeField] private float xOffset;
    [Tooltip("For how much time (seconds) you want to keep spawning the enemies ???")]
    [SerializeField] private float spawnTime = 10f;
    [SerializeField] private int spawnCount = 10;

    [Header("Radial Trigger")]
    [SerializeField] private float radius;
    [SerializeField] private float triggerOriginOffset = 0f;
    // These will help in handling events
    [SerializeField] public bool isPlayerDetected = false;
    [SerializeField] private bool wasPlayerDetected = false;
    [SerializeField] private bool canCheckPlayerDetection = true;

    [Header("References")]
    private EventService m_EventService;


    void Awake()
    {
        RegisterService();
    }

    void Start()
    {
        m_EventService = (ServiceLocator.Instance as IProvideService).PullService<EventService>(true);

        m_EventService.OnStartGame.AddListener(() => { canCheckPlayerDetection = true;});

    }

    // Update is called once per frame
    void Update()
    {
/*        if (Input.GetKeyDown(KeyCode.Space))
        {
            Instantiate(Bat_Prefab, (this.transform.position + this.transform.right * UnityEngine.Random.Range(-xOffset, xOffset)) , this.transform.rotation, null);
        }*/

        isPlayerDetected = (canCheckPlayerDetection) ? CheckForPlayerDetection() : false;
        CheckForDetectionEvents();
        
    }


    private void OnDrawGizmos()
    {   
        
        Gizmos.color = isPlayerDetected ? Color.green : Color.red;
        Gizmos.DrawWireSphere(this.transform.position + (Vector3.down * triggerOriginOffset), radius);

    }


    private void OnDisable()
    {

        UnregisterService();
    }




    #region Radial Trigger

    private bool CheckForPlayerDetection()
    {
        return (radius >= (Player.main.transform.position - this.transform.position).magnitude);
    }

    private void CheckForDetectionEvents()
    {
        if (isPlayerDetected == wasPlayerDetected)
            return;

        if (isPlayerDetected) // Player just got into the detection state
        {   
            m_EventService.OnPlayerDetected.InvokeEvent();
            StartCoroutine(SpawningCoroutine());
            wasPlayerDetected = true;
        }
        else // Player just went undetected
        {
            m_EventService.OnPlayerUnDetected.InvokeEvent();
            wasPlayerDetected = false;
        }

    }

    #endregion
    private IEnumerator SpawningCoroutine()
    {
        yield return new WaitForSeconds(2.5f);

        int spawnElapsed = 0;

        while (spawnElapsed < spawnCount)
        {
            yield return new WaitForSeconds(UnityEngine.Random.Range(0.1f, 0.5f));

            Transform enemy = Instantiate(Bat_Prefab, (this.transform.position + this.transform.up * UnityEngine.Random.Range(-xOffset, xOffset)), this.transform.rotation, null).GetComponent<Transform>();
            enemy.LookAt(Player.main.transform.position);
            enemy.gameObject.GetComponent<Enemy>().onDestroy += OnEnemyDestroy;
            spawnElapsed++;
        }

        while (spawnCount != 0f)
        {   
            yield return null;
        }

        canCheckPlayerDetection = false;
    }

    private void OnEnemyDestroy()
    {
        spawnCount--;
    }
    public void RegisterService()
    {
        (ServiceLocator.Instance as IProvideService).PushService(this, true);
    }

    public void UnregisterService()
    {
        (ServiceLocator.Instance as IProvideService).ClearService<EnemySpawner>();
    }
}
