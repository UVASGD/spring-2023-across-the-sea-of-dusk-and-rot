using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuItem : MonoBehaviour
{
    public int SceneId;
    private Renderer renderer;
    private Color startcolor;
    // Start is called before the first frame update
    void Start()
    {
        renderer = GetComponent<Renderer>();
        startcolor = Color.yellow;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    void OnMouseEnter()
    {
        print("Mouse is Over");
        startcolor = renderer.material.color;
        renderer.material.color = Color.yellow;
    }
    void OnMouseExit()
    {
        print("ByeBye Mouse");
        renderer.material.color = startcolor;
    }
    void OnMouseDown()
    {
        SceneManager.LoadScene(SceneId);
    }

}
