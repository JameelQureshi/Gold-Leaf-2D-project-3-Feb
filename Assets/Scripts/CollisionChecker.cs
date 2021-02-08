using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionChecker : MonoBehaviour
{
    public bool IsInTheBOX = false;

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "chest")
        {
            IsInTheBOX = true;
        }
       
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.tag == "chest")
        {
            IsInTheBOX = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "chest")
        {
            IsInTheBOX = false;

            GameController.instance.CheckStatusofBoxes();
        }
    }
}
