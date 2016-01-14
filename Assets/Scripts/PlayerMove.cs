using UnityEngine;
using UnityEngine.Networking;

public class PlayerMove : NetworkBehaviour
{
    public GameObject bulletPrefab;
    private GameObject playerCam;
    Vector3 camOffset;

    public override void OnStartLocalPlayer()
    {
        GetComponent<MeshRenderer>().material.color = Color.red;
        playerCam = Camera.main.gameObject;

        camOffset = new Vector3(0f, 1.05f, -3.5f);
    }

    [Command]
    void CmdFire()
    {
        // This [Command] code is run on the server!

        // create the bullet object locally
        var bullet = (GameObject)Instantiate(
             bulletPrefab,
             transform.position + transform.forward,
             Quaternion.identity);

        bullet.GetComponent<Rigidbody>().velocity = transform.forward * 12;

        // spawn the bullet on the clients
        NetworkServer.Spawn(bullet);

        // when the bullet is destroyed on the server it will automaticaly be destroyed on clients
        Destroy(bullet, 2.0f);
    }

    void Update()
    {
        if (!isLocalPlayer)
            return;

        var x = Input.GetAxis("Horizontal") * 0.1f;
        var z = Input.GetAxis("Vertical") * 0.1f;
        
        if(Input.GetKey(KeyCode.Q))
        {
            transform.Rotate(Vector3.up * -Time.deltaTime * 15, Space.Self);
        }
        if (Input.GetKey(KeyCode.E))
        {
            transform.Rotate(Vector3.up * Time.deltaTime * 15, Space.Self);
        }

        transform.Translate(x, 0, z);

        playerCam.transform.position = transform.position + camOffset;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            // Command function is called from the client, but invoked on the server
            CmdFire();
        }
    }
}
