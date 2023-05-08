using UnityEngine;
using UnityEngine.UI;

namespace Script.SoundManagerScripts
{
    public class VolumeUpdater : MonoBehaviour
    {
        [SerializeField] private Slider _slider;

        void Start()
        {
            SoundManager.instance.ChangeMasterVolume(_slider.value);
            _slider.onValueChanged.AddListener(val => SoundManager.instance.ChangeMasterVolume(val));
        }
    }
}
