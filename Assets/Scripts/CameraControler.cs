using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControler : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private Vector3 offset;

    [SerializeField] private float verticalMouseSensi;
    [SerializeField] private float horizontalMouseSensi;

    [SerializeField] private float maxHeight;
    [SerializeField] private float minHeight;

    // Start is called before the first frame update
    void Start()
    {
        offset = target.position - transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 cameraMouvement = new Vector2(-Input.GetAxis("Mouse Y") * horizontalMouseSensi, Input.GetAxis("Mouse X") * verticalMouseSensi);

        MoveWithZ0(cameraMouvement);

        if (transform.eulerAngles.x > minHeight && transform.eulerAngles.x < maxHeight)
        {
            MoveWithZ0(-cameraMouvement);
        }

        transform.position = target.position - offset;
    }

    private void MoveWithZ0(Vector2 move)
    {
        transform.Rotate(move);
        transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, 0);
    }
}