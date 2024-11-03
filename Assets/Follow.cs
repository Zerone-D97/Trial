using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follow : MonoBehaviour
{
    public GameObject V1;
    public float speed;

    void Start()
    {
        // You could initialize taggedObjects here if needed later
    }

    void OnCollisionEnter(Collision col)
    {
        if (col.transform.CompareTag("Aid"))
        {
            

            // Move V1 towards the collided object's position
            V1.transform.position = Vector3.MoveTowards(V1.transform.position, col.transform.position, speed * Time.deltaTime);
        }
    }
}
