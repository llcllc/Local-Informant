﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class P2Controller : MonoBehaviour{

    int childArrayValue = 0;
    int childArraySize;
    public int childCounter = 1;
    public Transform[] childs;
    public GameObject[] childObjects;
    public string playerNumberString = "p2-s";
    public string playerNumberStringSelection = "p2-selected";

    public Material activeMaterial;
    public Material backupMaterial;

    void Start()
    {
        // create array containing all children, to be used on the Interactive Objects game object
        childs = gameObject.GetComponentsInChildren<Transform>();
        childArraySize = childs.Length;
        childObjects = new GameObject[childArraySize];

        foreach (Transform trans in childs)
        {
            childArrayValue++;
            childObjects.SetValue(trans.gameObject, childArrayValue - 1);
        }
    }

    // Update is called once per frame
    void Update()
    {
        // player one movement between Interactive Objects;
		if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            childCounter++;

            //cap counter at array size and loop it
            if (childCounter >= childArraySize)
            {
                childCounter = 1;
            }

            if (childCounter < 1)
            {
                childCounter = childArraySize - 1;
            }
            activateObject(childCounter);
        }
		if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            childCounter--;

            //cap counter at array size and loop it
            if (childCounter >= childArraySize)
            {
                childCounter = 1;
            }

            if (childCounter < 1)
            {
                childCounter = childArraySize - 1;
            }
            activateObject(childCounter);
        }

		if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            childCounter++;

            //cap counter at array size and loop it
            if (childCounter >= childArraySize)
            {
                childCounter = 1;
            }

            if (childCounter < 1)
            {
                childCounter = childArraySize - 1;
            }
            activateObject(childCounter);

        }


        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            childCounter--;

            //cap counter at array size and loop it
            if (childCounter >= childArraySize)
            {
                childCounter = 1;
            }

            if (childCounter < 1)
            {
                childCounter = childArraySize - 1;
            }
            activateObject(childCounter);

        }


        // -------- Selection Input ------- //

        if (Input.GetKeyDown(KeyCode.N) || Input.GetKeyDown(KeyCode.M))
        {
            selectObject();
        }

        //   Debug.Log("P1 Counter = " + childCounter);
    }

    void activateObject(int index)
    {
        /*
            [name] => [name]-p1-active

            // Assigns a material named "Assets/Resources/DEV_Orange" to the object.
            Material newMat = Resources.Load("DEV_Orange", typeof(Material)) as Material;
            gameObject.renderer.material = newMat;
        */
        // change this line for player 2. It should work?
        string playerNumberString = "-p2-active";

        //first, turn all objects inactive:
        foreach (GameObject g in childObjects)
        {
            Renderer rnd = g.GetComponent<Renderer>();
            if (rnd != null)
            {
                string currentMaterialName = g.GetComponent<Renderer>().material.name;
                if (currentMaterialName.Contains(playerNumberString))
                {
                    currentMaterialName = currentMaterialName.Remove(currentMaterialName.Length - 11 - playerNumberString.Length);
                    // remove 11 for " (instance)" and 10 for playerNumberString
                    Material switchToThis = Resources.Load(currentMaterialName, typeof(Material)) as Material;
                    if (switchToThis != null)
                    {
                        // if loaded correctly, then change the material.
                        // print("Going to load " + currentMaterialName);
                        rnd.material = switchToThis;
                    }
                    else
                    {
                        // Debug.LogError("P1Controller.activateObject did not load a new texture for naviation. Name of texture: " + currentMaterialName);
                        if (backupMaterial != null)
                        {
                            rnd.material = backupMaterial;
                        }
                    }
                }
            }
        }

        //check of there is a game object in the array corrisponding to a index given
        if (childObjects[index] != null)
        {
            Renderer rnd = childObjects[index].GetComponent<Renderer>();
            if (rnd != null)
            {
                string currentMaterialName = rnd.material.name;
                currentMaterialName = currentMaterialName.Remove(currentMaterialName.Length - 11); // remove " (Instance)" from the name (11 chars)
                if (!currentMaterialName.Contains(playerNumberString))
                {
                    currentMaterialName = currentMaterialName + playerNumberString;
                    print(currentMaterialName + " after add");

                    // use that string to load the correct material:
                    Material switchToThis = Resources.Load(currentMaterialName, typeof(Material)) as Material;
                    if (switchToThis != null)
                    {
                        // if loaded correctly, then change the material.
                        //print("Going to load " + currentMaterialName);
                        rnd.material = switchToThis;
                    }
                    else
                    {
                        // Debug.LogError("P1Controller.activateObject did not load a new texture for naviation. Name of texture: " + currentMaterialName);
                        if (backupMaterial != null)
                        {
                            rnd.material = backupMaterial;
                        }
                    }
                }
                //Debug.Log(childObjects[index] + " is now active");
            }
        }
    }
    void selectObject()
    {

        // checl isSelected bool true in child object
        if (childObjects[childCounter] != null)
        {
            childObjects[childCounter].GetComponent<Stats>().isSelected = true;
        }

        // change material
        Debug.Log(childObjects[childCounter] + " is Selected!");
        if (childObjects[childCounter] != null)
        {
            Renderer rnd = childObjects[childCounter].GetComponent<Renderer>();
            if (rnd != null)
            {
                string currentMaterialName = rnd.material.name;
                currentMaterialName = currentMaterialName.Remove(currentMaterialName.Length - 20); // remove " (Instance)" from the name (11 chars)
                if (!currentMaterialName.Contains(playerNumberStringSelection))
                {
                    currentMaterialName = currentMaterialName + playerNumberStringSelection;
                    print(currentMaterialName + " after add");

                    // use that string to load the correct material:
                    Material switchToThis = Resources.Load(currentMaterialName, typeof(Material)) as Material;
                    if (switchToThis != null)
                    {
                        // if loaded correctly, then change the material.
                        //print("Going to load " + currentMaterialName);
                        rnd.material = switchToThis;
                    }
                    else
                    {
                        //  Debug.LogError("P1Controller.activateObject did not load a new texture for naviation. Name of texture: " + currentMaterialName);

                        rnd.material = Resources.Load(currentMaterialName, typeof(Material)) as Material;


                    }
                    //Debug.Log(childObjects[index] + " is now active");
                }
            }

        }
    }
}