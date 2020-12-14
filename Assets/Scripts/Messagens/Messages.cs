using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// Text showing mechanism, shows and hides messages
/// </summary>
[RequireComponent(typeof(Text))]
public class Messages : MonoBehaviour
{
    public float tempo = 4;
    Text txt_mensagem;
    private bool visivel = false;
    private float tempoContar = 0;
    private Color defaultColor;
    public static Messages instance;
    public RawImage imagem;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;

        Init();
    }

    public void Init()
    {
        txt_mensagem = GameObject.FindObjectOfType<Messages>().GetComponent<Text>();
        defaultColor = txt_mensagem.color;
    }

    /// <summary>
    /// Função para mostrar uma mensagem num objeto UI.Text
    /// </summary>
    public void showMessage(string texto)
    {

        if (!txt_Exists())
        {
            return;
        }
        //Debug.Log(texto);
        txt_mensagem.text = texto;
        visivel = true;
        tempoContar = tempo;
    }
    public void showMessage(string texto,Texture2D imagem)
    {

        if (!txt_Exists())
        {
            return;
        }
        //Debug.Log(texto);
        txt_mensagem.text = texto;
        visivel = true;
        tempoContar = tempo;
        this.imagem.enabled = true;
        this.imagem.texture = imagem;
    }
    public void showMessage(string texto, Color color)
    {

        if (!txt_Exists())
        {
            return;
        }
        txt_mensagem.text = texto;
        txt_mensagem.color = color;
        visivel = true;
        tempoContar = tempo;
    }
    public void showMessage(string texto, Color color, int _tempo)
    {

        if (!txt_Exists())
        {
            return;
        }
        txt_mensagem.text = texto;
        txt_mensagem.color = color;
        visivel = true;
        tempoContar = _tempo;
    }
    public void showMessage(string texto, int _tempo)
    {

        if (!txt_Exists())
        {
            return;
        }
        txt_mensagem.text = texto;
        visivel = true;
        tempoContar = _tempo;
    }
    public void clearMessage()
    {
        tempoContar = 0;
        visivel = true;
    }

    bool txt_Exists()
    {
        if (txt_mensagem == null)
        {
            Debug.Log("Please add a Text UI object with a UI_TXT tag");
            return false;
        }
        return true;
    }

    private void Update()
    {
        if (visivel == true)
        {
            tempoContar -= Time.deltaTime;
            if (tempoContar <= 0)
            {
                visivel = false;
                txt_mensagem.text = "";
                tempoContar = 0;
                txt_mensagem.color = defaultColor;
                if(this.imagem!=null) this.imagem.enabled = false;
               // this.imagem = null;
            }
        }
    }
}
