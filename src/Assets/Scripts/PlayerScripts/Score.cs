using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Score : MonoBehaviour
{
    public int winScore;
    public static bool isFilming; // booléen qui permet de vérifier si le joueur filme l'ennemi
                                  // il est mis a jour dans le script CameraRaycast, lorsqu'un raycast entre en colision avec l'ennemi

    private int currentScore;
    private RaycastHit hit;
    private Ray ray;
    private float coolDown;
    private StorageBar storageBar;

    public void SetStorageBar(StorageBar _sb)
    {
        storageBar = _sb;
    }
    private void Awake()
    {
        currentScore = 0;
        coolDown = 1.5f;
    }
    
    void Update()
    {
        if (isFilming)
        {
            if (coolDown <= 0.0f)
            {
                currentScore += 1;
                storageBar.SetStorage(currentScore,winScore);
                Debug.Log(currentScore);
                
                if (currentScore >= winScore)
                {
                    Debug.Log("VOUS AVEZ GAGNE !!");
                    Time.timeScale = 0f;
                }
                coolDown = 1.5f;
            }
            else
                coolDown -= Time.deltaTime; 
        }
    }
}
