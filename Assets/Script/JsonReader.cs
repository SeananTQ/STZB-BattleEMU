using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JsonReader : MonoBehaviour {

    public string jsonPath;
    public string jsonRes;

    public List<HeroData> heroDataList;

	void Start () {



        JsonUtility.FromJson<HeroData>(jsonRes);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
