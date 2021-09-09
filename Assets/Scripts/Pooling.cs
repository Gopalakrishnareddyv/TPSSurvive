using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Pooling : MonoBehaviour
{
    public GameObject prefab;
    public int amount;
    public static Pooling instance;
    public List<GameObject> pooleditems;
    private void Awake()
    {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        pooleditems = new List<GameObject>();
        for (int i = 0; i < amount; i++)
        {
            GameObject prefabobj = Instantiate(prefab);
            prefabobj.SetActive(false);
            pooleditems.Add(prefabobj);
        }
        
    }
    public GameObject Get(string tagname)
    {
        for (int i = 0; i <Pooling.instance.amount; i++)
        {
            if (!pooleditems[i].activeInHierarchy && pooleditems[i].tag == tagname)
            {
                Debug.Log(pooleditems[i]);
                return pooleditems[i];
            }
        }
        return null;
    }
    
}