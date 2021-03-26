using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using UnityEngine.TextCore;

public class RespawningHierarchy : MonoBehaviour
{
    //Variable Declaration
    public static GameObject CameraObject;
    public static GameObject[] PlaneArray;
    public static GameObject[] CarArrayListing;
    public static int RespawningCarDeviation;
    public static int CarMovement;
    public static int CarMovementPlaneReference;


    void Start(){
        //Assignment
        CameraObject = GameObject.FindWithTag("MainCamera");
        PlaneArray = GameObject.FindGameObjectsWithTag("Respawn");
        CarArrayListing = GameObject.FindGameObjectsWithTag("Car");
        RespawningCarDeviation = 0;
        CarMovement = 1;

        //CarDisappearance
        foreach (GameObject G in CarArrayListing){
            G.SetActive(false);
        }

    }
    void Update(){
        //Global Variable Updates
        float LaneTimer = Timer.TimerClock;
        //Respawning Planes
        for (int G = 0; G < PlaneArray.Length; G ++){
            //Activated when the Car travels halfway verticaly across a plane
            if (CameraObject.transform.position.z > PlaneArray[G].transform.position.z + 6){
                PlaneArray[G].transform.position = new Vector3(PlaneArray[G].transform.position.x,0,(PlaneArray[G].transform.position.z + 40));
                //Sus on this 
                CarActivator(G,false);
                //Control 
                if (LaneTimer > 8){
                    RespawnInitiator(G);
                }
            }
        }

        //Car Deviation
        if (RespawningCarDeviation == 1){
            int LaneCount = 1;
            for (int i = CarMovementPlaneReference*4;i < CarMovementPlaneReference*4 + 4; i++,LaneCount++){
                //Checks if Car Exists
                if (CarArrayListing[i].activeSelf == true){
                    if (CarMovement == 0 && LaneCount == 1){
                        //Moving Right
                        CarArrayListing[i].transform.Translate(new Vector3(0,0,-2*Time.deltaTime));
                    }
                    else if (CarMovement == 2 && LaneCount == 3){
                        //Moving Left
                        CarArrayListing[i].transform.Translate(new Vector3(0,0,2*Time.deltaTime));
                    }
                }
            }
        }
        


        //Debug
        if (Input.GetKey(KeyCode.M)){
            foreach (GameObject G in PlaneArray){
                Debug.Log("Plane Position: " + G.transform.position.z);
            }
            Debug.Log("Car Array Length: " + CarArrayListing.Length);
            Thread.Sleep(100);
        }

    }

    private void CarActivator(int PlaneNumber, bool State){
        int LaneCount = 1;
        string Lane;
        for(int i = PlaneNumber*4; i < PlaneNumber*4 + 4; i++,LaneCount++){
            CarArrayListing[i].SetActive(State);
            Lane = "Lane" + LaneCount.ToString();
            Vector3 Position = new Vector3(GameObject.FindWithTag(Lane).transform.position.x,0,PlaneArray[PlaneNumber].transform.position.z);
            CarArrayListing[i].transform.position = Position;
        }
    }
    public static void SelectiveCarActivator(int[] Array, int PlaneNumber){
        int Count = 0;
        for(int i = PlaneNumber*4;i< PlaneNumber*4 + 4;i++,Count++){
            if (Array[Count] == 1){
                CarArrayListing[i].SetActive(true);
            }
        }
    }
    private void RespawnInitiator(int PlaneNumber){
        //Timer Reset
        Timer.TimerClock = 0.0f;
        //TaskHandler
        TaskHandler.TaskProcessor(PlaneNumber);
        
    }
     public static int[] RandomAllocation(int Check)
    {
        var randomInt = UnityEngine.Random.Range(0,2);
        int[] PathRandom = new int[]{0,0,0,0};
        int count = 0;
        //This is different from PathRandom, as it looks at the time + state
        int[] TaskCountHolder = new int[]{0,0,0,0};
        //Clock
        float HolderFloat = Timer.TimerClock;
        //Randomly Allocates all slots in the Variable to be 2, 1s and 0s
        //Pass through Int value
        while (true){
            foreach (int i in PathRandom){
                if (i == 1){
                    count++;
                }   
            }
            //Force
            //Ensures that only 2 objects can be spawned at a time
            // ! checks for True/False, 1 = exists, 2 = not exists.
            if (Check == 1){
                if (count != 2 || PathRandom[1] != 1) {
                count = 0;
                // Rand = new System.Random();
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
            //Car Deviations
            else if (Check == 0 || Check == 2){
                if (count != 2 || PathRandom[1] != 0 || PathRandom[Check] != 1) {
                count = 0;
                // Rand = new System.Random();
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
            
        }
        return PathRandom;
    }
}