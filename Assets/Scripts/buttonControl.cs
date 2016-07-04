using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class buttonControl : MonoBehaviour, IPointerUpHandler {

	public string id;
	GameObject[] letters;
	GameObject[] multi;

	void Start () {
		if (id == "letters") {
			letters = GameObject.FindGameObjectsWithTag ("letters");
		}
		if (id == "multi") {
			multi = GameObject.FindGameObjectsWithTag ("multi");
		}
	}

	public virtual void OnPointerUp(PointerEventData ped) {
			
		if (id == "initialConnect") {
			Camera.main.GetComponent<sendControl> ().teamName = GameObject.Find ("selectTeamName").GetComponent<InputField> ().text;
			Camera.main.GetComponent<sendControl> ().InitialConnect ();
		}

		if (id == "buzz") {
			Camera.main.GetComponent<sendControl> ().Buzz ();
			StartCoroutine (buzzAnimation());
		}

		if (id == "number") {
			GameObject.Find ("answerText").GetComponent<Text> ().text = GameObject.Find ("answerText").GetComponent<Text> ().text + transform.name;
		}

		if (id == "cancel") {
			GameObject.Find ("answerText").GetComponent<Text> ().text = "";
		}

		if (id == "enter") {
			Camera.main.GetComponent<sendControl> ().SendNormalAnswer (GameObject.Find ("answerText").GetComponent<Text> ().text);
		}

		if (id == "letters") {
			if (transform.name == "Q V" || transform.name == "X Z") {
				Camera.main.GetComponent<sendControl> ().SendNormalAnswer ("B \n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\nA\nB\nC\nD\nE\nF\nG\nH\nI\nJ\nK\nL\nM\nN\nO\nP\nQ\nR\nS\nT\nU\nV\nY\nX\nY\nZ");
			} else {
				Camera.main.GetComponent<sendControl> ().SendNormalAnswer (transform.name);
			}
			foreach (GameObject obj in letters) {
				Color letterBoxDark, textDark = new Color();
				ColorUtility.TryParseHtmlString ("#010001FF", out letterBoxDark);
				ColorUtility.TryParseHtmlString ("#565457FF", out textDark);
				obj.GetComponent<Image> ().color = letterBoxDark;
				obj.GetComponent<Image> ().raycastTarget = false;
				obj.transform.GetChild (0).GetComponent<Text> ().color = textDark;
			}
			Color letterBoxLight = new Color();
			ColorUtility.TryParseHtmlString ("#929ac4FF", out letterBoxLight);
			GetComponent<Image> ().color = letterBoxLight;
			transform.GetChild (0).GetComponent<Text> ().color = new Color (1, 1, 1, 1);
		}

		if (id == "multi") {
			if (transform.name == "F") {
				Camera.main.GetComponent<sendControl> ().SendNormalAnswer ("B \n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\nA\nB\nC\nD\nE\nF");
			} else {
				Camera.main.GetComponent<sendControl> ().SendNormalAnswer (transform.name);
			}
			foreach (GameObject obj in multi) {
				Color letterBoxDark, textDark = new Color();
				ColorUtility.TryParseHtmlString ("#010001FF", out letterBoxDark);
				ColorUtility.TryParseHtmlString ("#565457FF", out textDark);
				obj.GetComponent<Image> ().color = letterBoxDark;
				obj.GetComponent<Image> ().raycastTarget = false;
				obj.transform.GetChild (0).GetComponent<Text> ().color = textDark;
			}
			Color letterBoxLight = new Color();
			ColorUtility.TryParseHtmlString ("#929ac4FF", out letterBoxLight);
			GetComponent<Image> ().color = letterBoxLight;
			transform.GetChild (0).GetComponent<Text> ().color = new Color (1, 1, 1, 1);
		}

	}

	IEnumerator buzzAnimation() {
		GameObject inner = GameObject.Find ("innerBuzz");
		GameObject middle = GameObject.Find ("middleBuzz");
		GameObject outer = GameObject.Find ("outerBuzz");

		Color innerS = inner.GetComponent<Image> ().color;
		Color middleS = middle.GetComponent<Image> ().color;
		Color outerS = outer.GetComponent<Image> ().color;

		Color innerC, middleC, outerC = new Color();
		ColorUtility.TryParseHtmlString ("#898db8FF", out innerC);
		ColorUtility.TryParseHtmlString ("#a2a7d8FF", out middleC);
		ColorUtility.TryParseHtmlString ("#bac6eeFF", out outerC);

		inner.GetComponent<Image> ().color = innerC;
		yield return new WaitForSeconds (0.1f);
		middle.GetComponent<Image> ().color = middleC;
		yield return new WaitForSeconds (0.1f);
		outer.GetComponent<Image> ().color = outerC;
		yield return new WaitForSeconds (0.1f);
		outer.GetComponent<Image> ().color = outerS;
		yield return new WaitForSeconds (0.1f);
		middle.GetComponent<Image> ().color = middleS;
		yield return new WaitForSeconds (0.1f);
		inner.GetComponent<Image> ().color = innerS;
	}

}
