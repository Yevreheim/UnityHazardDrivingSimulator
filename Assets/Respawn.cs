using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Threading;
using System.Linq;

public class Respawn : MonoBehaviour
{
    //Variables
    private float x = 0;
    private float y = 0;
    private float z = 0;
    private float initialX;
    private float initialY;
    private float initialZ;
    int[] Show = new int[2];
    public static GameObject[] CellArray;
    private List<GameObject> Car = new List<GameObject>();
    private System.Random Rand = new System.Random();
    int CountTwo = 0;
    private GameObject[] CarArray = new GameObject[4];
    private GameObject TestObject;
    private static Respawn Instance;
    private int Phase = 0;
    //LaneDeviationSpeed
    private float LaneDeviationSpeed;
    private int RespawningCarDeviation;
    private int LeftRightCarMove = 0;
    

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
                CarArray[CountTwo] = G;
                G.SetActive(false);
                CountTwo++;
            }
        }
        //RandomAllocation();
        Phase = 2;
        TestObject = GameObject.FindWithTag("MainCamera");
        RespawningCarDeviation = 0;
    }
    void Awake(){
        Instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        float LaneTimer = Timer.TimerClock;
        x = GameObject.FindWithTag("MainCamera").transform.position.x;
        y = GameObject.FindWithTag("MainCamera").transform.position.y;
        z = GameObject.FindWithTag("MainCamera").transform.position.z;

        if (Input.GetKey(KeyCode.X)){
            Debug.Log(Timer.TimerClock.ToString());
            Thread.Sleep(100);
        }
        if (Input.GetKey(KeyCode.H)){
            CarDeviation();
        }
        //Car Deviation Distraction
        if (RespawningCarDeviation == 1){
            if (CarArray[LeftRightCarMove].activeSelf == true){
                if (LeftRightCarMove == 0 && CarArray[LeftRightCarMove].transform.position.x < 6){
                    CarArray[LeftRightCarMove].transform.Translate(new Vector3(0,0,-2*Time.deltaTime));
                }
                else if (LeftRightCarMove == 2 && CarArray[LeftRightCarMove].transform.position.x > 6){
                    CarArray[LeftRightCarMove].transform.Translate(new Vector3(0,0,2*Time.deltaTime));
                }
                else {
                    RespawningCarDeviation = 0;
                }
            }
        }

        //Respawning Process
        if (z > transform.position.z + 5)
        {
            //Randomisation between tasks occur here
            //Changing this timer variable can taken in via cases
            if (LaneTimer > 8){
                //Reset Time somewhere
                Timer.TimerClock = 0.0f;
                //List of tasks generated
                int RandomSelection = UnityEngine.Random.Range(0,4);
                TaskSelection(RandomSelection);
                //Cars spawning + Press Button
                
            }
            //Reset Section
            else {
                foreach(GameObject G in CarArray){
                    if (G.tag == "Car"){
                        G.SetActive(false);
                    }
                }
            }
            //This part doesn't need to know about anything else
            // Modify by 10 for each plane
            transform.position = new Vector3(0,0,(transform.position.z + 30));
            Show = ArrayRandomiser();
            Debug.Log("New Position: " + transform.position.z);
        }
    }

    private void TaskSelection(int RandomSelection){
                //See a car infront
                if (RandomSelection == 0){
                    //PlayerController.TaskList.Add("Visual Distraction - Car");
                    RandomAllocation(1);
                }
                //Lane Deviation
                else if (RandomSelection == 1){
                    PlayerController.TaskList.Add("Visual Distraction - Lane");
                    TaskDeviation.Signal = 1;
                }
                //Sound and Select -- Auditory Distraction
                else if (RandomSelection == 2){
                    PlayerController.TaskList.Add("Visual Distraction - Sound");
                    AuditoryDistraction.Signal = 1;
                }
                //Emergency Braking
                else if (RandomSelection == 3){
                    PlayerController.TaskList.Add("Lane Distraction - Car Moving");
                    int QuickRandom = UnityEngine.Random.Range(0,2);
                    if (QuickRandom == 0){
                        LeftRightCarMove = 0;
                    }
                    else if (QuickRandom == 1){
                        LeftRightCarMove = 2;
                    }
                    RandomAllocation(LeftRightCarMove);
                    //Triggers Deviation
                    RespawningCarDeviation = 1;
                }
                //Lane Deviation -> Auditory
                else if (RandomSelection == 4){

                }
                //Auditory -> Lane Deviation
                else if (RandomSelection == 5){

                }
                //Lane Deviation -> Visual
                else if (RandomSelection == 6){

                }
                //Visual -> Lane Deviation
                else if (RandomSelection == 7){

                }
                //Emergency Braking -> Auditory
                else if (RandomSelection == 8){

                }
                //Auditory -> Emergency Braking
                else if (RandomSelection == 9){

                }
                //Emergency Braking -> Visual
                else if (RandomSelection == 10){

                }
                //Visual -> Emergency Braking
                else if (RandomSelection == 11){

                }
    }
    private void CarDeviation(){
        int NegPos = UnityEngine.Random.Range(0,2);
        if (NegPos == 0) {
            LaneDeviationSpeed = -2;
        }
        else if (NegPos == 1){
            LaneDeviationSpeed = 2;
        }
        if (true){
            TestObject.transform.Translate(new Vector3(LaneDeviationSpeed*Time.deltaTime,0,0));
        }
    }

    //Respawn Algorithm
    private void RandomAllocation(int Check)
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
            //Car Deviations
            else if (Check == 0 || Check == 2){
                if (count != 2 || PathRandom[1] != 0 || PathRandom[Check] != 1) {
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
            
        }
        //Checks Time + State to activate or deactive Cars
        for (int i = 0; i < PathRandom.Length; i++)
        {
            //Could have the functiom below return a value when True is done
            //For loop to check an array
            TaskCountHolder[i] = DistractionActive(PathRandom, HolderFloat, i);
            CarArray[i].transform.position = new Vector3(i*6f,0,(transform.position.z));
        }

        //Adds into Task count
        // if (Check == 1){
        //     PlayerController.TaskList.Add("Visual Distraction - Car");
        // }
        // else if (Check == 0 || Check == 2){
        //     PlayerController.TaskList.Add("Lane Distraction - Car Moving");
        // }

    }
    
    //Rendering
    private int DistractionActive(int[] PathRandom, float HolderFloat, int i)
    {  
        if (PathRandom[i] == 1)
        {
            CarArray[i].SetActive(true);
            return 1;
        }
        else
        {
            CarArray[i].SetActive(false);
            return 0;
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
