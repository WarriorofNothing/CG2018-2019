using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class speed : MonoBehaviour {
    Animation anim;
    public Slider slider;

    // Use this for initialization
    void Start(){
        anim = GetComponent<Animation>();
        anim.Play("Take 001");
        anim["Take 001"].speed = 1.0f;
    }

    // Update is called once per frame
    void Update(){
        anim["Take 001"].speed = slider.value;
        if(slider.value == 0){
            anim["Take 001"].speed = 1.0f;
        }
     
        else if(slider.value > 0 && slider.value <= 0.25){
            anim["Take 001"].speed = 1.25f;
        }

        else if (slider.value > 0.25 && slider.value < 0.5) {
            anim["Take 001"].speed = 1.35f;
        }

        else if (slider.value == 0.5) {
            anim["Take 001"].speed = 1.5f;
        }

        else if (slider.value > 0.5 && slider.value <=0.75){
            anim["Take 001"].speed = 1.6f;
        }

        else if (slider.value > 0.75 && slider.value < 1){
            anim["Take 001"].speed = 1.8f;
        } 

        else if (slider.value == 1) {
            anim["Take 001"].speed = 2.0f;
        }
    }
}