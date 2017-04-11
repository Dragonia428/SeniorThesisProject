using UnityEngine;
using System.Collections;

public class MyTrainMotion : MonoBehaviour {
    const int NUMBER_OF_STATIONS = 10;
    TrainDoors tr;
    public GameObject[] targets;
    public AudioSource[] station_messages;
    public float train_speed = 1.5f;
    static public bool trainStopped = false;
    [HideInInspector]
    public bool doorsready = true; 
    public int station_tracker = 0;
    static public bool PlayerInside = false;
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
    void GoToNextStation()
    {
        trainStopped = false;
        station_tracker++;
        return; 
    }
    IEnumerator WaitInStation(float wait_time_till_departure)
    {
        bool executed = false;
        if (!tr.isMoving && !executed)
        {
            tr.SetDoorVector(!tr.opened);
            tr.isMoving = !tr.isMoving;
            tr.StartCoroutine(tr.snapDoorsInState());
            yield return new WaitForSeconds(wait_time_till_departure);
            executed = true;
           
           
        }
        Invoke("GoToNextStation", wait_time_till_departure+5);
        // station_tracker++;
        //  station_tracker++;

        // tr.CloseDoors();

    }
   	// Update is called once per frame
	void Update () {
        //  Debug.DrawLine(gameObject.transform.position, target[station_tracker].position);
        //   Debug.Log(DetectStation(target[station_tracker]));
        try {
            Depart();
        }
        catch(System.IndexOutOfRangeException)
        {
            Brake();
        }
        
        StartCoroutine(CheckIfStopped());
        if(trainStopped)
        {
            StartCoroutine(WaitInStation(5f));
            trainStopped = false; 
            //station_tracker++;
        }
        
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            //PlayerInside = true;
            other.transform.SetParent(gameObject.transform);
        }
    }
    
    
    
    
}
