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

}
