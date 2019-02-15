using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Xml;

public class GameManager : MonoBehaviour {
    private Transform startPoint;
    private Transform spawnPoint;
    private Pin currentPin;
    private bool isGameOver = false;
    private int score = 0;
    private Camera mainCamera;

    public Text scoreText;
    public GameObject pinPrefab;
    public int speed = 3;
    // Use this for initialization
    void Start() {
        startPoint = GameObject.Find("StartPoint").transform;
        spawnPoint = GameObject.Find("SpawnPoint").transform;
        mainCamera = Camera.main;
        SpawnPin();
    }
    private void Update()
    {
        if (isGameOver) return;
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene(0);
        }
        if (Input.GetMouseButtonDown(0))
        {
            score++;
            scoreText.text = score.ToString();

            currentPin.Startfly();
            SpawnPin();
        }
    }
    void SpawnPin()
    {
        currentPin = GameObject.Instantiate(pinPrefab, spawnPoint.position, pinPrefab.transform.rotation).GetComponent<Pin>();
    }

    public void GameOver()
    {
        if (isGameOver) return;
        GameObject.Find("Circle").GetComponent<RotateSelf>().enabled = false;
        StartCoroutine(GameOverAnimation());
        isGameOver = true; 
    }

    IEnumerator GameOverAnimation()
    {
        while (true)
        {
            mainCamera.backgroundColor = Color.Lerp(mainCamera.backgroundColor, Color.red, speed * Time.deltaTime);
            mainCamera.orthographicSize = Mathf.Lerp(mainCamera.orthographicSize, 4, speed * Time.deltaTime);
            if(Mathf.Abs(mainCamera.orthographicSize - 4) < 0.01f)
            {
                break;
            }
            yield return 0;
        }
        yield return new WaitForSeconds(0.2f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void CreateSaveGO()
    {

    }
    private void Save()
    {
        //Save save = CreateSaveGO();
        //xml文件保存路径
        string filepath = Application.dataPath + "/Streamingfile" + "/data.txt";
        //创建xml文档
        XmlDocument xmlDoc = new XmlDocument();
        //创建根节点
        XmlElement root = xmlDoc.CreateElement("save");
        //设置根节点的值
        root.SetAttribute("name", "Rank");

        XmlElement score;
        XmlElement player;
    }

    private void Load()
    {

    }
}

