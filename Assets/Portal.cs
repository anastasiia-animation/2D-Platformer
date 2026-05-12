using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    public Transform destination;

    //public Vector3 destination;
    private void OnTriggerExit2D(Collider2D collision)
    {
        collision.transform.position = destination.position;
          //collision.transform.position = destination;
    }

    
}
