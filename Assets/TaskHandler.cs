using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Threading;
using System.Linq;


public class TaskHandler : MonoBehaviour {
    private static float GlobalTimer;
    private static float TaskTimerController;


    //Array to count both variations
    public static int TaskLaneDeviation = 0;
    public static int TaskEmergencyBraking = 0;
    public static int TaskAuditoryDistraction = 0;
    public static int TaskVisualCarDistraction = 0;
    public static int[] TaskLDA = new int[]{0,0};
    public static int[] TaskLDV = new int[]{0,0};
    public static int[] TaskEBA = new int[]{0,0};
    public static int[] TaskEBV = new int[]{0,0};
    private static int TaskCount = 9;
    private static int[] TaskArray = new int[]{0,0,0,0,0,0,0,0,0,0,0,0};

    void Start()    
    {
        GlobalTimer = 0;

        

    }

    void Update(){
        GlobalTimer = Timer.GlobalClock;

    }


    public static void TaskProcessor(int PlaneNumber){
        //This thing throw back out into RH
        RespawningHierarchy.RespawningCarDeviation = 0;
        //Main
        int RandomSelection = UnityEngine.Random.Range(0,4);
        TaskVisualCarDistraction = TaskArray[0];
        TaskLaneDeviation = TaskArray[1];
        TaskAuditoryDistraction = TaskArray[2];
        TaskEmergencyBraking = TaskArray[3];
        TaskLDA[0] = TaskArray[4];
        TaskLDA[1] = TaskArray[5];
        TaskLDV[0] = TaskArray[6];
        TaskLDV[1] = TaskArray[7];
        TaskEBA[0] = TaskArray[8];
        TaskEBA[1] = TaskArray[9];
        TaskEBV[0] = TaskArray[10];
        TaskEBV[1] = TaskArray[11];
        //Exception Handler
        // while (true) {
        //     int Lock = 0;
        //     for(int i = 0; i < TaskArray.Length; i++){
        //         if (RandomSelection == i && TaskArray[i] <= 9){
        //             //Task Approved
        //             TaskArray[i] += 1;
        //             Lock = 1;
        //             break;
        //         }
        //         else {
        //             RandomSelection = UnityEngine.Random.Range(0,4);
        //             break;
        //         }
        //     }
        //     if (Lock == 1){
        //         break;
        //     }
        // }
        //Visual Car Distraction
        if (RandomSelection == 0){
            PlayerController.TaskList.Add("Visual");
            Debug.Log("Visual Distraction - Car Appreance");
            //Respawn.RandomAllocation(1, PlanePosition);
            RespawningHierarchy.SelectiveCarActivator(RespawningHierarchy.RandomAllocation(1),PlaneNumber);
            
        }
        //Lane Deviation
        else if (RandomSelection == 1){
            PlayerController.TaskList.Add("LD");
            TaskDeviation.Signal = 1;
        }
        //Sound and Select -- Auditory Distraction
        else if (RandomSelection == 2){
            PlayerController.TaskList.Add("Auditory");
            AuditoryDistraction.Signal = 1;
        }
        //Emergency Braking
        else if (RandomSelection == 3){
            PlayerController.TaskList.Add("EB");
            int QuickRandom = UnityEngine.Random.Range(0,2);
            if (QuickRandom == 0){
                RespawningHierarchy.CarMovement = 0;
            }
            else if (QuickRandom == 1){
                RespawningHierarchy.CarMovement = 2;
            }
            RespawningHierarchy.SelectiveCarActivator(RespawningHierarchy.RandomAllocation(RespawningHierarchy.CarMovement),PlaneNumber);
            RespawningHierarchy.RespawningCarDeviation = 1;
            RespawningHierarchy.CarMovementPlaneReference = PlaneNumber;
        }
        //Lane Deviation -> Auditory
        else if (RandomSelection == 4){
            PlayerController.TaskList.Add("LD/A");
        }
        //Auditory -> Lane Deviation
        else if (RandomSelection == 5){
            PlayerController.TaskList.Add("A/LD");
        }
        //Lane Deviation -> Visual
        else if (RandomSelection == 6){
            PlayerController.TaskList.Add("LD/V");
        }
        //Visual -> Lane Deviation
        else if (RandomSelection == 7){
            PlayerController.TaskList.Add("V/LD");
        }
        //Emergency Braking -> Auditory
        else if (RandomSelection == 8){
            PlayerController.TaskList.Add("EB/A");
        }
        //Auditory -> Emergency Braking
        else if (RandomSelection == 9){
            PlayerController.TaskList.Add("A/EB");
        }
        //Emergency Braking -> Visual
        else if (RandomSelection == 10){
            PlayerController.TaskList.Add("EB/V");
        }
        //Visual -> Emergency Braking
        else if (RandomSelection == 11){
            PlayerController.TaskList.Add("V/EB");
        }
    }
}