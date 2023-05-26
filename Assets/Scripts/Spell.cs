using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Spell : MonoBehaviour
{
    protected Vector3 actionDirection;

    Vector3 scale;

    protected bool wasCollided = false;

    public float manaCost;

    // Start is called before the first frame update
    protected void Start()
    {
        scale = transform.localScale;
    }

    protected void Update()
    {
        if (!wasCollided) ActionBeforeTriggering();
        else Action();
    }

    protected void ActionBeforeTriggering()
    {
        transform.Translate(actionDirection * Time.deltaTime * 10);
    }

    protected void Action()
    {
        transform.localScale = scale * 20;
        GetComponent<Rigidbody>().isKinematic = true;
        GetComponent<SphereCollider>().isTrigger = true;
    }

    public void SetPlayerDirection(Vector3 dir)
    {
        actionDirection = dir;
    }

    private void OnCollisionEnter(Collision collision)
    {
        wasCollided = true;
    }
}
