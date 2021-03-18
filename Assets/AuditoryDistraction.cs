using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using UnityEngine.TextCore;

public class AuditoryDistraction : MonoBehaviour
{
    // Start is called before the first frame update
    public static GameObject[] ADTextObjects;
    public static int Signal;
    private static int FlipFlop;
    private static int RandomAnimal;

    void Start()
    {
        Signal = 0;
        FlipFlop = 0;
        RandomAnimal = 0;

        ADTextObjects = GameObject.FindGameObjectsWithTag("AudioDist");
        foreach (GameObject G in ADTextObjects){
            G.GetComponent<Text>().text ="";
            G.SetActive(false);
        }
        ADTextObjects[0].GetComponent<Text>().text = "Fish";
        ADTextObjects[1].GetComponent<Text>().text = "Dog";
        ADTextObjects[2].GetComponent<Text>().text = "Cat";
        ADTextObjects[3].GetComponent<Text>().text = "Chicken";

    }

    // Update is called once per frame
    void Update()
    {
        if (Signal == 1){
            if (FlipFlop == 0){
                RandomAnimal = UnityEngine.Random.Range(0,4);
                // PlayerController.TaskList.Add("Visual Distraction - Sound");
                FlipFlop = 1;
            }
            for (int i = 0; i < ADTextObjects.Length; i++){
                if (RandomAnimal == i){
                    ADTextObjects[i].SetActive(true);
                }
            }
        }
        else if (Signal == 0){
            FlipFlop = 0;
        }
    }
}
 