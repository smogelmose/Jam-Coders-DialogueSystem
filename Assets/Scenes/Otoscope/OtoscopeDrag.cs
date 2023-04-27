using System.Collections;
using System.Collections.Generic;
using UnityEditor.VersionControl;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class OtoscopeDrag : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    public RectTransform SnapOnCorrect;
    public float SnapPossition = 5f;

    private RectTransform TransformerMode;
    private CanvasGroup Group;

    private void Start ()
    {
        TransformerMode = GetComponent<RectTransform>();
        Group = GetComponent<CanvasGroup>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        Group.alpha = .6f;
        Group.blocksRaycasts = true;
    }

    public void OnDrag(PointerEventData eventData)
    {
        TransformerMode.anchoredPosition += eventData.delta;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Group.alpha = 1f;

        // Calculate the distance between the dropped position and the correct position
        float Possition = Vector2.Distance(TransformerMode.anchoredPosition, SnapOnCorrect.anchoredPosition);

        // If the distance is within the snap distance, snap the image to the correct position
        if (Possition <= SnapPossition)
        {
            TransformerMode.anchoredPosition = SnapOnCorrect.anchoredPosition;
            
            SceneManager.LoadScene("Scenes/Otoscope/Ear");
            
        }
    }
}