using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HealthText : NhoxBehaviour
{
    public Vector3 moveSpeed = new(0, 75, 0);
    public float timeToFade = 1f;

    RectTransform textTransform;
    TextMeshProUGUI textMeshPro;

    protected float timeElapsed = 0f;
    protected Color startColor;

    protected override void Awake()
    {
        base.Awake();
        startColor = textMeshPro.color;
    }
    private void Update()
    {
        TextMove();
        TextFade();
    }

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadRectTransform();
        this.LoadTextMeshPro();
    }

    protected virtual void LoadRectTransform()
    {
        if (textTransform != null) return;
        textTransform = GetComponent<RectTransform>();
        //Debug.Log(transform.name + " LoadRectTransform", gameObject);
    }

    protected virtual void LoadTextMeshPro()
    {
        if (textMeshPro != null) return;
        textMeshPro = GetComponent<TextMeshProUGUI>();
        //Debug.Log(transform.name + " LoadTextMeshPro", gameObject);
    }

    protected void TextMove()
    {
        textTransform.position += moveSpeed * Time.deltaTime;

        timeElapsed += Time.deltaTime;
    }

    protected void TextFade()
    {
        if (timeElapsed < timeToFade)
        {
            float fadeAlpha = startColor.a * (1 - (timeElapsed / timeToFade));
            textMeshPro.color = new Color(startColor.r, startColor.g, startColor.b, fadeAlpha);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
