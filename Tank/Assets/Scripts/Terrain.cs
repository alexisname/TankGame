using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Terrain : MonoBehaviour
{    
   [SerializeField]
   Tilemap terrain;

   public static Terrain instance;

   private void Awake() {
    instance = this;
   }

   public void DestroyTerrain(Vector3 explosionLocation, float radius){
    Vector3Int explosionPos = terrain.WorldToCell(explosionLocation);
    for(int x = -(int)radius; x <= radius; x++){
        for(int y = -(int)radius; y <= radius; y++){
            Vector3Int tilePos = explosionPos + new Vector3Int(x,y,0);
            if(terrain.GetTile(tilePos)!=null){
                terrain.SetTile(tilePos,null);
            }
        }
    }
        // Vector3Int tilePos = terrain.WorldToCell(explosionLocation);
        // if(terrain.GetTile(tilePos)!=null){
        //     DestroyTile(tilePos);
        // }
   }

}
