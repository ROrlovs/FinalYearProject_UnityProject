using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class MapDisplay : MonoBehaviour
{
    public Texture2D savedTexture;
    public Renderer textureRenderer;
    public MeshFilter meshFilter;
    public MeshRenderer meshRenderer;
    MeshData meshData;


    void Start(){
        UpdateTerrainMaterial(savedTexture);
    }



    public void UpdateTerrainMaterial(Texture2D texture){
        MapGenerator mapGenerator = FindObjectOfType<MapGenerator>();
        meshRenderer.material.mainTexture=texture;
    }

    public void DrawTexture(Texture2D texture){
        
        textureRenderer.sharedMaterial.mainTexture=texture;
        textureRenderer.material.mainTexture=texture;
        textureRenderer.transform.localScale=new Vector3(texture.width,1,texture.height);
    }

    public void DrawMesh(MeshData meshData,Texture2D texture){
        meshFilter.sharedMesh = meshData.CreateMesh();
        meshRenderer.sharedMaterial.mainTexture = texture;
    }



}
