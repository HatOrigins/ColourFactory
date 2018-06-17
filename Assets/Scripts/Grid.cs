using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Grid : MonoBehaviour
{
    private float rows, columns; //Sets initial grid size
    public GameObject initialGrid; //Game object to set the initial grid
    private Vector2 position;
    public bool startPlaced = false;
    public bool endPlaced = false;

    public GameObject[] bottomRooms;
    public GameObject[] topRooms;
    public GameObject[] leftRooms;
    public GameObject[] rightRooms;

    public int direction;
    public GameObject sectionArea;
    public List<GameObject> level;

    private GameObject sectionAdd;
    public GameObject colours;

    private bool hasInputted = false;
    public bool hasTriggered = false;

    public int buttonCount = 0;
    // Use this for initialization
    void Start()
    {
        position = transform.position;
        rows = 10;
        columns = 20;

        for (float x = 0; x < columns; x++)
        {
            for (float y = 0; y < rows; y++)
            {
                Instantiate(initialGrid, new Vector2(position.x + x, position.y + y), Quaternion.identity);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (startPlaced)
        {
            switch (Input.inputString)
            {
                case "1":
                    foreach (GameObject sections in GameObject.FindGameObjectsWithTag("InitialGrid"))
                    {
                        if (sections.GetComponent<SectionPlacement>().isNextPosition)
                        {
                            SpawnSection(0, sections.transform.position);
                            Destroy(sections.gameObject);
                            hasInputted = true;
                            StartCoroutine(endChecker());
                        }
                    }
                    break;
                case "2":
                    foreach (GameObject sections in GameObject.FindGameObjectsWithTag("InitialGrid"))
                    {
                        if (sections.GetComponent<SectionPlacement>().isNextPosition)
                        {
                            SpawnSection(1, sections.transform.position);
                            Destroy(sections.gameObject);
                            hasInputted = true;
                            StartCoroutine(endChecker());
                        }
                    }
                    break;
                case "3":
                    foreach (GameObject sections in GameObject.FindGameObjectsWithTag("InitialGrid"))
                    {
                        if (sections.GetComponent<SectionPlacement>().isNextPosition)
                        {
                            SpawnSection(2, sections.transform.position);
                            Destroy(sections.gameObject);
                            hasInputted = true;
                            StartCoroutine(endChecker());
                        }
                    }
                    break;
                case "4":
                    foreach (GameObject sections in GameObject.FindGameObjectsWithTag("InitialGrid"))
                    {
                        if (sections.GetComponent<SectionPlacement>().isNextPosition)
                        {
                            SpawnSection(3, sections.transform.position);
                            Destroy(sections.gameObject);
                            hasInputted = true;
                            StartCoroutine(endChecker());

                            foreach (GameObject section in GameObject.FindGameObjectsWithTag("InitialGrid"))
                            {
                                section.GetComponent<SpriteRenderer>().material.color = Color.white;
                                section.GetComponent<SectionPlacement>().canBePlaced = true;
                            }

                            endPlaced = true;
                        }
                    }
                    break;
                case "r":
                    if (level.Count > 0)
                    {
                        GameObject inst = Instantiate(initialGrid, level[level.Count - 1].transform.position, Quaternion.identity);
                        Destroy(level[level.Count - 1]);
                        level.RemoveAt(level.Count - 1);

                        foreach (GameObject section in GameObject.FindGameObjectsWithTag("InitialGrid"))
                        {
                            section.GetComponent<SpriteRenderer>().material.color = Color.red;
                            section.GetComponent<SectionPlacement>().canBePlaced = false;
                            section.GetComponent<SectionPlacement>().isNextPosition = false;
                        }

                        inst.GetComponent<SectionPlacement>().canBePlaced = true;

                        if (endPlaced)
                            endPlaced = false;
                    }
                    break;
                default:
                    if (Input.anyKeyDown) //Not important, used because area colour keeps tunring red
                        GameObject.Find("AvailableSections").GetComponent<Image>().material.color = Color.white;
                    break;
            }
        }

        if (Input.GetKeyDown(KeyCode.P))
        {
            Instantiate(colours, GameObject.Find("Start(Clone)").transform.position, Quaternion.identity);
        }
    }

    IEnumerator ColourReturn()
    {
        yield return new WaitForSeconds(0.2f);
        GameObject.Find("AvailableSections").GetComponent<Image>().material.color = Color.white;
    }

    IEnumerator endChecker()
    {
        yield return new WaitForSeconds(0.1f);

        if (hasInputted && !hasTriggered)
        {
            Debug.Log("Buggered");
            //Replace last section with end
            int lastItem = level.Count - 1;
            GameObject inst = Instantiate(bottomRooms[3], level[lastItem].transform.position, Quaternion.identity);
            Destroy(level[lastItem].gameObject);
            level[level.Count - 1] = inst;

            foreach (GameObject section in GameObject.FindGameObjectsWithTag("InitialGrid"))
            {
                section.GetComponent<SpriteRenderer>().material.color = Color.white;
                section.GetComponent<SectionPlacement>().canBePlaced = true;
            }

            endPlaced = true;
        }
        else if (hasInputted && hasTriggered)
        {
            Debug.Log("Still Going");
            hasTriggered = false;
        }
    }

    private void SpawnSection(int index, Vector2 position)
    {

        switch (direction)
        {
            case 1:
                sectionAdd = Instantiate(leftRooms[index], position, Quaternion.identity);
                break;
            case 2:
                sectionAdd = Instantiate(topRooms[index], position, Quaternion.identity);
                break;
            case 3:
                sectionAdd = Instantiate(rightRooms[index], position, Quaternion.identity);
                break;
            case 4:
                sectionAdd = Instantiate(bottomRooms[index], position, Quaternion.identity);
                break;
            default:
                break;
        }

        level.Add(sectionAdd);

        foreach (Transform child in sectionArea.transform)
        {
            Destroy(child.gameObject);
        }
    }
}
