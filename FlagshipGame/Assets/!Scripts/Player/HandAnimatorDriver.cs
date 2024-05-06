using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Unity.Collections;
using Unity.Burst.CompilerServices;
public class HandAnimatorDriver : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [Range(-1f , 1f)]
    [SerializeField] private float XInput;
    [Range(-1f , 1f)]
    [SerializeField] private float YInput;

    [SerializeField] private GameObject hint;
    [SerializeField] private Transform relativeOrigin_Transform;

    #region hint Positions
    private Vector3 originalPos;
    private Vector3 downRightAnim_HintPos;
    #endregion



    void Start()
    {   
        originalPos = hint.transform.localPosition;
        downRightAnim_HintPos = new Vector3(0.573f, 1.315f, -0.561f); 

    }

    // Update is called once per frame
    void Update()
    {
        _animator.SetFloat("BlendX", Input.GetAxis("Horizontal"));
        _animator.SetFloat("BlendY", Input.GetAxis("Vertical"));

        if (Input.GetAxisRaw("Horizontal") == 1f && Input.GetAxisRaw("Vertical") == -1f) // Down Right
            hint.transform.position = relativeOrigin_Transform.TransformPoint(downRightAnim_HintPos);
        else 
            hint.transform.position = relativeOrigin_Transform.TransformPoint(originalPos);
        

    }



}
