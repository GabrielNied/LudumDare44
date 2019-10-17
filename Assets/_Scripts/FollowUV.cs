using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowUV : MonoBehaviour
{
    float parallax = 5;
    void Update()
    {
        MeshRenderer mr = GetComponent<MeshRenderer>();

        Material mat = mr.material;

        Vector2 offset = mat.mainTextureOffset;

        offset.x = transform.position.x / transform.localScale.x * parallax;
        offset.y = transform.position.y / transform.localScale.y * parallax;

        mat.mainTextureOffset = offset;
    }
}
