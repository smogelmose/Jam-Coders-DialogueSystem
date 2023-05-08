using UnityEngine;

namespace Script.SoundManagerScripts
{
    public class ToggleAudio : MonoBehaviour
    {
        [SerializeField] private bool _toggleMusic, _toggleEffects;

        public void Toggle()
        {
            if(_toggleEffects) SoundManager.instance.ToggleEffects();
            if (_toggleMusic) SoundManager.instance.ToggleMusic();
        }
    }
}
