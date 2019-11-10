using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorScript : MonoBehaviour
{
    public Material[] materials;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(WaitToChange());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private IEnumerator WaitToChange() {
        yield return new WaitForSeconds(5);
        for (int n = 0; n > 100; n += 10)
        {
            for (int i = 0; i < 10; i++)
            {
                transform.GetChild(n+i).GetComponent<Renderer>().materials[0] = materials[0];
            }
        }
        yield return new WaitForSeconds(1);

    }
}
