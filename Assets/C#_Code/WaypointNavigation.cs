using UnityEngine;
using System.Collections;

public class WaypointNavigation : MonoBehaviour {
    public bool occupied;
    HashID hash;
	// Use this for initialization
    void Awake()
    {
        occupied = false; 
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "NPC")
        {
            occupied = true;
           // other.gameObject.GetComponent<Animator>().CrossFade("Idle", 0);
        }
    }
}
