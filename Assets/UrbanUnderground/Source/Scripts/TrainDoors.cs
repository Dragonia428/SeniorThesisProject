using UnityEngine;
using System.Collections;

public class TrainDoors : MonoBehaviour {

	//Identifies doors based on rightdoors and leftdoors classes, sets an offset of 0.8f in each direction and handles opening and closing with Vector3.Slerp.
	//Used in demo scene to demonstrate functionality. This class is used on the animated train. The parked train works with ParkedRailcar_Interaction.cs!

	private RightDoor[] rightDoors;
	private LeftDoor[] leftDoors;

	[HideInInspector]
    public bool opened = false;
    public float speed = 1f;
    [HideInInspector]
    public bool isMoving = false;
    void Awake()
    {
        rightDoors = GetComponentsInChildren<RightDoor>();
        leftDoors = GetComponentsInChildren<LeftDoor>();
       
    }
	void Start() {

	}

	void Update() {

        if (isMoving)
            MoveDoors();
        //
		//}

	}
    public void InitializeDoors()
    {
        SetDoorVector(!opened);
        isMoving = !isMoving; 
        StartCoroutine(snapDoorsInState());
        
    }

    private void MoveDoors()
    {
        foreach (RightDoor r in rightDoors)
        {
            r.transform.position = Vector3.Slerp(r.transform.position, r.targetValue, Time.deltaTime * speed);
        }
        foreach (LeftDoor l in leftDoors)
        {
            l.transform.position = Vector3.Slerp(l.transform.position, l.targetValue, Time.deltaTime * speed);
        }
    }
    public void SetDoorVector(bool toOpen)
    {
        if (toOpen)
        {
            for (int i = 0; i < rightDoors.Length; i++)
            {
                rightDoors[i].SetDoorVector(0.8f);
                leftDoors[i].SetDoorVector(0.8f);
            }
        }
        else {
            for (int i = 0; i < rightDoors.Length; i++)
            {
                rightDoors[i].SetDoorVector(-0.8f);
                leftDoors[i].SetDoorVector(-0.8f);
            }
        }
    }
    public void OpenDoors()
    {
        //   gameObject.GetComponent<MyTrainMotion>().doorsready = true;
       
        if (isMoving)
        {
            foreach (RightDoor r in rightDoors)
            {
                r.transform.position = r.targetValue;
               
                
            }
            foreach (LeftDoor l in leftDoors)
            {

                l.transform.position = l.targetValue;
                
            }
        }

            // gameObject.GetComponent<MyTrainMotion>().doorsready = true;
            //	StartCoroutine (SnapDoorsOpen ());

        }
    public IEnumerator snapDoorsInState()
    {
        yield return new WaitForSeconds(10f);
        isMoving = false;
        opened = !opened;
        foreach (RightDoor r in rightDoors)
        {
            r.transform.position = r.targetValue;
        }
        foreach (LeftDoor l in leftDoors)
        {
            l.transform.position = l.targetValue;
        }
      //  yield return new WaitForSeconds(20f);
    }
    

	public void SecureDoors()
    {
       // doorsMoving = true; 
        if (gameObject.GetComponent<MyTrainMotion>().doorsready)
        {
            
            foreach (RightDoor r in rightDoors)
            {
                r.transform.position = new Vector3(r.transform.position.x, r.transform.position.y, r.transform.position.z + 0.8f);
                
            }

            foreach (LeftDoor l in leftDoors)
            {

                l.transform.position = new Vector3(l.transform.position.x, l.transform.position.y, l.transform.position.z - 0.8f);
                
            }


            gameObject.GetComponent<MyTrainMotion>().doorsready = false;
        }
        
        

	}

	IEnumerator SnapDoorsOpen() {
		yield return new WaitForSeconds (1f);
		SecureDoors ();
	}


}
