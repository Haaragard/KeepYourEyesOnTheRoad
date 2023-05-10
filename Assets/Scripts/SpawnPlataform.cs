using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPlataform : MonoBehaviour
{

    public List<GameObject> plataforms = new List<GameObject>();
    public List<Transform> currentPlataforms = new List<Transform>();

    public int offset;

    private Transform player;
    private Transform currentPlataformPoint;
    private int plataformIndex;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;

        for(int i =0; i < plataforms.Count; i++)
        {
            Transform p = Instantiate(plataforms[i], new Vector3(0,0,i * 86), transform.rotation).transform;
            currentPlataforms.Add(p);
            offset += 86; 
        }

        currentPlataformPoint = currentPlataforms[plataformIndex].GetComponent<Plataform>().point;
    }

    // Update is called once per frame
    void Update()
    {
        float distance = player.position.z - currentPlataformPoint.position.z;

        if(distance >= 5)
        {
            Recycle(currentPlataforms[plataformIndex].gameObject);
            plataformIndex++;

            if(plataformIndex > currentPlataforms.Count - 1)
            {
                plataformIndex = 0;
            }

            currentPlataformPoint = currentPlataforms[plataformIndex].GetComponent<Plataform>().point;
        }
    }

    public void Recycle(GameObject plataform)
    {
        plataform.transform.position = new Vector3(0,0,offset);
        offset += 86;
    }
}
