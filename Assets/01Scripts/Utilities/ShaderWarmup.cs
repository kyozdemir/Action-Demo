using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Rendering;

namespace ActionDemo
{
    public class ShaderWarmup : MonoBehaviour
{
    private GameObject quad;
    private void Start()
    {
        WarmSceneShaders();
    }
    // Call at scene creation
    private void WarmSceneShaders()
    {
        quad = GameObject.CreatePrimitive(PrimitiveType.Quad);

        quad.name = "ShaderWarmer";
        quad.transform.localScale = Vector3.one * 0.00001f;
        var cam = Camera.main;
        if (cam != null)
        {
            quad.transform.SetParent(cam.transform);
            quad.transform.localPosition = new Vector3(0, 0, cam.nearClipPlane);
        }

        Renderer[] allRenderersOnScene = Resources.FindObjectsOfTypeAll<Renderer>();
        HashSet<Material> uniqueMats = new HashSet<Material>();

        foreach (var renderer in allRenderersOnScene)
        {
            if (renderer.sharedMaterials == null) return;
            foreach (var mat in renderer.sharedMaterials)
            {
                uniqueMats.Add(mat);
            }
        }

        var quadRenderer = quad.GetComponent<Renderer>();
        quadRenderer.materials = uniqueMats.ToArray();
        RenderPipelineManager.endFrameRendering += Deactivate;
    }
    public void Deactivate(ScriptableRenderContext context, Camera[] cam)
    {
        Destroy(quad);
        RenderPipelineManager.endFrameRendering -= Deactivate;
    }
}
}
