using UnityEngine;
using System.Collections;

public class TrainDoors : MonoBehaviour {

	//Identifies doors based on rightdoors and leftdoors classes, sets an offset of 0.8f in each direction and handles opening and closing with Vector3.Slerp.
	//Used in demo scene to demonstrate functionality. This class is used on the animated train. The parked train works with ParkedRailcar_Interaction.cs!

	private RightDoor[] rightDoors;
	private LeftDoor[] leftDoors;

	[HideInInspector]
	public bool doorsMoving = false;

	void Start() {
		rightDoors = GetComponentsInChildren<RightDoor>();
		leftDoors = GetComponentsInChildren<LeftDoor>();
	}

	void Update() {
        /*
		if (doorsMoving) {
			foreach ( RightDoor r in rightDoors) {
				r.transform.position = Vector3.Slerp(r.transform.position, r.targetValue, Time.deltaTime);
			}
			foreach ( LeftDoor l in leftDoors) {
				l.transform.position = Vector3.Slerp(l.transform.position, l.targetValue, Time.deltaTime);
			}
		}
        */
	}

	public void OpenDoors() {
		foreach ( RightDoor r in rightDoors) {
			r.SetDoorVector(0.8f);
		}
		foreach ( LeftDoor l in leftDoors) {
			l.SetDoorVector(-0.8f);
		}
		doorsMoving = true;
		StartCoroutine (SnapDoorsOpen ());
		//FindObjectOfType<Train1> ().trainStopped = true;
	}

	public void CloseDoors() {
		foreach ( RightDoor r in rightDoors) {
			r.SetDoorVector(-0.8f);
		}
		foreach ( LeftDoor l in leftDoors) {
			l.SetDoorVector(-0.8f);
		}
		doorsMoving = true;
	}

	public void SecureDoors() {
		foreach ( RightDoor r in rightDoors) {
            r.transform.position = new Vector3(r.transform.position.x, r.transform.position.z + 0.8f);
		}

		foreach ( LeftDoor l in leftDoors) {

            l.transform.position = new Vector3(l.transform.position.x, l.transform.position.z - 0.8f);
        }
	}

	IEnumerator SnapDoorsOpen() {
		yield return new WaitForSeconds (1f);
		SecureDoors ();
	}


}
