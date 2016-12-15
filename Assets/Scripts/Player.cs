using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Player : MonoBehaviour {

    // 1) declaration mouse sensitivity
    public float sensitivityX = 5f;
    public float sensitivityY = 5f;

    // 2) Checkpoint
    private GameObject lastCheckPoint;

    // 4) Too fast? Add fading
    public Image FadeMask;

    // 5) Still too fast? Add cursor 
    public Image Cursor;

    // 6) Shoot
    public GameObject Projectile;


	// Use this for initialization
	void Start () {	}
	
	// Update is called once per frame
	void Update () {

        // 1) Head rotation with mouse
        if (Input.GetKey(KeyCode.LeftControl))
        {
            var curr = transform.eulerAngles;
            var mouseX = Input.GetAxis("Mouse X") * sensitivityX;
            var mouseY = Input.GetAxis("Mouse Y") * sensitivityY;
            transform.eulerAngles = new Vector3(curr.x - mouseY, curr.y + mouseX, curr.z);
        }

        // 2) Cast ray from camera
	    Ray ray = Camera.main.ViewportPointToRay (new Vector3(0.5f, 0.5f, 0f));        
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {

            //3) Teleport
            var teleport = hit.collider.gameObject.GetComponent<Teleport>();
            if (teleport != null)
            {
                // 5) Add cursor
                if (Cursor.fillAmount > 0f)
                {
                    Cursor.fillAmount -= 0.1f;
                }
                else
                {
                    Cursor.fillAmount = 1f;

                    Teleport(teleport.transform.position);
                    
                    if (lastCheckPoint != null)
                        lastCheckPoint.SetActive(true);
                    teleport.gameObject.SetActive(false);
                    lastCheckPoint = teleport.gameObject;
                }
            }
            else
            {
                // 4) Add cursor
                if (Cursor.fillAmount < 1f)
                    Cursor.fillAmount += 0.1f;
            }
        }

        // 6) Shoot
	    if (Input.GetMouseButtonDown(0))
	    {
	        var projectile = Instantiate(Projectile, transform.position, transform.rotation);
            Destroy(projectile, 2f);

            Debug.Log(transform.forward);

            var rigidBody = projectile.GetComponent<Rigidbody>();
	        rigidBody.AddForce(transform.forward * 50f, ForceMode.Impulse);
	    }
	}

    private void Teleport(Vector3 position)
    {
        //transform.position = position;

        StartCoroutine(TeleportCoroutine(position));
    }

    //4) Add Fadeplane
    private IEnumerator TeleportCoroutine(Vector3 position)
    {
        while (FadeMask.color.a < 1)
        {
            FadeMask.color = new Color(FadeMask.color.r, FadeMask.color.g, FadeMask.color.b, FadeMask.color.a + 0.1f);
            yield return new WaitForEndOfFrame();
        }

        transform.position = position;

        while (FadeMask.color.a > 0)
        {
            FadeMask.color = new Color(FadeMask.color.r, FadeMask.color.g, FadeMask.color.b, FadeMask.color.a - 0.1f);
            yield return new WaitForEndOfFrame();
        }


    }
}
