using UnityEngine;
using System.Collections;

public class CPU_Behavior : MonoBehaviour { 
    NavMeshAgent navigation_path;

    Vector3 previousPos;
    Animator animate;

    public float Forward_Distance;
   // AnimationState anims;

    public Transform target; 
    void Awake()
    {
         navigation_path = gameObject.GetComponent<NavMeshAgent>();
        
    }
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (gameObject.transform.position != target.position)
            animate.SetFloat("Forward",Forward_Distance, 0.1f, Time.deltaTime);


    }
}
