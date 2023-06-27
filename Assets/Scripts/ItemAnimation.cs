using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemAnimation : MonoBehaviour
{
    [SerializeField] private Vector3 direction;

    public float amplitude;
    public float speed;

    private Vector3 startPosition;
    private float minYPosition;

    private void Start()
    {
        startPosition = transform.position;
        minYPosition = startPosition.y;
    }

    private void Update()
    {
        transform.Rotate(direction * Time.deltaTime);

        float time = Time.time * speed;
        float newYPosition = Mathf.Lerp(minYPosition, minYPosition + amplitude, (Mathf.Sin(time) + 1f) / 2f);

        transform.position = new Vector3(transform.position.x, newYPosition, transform.position.z);
    }
}