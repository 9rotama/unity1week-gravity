using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;


public static class Common
{   
    public static bool TrueOrFalse()
    {
        return UnityEngine.Random.Range(0, 2) == 0;
    }


}
