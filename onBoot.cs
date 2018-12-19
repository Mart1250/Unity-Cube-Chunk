using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class onBoot : MonoBehaviour
{
    public Transform floorBlock;
    public Transform dirtBlock;
    public Transform stoneBlock;
    public Transform airBlock;
    public int[,][,,] worldChunkData = new int[100,100][,,]; //Bevat allemaal gegevens van individuele chunks.

    void Start()
    {
        generateChunk(0, 0);
        loadChunk(worldChunkData, 0, 0);
    }

    public Transform getBlock(int blockID)
    {
        switch (blockID)
        {
            case 0:
                return airBlock;
            case 1:
                return floorBlock;
            case 2:
                return dirtBlock;
            case 3:
                return stoneBlock;
            default:
                return airBlock;

        }

    }


    //Texture2D tempTexture = (Texture2D)Resources.Load("Image_name") as Texture2D;
    //obj.renderer.material.mainTexture = tempTexture;

    public void generateChunk(int xc, int zc)
    {

        int[,,] chunkData = new int[40, 40, 200];
        int dirtz= 1000000000;

        for (int x = 0; x < 10; x++)

        {

            for (int y = 0; y < 10; y++)

            {

                for (int z = 0; z < 200; z++)

                {
                    chunkData[x, y, z] = 0;
                }
            }
        }

        for (int x = 0; x < 10; x++)

        {

            for (int y = 0; y < 10; y++)

            {

                for (int z = 0; z < 200; z++)

                {


                    

                    //float xt = x + (float)0.5;
                    //float yt = y + (float)0.5;
                    //float zt = z + (float)0.5;                

                    if (z == 0)
                    {

                        chunkData[x, y, z] = 1;


                    }
                    else if (z == 95)
                    {

                        float sin = (2 * (Mathf.Sin((float)0.8*x)));
                        //print(sin);
                        chunkData[x , y, z + (int)sin] = 2;

                    }
                    //chunkData[x, y, z] = 3;
                    /*if ((y != 9))
                        {
                            chunkData[x, y, z] = Random.Range(1, 3);

                        }*/


                    //if (z < 96)
                    //{
                    if (chunkData[x, y, z] == 0)
                    {
                        if (chunkData[x, y, z - 1] == 2)
                        {
                            //chunkData[x, y, z] = 0;
                            dirtz = (z - 1);
                        }
                        else if (z > dirtz)
                        {
                            chunkData[x, y, z] = 0;

                        }else if (chunkData[x, y, z - 1] != 2)

                        {

                            chunkData[x, y, z] = 3;

                        }
                    }
                    //}
                }
            }
            
        }

        worldChunkData[xc, zc] = chunkData;


    }

    public void loadChunk(int[,][,,] worldChunkData, int xc, int yc) { 

        int blockId;

        for (int x = 0; x < 10; x++)
        {

            for (int y = 0; y < 10; y++)
                
            {

                for (int z = 0; z < 200; z++)

                {

                    float xt = x + (float)0.5;
                    float zt = z + (float)0.5;
                    float yt = y + (float)0.5;
                    



                    blockId = worldChunkData[xc, yc][x, y, z];
                    //print(blockId.ToString());

                    Instantiate(getBlock(blockId), new Vector3((xc * 10) + xt, (yc * 10) + zt, yt), Quaternion.identity); //zit ingewikkeld in elkaar, maar op deze manier kun je in de array de blokken benaderen in het format: [x, y, z]
                                                                                                                          

                }
            }
        }
    }
}

