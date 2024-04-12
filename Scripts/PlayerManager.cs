using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour
{

    Rigidbody2D playerRB;
    public float health, bulletSpeed;
    public bool dead = false;
    Transform muzzle;
    public Transform bullet;
    public Slider slider;

    public GameObject inGameScreen, pauseScreen;
    bool mouseIsNotOverUI;

    // Start is called before the first frame update
    void Start()
    {
        playerRB = GetComponent<Rigidbody2D>();
        muzzle = transform.GetChild(1);
        slider.maxValue = health;
        slider.value = health;
    }

    // Update is called once per frame
    void Update()
    {
        mouseIsNotOverUI=EventSystem.current.currentSelectedGameObject == null;
        if (Input.GetMouseButtonDown(0) && mouseIsNotOverUI)
        {
            ShootBullet();
            
        }
    }
    
    public void GetDamage(float damage)
    {
        if ((health - damage) >= 0)
        {
            health -= damage;
        }
        else
        {
            health = 0;
        }
        slider.value = health;
        AmIDad();
    }
    void AmIDad()
    {
        if(health<=0)
        {
            dead=true;
            PauseButton();
        }
        
    }
   
    void ShootBullet()
    {
        Transform tempBullet;
        tempBullet = Instantiate(bullet, muzzle.position, Quaternion.identity);
        tempBullet.GetComponent<Rigidbody2D>().AddForce(muzzle.forward * bulletSpeed);
        DataManager.instance.ShotBullet++;
    }
    public void PauseButton()
    {
        Time.timeScale = 0;
        inGameScreen.SetActive(false);
        pauseScreen.SetActive(true);
        
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Zemin")
        {
            PauseButton();
        }


        if (collision.gameObject.tag == "Coin")
        {
            
            Destroy(collision.gameObject);
            //SceneManager.LoadScene("Oyun2");
        }
    }

}
