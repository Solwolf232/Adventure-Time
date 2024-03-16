using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParllaxEffect : MonoBehaviour
{
    [SerializeField] Vector2 ParallaxEffect;
    private Transform cameraTransform;
    private Vector3 lastCamPos;
    private float textureUnitSizeX;

    private void Start()
    {
        cameraTransform = Camera.main.transform;
        lastCamPos = cameraTransform.position;

        Sprite sprite = GetComponent<SpriteRenderer>().sprite;
        Texture2D texture = sprite.texture;
        textureUnitSizeX = texture.width / sprite.pixelsPerUnit;
    }

    private void LateUpdate()
    {
        Vector3 deltaMovement = cameraTransform.position - lastCamPos;
        transform.position += new Vector3(deltaMovement.x * ParallaxEffect.x, deltaMovement.y * ParallaxEffect.y, 0f);
        lastCamPos = cameraTransform.position;

       
    }
}
