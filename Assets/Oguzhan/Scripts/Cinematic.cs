using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cinematic : MonoBehaviour
{
    private Animator kidAnimator;
    private Transform camera;
    private Transform kid;
    private GameObject ui;
    
    
    // Start is called before the first frame update
    void Start()
    {
        ui = GameObject.Find("UI");
        kidAnimator = GameObject.Find("Cinematickid").GetComponent<Animator>();
    }
    
    public void HitFridge()
    {
        kidAnimator.SetTrigger("HitFridge");
        Destroy(kidAnimator.gameObject, 2);
    }

    public void StopAnimation()
    {
        ui.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
        Destroy(GetComponent<Animator>());
    }
}
