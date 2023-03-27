using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu]
public class OptionsMaster : ScriptableObject
{
    [Range( 0, 1)]
    public float masterVol = 1;
    [Range(0, 1)]
    public float musicVol = 1;
    [Range(0, 1)]
    public float sFXVol = 1;
    //[Range(0, 1)]
    //public float vCVol = 1;

    public bool fullscreen = true;
    [Header("Resolution Value: 0 = 1980x1080 1 = 1280x720 2 = 640x480")]
    public int resoValue = 0;
}
