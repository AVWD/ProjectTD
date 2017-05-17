using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Canvas))]
public class GuiManager : MonoBehaviour {
    public Vector2 ScreenPadding = new Vector2(5, 5);

    public static void LoadScene(int scene)
    {
        Application.LoadLevel(scene);
    }

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    // Factories
    public void BuildSideMenu(string name)
    {
        // 1/5 screen width
        float pct = Camera.main.pixelWidth - (ScreenPadding.x * 2) / 5;
        Canvas canvas = GetComponent<Canvas>();

        GameObject panel = new GameObject(name);
        panel.AddComponent<CanvasRenderer>();
        Image img = panel.AddComponent<Image>();
        img.color = new Color(1, 1, 1, 0);
        panel.transform.SetParent(canvas.transform, false);


    }
}
