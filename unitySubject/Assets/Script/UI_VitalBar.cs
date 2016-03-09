using UnityEngine;
using System.Collections;

public class UI_VitalBar : MonoBehaviour {
	
	public UILabel label;
	private UISlider _slider;
	private bool _displayText = false;
	
	void Awake() {
		//獲得Slider，並防呆
		_slider = GetComponent<UISlider>();	
		if(_slider == null) {
			Debug.LogError("找不到UISlider的Component!");
			return;
		}
		DisplayText = _displayText;
	}
	
	public void UpdateDisplay(float x) {
		//血量防呆
		if(x < 0.0f) { x = 0.0f; }
		else if(x > 1.0f) { x = 1.0f; }
		_slider.value = x;
		DisplayText = false;
	}
	
	public void UpdateDisplay(float x, string str) {
		UpdateDisplay(x);
		if(str != "") {
			DisplayText = true;
			label.text = str;
		}
	}
	
	public bool DisplayText {
		get { return _displayText; }
		set {
			_displayText = value;
			if(!_displayText) {
				label.text = "";
			}
		}
	}
}
