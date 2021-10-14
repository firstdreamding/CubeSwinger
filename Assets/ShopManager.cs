using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopManager : MonoBehaviour
{
    public GameObject shopContainer;

    public Vector3 startingPos;
    public Vector3 rot;
    public float xOffSet;
    public List<GameObject> allBlockList;

    private float currentOffset;
    private List<GameObject> currentInstance;
    private bool mouseDown;
    private Vector3 mousePos;
    private Vector3 gameObjectPos;
    private GameObject selectedGameObject;
    private int index;

    // Start is called before the first frame update
    void Start()
    {
        index = 0;
        currentInstance = new List<GameObject>();
        foreach(GameObject go in allBlockList)
        {
            GameObject temp = Instantiate(go);
            temp.transform.parent = shopContainer.transform;
            Vector3 pos = startingPos;
            pos.x = currentOffset;
            temp.transform.localRotation = Quaternion.Euler(rot);
            temp.transform.localPosition = pos;
            currentInstance.Add(temp);

            currentOffset -= xOffSet;
        }

        selectedGameObject = currentInstance[0];
        selectedGameObject.GetComponent<ShopObject>().Toggle();
        GameObject.Find("Canvas").transform.Find("ShopMenu").Find("ItemName").GetComponent<Text>().text = selectedGameObject.GetComponent<ShopObject>().objectName;
    }

    private void OnEnable()
    {
        mouseDown = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            mouseDown = true;
            mousePos = Input.mousePosition;
            gameObjectPos = shopContainer.transform.localPosition;
        }

        if (Input.GetMouseButtonUp(0))
        {
            mouseDown = false;
        }

        if (mouseDown)
        {
            Vector3 tempVector = gameObjectPos;
            tempVector.x += (mousePos.x - Input.mousePosition.x)/100;
            tempVector.x = Mathf.Clamp(tempVector.x, 0, (currentInstance.Count - 1) * xOffSet);
            shopContainer.transform.localPosition = tempVector;
        } else
        {
            Vector3 tempVector = shopContainer.transform.position;
            tempVector.x = selectedGameObject.transform.localPosition.x * -1;
            shopContainer.transform.localPosition = Vector3.Lerp(shopContainer.transform.localPosition, tempVector, Time.deltaTime*4);
        }

        if (selectedGameObject != currentInstance[(int)((shopContainer.transform.position.x + xOffSet / 2) / xOffSet)])
        {
            ChangedHover();
        }
    }

    private void ChangedHover()
    {
        selectedGameObject.GetComponent<ShopObject>().Toggle();
        index = (int)((shopContainer.transform.position.x + xOffSet / 2) / xOffSet);
        selectedGameObject = currentInstance[index];
        selectedGameObject.GetComponent<ShopObject>().Toggle();

        GameObject.Find("ItemName").GetComponent<Text>().text = selectedGameObject.GetComponent<ShopObject>().objectName;
        GameObject.Find("ItemName").GetComponent<Animator>().SetTrigger("New Name");
    }

    public void Back()
    {
        GameObject.Find("Canvas").GetComponent<Animator>().SetTrigger("CloseShop");
        gameObject.SetActive(false);
    }

    public void Play()
    {
        PlayerPrefs.SetInt("PlayerModel", index);

        GameManager.GameInstance.NewPlayer(index);
        Debug.Log(index);
        GameObject.Find("Canvas").GetComponent<Animator>().SetTrigger("CloseShop");
        gameObject.SetActive(false);
    }
}
