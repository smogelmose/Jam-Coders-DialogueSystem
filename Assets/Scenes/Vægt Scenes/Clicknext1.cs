using UnityEngine;
using UnityEngine.SceneManagement;

namespace Scenes.Vægt_Scenes
{
    public class Clicknext1 : MonoBehaviour
    {

        // Update is called once per frame
        void Update()
        {
            if (Input.GetMouseButton(0))
            {
                SceneManager.LoadScene("GetActiveScene"+1);
            }
        }
    }
}
