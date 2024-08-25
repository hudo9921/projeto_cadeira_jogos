using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class TypingEffect : MonoBehaviour
{
    public float typingSpeed = 0.05f; // Velocidade da digitação

    // Três opções de texto
    private string textoStart = "O mundo em que o sobrevivente desperta é um lugar onde a civilização não passa de uma lembrança distante. Em 2045, um vírus catastrófico varreu o planeta, deixando para trás uma cidade em ruínas, dominada por horrores indescritíveis. As ruas, antes cheias de vida, agora são habitadas por criaturas que não conhecem piedade.\n\nZumbis, os primeiros a cair sob o poder do vírus, vagam incansavelmente pelos becos e avenidas, seus olhos vazios fixos em qualquer movimento ou som. Mas eles são apenas o prelúdio do verdadeiro terror. O vírus também deu origem a monstros deformados, aberrações nascidas da carne humana, que agora espreitam nas sombras, prontas para atacar qualquer coisa viva.\n\nNo entanto, o maior desafio ainda está por vir. O sobrevivente descobre que o vírus foi criado por um cientista maluco, cuja obsessão levou a humanidade ao colapso. Este cientista esconde-se em um laboratório fortemente guardado, onde trabalha em segredo na única esperança que resta: o antídoto. Para salvar o que restou do mundo, o sobrevivente precisa derrotar esse cientista e obter o antídoto. A luta pela sobrevivência não é apenas contra as hordas de monstros, mas também contra o próprio criador do caos.";
    private string textoWin = "Após uma luta árdua e implacável, o sobrevivente finalmente derrota o cientista maluco e consegue obter o antídoto que pode mudar o destino da humanidade. Com o antídoto em mãos, ele inicia a tarefa monumental de restaurar o que foi perdido.\n\nO mundo devastado começa a vislumbrar uma nova esperança. As ruas, antes preenchidas com o som do caos e da destruição, agora testemunham os primeiros sinais de recuperação. O antídoto é distribuído cuidadosamente, e aqueles que foram infectados começam a se recuperar, suas formas grotescas e deformadas se revertendo gradualmente ao estado humano. A cidade, uma vez um cenário de terror, começa a ver pequenos sinais de renascimento.\n\nÀ medida que o antídoto faz efeito, os sobreviventes se reúnem para reconstruir e reerguer a civilização. O trabalho é árduo, mas a determinação é forte. O que antes parecia um futuro sombrio agora dá lugar a um novo começo. A luta pela sobrevivência foi substituída por uma batalha pela reconstrução, onde cada passo é um avanço em direção a um futuro mais promissor.\n\nO sobrevivente, agora um símbolo de esperança e resiliência, lidera o caminho para a restauração da raça humana. Embora o caminho seja longo e difícil, a humanidade tem, finalmente, uma chance de se reerguer e reconstruir a partir das cinzas do que uma vez foi perdido.";
    private string textoLose = "O sobrevivente cai, sua vida se esvaindo sem que o antídoto tenha sido alcançado. Com sua morte, a última esperança de salvar a humanidade se perde. O mundo, agora sem uma solução para a pandemia, mergulha em um caos ainda mais profundo. As ruas, outrora já dominadas por horrores, se tornam um campo de batalha interminável, onde os monstros e zumbis proliferam sem controle.\n\nO vírus continua a se espalhar, infectando mais pessoas e gerando novas criaturas horrendas. A cidade em ruínas, que já era um cenário de desespero, se transforma em um pesadelo absoluto, onde a luta pela sobrevivência se torna cada vez mais desesperadora. O futuro da humanidade, uma vez tão promissor com a possibilidade de um antídoto, agora se dissolve em escuridão e destruição. Sem a intervenção necessária, a esperança de redenção desaparece, e o caos reina supremo.";

    public string nextSceneName; // Nome da cena para a qual transitar

    public string currentScene;
    public float scrollSpeed = 20f; // Velocidade de rolagem para textos longos

    private TextMeshProUGUI textMeshPro;
    private RectTransform rectTransform;
    private string currentText = "";
    private string fullText = "";
    private float initialPosY;

    void Start()
    {
        textMeshPro = GetComponent<TextMeshProUGUI>();
        rectTransform = GetComponent<RectTransform>();
        initialPosY = rectTransform.anchoredPosition.y;

        // Seleciona o texto com base no nome da cena
        SelectTextBasedOnSceneName();

        StartCoroutine(TypeText());
    }

    void Update()
    {
        // Verifica se o usuário clicou na tela e se a digitação está completa
        if (Input.GetMouseButtonDown(0))
        {
            LoadNextScene();
        }

        // Se o texto ultrapassar a área visível, mova-o para cima
        if (textMeshPro.textBounds.size.y > rectTransform.rect.height)
        {
            Vector3 newPos = rectTransform.anchoredPosition;
            newPos.y += scrollSpeed * Time.deltaTime;
            rectTransform.anchoredPosition = newPos;
        }
    }

    void SelectTextBasedOnSceneName()
    {
        // Seleciona o texto com base no nome da cena
        switch (currentScene)
        {
            case "CutSceneStart":
                fullText = textoStart;
                break;
            case "CutSceneWin":
                fullText = textoWin;
                break;
            case "CutSceneLose":
                fullText = textoLose;
                break;
        }
    }

    IEnumerator TypeText()
    {
        foreach (char letter in fullText.ToCharArray())
        {
            currentText += letter;
            textMeshPro.text = currentText;
            yield return new WaitForSeconds(typingSpeed);
        }
    }

    void LoadNextScene()
    {
        // Carrega a próxima cena
        SceneManager.LoadScene(nextSceneName);
    }
}
