using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class PlayerController : MonoBehaviour
{
    private Rigidbody rb;
    private float movement_x, movement_y;
    public float speed = 0;
    public TextMeshProUGUI countText;
    private int count;
    public TextMeshProUGUI winTextObject;
    private bool color;
    private int num_cubes;
    public Material Material1;
    public Material Material2;
    private AudioSource source;
    public AudioClip plusSound;
    public AudioClip minusSound;
    public AudioClip complete;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        count = 0;
        color = true;
        num_cubes = 22;
        SetCountText();
        winTextObject.text="";
        source=GetComponent<AudioSource>();
    }

    void OnMove(InputValue movementValue)
    {
        Vector2 movementVector = movementValue.Get<Vector2>(); //get movement
        movement_x = movementVector.x;
        movement_y = movementVector.y;
    }
 
    void SetCountText(){
        countText.text = "Points: "+count.ToString();
        if (num_cubes==0){
            source.PlayOneShot(complete, 1F);
            string win_text ="Total points = "+count.ToString();
            if(count==22){
                win_text+="\nCongratulations you have the max score!";
            }
            else if(count<=0){
                win_text+="\nYou need some improvement";
            }
            else if((count>0)&&(count<10)){
                win_text+="\nGood Job!";
            }
            else if((count<22)&&(count>=10)){
                win_text+="\nNice Job!";
            }
            winTextObject.text=win_text;
            //winTextObject.SetActive(true);
        }
    }

    void FixedUpdate () {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");
        Vector3 movement = new Vector3 (moveHorizontal, 0.0f, moveVertical) * speed;
        rb.AddForce(movement);
    }
    private void OnTriggerEnter(Collider other){
        if(other.gameObject.CompareTag("Capsule")){
            color = !color;
            if(color){
                other.GetComponent<MeshRenderer> ().material = Material1;
            }
            else{
                other.GetComponent<MeshRenderer> ().material = Material2;
            }
        }
        else if(color==true){
            if(other.gameObject.CompareTag("PickUp")){
                source.PlayOneShot(plusSound, 1F);
                other.gameObject.SetActive(false);
                count++;
                num_cubes--;
                SetCountText();
            }
            if(other.gameObject.CompareTag("Obstacle")){
                source.PlayOneShot(minusSound, 1F);
                other.gameObject.SetActive(false);
                count--;
                num_cubes--;
                SetCountText();
            }
        }
        else if(color==false){
            if(other.gameObject.CompareTag("PickUp")){
                source.PlayOneShot(minusSound, 1F);
                other.gameObject.SetActive(false);
                count--;
                num_cubes--;
                SetCountText();
            }
            if(other.gameObject.CompareTag("Obstacle")){
                source.PlayOneShot(plusSound, 1F);
                other.gameObject.SetActive(false);
                count++;
                num_cubes--;
                SetCountText();
            }
        }
    }
    // Update is called once per frame
    /*void Update()
    {
        
    }*/
}
