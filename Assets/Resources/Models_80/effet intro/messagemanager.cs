using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Text.RegularExpressions;

public class messagemanager : MonoBehaviour
{
    //3.　a attacher a MessageUI
    private Text messageText;
    //3. message qui sera affiche
    [SerializeField]
    [TextArea(1, 20)]
    private string allMessage = null;
    /*"Bonjour à toi, moi du passé ! J’ai toujours rêvé de dire ça. . .bref !\n"
            + " Ne t’inquiète pas, je ne suis pas venu te tuer, mais pour demander ton aide.\n<>"
            + "Vois-tu, notre espèce n’a plus la chance de vivre aux côtés d’arbres, ou ne serait-ce que d’un brin d’herbe.\n<>"
            + "Je m’attendais à ce que tu me dévisages, mais sache que mon corps a été victime de radiations, et non pas d’une modélisation lowpoly hasardeuse.\n<>"
            + " Malgré de nombreux signaux d’alarme, vous n’avez pas été capables de nous sauver du réchauffement climatique et la pollution nous intoxique et nous asphyxie, à tel point que notre espèce est vouée à s’éteindre d’ici quelques années.<>"
            + "Mais tu peux changer notre funeste destin en ramenant les arbres dans notre époque.<>"
            + "Cette aventure te mènera à travers différents âges et époques, entre lesquels tu pourras voyager grâce à Boris, ton fidèle walkman et différentes cassettes audios.<>"
            + "Utilise donc celle-ci qui t’amènera à notre époque pour constater les dégâts."
            + "Nous comptons sur moi, euh sur toi, Détective!";
    */


       
    //3. 　string pour separer les textes
    [SerializeField]
    private string splitString = "<>";
    //3.　 tableau des messages separes
    private string[] splitMessage;
    //　indice pour les messages separes 
    private int messageNum;
    //3.　vitesse message
    [SerializeField]
    private float textSpeed = 0.05f;
    //3.　temps ecoule
    private float elapsedTime = 0f;
    //3.　nb de text actuel 
    private int nowTextNum = 0;
    //3.　icon pour clicker
    private Image clickIcon;
    //3.　temps pour la clignotant
    [SerializeField]
    private float clickFlashTime = 0.2f;
    //3.　si le message d'une fois(un message) ait été affiché 
    private bool isOneMessage = false;
    //3.　si tous les messages ont ete affiche
    private bool isEndMessage = false;
    public bool IsEndMessage
    {
        get { return this.isEndMessage; }
    }

    void Start()
    {
        clickIcon = transform.Find("Panel/Image").GetComponent<Image>();
        clickIcon.enabled = false;
        messageText = GetComponentInChildren<Text>();
        messageText.text = "";
        //SetMessage(allMessage);
        isEndMessage = true;
        transform.GetChild(0).gameObject.SetActive(false);
    }

    void Update()
    {
        //　si le message est fini. Si'l n'y a plus de message, on n'affiche desormais plus rien
        if (isEndMessage || allMessage == null)
        {
            return;
        }

        // si le message d'une fois est affiche　
        if (!isOneMessage)
        {
            //　ajouter le message apres le temps ecoule 
            if (elapsedTime >= textSpeed)
            {
                messageText.text += splitMessage[messageNum][nowTextNum];

                nowTextNum++;
                elapsedTime = 0f;

                // si le message a ete tout affiche ou non
                if (nowTextNum >= splitMessage[messageNum].Length)
                {
                    isOneMessage = true;
                }
            }
            elapsedTime += Time.deltaTime;

            //　a modifier 
            //3. si on appuye sur le button de souris gauche, affiche tout d'un coup
            if (Input.GetMouseButtonDown(0)) // doit marcher avec les manettes
            {
                //　ajout du message restant
                messageText.text += splitMessage[messageNum].Substring(nowTextNum);
                isOneMessage = true;
            }
            //3.　ici on a affiche un message 
        }
        else
        {

            elapsedTime += Time.deltaTime;

            //　clignoter l'icon 
            if (elapsedTime >= clickFlashTime)
            {
                clickIcon.enabled = !clickIcon.enabled;
                elapsedTime = 0f;
            }

            //　si on clique le button de souris, on traite le message suivant
            if (Input.GetMouseButtonDown(0))
            {
                nowTextNum = 0;
                messageNum++;
                messageText.text = "";
                clickIcon.enabled = false;
                elapsedTime = 0f;
                isOneMessage = false;

                //　si tout le mesaage a ete affiche, on suprime gameobject(canvas)
                if (messageNum >= splitMessage.Length)
                {
                    
                    isEndMessage = true;
                    transform.GetChild(0).gameObject.SetActive(false);

                }
            }
        }
    }


    //3.　set un nouveau message
    void SetMessage(string message)
    {
        this.allMessage = message;
        //　separer une message en plusieurs 
        splitMessage = Regex.Split(allMessage, @"\s*" + splitString + @"\s*", RegexOptions.IgnorePatternWhitespace);
        nowTextNum = 0;
        messageNum = 0;
        messageText.text = "";
        isOneMessage = false;
        isEndMessage = false;
    }
    //3.　set un nouceau message a partir d'autre script et l'activate
    public void SetMessagePanel(string message)
    {
        SetMessage(message);
        transform.GetChild(0).gameObject.SetActive(true);
    }
}
