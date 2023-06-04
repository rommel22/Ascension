using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UVMapping : MonoBehaviour
{
    // // Start is called before the first frame update
    // void Start()
    // {
        
    // }

    // // Update is called once per frame
    // void Update()
    // {
        
    // }
    private MeshFilter meshFilter;
    private MeshRenderer meshRenderer;
    private Material material;
    private Texture texture;

    void Awake() {
        meshFilter = GetComponent<MeshFilter>();
        meshRenderer = GetComponent<MeshRenderer>();
        material = meshRenderer.material;
        texture = material.GetTexture("_MainTex");

        Vector2[] uvCoord = new Vector2[meshFilter.mesh.vertexCount];

        for (int i = 0; i < uvCoord.Length; i++){
            uvCoord[i] = new Vector2(meshFilter.mesh.vertices[i].x, meshFilter.mesh.vertices[i].z);
        }

        meshFilter.mesh.uv = uvCoord;

        meshRenderer.material.mainTexture = texture;
    }
}
