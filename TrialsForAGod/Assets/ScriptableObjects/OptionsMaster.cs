using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Resolutions {full = 0, notFull = 1};
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
    [SerializeField]
    public Resolutions resoSize = new Resolutions();
}
