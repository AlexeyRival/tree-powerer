using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    [SerializeField]
    public GameObject getEffect, dieEffect, ui, dieui, ingameui;
    [SerializeField]
    public Text recordText, prevRecordText, scoreText;
    [SerializeField]
    private Swipe swipe;
    private int score, record;
    private Rigidbody rb;
    private int rotx;
    private bool alive;
    private byte jumptimer;
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        try
        {
            StreamReader file = new StreamReader(Application.persistentDataPath + "/data.ixo");
            record = int.Parse(file.ReadLine());
            file.Close();
        }
        catch {
            record = 1;
        }
        prevRecordText.text = record.ToString();
    }
    public void Go() {
        alive = true;
        ui.SetActive(false);
        ingameui.SetActive(true);
    }
    public void Restart() {
        StreamWriter file = new StreamWriter(Application.persistentDataPath + "/data.ixo");
        file.WriteLine(record);
        file.Close();
        Application.LoadLevel(0);
    }
    public void Exit() {
        Application.Quit();
    }
    void Update()
    {
        if (alive)
        {
            transform.Translate(0, 0, (0.015f + 0.00125f * score)*Time.deltaTime*150);
            if (swipe.swipeUp&&jumptimer==0)
            {
                rb.AddRelativeForce(0, 6, 0, ForceMode.Impulse);
                jumptimer = 255;
            }
            if (swipe.swipeLeft)
            {
                rotx -= 15;
            }
            if (swipe.swipeRight)
            {
                rotx += 15;
            }

            if (rotx > 0)
            {
                transform.Rotate(0, 1, 0);
                --rotx;
            }
            if (rotx < 0)
            {
                transform.Rotate(0, -1, 0);
                ++rotx;
            }
            if (jumptimer > 0) {
                --jumptimer;
            }
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("Eat"))
        {
            ++score;
            scoreText.text = score.ToString();
            Destroy(Instantiate(getEffect,transform.position,transform.rotation),1f);
            Destroy(collision.gameObject);
        }
        else
        if (collision.transform.CompareTag("Problem"))
        {
            transform.GetChild(0).parent = null;
            dieui.SetActive(true);
            if (record < score) {
                record = score;
            }
            ingameui.SetActive(false);
            recordText.text = "Счёт: "+score;
            Destroy(Instantiate(dieEffect,transform.position,transform.rotation),1f);
            GetComponent<MeshRenderer>().enabled = false;
            rb.isKinematic = true;
            alive = false;
        }
    }
}
