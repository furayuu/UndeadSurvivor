using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class HPBar : MonoBehaviour
{

    int maxHp = 100;
    int currentHp;
    public Slider slider;
   
    // Start is called before the first frame update
    void Start()
    {

        slider.value = 1;

        currentHp = maxHp;  
        //Debug.Log("Start currentHp : " + currentHp);
    }


     public void OnTriggerEnter2D(Collider2D colider2d)
    {

        if (colider2d.gameObject.tag == "Enemy")
        {

            int damage = Random.Range(1, 20);
            //Debug.Log("damage : " + damage);


            currentHp = currentHp - damage;
            //Debug.Log("After currentHp : " + currentHp);

            slider.value = (float)currentHp / (float)maxHp; 
            Debug.Log("slider.value : " + slider.value);
        }
    }
}


