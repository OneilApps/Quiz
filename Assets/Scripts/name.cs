using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class name : MonoBehaviour {

	void FixedUpdate () {
		GetComponent<Text> ().text = Camera.main.GetComponent<sendControl> ().teamName;
	}

}
