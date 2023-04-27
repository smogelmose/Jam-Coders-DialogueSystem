using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEditor.VersionControl;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class MinigameSnap : MonoBehaviour
{
    public RectTransform SnapOnCorrect;
    public float SnapPossition = 5f;
    private RectTransform TransformerMode;
    private void Start()
    {
        TransformerMode = GetComponent<RectTransform>();
    }

    private void Update()
    {
        // Calculate the distance between the current position and the correct position
        float distance = Vector2.Distance(TransformerMode.anchoredPosition, SnapOnCorrect.anchoredPosition);

        // If the distance is within the snap distance, snap the image to the correct position
        if (distance <= SnapPossition)
        {
            TransformerMode.anchoredPosition = SnapOnCorrect.anchoredPosition;

            SceneManager.LoadScene(17);
        }
    }
}