using UnityEngine;
using System.Collections;

public class MyTrainMotion : MonoBehaviour {
    const int NUMBER_OF_STATIONS = 10;
    TrainDoors tr;
    public GameObject[] targets;
    public AudioSource[] station_messages;
    public float train_speed = 1.5f;
    [HideInInspector]
    static public bool trainStopped = false;
    [HideInInspector]
    public bool doorsready = true; 
    public int station_tracker = 0;
    static public bool PlayerInside = false;
    [HideInInspector]
    private bool executed = false;
    [HideInInspector]
    private bool executed2 = false;
    [HideInInspector]
    private bool executed3 = false; 
   void Awake()
    {
       // target = new GameObject[NUMBER_OF_STATIONS];
        station_tracker = 0;
        if(targets==null || targets.Length == 0)
             targets = GameObject.FindGameObjectsWithTag("Station");
    }
	// Use this for initialization
	void Start () {
        tr = gameObject.GetComponent<TrainDoors>();
     
	}
	int CalculateRemainingDistance(Transform the_other_object)
    {
        
            return (int)(the_other_object.position.z - gameObject.transform.position.z);
        
        
    }
    bool AtStation(Transform the_other_object)
    {
        try {
            return CalculateRemainingDistance(the_other_object) == 0;
        }
        catch(System.IndexOutOfRangeException)
        {
            //Debug.Log(station_tracker);
            return false; 
        }
    }
    IEnumerator DepartFromStation(float waittime)
    {
        yield return new WaitForSeconds(waittime);
        Depart();
    }
    IEnumerator CheckIfStopped()
    {
        Vector3 currpos = gameObject.transform.position;
        yield return new WaitForSeconds(1f);
        Vector3 nextpos = gameObject.transform.position;
        trainStopped = currpos == nextpos ? true : false;

    }
    bool DetectStation(Transform station)
    {
        return Vector3.Distance(gameObject.transform.position, station.transform.position) < 100;
    }
    void Depart()
    {
       
        if (!AtStation(targets[station_tracker].transform))
            MoveTrain(train_speed);
        if (DetectStation(targets[station_tracker].transform))
            Brake();
    }
    void MoveTrain(float train_speed)
    {
        gameObject.transform.position += new Vector3(0, 0, train_speed);

    }
    void Brake()
    {
        gameObject.transform.position += new Vector3(0, 0, 0);
    }
    void DoorsClosing()
    {
            
        
            tr.SetDoorVector(!tr.opened);
            tr.isMoving = !tr.isMoving;
            tr.StartCoroutine(tr.snapDoorsInState());
            executed2 = true;
    }
    void GoToNextStation()
    {
        executed3 = true;
        station_tracker++;
    }
    IEnumerator WaitInStation(float wait_time_till_departure)
    {
            executed = true;

            tr.SetDoorVector(!tr.opened);
            tr.isMoving = !tr.isMoving;
            tr.StartCoroutine(tr.snapDoorsInState());
        yield return new WaitForSeconds(wait_time_till_departure);
    }
   	// Update is called once per frame
	void Update () {
        //Debug.Log(trainStopped);
        try {
            Depart();
        }
        catch(System.IndexOutOfRangeException)
        {
            Brake();
        }
        
        StartCoroutine(CheckIfStopped());
        if(trainStopped && !executed)
        {
            StartCoroutine(WaitInStation(5f));
            //station_tracker++;
        }
        if(trainStopped && !executed2 && tr.opened)
        {
            DoorsClosing();
        }
        if(trainStopped && executed && executed2 && !executed3)
        {
           //Invoke("GoToNextStation", 10f);
        }
    }
    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {

        }
    }
    
    
    
    
}
