using UnityEngine;

public class DebugLogPersistentDataPath : MonoBehaviour
{
    void Start()
    {
        Debug.Log(Application.persistentDataPath);
    }
}
