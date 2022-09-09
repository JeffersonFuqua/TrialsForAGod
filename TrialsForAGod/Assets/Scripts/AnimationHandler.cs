using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationHandler : MonoBehaviour
{
    public void HitBoxOn(int eventVal)
    {
        GetComponent<HitBoxManager>().HitBoxActive(eventVal);
    }
    public void HitBoxOff()
    {
        GetComponent<HitBoxManager>().HitBoxInActive();
    }
}
