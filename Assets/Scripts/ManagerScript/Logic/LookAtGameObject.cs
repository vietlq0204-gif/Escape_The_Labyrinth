using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtGameObject : MonoBehaviour
{
    [SerializeField] public GameObject lookAtgameObject;
    [SerializeField] private Vector2 offset;

    private RectTransform rectTransform;
    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    void Update()
    {
        if (lookAtgameObject != null)
        {
            Vector2 playerScreenPosition = lookAtgameObject.transform.position;
            rectTransform.position = playerScreenPosition;
        }
    }
}
