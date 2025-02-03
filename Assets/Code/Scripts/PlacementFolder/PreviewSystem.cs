using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PreviewSystem : MonoBehaviour
{
    [SerializeField]
    private float previewYOffset = 0.06f;

    private GameObject previewItem;

    [SerializeField]
    private Material previewMaterialsPrefab;
    private Material previewMaterialInstance;
    [SerializeField]

    private Material previewMaterialDelete;

    private GameObject lastHoveredObject;



    private void Start()
    {
        previewMaterialInstance = new Material(previewMaterialsPrefab);

    }

    public void StartShowingPlacementPreview(GameObject prefab)
    {
        previewItem = Instantiate(prefab);
        PreparePreview(previewItem);
    }



    /// <summary>
    /// Set material of selected item for preview
    /// </summary>
    /// <param name="previewItem"></param>
    private void PreparePreview(GameObject previewItem)
    {
        Renderer[] renderers = previewItem.GetComponentsInChildren<Renderer>();
        previewItem.layer = 0;
        foreach (Transform child in previewItem.transform)
        {
            child.gameObject.layer = 0;    
        }
        foreach (Renderer renderer in renderers)
        {
            Material[] materials = renderer.materials;
            for (int i = 0; i < materials.Length; i++)
            {
                materials[i] = previewMaterialInstance;
            }
            renderer.materials = materials;
        }
    }

    public void StopShowingPreview()
    {
        Destroy(previewItem);

        if (lastHoveredObject != null)
        {
            ResetMaterial(lastHoveredObject);
            lastHoveredObject = null; // Clear reference
        }
    }

    public void UpdatePosition(Vector3 pos, bool validity)
    {
        MovePreview(pos);
        ApplyFeedback(validity);
    }

    private void ApplyFeedback(bool validity)
    {
        Color col = validity ? Color.green : Color.red;
        col.a = 0.5f;
        previewMaterialInstance.color = col;
    }


    private void MovePreview(Vector3 pos)
    {
        if (previewItem != null && pos.x < 1)
        previewItem.transform.position = new Vector3(pos.x, pos.y + previewYOffset, pos.z);
    }












    internal void StartShowingRemovePreview()
    {
        // Raycast to find the object under the cursor
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            GameObject hoveredObject = hit.collider.gameObject;
            // Ensure we only highlight objects with the "Cargo" tag
            if (!hoveredObject.CompareTag("Player"))
            {
                StopShowingPreview();
                return;
            }
            Renderer renderer = hoveredObject.GetComponentInChildren<Renderer>();
            previewMaterialsPrefab = renderer.material;
            if (renderer == null) return;
            // Reset the material of the last hovered object (if different)
            if (lastHoveredObject != null && lastHoveredObject != hoveredObject)
            {
                ResetMaterial(lastHoveredObject);
            }

            // Store reference to the current hovered object
            lastHoveredObject = hoveredObject;

            // Set the material to preview
            Material[] materials = renderer.materials;
            for (int i = 0; i < materials.Length; i++)
            {
                materials[i] = previewMaterialDelete;
            }
            renderer.materials = materials;
        }
        else
        {
            // No object under the cursor  Reset last hovered object
            StopShowingPreview();
        }
    }


    // Function to reset the material of an object
    private void ResetMaterial(GameObject obj)
    {
        Renderer[] renderers = obj.GetComponentsInChildren<Renderer>();
        foreach (Renderer renderer in renderers)
        {
            renderer.material = previewMaterialsPrefab; // Reset to original
        }
    }
}
