using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WriteLogs : MonoBehaviour
{
    static int i = 0;
    void Update()
    {

        Main.iLog.Debug("쓴당" + i++);
        if (i % 10 == 0)
            Main.iLog.Warn("십!");
    }
}
