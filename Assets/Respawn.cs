using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Threading;
using System.Linq;

public class Respawn : MonoBehaviour
{
    //Variables
    float x = 0;
    float y = 0;
    float z = 0;
    float initialX;
    float initialY;
    float initialZ;
    int[] Show = new int[2];
    private GameObject[] CellArray;
    private List<GameObject> Car = new List<GameObject>();
    private System.Random Rand = new System.Random();
    int CountTwo = 0;
    private GameObject[] CarArray = new GameObject[4];

    // Start is called before the first frame update
    void Start()
    {
        initialX = transform.position.x;
        initialY = transform.position.y;
        initialZ = transform.position.z;
        Show = ArrayRandomiser();
        CellArray = CellCount(CellArray);

        foreach (GameObject G in CellArray){
            if (G.tag == "Car"){
                //Convert to array instead
                CarArray[CountTwo] = G;
                CountTwo++;
            }
            if (G.name == "Plane1"){
                G.GetComponent<Renderer>().material.SetColor("_Color",Color.blue);
            }
            else if (G.name == "Plane2"){
                G.GetComponent<Renderer>().material.SetColor("_Color",Color.black);
            }
            else if (G.name == "Plane3"){
                G.GetComponent<Renderer>().material.SetColor("_Color",Color.blue);
            }
            else if (G.name == "Plane4"){
                G.GetComponent<Renderer>().material.SetColor("_Color",Color.black);
            }
        }
        RandomAllocation();

    }

    // Update is called once per frame
    void Update()
    {
        x = GameObject.FindWithTag("MainCamera").transform.position.x;
        y = GameObject.FindWithTag("MainCamera").transform.position.y;
        z = GameObject.FindWithTag("MainCamera").transform.position.z;

        if (Input.GetKey(KeyCode.P))
        {
            Debug.Log("Postion of Camera X: " + x + " Y: " + y + " Z: " + z);
            Debug.Log("Randomiser Value " + Show.First() + ","+ Show.Last());
            Thread.Sleep(100);
        }

        //Respawning Process
        if (z > transform.position.z + 5)
        {
            RandomAllocation();
            transform.position = new Vector3(0,0,(transform.position.z + 50));
            Show = ArrayRandomiser();
            Debug.Log("New Position: " + transform.position.z);
        }
    }

    private void RandomAllocation()
    {
        var randomInt = UnityEngine.Random.Range(0,2);
        int[] PathRandom = new int[]{0,0,0,0};
        int count = 0;
        while (true){
            foreach (int i in PathRandom){
                if (i == 1){
                    count++;
                }   
            }
            if (count != 2) {
                count = 0;
                Rand = new System.Random();
                for (int j = 0; j < PathRandom.Length; j++){
                    if (randomInt == 0){
                        PathRandom[j] = 0;
                    }
                    else if (randomInt == 1){
                        PathRandom[j] = 1;
                    }
                    randomInt = UnityEngine.Random.Range(0,2);

                }
            }
            else {
                break;
            }
        }
        for (int i = 0; i < PathRandom.Length; i++){
            if (PathRandom[i] == 0){
                CarArray[i].active = false;
            }
            else if (PathRandom[i] == 1){
                CarArray[i].active = true;
            }
        }
    }

    private GameObject[] CellCount(GameObject[] G){
        G = new GameObject[transform.childCount];
        for (int i = 0; i < transform.childCount; i++)
        {
            G[i] = transform.GetChild(i).gameObject;
        }
        return G;
    }

    private static int[] ArrayRandomiser()
    {
        int[] Test = new int[2];
        System.Random Rd1 = new System.Random();
        System.Random Rd2 = new System.Random();
        int RandomNumber1 = Rd1.Next(0, 5);
        int RandomNumber2 = Rd2.Next(0, 5);
        Test[0] = RandomNumber1;
        while (RandomNumber1 == RandomNumber2)
        {
            RandomNumber2 = Rd2.Next(1, 5);
        }
        Test[1] = RandomNumber2;
        return Test;
    }
}
