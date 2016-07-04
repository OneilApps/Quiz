using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class imageReturn : MonoBehaviour, IPointerUpHandler {

	public virtual void OnPointerUp(PointerEventData ped) {
		if (Camera.main.GetComponent<sendControl> ().usingScreen == "letters") {
			Camera.main.GetComponent<sendControl>().currentCanvas.transform.localPosition = new Vector3 (-800, 0, 0);
			GameObject.Find ("lettersScreen").transform.localPosition = Vector3.zero;
			Camera.main.GetComponent<sendControl>().currentCanvas = GameObject.Find ("lettersScreen");
		}

		if (Camera.main.GetComponent<sendControl> ().usingScreen == "multi") {
			Camera.main.GetComponent<sendControl>().currentCanvas.transform.localPosition = new Vector3 (-800, 0, 0);
			GameObject.Find ("multiScreen").transform.localPosition = Vector3.zero;
			Camera.main.GetComponent<sendControl>().currentCanvas = GameObject.Find ("multi");
		}
	}

}
