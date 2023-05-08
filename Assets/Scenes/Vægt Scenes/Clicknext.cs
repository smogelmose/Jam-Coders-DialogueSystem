using System.Threading;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Scenes.Vægt_Scenes
{
    public class Clicknext : MonoBehaviour
    {
        public int ms = 500;

        // Update is called once per frame
        void Update()
        {
            int nextSceneIndex =SceneManager.GetActiveScene().buildIndex +1;
            if (Input.GetMouseButton(0)) // 
            {
           
                SceneManager.LoadScene(nextSceneIndex);
                Thread.Sleep(ms);
            }
        }
    }
}
