using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class OptionsMaster : ScriptableObject
{
    [Range( 0, 1)]
    public float masterVol;
    [Range(0, 1)]
    public float musicVol;
    [Range(0, 1)]
    public float sFXVol;
    [Range(0, 1)]
    public float vCVol;
}
