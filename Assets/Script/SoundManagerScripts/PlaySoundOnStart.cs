using UnityEngine;

namespace Script.SoundManagerScripts
{
    public class PlaySoundOnStart : MonoBehaviour
    {
        [SerializeField] private AudioClip _clip;

        void Start()
        {
            SoundManager.instance.PlaySound(_clip);
        }
    }
}
