using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    private Animator anim;
    private bool aidInRange =  false;

    void Start()
    {
        anim = GetComponent<Animator>();

    }

    void Update()
    {
        // Any per-frame logic can go here
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Aid"))
        {
            transform.LookAt(gameObject.transform);
            anim.SetBool("aidInRange", true);
            anim.Play("grabAid");
            
        }
    }


}
