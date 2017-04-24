using UnityEngine;
using System.Collections;

public class AddPlayerToCar : MonoBehaviour {

    void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "Player" || collision.collider.tag == "NPC")
        collision.transform.SetParent(this.transform);
    }
    void OnCollisionExit(Collision collision)
    {
        if(collision.collider.tag == "Player" || collision.collider.tag == "NPC")
        collision.transform.SetParent(collision.transform);
    }
}
