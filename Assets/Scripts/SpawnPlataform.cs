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

        for(int i = 0; i < plataforms.Count; i++)
        {
            Transform p = Instantiate(plataforms[i], new Vector3(0f, 0f, i * 86), transform.rotation).transform;
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
            this.RemoveInstantiatePlatform();
            plataformIndex++;

            if(plataformIndex > currentPlataforms.Count - 1)
            {
                plataformIndex = 0;
            }

            currentPlataformPoint = currentPlataforms[plataformIndex].GetComponent<Plataform>().point;
        }
    }

    private void RemoveInstantiatePlatform()
    {
        Transform lastPlatform = currentPlataforms[plataformIndex];

        Transform p = Instantiate(plataforms[plataformIndex], new Vector3(0f, 0f, offset), transform.rotation).transform;
        currentPlataforms[plataformIndex] = p;

        offset += 86;

        Destroy(lastPlatform.gameObject);
    }
}
