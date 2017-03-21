using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityStandardAssets.Characters.ThirdPerson;
[RequireComponent(typeof(NavMeshAgent))]
public class CPU_Behavior : MonoBehaviour {
    private int currentindex = 0; 
    NavMeshAgent navigation_path;
    HashID hash; 
    Animator anim;
    public List<GameObject> waypoints;
 



    public Transform target; 
    void Awake()
    {
        navigation_path = gameObject.GetComponent<NavMeshAgent>();

        anim = gameObject.GetComponent<Animator>();

        //Note: better to initialize: VERY slow!
        if(waypoints.Count == 0)
            waypoints = new List<GameObject>(GameObject.FindGameObjectsWithTag("WayPoint"));
        
         
    }
	// Use this for initialization
	void Start () {
	    
	}
	private bool AreAllWayPointsAreOccupied()
    {
        foreach (GameObject go in waypoints)
            if (!go.gameObject.GetComponent<WaypointNavigation>().occupied) return false;
        return true;
    }
    private bool IsAtDestination()
    {
        return navigation_path.remainingDistance != 0;
    }
	// Update is called once per frame
	void Update () {
        if(!AreAllWayPointsAreOccupied())
             navigation_path.SetDestination(waypoints[currentindex].transform.position);
 


    }


}
