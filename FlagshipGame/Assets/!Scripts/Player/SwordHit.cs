using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordHit : MonoBehaviour
{


    private void OnTriggerEnter(Collider other)
    {
        
        if (Input.GetAxisRaw("Horizontal") != 0f && Input.GetAxisRaw("Vertical") != 0f)
        {
            Destroy(other.gameObject);
        }
    }
}
