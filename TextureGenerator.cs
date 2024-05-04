using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public static class TextureGenerator
{
  

        public static Texture2D TextureFromColourMap(Color[] colourMap, int width, int height){
            Texture2D texture = new Texture2D(width,height);
            texture.filterMode = FilterMode.Point;
            texture.wrapMode = TextureWrapMode.Clamp;
            texture.SetPixels(colourMap);
            texture.Apply();
            SavedTexture(texture);
            texture.name = "TextureFromColourMap";
            return texture;
        }

        public static Texture2D TextureFromHeightMap(float[,] heightMap){
        int width = heightMap.GetLength(0);
        int height = heightMap.GetLength(1);



        Color[] colourMap = new Color[width*height];
        for(int y=0;y<height;y++){
            for (int x=0;x<width;x++){
                colourMap[y * width + x] = Color.Lerp(Color.black,Color.white, heightMap[x,y]);
            }
        }
            return TextureFromColourMap(colourMap,width,height);
        }

        public static void SavedTexture(Texture2D texture){

            GameObject.Find("MapGenerator").GetComponent<MapDisplay>().savedTexture=texture;
        }

}
