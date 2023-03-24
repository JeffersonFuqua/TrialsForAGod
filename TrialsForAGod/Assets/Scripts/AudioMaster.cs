using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioMaster : MonoBehaviour
{
    public OptionsMaster optionsVal;
    enum AudioTypes {music, sFX, vC};

    [SerializeField]
    AudioTypes audioType = new AudioTypes();

    private void Update()
    {
        switch (audioType)
        {
            case AudioTypes.music:

                GetComponent<AudioSource>().volume = optionsVal.musicVol;
                break;

            case AudioTypes.sFX:

                GetComponent<AudioSource>().volume = optionsVal.musicVol;
                break;

            case AudioTypes.vC:

                GetComponent<AudioSource>().volume = optionsVal.musicVol;
                break;
        }
    }
}
