using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonController : MonoBehaviour {

    public GameObject cannonBallPrefab;
    public int poolSize = 20;
    public float speed = 5.0f;
    public float delay = 0.3f;
    //float lastTime;
    public bool expandeblePoolSize = true;


    public List<Rigidbody2D> cannonBallPool;

	// Use this for initialization
	void Start () {

//        lastTime = Time.time;
        cannonBallPool = new List<Rigidbody2D>();
        for (int i = 0; i < poolSize; i++ ) {
            GameObject cannonBall = Instantiate(cannonBallPrefab);
            cannonBall.SetActive(false);
           
            cannonBallPool.Add(cannonBall.GetComponent<Rigidbody2D>());
        }


        StartCoroutine(ShootCannon());

	}

    // Same as corutine
    //private void Update()
    //{
        
    //    if (lastTime + delay < Time.time)
    //    {
    //        lastTime = Time.time;
    //        GameObject cannonBall = Instantiate(cannonBallPrefab);

    //        //GameObject cannonBall = GetCannonBall();
    //        if (cannonBall != null)
    //        {
    //            cannonBall.transform.position = gameObject.transform.position;
    //            cannonBall.SetActive(true);

    //            cannonBall.GetComponent<Rigidbody2D>().AddForce(Vector2.right * speed);
    //        }

    //    }


    //}

    IEnumerator ShootCannon() {
        while (true)
        {

            //GameObject cannonBall = Instantiate(cannonBallPrefab);

            Rigidbody2D cannonBall = GetCannonBall();
            if (cannonBall != null)
            {
                cannonBall.transform.position = gameObject.transform.position;
                cannonBall.gameObject.SetActive(true);

                cannonBall.AddForce(Vector2.right * speed);
            }
            yield return new WaitForSeconds(delay);
        }
    }




    Rigidbody2D GetCannonBall() {

        for (int i = 0; i < cannonBallPool.Count; i++) {

            if(cannonBallPool[i] == null) {
                GameObject cannonBall = Instantiate(cannonBallPrefab);
                cannonBallPool[i] = cannonBall.GetComponent<Rigidbody2D>();
                cannonBall.SetActive(false);
                return cannonBallPool[i];
            }
            if(!cannonBallPool[i].gameObject.activeInHierarchy) {
                return cannonBallPool[i];
            }
        }

        if (expandeblePoolSize) {
            GameObject cannonBall = Instantiate(cannonBallPrefab);
            Rigidbody2D body = cannonBall.GetComponent<Rigidbody2D>();
            cannonBallPool.Add(body);
            cannonBall.SetActive(false);
            return body;
        }

        return null;
    }


}
