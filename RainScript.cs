using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class RainScript : MonoBehaviour
{
    ParticleSystem rainParticleSystem;
    GameObject meshObject;
    RaycastHit hit;
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("AttemptRain",1,15);
        
    }

    void Update(){

    }

    void AttemptRain(){
        StartCoroutine(rainWaiter());
    }

    IEnumerator rainWaiter(){
        int chance = Random.Range(0,2);
        print("chance to rain= "+chance);
        int randX = Random.Range(-500,500);
        int randZ = Random.Range(-500,500);
        int randRadius = Random.Range(100,250);
        int randRate = Random.Range(40,400);
        int randDuration = Random.Range(10,60);
        if (chance == 1){
            print("fired rain ray");
            Debug.DrawRay(new Vector3(randX,9999,randZ), Vector3.down*99999,Color.green,3);
            if(Physics.Raycast(new Vector3(randX,9999,randZ), Vector3.down,out hit)){
                if(hit.point.y<150){ //ensure below snow line
                    GameObject newRain = Instantiate(GameObject.Find("RainParticleSystem"),new Vector3(hit.point.x,hit.point.y+500,hit.point.z),Quaternion.identity);
                    ParticleSystem rainComponent = newRain.GetComponent<ParticleSystem>();
                    rainComponent.Stop();
                    var shape = rainComponent.shape;
                    var emmision = rainComponent.emission;
                    var main = rainComponent.main;
                    main.stopAction = ParticleSystemStopAction.Destroy;
                    shape.radius = randRadius;
                    emmision.rateOverTime = randRate;
                    main.duration = randDuration;
                    rainComponent.Play();
                }
                
            }
            
        }
        yield return new WaitForSecondsRealtime(1);
    }
}
