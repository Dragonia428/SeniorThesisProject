using UnityEngine;
using System.Collections;

public class MyTrainMotion : MonoBehaviour {
    const int NUMBER_OF_STATIONS = 10;
    TrainDoors tr;
    public Transform[] target;
    public AudioSource[] station_messages;
    public float train_speed;
    static public bool trainStopped = false;
    [HideInInspector]
    public bool doorsready = false; 
    [HideInInspector]
    int station_tracker = 0;
    static public bool PlayerInside = false; 
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
        return CalculateRemainingDistance(the_other_object) == 0;
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
    bool DetectStation(Transform station, float how_far_away)
    {
        Ray r = new Ray(gameObject.transform.position, Vector3.forward * 5);
        return Physics.Raycast(r, how_far_away);
    }
    void Depart()
    {
        if (!AtStation(target[station_tracker]))
            MoveTrain(train_speed);
        if (DetectStation(target[station_tracker], 20f))
            Brake();
    }
    void MoveTrain(float train_speed)
    {
        gameObject.transform.position += new Vector3(0, 0, train_speed);
    }
    void Brake()
    {
           train_speed = Mathf.Lerp(train_speed, 0, Time.deltaTime);
    }
    void GoToNextStation()
    {
        tr.CloseDoors();
    }
    IEnumerator WaitInStation(float wait_time_till_departure)
    {
        tr.OpenDoors();
        yield return new WaitForSeconds(wait_time_till_departure);
       // tr.CloseDoors();
        
    }
   	// Update is called once per frame
	void Update () {
        Debug.DrawLine(gameObject.transform.position, target[station_tracker].position);
        Debug.Log(trainStopped);
        //  Depart();
        if(!doorsready)
        {
            tr.OpenDoors();
        }
     //   StartCoroutine(CheckIfStopped());
        //if (trainStopped)
      //      StartCoroutine(WaitInStation(10f));
        
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
            PlayerInside = true; 
    }
    
    
}
