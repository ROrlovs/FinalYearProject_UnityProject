using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class SnowScript : MonoBehaviour
{
    RaycastHit hit;
    int raycastsShot;
    int zIndex=0;
    int xIndex=0;
    int i;
    List<Vector3> heights = new List<Vector3>();
    Vector3 cornerGO;
    bool generateSnow;
    // Start is called before the first frame update
    void Start()
    {
        //InvokeRepeating("SearchHighestPoint",1,5);
        cornerGO = GameObject.Find("MeshCorner").GetComponent<Transform>().position;
        
    }

    // Update is called once per frame
    void Update()
    {
        
        
    }

    public void GenerateSnow(){ 
        DeleteSnow();
        generateSnow=true;
        StartCoroutine(waiter());
    }

    public void DeleteSnow(){
        GameObject[] snow = GameObject.FindGameObjectsWithTag("generatedSnow");
    foreach (GameObject gameObject in snow){
        Destroy(gameObject);
    }
    }

    void SearchHighestPoint(){
        /* for(int z=0; z<100;z++){
        for(int i=0; i<raycastsToShoot; i++){
            Debug.DrawRay(new Vector3((cornerGO.x),gameObject.transform.position.y+999,gameObject.transform.position.z+(zIndex*10)),Vector3.down*999,Color.cyan,1);
            if(Physics.Raycast(new Vector3((cornerGO.x),gameObject.transform.position.y+999,gameObject.transform.position.z+(zIndex*10)),Vector3.down,out hit)){
            //print(hit.point.y);
        }
        xIndex++;
        raycastsShot++;
        print("incremented X index to"+xIndex);
        print(cornerGO.x);
        }
        xIndex=-50;
        zIndex++;
        print("incremented Z index to"+zIndex);
        } */
    }

    IEnumerator waiter()
{
    heights.Clear();
    xIndex=0;
    zIndex=0;
    if(generateSnow){
    for(int z=0; z<11;z++){
        for(i=0; i<11; i++){
            Debug.DrawRay(new Vector3((gameObject.transform.position.x-500)+(xIndex*100),
            transform.position.y+999,
            transform.position.z-500+(zIndex*100)),
            Vector3.down*999,
            Color.cyan,1);
            if(Physics.Raycast(new Vector3((gameObject.transform.position.x-500)+(xIndex*100),
            gameObject.transform.position.y+999,
            gameObject.transform.position.z-500+(zIndex*100)),
            Vector3.down,
            out hit)){
            heights.Add(hit.point);
            
        }
        raycastsShot++;
        xIndex++;
        print("incremented X index to"+xIndex);
        yield return new WaitForSecondsRealtime(0.001f);
        }
        xIndex=0;
        zIndex++;
        i=0;
        print("incremented Z index to"+zIndex);
        }

    print("Final heights array size: "+heights.Count);
    heights.Sort((a,b) => a.y.CompareTo(b.y));
    int lastElementIndex = heights.Count;
    print(heights[^1]);
    print(heights[^2]);
    print(heights[^3]);
    //List<Vector3> top3heights = heights.GetRange(heights.Count-3,heights.Count);
    //print(top3heights.ElementAt(0));

    for(int s=1;s<=3;s++){
        if(heights[^s].y>150){
        GameObject snow = Instantiate(GameObject.Find("SnowParticleSystem"),new Vector3(heights[^s].x,heights[^s].y+100,heights[^s].z),Quaternion.identity);
        snow.tag = "generatedSnow";
    }
    }

/*     if(heights[^1].y>170){
        Instantiate(GameObject.Find("SnowParticleSystem"),heights[^1],Quaternion.identity);
    }

    if(heights[^2].y>170){
        Instantiate(GameObject.Find("SnowParticleSystem"),heights[^2],Quaternion.identity);
    }

    if(heights[^3].y>170){
        Instantiate(GameObject.Find("SnowParticleSystem"),heights[^3],Quaternion.identity);
    } */
    generateSnow=false;

    }



}
}
