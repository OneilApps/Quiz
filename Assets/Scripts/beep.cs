using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class beep : MonoBehaviour {

	void Start () {
		InvokeRepeating ("Beep", 3f, 3f);
	}

	void Beep() {
		if (GetComponent<Image> ().color == new Color (1, 1, 1, 1)) {
			GetComponent<Image> ().color = new Color (1, 1, 1, 0.16f);
		} else {
			GetComponent<Image> ().color = new Color (1, 1, 1, 1);
		}
	}

}
