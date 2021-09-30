using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Revolver : MonoBehaviour
{
    private int lastBulletCount = 6;
    public int bulletCount = 6;

    public Animator animator;

    private void Start()
    {
        animator.SetInteger("Num_Bullets", bulletCount);
    }

    private void Update()
    {
        if(bulletCount != lastBulletCount)
        {
            animator.SetInteger("Num_Bullets", bulletCount);
            lastBulletCount = bulletCount;
        }
    }

    public void RefillBullets()
    {
        bulletCount = 6;
    }
}
