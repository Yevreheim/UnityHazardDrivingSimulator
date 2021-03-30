using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Threading;
using System.Linq;


public class TaskHandler : MonoBehaviour {
    private static float GlobalTimer;
    private static float TaskTimerController;
    private static float SecondTaskTimer;
    private static float SecondTaskReference;
    private static float PlaneHolder;

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
        SecondTaskTimer = 0;
        SecondTaskReference = 0;

    }

    void Update(){
        GlobalTimer = Timer.GlobalClock;
        SecondTaskTimer = Timer.GlobalClock;
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

        //Second Task Recognition
        if (SecondTaskReference != 0){
            if (SecondTaskTimer > 0.4){
                //Visual
                if (SecondTaskReference == 1){
                    RespawningHierarchy.SelectiveCarActivator(RespawningHierarchy.RandomAllocation(1),PlaneHolder);
                }
                //Lane Deviation
                else if (SecondTaskReference == 2){
                    TaskDeviation.Signal = 1;
                }
                //Auditory
                else if (SecondTaskReference == 3){
                    AuditoryDistraction.Signal = 1;
                }
                //Emergency Braking
                else if (SecondTaskReference == 4){
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
                SecondTaskReference = 0;
            }
        }
    }


    public static void TaskProcessor(int PlaneNumber){
        //This thing throw back out into RH
        RespawningHierarchy.RespawningCarDeviation = 0;
        //Main
        int RandomSelection = UnityEngine.Random.Range(0,4);
        TaskArray[RandomSelection]++;
        //Visual Car Distraction
        if (RandomSelection == 0){
            PlayerController.TaskList.Add("Visual");
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
            PlaneHolder = PlaneNumber;
            SecondTaskReference = 3;
        }
        //Auditory -> Lane Deviation
        else if (RandomSelection == 5){
            PlayerController.TaskList.Add("A/LD");
            PlaneHolder = PlaneNumber;
            SecondTaskReference = 2;
        }
        //Lane Deviation -> Visual
        else if (RandomSelection == 6){
            PlayerController.TaskList.Add("LD/V");
            PlaneHolder = PlaneNumber;
            SecondTaskReference = 1;
        }
        //Visual -> Lane Deviation
        else if (RandomSelection == 7){
            PlayerController.TaskList.Add("V/LD");
            PlaneHolder = PlaneNumber;
            SecondTaskReference = 2;
        }
        //Emergency Braking -> Auditory
        else if (RandomSelection == 8){
            PlayerController.TaskList.Add("EB/A");
            PlaneHolder = PlaneNumber;
            SecondTaskReference = 3;
        }
        //Auditory -> Emergency Braking
        else if (RandomSelection == 9){
            PlayerController.TaskList.Add("A/EB");
            PlaneHolder = PlaneNumber;
            SecondTaskReference = 4;
        }
        //Emergency Braking -> Visual
        else if (RandomSelection == 10){
            PlayerController.TaskList.Add("EB/V");
            PlaneHolder = PlaneNumber;
            SecondTaskReference = 1;
        }
        //Visual -> Emergency Braking
        else if (RandomSelection == 11){
            PlayerController.TaskList.Add("V/EB");
            PlaneHolder = PlaneNumber;
            SecondTaskReference = 4;
        }
    }
}