using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LogConfig", menuName = "Scriptable Object/LogConfig", order = int.MaxValue)]
public class LoggerConfig : ScriptableObject
{
    [SerializeField] string path;

}
