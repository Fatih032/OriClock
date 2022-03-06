using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class sayac : MonoBehaviour
{
    public float saniye;
    public int dakika;
    public int saat;
    public Text zaman_t;
    bool timerActive = false;

    void Start()
    {
       saniye = 0;
       dakika = 0;
       saat = 0;

       zaman_t.text = ""+saat.ToString("00") + ":" + dakika.ToString("00") + ":" + saniye.ToString("00");
    }

    // Update is called once per frame
    void Update()
    {
        if(timerActive) {
        saniye += Time.deltaTime;
        zaman_t.text = ""+saat.ToString("00") + ":" + dakika.ToString("00") + ":" + saniye.ToString("00");
        }
        if(saniye>59){
            dakika += 1;
            saniye = 0;
            
        }
        if(dakika>59){
            saat += 1;
            dakika =0;
        }
    }
    public void play() {
        timerActive = true;
        } 
    public void pause(){
        timerActive = false;
    }
    public void reset(){
        timerActive = false;
        saniye=0;
        saat=0;
        dakika=0;
        zaman_t.text = ""+saat.ToString("00") + ":" + dakika.ToString("00") + ":" + saniye.ToString("00");
    }
}
