using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SectionPlacement : MonoBehaviour
{
    public bool canBePlaced = true;
    public GameObject startSection;
    public GameObject endSection;
    public GameObject gridSectiopn;
    public GameObject colourButton;
    public List<GameObject> sections;
    public Image availableSections;
    public Image availableSectionsArea;
    private List<Image> possibleSections;
    public bool isNextPosition = false;
    GameObject gridManager;
    private bool hasMoved = false;
    // Use this for initialization
    void Start()
    {
        gridManager = GameObject.FindGameObjectWithTag("GridManager");
        availableSectionsArea = GameObject.Find("AvailableSections").GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void OnMouseOver()
    {
        if (gameObject.name == "GridSection(Clone)")
            InitialPlacement();
        else
        {
            Deletion();

            foreach (Transform child in transform)
            {
                if (!hasMoved)
                {
                    Vector3 tempPos = child.transform.position;
                    child.transform.position = new Vector2(child.transform.position.x + 0.001f, child.transform.position.y);
                    child.transform.position = tempPos;
                }

                hasMoved = true;
            }
        }
    }

    private void Deletion() //Allows for the delection of existing sections, replaced by default section
    {
        if (Input.GetKey(KeyCode.Delete))
        {
            Instantiate(gridSectiopn, transform.position, Quaternion.identity);

            if (name == "Start(Clone)")
                GameObject.Find("GridManager").GetComponent<Grid>().startPlaced = false;

            Destroy(gameObject);
        }
    }

    private void InitialPlacement() //allows placment of sections, if there is no start, that will be the default
    {
        gameObject.GetComponent<SpriteRenderer>().material.color = Color.magenta;

        if (Input.GetMouseButton(0) && canBePlaced)
        {
            if (!GameObject.Find("GridManager").GetComponent<Grid>().startPlaced)
            {
                Instantiate(startSection, transform.position, Quaternion.identity);
                Destroy(gameObject);
                GameObject.Find("GridManager").GetComponent<Grid>().startPlaced = true;
                AvailableGridPositions();
            }
            else if (GameObject.Find("GridManager").GetComponent<Grid>().endPlaced)
            {
                if (GameObject.Find("GridManager").GetComponent<Grid>().buttonCount < 4)
                {
                    Instantiate(colourButton, transform.position, Quaternion.identity);
                    Destroy(gameObject);
                    GameObject.Find("GridManager").GetComponent<Grid>().buttonCount++;
                }
            }
        }
    }

    private void AvailableGridPositions()
    {
        foreach (GameObject section in GameObject.FindGameObjectsWithTag("InitialGrid"))
        {
            section.GetComponent<SpriteRenderer>().material.color = Color.red;
            section.GetComponent<SectionPlacement>().canBePlaced = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (gameObject.tag == "InitialGrid")
        {
            if (collision.gameObject.tag == "GridChecker")
            {
                GetComponent<SpriteRenderer>().material.color = Color.white;
                GetComponent<SectionPlacement>().canBePlaced = true;
                isNextPosition = true;
                Transform pos = availableSectionsArea.transform;
                float temp = -319;
                gridManager.GetComponent<Grid>().hasTriggered = true;

                switch (collision.GetComponent<Checker>().direction)
                {
                    case 1:
                        for (int i = 0; i < gridManager.GetComponent<Grid>().leftRooms.Length; i++)
                        {
                            Image inst = Instantiate(availableSections, new Vector2(temp, pos.position.y), Quaternion.identity);
                            inst.transform.SetParent(availableSectionsArea.transform, false);
                            inst.rectTransform.position = new Vector3(inst.transform.position.x, inst.transform.position.y, -1);
                            inst.GetComponent<Image>().sprite = gridManager.GetComponent<Grid>().leftRooms[i].GetComponent<SpriteRenderer>().sprite;
                            temp += 100;
                        }
                        GameObject.Find("GridManager").GetComponent<Grid>().direction = 1;
                        break;
                    case 2:
                        for (int i = 0; i < gridManager.GetComponent<Grid>().topRooms.Length; i++)
                        {
                            Image inst = Instantiate(availableSections, new Vector2(temp, pos.position.y), Quaternion.identity);
                            inst.transform.SetParent(availableSectionsArea.transform, false);
                            inst.rectTransform.position = new Vector3(inst.transform.position.x, inst.transform.position.y, -1);
                            inst.GetComponent<Image>().sprite = gridManager.GetComponent<Grid>().topRooms[i].GetComponent<SpriteRenderer>().sprite;
                            temp += 100;
                        }
                        GameObject.Find("GridManager").GetComponent<Grid>().direction = 2;
                        break;
                    case 3:
                        for (int i = 0; i < gridManager.GetComponent<Grid>().rightRooms.Length; i++)
                        {
                            Image inst = Instantiate(availableSections, new Vector2(temp, pos.position.y), Quaternion.identity);
                            inst.transform.SetParent(availableSectionsArea.transform, false);
                            inst.rectTransform.position = new Vector3(inst.transform.position.x, inst.transform.position.y, -1);
                            inst.GetComponent<Image>().sprite = gridManager.GetComponent<Grid>().rightRooms[i].GetComponent<SpriteRenderer>().sprite;
                            temp += 100;
                        }
                        GameObject.Find("GridManager").GetComponent<Grid>().direction = 3;
                        break;
                    case 4:
                        for (int i = 0; i < gridManager.GetComponent<Grid>().bottomRooms.Length; i++)
                        {
                            Image inst = Instantiate(availableSections, new Vector2(temp, pos.position.y), Quaternion.identity);
                            inst.transform.SetParent(availableSectionsArea.transform, false);
                            inst.rectTransform.position = new Vector3(inst.transform.position.x, inst.transform.position.y, -1);
                            inst.GetComponent<Image>().sprite = gridManager.GetComponent<Grid>().bottomRooms[i].GetComponent<SpriteRenderer>().sprite;
                            temp += 100;
                        }
                        GameObject.Find("GridManager").GetComponent<Grid>().direction = 4;
                        break;
                    default:
                        break;
                }

            }
        }
    }

    private void OnMouseExit()
    {
        if (!canBePlaced)
            gameObject.GetComponent<SpriteRenderer>().material.color = Color.red;
        else
            gameObject.GetComponent<SpriteRenderer>().material.color = Color.white;

    }
}
