using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TerrainSliderScript : MonoBehaviour
{

    public Slider seedSlider;
    public Slider densitySlider;
    int seedSliderValue;
    int densitySliderValue;
    MapGenerator mapGen;
    refreshMeshCollider refreshColliderScript;
    RayCastTreeScript rayTreeScript;
    SnowScript snowScript;
    TextMeshProUGUI terrainSeedText;
    TextMeshProUGUI treeDensityText;

    // Start is called before the first frame update
    void Start()
    {
        rayTreeScript = GetComponent<RayCastTreeScript>();
        mapGen = GetComponent<MapGenerator>();
        snowScript = GetComponent<SnowScript>();
        refreshColliderScript = GameObject.Find("Mesh").GetComponent<refreshMeshCollider>();
        mapGen.seed=0;
        mapGen.GenerateMap();
        
    }

    // Update is called once per frame
    void Update()
    {
        //bug fix
        if(refreshColliderScript==null){
            refreshColliderScript = GameObject.Find("Mesh").GetComponent<refreshMeshCollider>();
        }


        if(densitySlider.value != densitySliderValue){
            GetComponent<RayCastTreeScript>().numOfRays = (int)densitySlider.value;
            densitySliderValue = (int)densitySlider.value;
            UpdateText();
        }

        if(seedSlider.value != seedSliderValue){
            GetComponent<MapGenerator>().seed = (int)seedSlider.value;
            mapGen.GenerateMap();
            refreshColliderScript.regenerateMeshCollider();
            rayTreeScript.DeleteTrees();
            snowScript.DeleteSnow();
            seedSliderValue = (int)seedSlider.value;
            UpdateText();
        }

    
        
    }

    void UpdateText(){
        terrainSeedText = GameObject.Find("TerrainSeedText").GetComponent<TextMeshProUGUI>();
        terrainSeedText.text = seedSliderValue.ToString();
        treeDensityText = GameObject.Find("TreeDensityText").GetComponent<TextMeshProUGUI>();
        treeDensityText.text = densitySliderValue.ToString();
    }
}
