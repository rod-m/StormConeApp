using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaGizmos : MonoBehaviour
{
    [SerializeField] private Transform bandstand;
    [SerializeField] private float yellowRadius;
    [SerializeField] private float redRadius;
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Vector3 bandstandPos = new Vector3(bandstand.position.x, bandstand.position.y, bandstand.position.z);
        Gizmos.DrawWireSphere(bandstandPos, yellowRadius);
        
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(bandstandPos, redRadius);
}
}
