using UnityEngine;
using UnityEngine.UI;

public class CoinsPannel : MonoBehaviour {

	// Use this for initialization
	void Start () {
        GetComponent<Text>().text = PlayerPrefs.GetInt("coins", 0).ToString("D4");
	}
	
}
