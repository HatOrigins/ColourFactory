using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColourBehaviour : MonoBehaviour {

    public int colour;
    private int sectionIndex = 0;
    private GameObject gridManager;
    private List<GameObject> level;
    private int speed = 3;

	// Use this for initialization
	void Start () {
        gridManager = GameObject.Find("GridManager");
        level = gridManager.GetComponent<Grid>().level;

        colour = Random.Range(1, 4);

        //This can be used to change the sprite instead of the colour incase different/more complex sprite were to be used
        switch (colour)
        {
            case 1:
                gameObject.GetComponent<SpriteRenderer>().material.color = Color.red;
                break;
            case 2:
                gameObject.GetComponent<SpriteRenderer>().material.color = Color.blue;
                break;
            case 3:
                gameObject.GetComponent<SpriteRenderer>().material.color = Color.green;
                break;
            case 4:
                gameObject.GetComponent<SpriteRenderer>().material.color = Color.yellow;
                break;
        }
	}
	
	// Update is called once per frame
	void FixedUpdate () {

        if (transform.position != level[sectionIndex].transform.position)
        {
            transform.position = Vector2.MoveTowards(transform.position, level[sectionIndex].transform.position, speed * Time.deltaTime);
        }else
        {
            if (sectionIndex != gridManager.GetComponent<Grid>().level.Count - 1)
                sectionIndex++;
            else
                Destroy(gameObject);
        }
    }
}
