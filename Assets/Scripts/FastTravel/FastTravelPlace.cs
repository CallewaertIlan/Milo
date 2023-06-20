using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FastTravelPlace : MonoBehaviour
{
    public Vector3 fastTravelPosition;

    // Start is called before the first frame update
    void Start()
    {
        fastTravelPosition = new Vector3(transform.position.x, transform.position.y + 1.6f, transform.position.z);
    }


}
