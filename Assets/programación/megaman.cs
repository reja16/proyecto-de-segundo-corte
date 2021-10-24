using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class megaman : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] float jumpSpeed;
    [SerializeField] BoxCollider2D misPies;
    [SerializeField] float fireRate;
    [SerializeField] GameObject Bala;
    [SerializeField] GameObject Bala2;
    [SerializeField] private Transform disparador;

    Animator MyAnimator;
    Rigidbody2D myBody;
    BoxCollider2D myCollider;
    float movH;
    float nextFire = 0;
    int saltosExtras = 1;
    int contandoSaltos = 0;



    // Start is called before the first frame update
    void Start()
    {
        MyAnimator = GetComponent<Animator>();
        MyAnimator.SetBool("isrunning", false);
        myBody = GetComponent<Rigidbody2D>();
        myCollider = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        correr();
        saltodoble();
        caer();
        Disparar();
    }

    void Disparar()
    {
        if (Input.GetKeyDown(KeyCode.W) && Time.time >= nextFire)
        {
            MyAnimator.SetLayerWeight(1, 1);

            Instantiate(Bala, disparador.transform.position, disparador.rotation);
            nextFire = Time.time + fireRate;
        }
        else
        { 
         MyAnimator.SetLayerWeight(1, 0);
         
        }
        

    }
    void caer()
    {
        if(myBody.velocity.y<0)
        {
            MyAnimator.SetBool("falling", true);
        }
        else
        {
            MyAnimator.SetBool("falling", false);
        }
    }
    void Dash()
    {
       
    }

    bool suelo()
    {
        return misPies.IsTouchingLayers(LayerMask.GetMask("Ground"));
    }

    void saltodoble()
    {


        if (suelo())
        {
            MyAnimator.SetBool("falling", true);

            if (Input.GetKey(KeyCode.K))
            {
                myBody.AddForce(new Vector2(0, jumpSpeed), ForceMode2D.Impulse);
                MyAnimator.SetTrigger("jumping");
                contandoSaltos++;

            }
            else
                MyAnimator.SetBool("falling", false);
        }

        if (Input.GetKey(KeyCode.K) && contandoSaltos == saltosExtras && myBody.velocity.y < 0)
        {
            myBody.AddForce(new Vector2(0, jumpSpeed + Mathf.Abs(myBody.velocity.y)), ForceMode2D.Impulse);
            MyAnimator.SetTrigger("jumping");
            contandoSaltos = 0;
            Debug.Log("Salto doble");
        }
    
}

    

    public void correr()
    {
        float direccion = Input.GetAxis("Horizontal");

        transform.Translate(new Vector2(direccion * Time.deltaTime * speed, 0));

        if (direccion != 0)
        {
            if (direccion < 0)
            {
                transform.localScale = new Vector2(-1, 1);
                //transform.eulerAngles = new Vector3(0, 180, 0);
                
            }
            
            if(direccion > 0)
            { 
                transform.localScale = new Vector3(1, 1);
                //transform.eulerAngles = new Vector3(0, 180, 0);
                
            }
                
                

            MyAnimator.SetBool("isrunning", true);
        }
        else
            MyAnimator.SetBool("isrunning", false);

        transform.Translate(new Vector2(direccion * Time.deltaTime * speed, 0));
    }

   
    }

    
   

