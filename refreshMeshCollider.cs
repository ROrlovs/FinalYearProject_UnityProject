using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class refreshMeshCollider : MonoBehaviour
{

    private void Start() {
        gameObject.AddComponent<MeshCollider>();
    }


    public void regenerateMeshCollider(){
        Destroy(GetComponent<MeshCollider>());
        gameObject.AddComponent<MeshCollider>();
    }


    

}
