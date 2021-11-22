
// Deprecated code. Refactored away from using this.







//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class scr_BounceShot : MonoBehaviour
//{
//    public float shotSpeed;
//    private Rigidbody2D rb;

//    // Start is called before the first frame update
//    void Start()
//    {
//        //rb = gameObject.GetComponent<Rigidbody2D>();
//        //rb.velocity = transform.up * shotSpeed * Time.deltaTime;
//    }

//    // Update is called once per frame
//    void Update()
//    {
//        MoveProjectile();   
//    }

//    private void OnTriggerEnter2D(Collider2D collision)
//    {
//        //Vector3 directionOfCollision = transform.position - collision.gameObject.transform.position;

//        //gameObject.transform.eulerAngles = new Vector3(gameObject.transform.eulerAngles.x, gameObject.transform.eulerAngles.y, -gameObject.transform.eulerAngles.z);
//        //Debug.Log("New rotation = " + gameObject.transform.eulerAngles);

//        //if (directionOfCollision.x > 0)
//        //{
//        //    gameObject.transform.eulerAngles = new Vector3(gameObject.transform.rotation.x, gameObject.transform.rotation.y, -gameObject.transform.rotation.z * 2f);
//        //}
//    }

//    private void OnCollisionEnter2D(Collision2D collision)
//    {
//        //Vector3 directionOfCollision = transform.position - collision.gameObject.transform.position;

//        gameObject.transform.eulerAngles = new Vector3(gameObject.transform.eulerAngles.x, gameObject.transform.eulerAngles.y, -gameObject.transform.eulerAngles.z);
//    }

//    void MoveProjectile()
//    {
//        // Translate movement
//        transform.Translate(Vector3.up * shotSpeed * Time.deltaTime, Space.Self);

//        // Force movement

//    }
//}
