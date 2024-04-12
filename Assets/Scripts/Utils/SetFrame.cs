using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetFPS : MonoBehaviour
{
    [SerializeField] int fps = 60;
    private void Awake()
    {
        Application.targetFrameRate = fps;
    }
}
