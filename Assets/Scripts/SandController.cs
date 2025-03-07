using UnityEngine;
using TMPro;
using UnityEngine.VFX;
using System;

public class SandController : MonoBehaviour
{
    public TMP_InputField RadiusInput;
    public TMP_InputField SizeInput;
    public TMP_InputField VeloInput;

    public TextMeshProUGUI FPSText;

    public float Radius;
    public float Size;
    public float Velo;

    public VisualEffect SandVFX;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Radius = SandVFX.GetFloat("Radius");
        Size = SandVFX.GetFloat("Size");
        Velo = SandVFX.GetFloat("Velo");
        RadiusInput.text = Radius.ToString();
        SizeInput.text = Size.ToString();
        VeloInput.text = Velo.ToString();

        RadiusInput.onEndEdit.AddListener(OnRadiusChange);
        SizeInput.onEndEdit.AddListener(OnSizeChange);
        VeloInput.onEndEdit.AddListener(OnVeloChange);
    }

    private void OnVeloChange(string arg0)
    {
        Velo = float.Parse(arg0);
        SandVFX.SetFloat("Velo", Velo);
    }

    private void OnRadiusChange(string arg0)
    {
        Radius = float.Parse(arg0);
        SandVFX.SetFloat("Radius", Radius);
    }

    private void OnSizeChange(string arg0)
    {
        Size = float.Parse(arg0);
        SandVFX.SetFloat("Size", Size);
    }

    void Update()
    {
        FPSText.text = "FPS: " + (1 / Time.deltaTime).ToString("F2");
    }
}
