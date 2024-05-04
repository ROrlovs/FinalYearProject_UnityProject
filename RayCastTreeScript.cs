using System.Collections;
using System.Collections.Generic;
using System.Data;
using JetBrains.Annotations;
using UnityEditor;
using UnityEngine;

public class RayCastTreeScript : MonoBehaviour
{
    [Header("(Number of Rays In Thousands)")]
    [Range(0,30)]
    public int numOfRays;
    int raysShot = 999;
    int i;
    GameObject[] treeArray;
    [Header("(Number of Trees Spawned Per Frame,\n anything over 500 is dangerous)")]
    public int treesSpawnedPerFrame;

    bool deleteTrees;
    [Header("(Lowest Y coordinate for trees to spawn) Default: 5")]
    public int treeLowerLimit;
    [Header("(Highest Y coordinate for trees to spawn) Default: 38")]
    public int treeUpperLimit;
    // Start is called before the first frame update
    void Start()
    {
        
        treeArray = new GameObject[numOfRays];
        if (treesSpawnedPerFrame<1){treesSpawnedPerFrame=1;}

    }

    // Update is called once per frame
    void Update()
    {
        for(i=0; i<treesSpawnedPerFrame; i++){
        if (raysShot<numOfRays){
            if(numOfRays<1000){
                numOfRays*=1000;
            }
            float randX = Random.Range(-1000,1000);
            float randZ = Random.Range(-1000,1000);
            float randScale = Random.Range(0.5f,1.5f);
        
        RaycastHit hit;
        
        if (Physics.Raycast(new Vector3(GameObject.Find("Mesh").transform.position.x+randX,1000,GameObject.Find("Mesh").transform.position.z+randZ), Vector3.down, out hit, 9999)){
            Debug.DrawRay(new Vector3(GameObject.Find("Mesh").transform.position.x+randX,1000,GameObject.Find("Mesh").transform.position.z+randZ), Vector3.down*hit.distance, Color.red, 1,false);
            //print(hit);
            Vector3 hitPoint = hit.point;
            print(hitPoint.y);
            if ((hitPoint.y>treeLowerLimit) && (hitPoint.y<treeUpperLimit)){
            GameObject newTree = Instantiate(GameObject.Find("Fir_Tree"),hitPoint,Quaternion.identity);
            newTree.transform.localScale = new Vector3(newTree.transform.localScale.x,randScale,newTree.transform.localScale.z);
            newTree.gameObject.tag = "generatedTree";
            //print("checking tree height restriction");


            /*print(newTree.transform.position.y);
            if ((newTree.transform.position.y<treeLowerLimit) || (newTree.transform.position.y>treeUpperLimit)){
                print("tree not within height restriction... deleting");
                Destroy(newTree);
            }*/
            }
            raysShot++;
            print("tree rays shot: "+raysShot);
        }
        }
        }

        /* if(deleteTrees){
            for(int o = 0; o<raysShot; o++){
            Destroy(treeArray[o]);
            treeArray[o] = null;
            }
            deleteTrees=false;
            raysShot=0;
        } */

    }

public void DeleteTrees(){
    print("Running DeleteTrees Method");

    deleteTrees=true;
    GameObject[] generatedTrees = GameObject.FindGameObjectsWithTag("generatedTree");
    foreach (GameObject gameObject in generatedTrees){
        Destroy(gameObject);
    }
    

    deleteTrees=false;
    /*
    for(int o = 0; o<raysShot; o++){
            Destroy(treeArray[o]);
            treeArray[o] = null;
            }
            deleteTrees=false;
            raysShot=0;
            */
}

public void RegenerateTrees(){
    print("Running RegenerateTrees Method");
    DeleteTrees();
    raysShot=0;
    i=0;
}
}


