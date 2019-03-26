using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TestMaterialAnimation : MonoBehaviour
{
    public Material testMaterial;

    public float xSpeed;
    public float ySpeed;

    private float noiseX;
    private float noiseY;

    // Start is called before the first frame update
    void Start()
    {
        noiseX = testMaterial.GetTextureOffset("_MainTex").x;
        noiseY = testMaterial.GetTextureOffset("_MainTex").y;
    }

    // Update is called once per frame
    void Update()
    {
        noiseX += (xSpeed * Time.deltaTime);
        if (noiseX > 1)
        {
            noiseX -= 1;
        }
        else if (noiseX < 1)
        {
            noiseX += 1;
        }

        noiseY += (ySpeed * Time.deltaTime);
        if (noiseY > 1)
        {
            noiseY -= 1;
        }
        else if (noiseY < 1)
        {
            noiseY += 1;
        }

        testMaterial.SetTextureOffset("_MainTex", new Vector2(noiseX, noiseY));
    }
}
