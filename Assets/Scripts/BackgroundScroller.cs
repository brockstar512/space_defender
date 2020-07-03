using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundScroller : MonoBehaviour
{
    [SerializeField] float backgroundScrollSpeed = 0.5f;
    //this is also known as Structs
    //I think its Material because I am making it reference a variable that references the material component
    Material myMaterial;
    Vector2 offSet;

    // Start is called before the first frame update
    void Start()
    {
        //grabbing the background material
        myMaterial = GetComponent<Renderer>().material;
        offSet = new Vector2(0f,backgroundScrollSpeed);
    }

    // Update is called once per frame
    void Update()
    {
        //this grabs the textire background movements coord
     myMaterial.mainTextureOffset += offSet * Time.deltaTime;
    }
}
