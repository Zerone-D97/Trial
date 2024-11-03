using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VictimSpawn : MonoBehaviour
{
    public GameObject Victim;
    public int xPos;
    public int zPos;
    public int victimCount;
    
    void Start()
    {
        StartCoroutine(VictimDrop());
    }
    
       IEnumerator VictimDrop()
       {
        while (victimCount <= 3)
        {
            xPos = Random.Range(-5,6);
            zPos = Random.Range(-5,9);
            Instantiate(Victim, new Vector3(xPos, -2.3f, zPos), Quaternion.identity);
            yield return new WaitForSeconds(100f);
            victimCount += 1;
        }

       }
    

    
}