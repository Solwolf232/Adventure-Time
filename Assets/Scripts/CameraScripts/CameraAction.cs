using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraAction : MonoBehaviour
{
    public GameObject Cam;
    public Transform BookPos;
    public float smoothSpeed;
    private Transform CameraTransform;
    private CameraFollow CameraFollowScript;

    void Start()
    {
        CameraFollowScript = Cam.GetComponent<CameraFollow>();
        CameraTransform = Cam.GetComponent<Transform>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        CameraFollowScript.enabled = false;
        StartCoroutine(MoveCameraSmoothly());
    }

    private IEnumerator MoveCameraSmoothly()
    {
        while (Vector3.Distance(CameraTransform.position, BookPos.position) > 0.1f)
        {
            CameraTransform.position = Vector3.MoveTowards(CameraTransform.position, BookPos.position, smoothSpeed * Time.deltaTime);
            yield return null;
        }

        CameraTransform.position = BookPos.position; // Ensure exact position at the end
    }
}
