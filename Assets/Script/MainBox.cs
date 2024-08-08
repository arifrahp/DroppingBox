using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainBox : MonoBehaviour
{
    private bool isDragging = false;
    private Vector3 offset;
    public float minX;
    public float maxX;

    public bool isImune;
    private Renderer objectRenderer;
    private Color originalColor;

    void Start()
    {
        objectRenderer = GetComponent<Renderer>();
        if (objectRenderer != null)
        {
            originalColor = objectRenderer.material.color;
        }
    }

    void Update()
    {
        // Check for mouse or touch input
        if (Input.GetMouseButtonDown(0))
        {
            // Check if the mouse is over the box
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform == transform)
                {
                    isDragging = true;
                    offset = transform.position - GetMouseWorldPos();
                }
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            isDragging = false;
        }

        if (isDragging)
        {
            Vector3 newPosition = GetMouseWorldPos() + offset;
            newPosition.y = transform.position.y; // Lock Y position
            newPosition.z = transform.position.z; // Lock Z position

            // Clamp the X position to be within the specified range
            newPosition.x = Mathf.Clamp(newPosition.x, minX, maxX);

            transform.position = newPosition;
        }

        if(isImune)
        {
            /*Beeping();*/
        }
    }

    private Vector3 GetMouseWorldPos()
    {
        // Get the mouse position in world coordinates
        Vector3 mousePoint = Input.mousePosition;
        mousePoint.z = Camera.main.WorldToScreenPoint(gameObject.transform.position).z;

        return Camera.main.ScreenToWorldPoint(mousePoint);
    }

    public void Beeping()
    {
        LeanTween.color(this.gameObject, new Color(originalColor.r, originalColor.g, originalColor.b, 0), 0.1f).setOnComplete(() =>
        {
            LeanTween.color(this.gameObject, new Color(originalColor.r, originalColor.g, originalColor.b, 255), 0.1f).setOnComplete(() =>
            {
                Beeping();
            });
        });
    }

    public void OnImune()
    {
        isImune = true;
        Invoke("DeactivateImmunity", 2f);
    }

    public void DeactivateImmunity()
    {
        isImune = false;
    }
}
