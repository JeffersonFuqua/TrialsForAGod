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
    [Range(0, 1)]
    public float vCVol = 1;
}
