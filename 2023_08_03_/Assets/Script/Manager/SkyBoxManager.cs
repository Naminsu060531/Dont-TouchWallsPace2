using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkyBoxManager : MonoBehaviour
{
    public float rotateSpeed;

    private void Update()
    {
        RenderSettings.skybox.SetFloat("_Rotation", rotateSpeed * Time.time);
    }
}
