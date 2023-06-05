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

    public void ChangeTex()
    {
        SkinnedMeshRenderer mat = GameObject.Find("Ekran").GetComponent<SkinnedMeshRenderer>();
        mat.materials[1].SetTexture("_MainTex",(Texture)Resources.Load("angry"));
    }
}
