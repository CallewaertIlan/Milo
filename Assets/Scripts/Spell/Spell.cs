using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Spell : MonoBehaviour
{ 
    public enum State
    {
        Launched,
        Collided,
        WasCollided,
    }

    protected Vector3 actionDirection;

    protected State state;

    public float manaCost;

    protected float timeFirstCollision;

    // Start is called before the first frame update
    protected void Start()
    {
        state = State.Launched;
    }

    protected void Update()
    {

    }

    protected void ActionBeforeTriggering()
    {
        // Action avant la collision
        Debug.Log("before collision");
    }

    protected void ActionAfterTriggering()
    {
        // Action apres la collision
        Debug.Log("after collision");
    }

    protected void ActionOnCollision()
    {
        // Action au moment de la collision
        Debug.Log("on collision");
    }

    public void SetDirection(Vector3 dir)
    {
        transform.forward = dir;
    }

    protected void OnCollisionEnter(Collision collision)
    {
        state = State.Collided;
    }

    protected void OnCollisionExit(Collision collision)
    {
        
    }
}
