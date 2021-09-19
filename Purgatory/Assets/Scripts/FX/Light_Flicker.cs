using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class Light_Flicker : MonoBehaviour
{
    public float min_Flicker_Wait;
    public float max_Flicker_Wait;

    private Light2D f_light;

    // Start is called before the first frame update
    void Start()
    {
        f_light = GetComponent<Light2D>();
        StartCoroutine(Flicker_Cycle());
    }

    public void Flicker()
    {       
        f_light.enabled = !f_light.enabled;
    }

    IEnumerator Flicker_Cycle()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(min_Flicker_Wait, max_Flicker_Wait));

            int flicker_Num = Random.Range(1, 4);

            for (int i = 0; i < flicker_Num; i++)
            {
                Flicker();
                yield return new WaitForSeconds(Random.Range(0.01f, 1.0f));
            }

            if (!f_light.enabled)
                f_light.enabled = true;
        }
    }
}
