using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class saat : MonoBehaviour
{
    public string saniye;
    public Text saat_t;
    public GameObject ses;
    public GameObject GeriSayımSes;
    int secondsInt;
    int hoursInt;
    public Toggle secim;
    public Toggle secim2;

    void Start(){
        secim.GetComponent<Toggle>();
        secim2.GetComponent<Toggle>();
    }
 
    void Update()
    {
        string seconds = System.DateTime.Now.ToString("ss");

        if(seconds != saniye) {
            UpdateTimer();
        }
        saniye = seconds;
        secim_durum(secim.isOn);
        zamanDilimi(secim2.isOn);

        
    }

    void UpdateTimer() {
        secondsInt = int.Parse(System.DateTime.Now.ToString("ss"));
        int minutesInt = int.Parse(System.DateTime.Now.ToString("mm"));
        saat_t.text = ""+hoursInt.ToString("00")+ ":" + minutesInt.ToString("00")+ ":" + secondsInt.ToString("00");

        if(secondsInt >= 56){

            GeriSayımSes.SetActive(true);
        }
        if(secondsInt==3)
        {

            ses.SetActive(false);
        }
        if(secondsInt == 0){
            ses.SetActive(true);
            GeriSayımSes.SetActive(false);

        }

        


    }
    public void secim_durum(bool deger){
        if(deger == true){
            if(secondsInt==26){
                GeriSayımSes.SetActive(true);
            }
            if(secondsInt==30){
               GeriSayımSes.SetActive(false);
                ses.SetActive(true);
            }
            if(secondsInt==31){
                ses.SetActive(false);
            }
        }
    }
    public void zamanDilimi(bool deger1){
        if(deger1 == true){
            hoursInt = 12 + int.Parse(System.DateTime.Now.ToString("hh"));

        }
        if(deger1 == false){
            hoursInt = int.Parse(System.DateTime.Now.ToString("hh"));
        }
    }
}
