using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Revolver : MonoBehaviour
{
    private int lastBulletCount = 6;
    public int bulletCount = 6;

    public Transform[] Chambers;  

    private void Update()
    {
        if(bulletCount != lastBulletCount)
        {
            UpdateBullets();
            lastBulletCount = bulletCount;
        }
    }   

    public void UpdateBullets()
    {
        for(int i = 0; i < (Chambers.Length); i++)
        {
            if (bulletCount + i < Chambers.Length)
            {
                Chambers[i].GetChild(0).gameObject.SetActive(false);
            }
            else
            {
                Chambers[i].GetChild(0).gameObject.SetActive(true);
            }
        }
    }

    public void RefillBullets()
    {
        bulletCount = 6;
    }
}
