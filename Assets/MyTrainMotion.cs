using UnityEngine;
using System.Collections;

public class MyTrainMotion : MonoBehaviour {
    const int NUMBER_OF_STATIONS = 10;
    TrainDoors tr;
    public Transform[] target;
    public float stopping_speed;
    public float train_speed; 
    [HideInInspector]
    public bool trainStopped = false;
    [HideInInspector]
    int station_tracker = 0;

	// Use this for initialization
	void Start () {
        target = new Transform[NUMBER_OF_STATIONS];
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
    bool Stopped()
    {
        return trainStopped;
    }
    bool DetectStation(Transform station)
    {
        return Physics.Raycast(gameObject.transform.position, station.position, 10f);
    }
    void Depart()
    {
        if(!AtStation(target[station_tracker]))
        gameObject.transform.position += new Vector3(0, 0, train_speed);
    }
    void Brake()
    {
        if(DetectStation(target[station_tracker]))
        {
            train_speed = Mathf.Lerp(train_speed, 0, Time.deltaTime);
        }
    }
	// Update is called once per frame
	void Update () {
        DepartFromStation(5f);

	}
    void OnTriggerEnter(Collider other)
    {

    }
}
