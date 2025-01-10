using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PreviewSystem : MonoBehaviour
{
    [SerializeField]
    private float previewYOffset = 0.06f;

    private GameObject previewObject;

    [SerializeField]
    private Material previewMaterialsPrefab;
    private Material previewMaterialInstance;


    private void Start()
    {
        previewMaterialInstance = new Material(previewMaterialsPrefab);

    }

    public void StartShowingPlacementPreview(GameObject prefab)
    {
        previewObject = Instantiate(prefab);
        PreparePreview(previewObject);
    }


    private void PreparePreview(GameObject previewObject)
    {
        Renderer[] renderers = previewObject.GetComponentsInChildren<Renderer>();
        previewObject.layer = 0;
        foreach (Transform child in previewObject.transform)
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
        Destroy(previewObject);
    }

    public void UpdatePosition(Vector3 pos, bool validity)
    {
        MovePreview(pos);
        ApplyFeedback(validity);
    }

    private void ApplyFeedback(bool validity)
    {
        Color col = validity ? Color.white : Color.red;
        col.a = 0.5f;
        previewMaterialInstance.color = col;
    }


    private void MovePreview(Vector3 pos)
    {
        if (previewObject != null)
        previewObject.transform.position = new Vector3(pos.x, pos.y + previewYOffset, pos.z);
    }
}
