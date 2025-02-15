using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class UIManager : NhoxBehaviour
{
    public GameObject damageText;
    public GameObject healText;

    public Canvas gameCanvas;

    protected virtual void OnEnable()
    {
        CharacterEvents.characterDamaged += CharacterTookDamage;
        CharacterEvents.characterHealed += CharacterHealed;
    }

    protected virtual void OnDisable()
    {
        CharacterEvents.characterDamaged -= CharacterTookDamage;
        CharacterEvents.characterHealed -= CharacterHealed;
    }

    public void CharacterTookDamage(GameObject character, int damage)
    {
        Vector3 spawnPosition = Camera.main.WorldToScreenPoint(character.transform.position);

        TMP_Text tmpText = Instantiate(damageText, spawnPosition, Quaternion.identity, gameCanvas.transform).GetComponent<TMP_Text>();

        tmpText.text = damage.ToString();
    }

    public void CharacterHealed(GameObject character, int heal)
    {
        Vector3 spawnPosition = Camera.main.WorldToScreenPoint(character.transform.position);

        TMP_Text tmpText = Instantiate(healText, spawnPosition, Quaternion.identity, gameCanvas.transform).GetComponent<TMP_Text>();

        tmpText.text = heal.ToString();
    }

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadCanvas();
        this.LoadDamageText();
        this.LoadHealText();
    }

    protected virtual void LoadCanvas()
    {
        if (gameCanvas != null) return;
        gameCanvas = FindObjectOfType<Canvas>();
        Debug.Log(transform.name + " LoadCanvas", gameObject);
    }

    protected virtual void LoadDamageText()
    {
        if (damageText != null) return;
        damageText = Resources.Load<GameObject>("DamageText");
        Debug.Log(transform.name + " LoadDamageText", gameObject);
    }

    protected virtual void LoadHealText()
    {
        if (healText != null) return;
        healText = Resources.Load<GameObject>("HealText");
        Debug.Log(transform.name + " LoadHealText", gameObject);
    }

    public void OnExitGame(InputAction.CallbackContext context)
    {
        if(context.started)
        {
            #if (UNITY_EDITOR || DEVELOPMENT_BUILD)
                        Debug.Log(this.name + " : " + this.GetType() + " : " + System.Reflection.MethodBase.GetCurrentMethod().Name);
            #endif
            #if (UNITY_EDITOR)
                        UnityEditor.EditorApplication.isPlaying = false;
            #elif (UNITY_STANDALONE)
                        Application.Quit();
            #elif (UNITY_WEBGL)
                        SceneManager.LoadScene("QuitScene");
            #endif


        }
    }
}
