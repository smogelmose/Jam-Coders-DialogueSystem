using UnityEngine;

namespace CodeJamImprovements.DialogueSystem.Scripts
{
    public class DebugLogPersistentDataPath : MonoBehaviour
    {
        void Start()
        {
            Debug.Log(Application.persistentDataPath);
        }
    }
}
