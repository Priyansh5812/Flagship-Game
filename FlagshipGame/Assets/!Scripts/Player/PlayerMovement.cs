using GenericServiceLocator;
using GenericServiceLocator.Templates;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;
using UnityEngine.Splines;

public class PlayerMovement : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private Animator m_Animator;
    [SerializeField] private SplineAnimate m_SplineAnimator;
    [SerializeField] private EventService m_EventService;
    [SerializeField] private RigBuilder m_RigBuilder;
    [SerializeField] private List<RigLayer> rigLayers;
    [SerializeField] private bool isFinished = false;
    void Awake()
    {
        this.TryGetComponent<Animator>(out m_Animator);
        this.TryGetComponent<SplineAnimate>(out m_SplineAnimator);
        if (this.TryGetComponent<RigBuilder>(out m_RigBuilder))
        {
            rigLayers = m_RigBuilder.layers;
        }
        m_EventService = (ServiceLocator.Instance as IProvideService).PullService<EventService>(true);
        m_EnemySpawner = (ServiceLocator.Instance as IProvideService).PullService<EnemySpawner>(true);
    }

    private void OnEnable()
    {
        m_EventService.OnPlayerUnDetected.AddListener(StartPlayerMovement);
        m_EventService.OnPlayerDetected.AddListener(StopPlayerMovement);
        m_EventService.OnStartGame.AddListener(StartPlayerMovement);

    }

    private void Update()
    {
        
    }

    private void OnDisable()
    {
        m_EventService.OnPlayerUnDetected.RemoveListener(StartPlayerMovement);
        m_EventService.OnPlayerDetected.RemoveListener(StopPlayerMovement);
        m_EventService.OnStartGame.RemoveListener(StartPlayerMovement);
    }


    private void StartPlayerMovement()
    {   
        m_SplineAnimator.Play();
        foreach (var i in rigLayers)
        {
            i.rig.weight = 0f;
        }
        m_Animator.CrossFadeInFixedTime("Running",0.720f);
    }

    private void StopPlayerMovement()
    {
        m_SplineAnimator.Pause();
        foreach (var i in rigLayers)
        {
            i.rig.weight = 1f;
        }
        m_Animator.CrossFadeInFixedTime("Idle", 0.120f);
    }

    

}
