using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SFXHelper
{
    public static SFXManager sfxManager;
    public static void Init(SFXManager manager)
    {
        sfxManager = manager;
    }

    public static void PlayEffect(SFXs sfx)
    {
        sfxManager.Play((int) sfx);
    }

}
