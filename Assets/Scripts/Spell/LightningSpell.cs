using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightningSpell : Spell
{
    [SerializeField] private float lightningSpeed;

    // Start is called before the first frame update
    new void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    new void Update()
    {
        if (state == State.Launched) ActionBeforeTriggering();
        else if (state == State.Collided) ActionOnCollision();
        else if (state == State.WasCollided) ActionAfterTriggering();
    }

    new protected void ActionBeforeTriggering()
    {
        transform.Translate(transform.forward * Time.deltaTime * lightningSpeed);
    }

    new protected void ActionAfterTriggering()
    {
    }

    new protected void ActionOnCollision()
    {
    }

    new protected void OnCollisionEnter(Collision collision)
    {
        base.OnCollisionEnter(collision);
        timeFirstCollision = Time.time;
    }

}
