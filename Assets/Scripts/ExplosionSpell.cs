using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionSpell : Spell
{
    [SerializeField] private float timeBeforeExplosionAfterCollision;

    private Vector3 scale;

    // Start is called before the first frame update
    new void Start()
    {
        base.Start();
        scale = transform.localScale;
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
        transform.Translate(actionDirection * Time.deltaTime * 10);
    }

    new protected void ActionAfterTriggering()
    {
        transform.localScale = scale * 20;
        GetComponent<Rigidbody>().isKinematic = true;
        GetComponent<SphereCollider>().isTrigger = true;
    }

    new protected void ActionOnCollision()
    {
        GetComponent<Rigidbody>().isKinematic = true;
        GetComponent<SphereCollider>().isTrigger = true;

        if (Time.time > timeFirstCollision + timeBeforeExplosionAfterCollision) state = State.WasCollided;
    }

    new protected void OnCollisionEnter(Collision collision)
    {
        base.OnCollisionEnter(collision);
        timeFirstCollision = Time.time;
    }

}
