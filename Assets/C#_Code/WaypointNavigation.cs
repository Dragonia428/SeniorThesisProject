using UnityEngine;
using System.Collections;

public class WaypointNavigation : MonoBehaviour {
    public bool occupied = false; 
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player" || other.tag == "NPC" )
        {
            occupied = true; 
        }
    }
}
