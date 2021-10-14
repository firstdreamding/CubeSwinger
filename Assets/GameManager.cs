using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager GameInstance;
    public GameObject player;
    public Vector3 playerSpawnPoint;
    public GameObject line;
    public ShopManager shop;
    public ScoreUI score;
    public GameObject LevelPrefab;
    public GameObject WallBreak;

    public GameObject defaultPlayerModel;
    public GameObject defaultParticle;

    private GameObject connectedPoint;
    private GameObject[] levelPieces;
    public bool gameOver;
    public GameObject currentModel;
    private GameObject currentParticle;

    private Transform guiCanvas;

    void Awake()
    {
        if (GameInstance != null)
            GameObject.Destroy(GameInstance);
        else
            GameInstance = this;
        //DontDestroyOnLoad(this);
    }

    void Start()
    {
        levelPieces = new GameObject[3];

        levelPieces[1] = Instantiate(LevelPrefab);
        levelPieces[1].transform.position = new Vector2(0, 0);
        levelPieces[1].GetComponent<LevelPiece>().HandleFirst();
        levelPieces[1].GetComponent<LevelPiece>().SetUpLevelPiece();

        levelPieces[2] = Instantiate(LevelPrefab);
        levelPieces[2].transform.position =
            new Vector2(levelPieces[1].GetComponent<LevelPiece>().currentXOffset + levelPieces[1].transform.position.x, 0);
        levelPieces[2].GetComponent<LevelPiece>().SetUpLevelPiece();
        NewPlayer(PlayerPrefs.GetInt("PlayerModel"));
        guiCanvas = GameObject.Find("Canvas").transform.Find("GUIInGame");

        Vector3 temp = guiCanvas.Find("CoinIndicator").position;
        temp.y = Camera.main.WorldToScreenPoint(new Vector3(0,levelPieces[1].GetComponent<LevelPiece>().coinLocation)).y;
        guiCanvas.Find("CoinIndicator").position = temp;

        UpdateCoin();
    }

    public void Connect(GameObject point)
    {    
        if (!gameOver)
        {
            if (connectedPoint != point)
            {
                BreakConnection();
                line.SetActive(true);
                line.GetComponent<LineConnection>().LineEndPoint(point.transform);
                connectedPoint = point;
                connectedPoint.GetComponent<Joint>().connectedBody = player.GetComponent<Rigidbody>();
                //player.GetComponent<Rigidbody>().velocity *= 1.3f;
                Vector3.ClampMagnitude(player.GetComponent<Rigidbody>().velocity, 25);
            }
            else
            {
                BreakConnection();
            }
        }
    }

    public void BreakConnection()
    {
        if (!gameOver)
        {
            if (connectedPoint)
            {
                connectedPoint.GetComponent<Joint>().connectedBody = null;
                Destroy(connectedPoint);
            }
            line.SetActive(false);
            connectedPoint = null;
        }
    }

    public void StartGame()
    {
        gameOver = false;
        player.GetComponent<Rigidbody>().useGravity = true;
        player.GetComponent<Rigidbody>().AddForce(new Vector3(0, 600));
    }

    public void OnWallCompleted()
    {
        Debug.Log("adding addding");
        score.otherModifiers += 100;
        Destroy(levelPieces[0]);
        levelPieces[0] = levelPieces[1];
        levelPieces[1] = levelPieces[2];
        levelPieces[2] = Instantiate(LevelPrefab);
       
        levelPieces[2].transform.position = 
            new Vector2(levelPieces[1].GetComponent<LevelPiece>().currentXOffset + levelPieces[1].transform.position.x, 0);
        levelPieces[2].GetComponent<LevelPiece>().SetUpLevelPiece();

        //Adding particles
        Instantiate(WallBreak).transform.position = player.transform.position;

        //Adding coin indicator
        Vector3 temp = guiCanvas.Find("CoinIndicator").position;
        temp.y = Camera.main.WorldToScreenPoint(new Vector3(0, levelPieces[1].GetComponent<LevelPiece>().coinLocation)).y;
        guiCanvas.Find("CoinIndicator").position = temp;
    }

    public void NewPlayer(int index)
    {
        GameObject newModel = shop.allBlockList[index];

        Destroy(currentModel);
        currentModel = Instantiate(newModel);
        Destroy(currentModel.GetComponent<ShopObject>());
        currentModel.AddComponent<PlayerModel>();

        currentModel.GetComponent<PlayerModel>().player = player.transform;
        currentModel.transform.position = playerSpawnPoint;

        GameObject temp = Instantiate(defaultParticle);
        temp.transform.parent = currentModel.transform;
        temp.transform.localPosition = new Vector3(0, 0, 0);

        GameObject.Find("Main Camera").GetComponent<CameraScript>().player = currentModel;

        line.GetComponent<LineConnection>().Player = currentModel.transform;
    }

    public void UpdateCoin()
    {
        GameObject.Find("Canvas").transform.Find("CoinUI").GetComponent<Text>().text = PlayerPrefs.GetInt("CoinsAmount") + "";
    }

    public void FixedUpdate()
    {
        if (!gameOver && player.transform.position.y < 0 && !connectedPoint)
        {
            GameObject.Find("Canvas").GetComponent<Animator>().SetTrigger("Died");
            gameOver = true;
            player.GetComponent<Rigidbody>().velocity *= 0;
            player.GetComponent<Rigidbody>().useGravity = false;
        }
    }
}
