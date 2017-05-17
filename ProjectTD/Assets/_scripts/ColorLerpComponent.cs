using System;
using UnityEngine;
using UnityEngine.UI;

public class ColorLerpComponent : MonoBehaviour
{
    public AnimationCurve PulseCurve;
    public Color StartColor;
    public Color EndColor;
    private Graphic _graphic;

    // Use this for initialization
    void Start()
    {
        _graphic = base.GetComponent<Graphic>();
    }

    // Update is called once per frame
    void Update()
    {
        _graphic.color = Color.Lerp(this.StartColor, this.EndColor, this.PulseCurve.Evaluate(Time.time));
    }
}
