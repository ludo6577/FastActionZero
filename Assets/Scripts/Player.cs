using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Player : MonoBehaviour {

    // 1) declaration mouse sensitivity
    public float sensitivityX = 5f;
    public float sensitivityY = 5f;

    public GameObject lastCheckPoint;
    public Image FadeMask;

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

        //2) Cast ray from camera
	    Ray ray = Camera.main.ViewportPointToRay (new Vector3(0.5f, 0.5f, 0f));        
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            var gameObject = hit.collider.gameObject;
            var teleport = gameObject.GetComponent<Teleport>();
            if (teleport != null)
            {
                Teleport(gameObject.transform.position);

                if (lastCheckPoint != null)
                    lastCheckPoint.SetActive(false);
                lastCheckPoint = gameObject;
            }
        }
	}

    //3) Teleport
    private void Teleport(Vector3 position)
    {
        //transform.position = position;
        StartCoroutine(TeleportCoroutine(position));
    }

    //4) Add Fadeplane With fadeplane
    private IEnumerator TeleportCoroutine(Vector3 position)
    {
        while (FadeMask.color.a <= 1)
        {
            FadeMask.color = new Color(FadeMask.color.r, FadeMask.color.g, FadeMask.color.b, FadeMask.color.a + 0.1f);
            yield return new WaitForEndOfFrame();
        }
        Debug.Log(FadeMask.color    );
        transform.position = position;

        while (FadeMask.color.a >= 0)
        {
            FadeMask.color = new Color(FadeMask.color.r, FadeMask.color.g, FadeMask.color.b, FadeMask.color.a - 0.1f);
            yield return new WaitForEndOfFrame();
        }


    }
}
