using UnityEngine;
using System.Collections;
[RequireComponent(typeof(NavMeshAgent))]
public class CPU_Behavior : MonoBehaviour { 
    
    NavMeshAgent navigation_path;


    public GameObject[] waypoints;

  //  public float Forward_Distance;
   // AnimationState anims;

    public Transform target; 
    void Awake()
    {
         navigation_path = gameObject.GetComponent<NavMeshAgent>();

         
    }
	// Use this for initialization
	void Start () {
	
	}
	bool CheckIfAllWayPointsAreOccupied()
    {
        foreach(GameObject g in waypoints)
        {
            WaypointNavigation wp = g.GetComponent<WaypointNavigation>();
            if (!wp.occupied) return false;
        }
        return true; 
    }
	// Update is called once per frame
	void Update () {
        if (waypoints.Length == 0)
            navigation_path.SetDestination(gameObject.transform.position);
        
        navigation_path.SetDestination(target.position);


    }

}
