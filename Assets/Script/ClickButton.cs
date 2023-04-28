using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using JetBrains.Annotations;

public class ClickButton : MonoBehaviour,IPointerDownHandler,IPointerUpHandler
{
    [SerializeField] Image _img;
    [SerializeField] Sprite _default, _pressed;
    [SerializeField] AudioClip _compressClip, _uncompressedClip;
    [SerializeField] AudioSource _source;

        public void OnPointerDown(PointerEventData eventData)
        {
            _img.sprite = _pressed;
            _source.PlayOneShot(_compressClip);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        _img.sprite = _default;
        _source.PlayOneShot(_uncompressedClip);
        SceneManager.LoadScene(1); 
    }
}
