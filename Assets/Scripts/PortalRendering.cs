using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering;
using UnityEngine.Rendering;
using UnityEngine.Rendering.LWRP;
using RenderPipeline = UnityEngine.Experimental.Rendering.RenderPipeline;

[ExecuteInEditMode]
public class PortalRendering : MonoBehaviour
{
    public Camera portalCamera;
    public PortalRendering pairPortal;
    public MeshRenderer portalRenderer;

    private RenderTexture renderTexture;
    private Material renderMaterial;
    
    private void Awake()
    {
        renderTexture = new RenderTexture(2048, 2048, 32, RenderTextureFormat.ARGB32);
        renderTexture.name = $"{pairPortal.name}";

        portalCamera.targetTexture = renderTexture;

        renderMaterial = new Material(Shader.Find("Shader Graphs/PortalShader"));
        renderMaterial.mainTexture = renderTexture;
        portalRenderer.material = renderMaterial;
    }

    private void OnEnable()
    {
        RenderPipeline.beginCameraRendering += UpdateCamera;
    }

    private void OnDisable()
    {
        RenderPipeline.beginCameraRendering -= UpdateCamera;
    }

    void UpdateCamera(Camera camera)
    {
        if ((camera.cameraType == CameraType.Game || camera.cameraType == CameraType.SceneView) &&
            camera.tag != "Portal Camera")
        {
            portalCamera.projectionMatrix = camera.projectionMatrix; // Match matrices

            var relativePosition = transform.InverseTransformPoint(camera.transform.position);
            relativePosition = Vector3.Scale(relativePosition, new Vector3(-1, 1, -1));
            portalCamera.transform.position = pairPortal.transform.TransformPoint(relativePosition);

            var relativeRotation = transform.InverseTransformDirection(camera.transform.forward);
            relativeRotation = Vector3.Scale(relativeRotation, new Vector3(-1, 1, -1));
            portalCamera.transform.forward = pairPortal.transform.TransformDirection(relativeRotation);
        }
    }
    
    // Calculates reflection matrix around the given plane
    private static void CalculateReflectionMatrix(ref Matrix4x4 reflectionMat, Vector4 plane)
    {
        reflectionMat.m00 = (1F - 2F * plane[0] * plane[0]);
        reflectionMat.m01 = (-2F * plane[0] * plane[1]);
        reflectionMat.m02 = (-2F * plane[0] * plane[2]);
        reflectionMat.m03 = (-2F * plane[3] * plane[0]);

        reflectionMat.m10 = (-2F * plane[1] * plane[0]);
        reflectionMat.m11 = (1F - 2F * plane[1] * plane[1]);
        reflectionMat.m12 = (-2F * plane[1] * plane[2]);
        reflectionMat.m13 = (-2F * plane[3] * plane[1]);

        reflectionMat.m20 = (-2F * plane[2] * plane[0]);
        reflectionMat.m21 = (-2F * plane[2] * plane[1]);
        reflectionMat.m22 = (1F - 2F * plane[2] * plane[2]);
        reflectionMat.m23 = (-2F * plane[3] * plane[2]);

        reflectionMat.m30 = 0F;
        reflectionMat.m31 = 0F;
        reflectionMat.m32 = 0F;
        reflectionMat.m33 = 1F;
    }
}
