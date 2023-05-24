using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Score : MonoBehaviour
{
    public int winScore;
    public static bool IsFilmingEnnemy; // booléen qui permet de vérifier si le joueur filme l'ennemi
                                  // il est mis a jour dans le script CameraRaycast, lorsqu'un raycast entre en colision avec l'ennemi
    public float coolDownTime;

    private int currentScore;
    private float currentCD;
    private RaycastHit hit;
    private Ray ray;
    private StorageBar storageBar;

    public void SetStorageBar(StorageBar _sb)
    {
        storageBar = _sb;
    }
    private void Awake()
    {
        currentScore = 0;
        currentCD = coolDownTime;
    }
    
    void Update()
    {
        if (IsFilmingEnnemy)
        {
            if (currentCD <= 0.0f)
            {
                currentScore += 1;
                storageBar.SetStorage(currentScore,winScore);
                Debug.Log(currentScore);
                
                if (currentScore >= winScore)
                {
                    StartCoroutine(PlayerUI.Notify("You Won !", 2f));
                    Time.timeScale = 0f;
                }
                currentCD = coolDownTime;
            }
            else
                currentCD -= Time.deltaTime; 
        }
    }
}
