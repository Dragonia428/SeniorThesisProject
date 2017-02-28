using UnityEngine;
using System.Collections;

public class CPU_Behavior : MonoBehaviour {
    NavMeshAgent navigation_path;
    NavMeshHit target; 
    void Awake()
    {
         navigation_path = gameObject.GetComponent<NavMeshAgent>();
        
    }
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	    
	}
}
