using System.Collections.Generic;
using UnityEngine;

public class ArtManager : MonoBehaviour
{
    public List<Texture> allPaintings; // Assign all painting textures in the inspector
    public List<string> allRiddles; // Corresponding riddles for each painting
    public GameObject paintingPrefab; // Assign the prefab used for paintings

    private List<GameObject> paintingObjects;
    private List<int> availableIndices;
    private GameObject correctPainting;
    private string currentRiddle;

    void Start()
    {
        // Initialize painting objects and indices
        InitializePaintings();
        ShufflePaintings();
    }

    void InitializePaintings()
    {
        paintingObjects = new List<GameObject>();
        availableIndices = new List<int>();
        // Populate the painting objects and indices from the children of this GameObject
        for (int i = 0; i < transform.childCount; i++)
        {
            availableIndices.Add(i);
            paintingObjects.Add(transform.GetChild(i).gameObject);
        }
    }

    void ShufflePaintings()
    {
        // Randomly shuffle the painting objects to vary their positions
        for (int i = 0; i < paintingObjects.Count; i++)
        {
            int randomIndex = Random.Range(0, paintingObjects.Count);
            GameObject temp = paintingObjects[i];
            paintingObjects[i] = paintingObjects[randomIndex];
            paintingObjects[randomIndex] = temp;
        }

        AssignPaintingsToRiddles();
    }

    // Assign paintings to shuffled positions and pair them with riddles
    void AssignPaintingsToRiddles()
    {
        // Set textures on shuffled painting objects based on their new order
        for (int i = 0; i < paintingObjects.Count; i++)
        {
            Renderer renderer = paintingObjects[i].GetComponent<Renderer>();
            if (renderer != null)
            {
                Material newMat = new Material(renderer.material);
                newMat.mainTexture = allPaintings[i]; // Assigning each painting based on shuffled list
                renderer.material = newMat;
            }
        }
        ChooseRiddleAndPainting();
    }

    // Choose a random riddle and ensure the corresponding painting is set as correct
    void ChooseRiddleAndPainting()
    {
        // Ensure there are available riddles and paintings to choose from
        if (availableIndices.Count > 0)
        {
            int riddleIndex = Random.Range(0, availableIndices.Count);
            currentRiddle = allRiddles[riddleIndex];
            correctPainting = paintingObjects[riddleIndex]; // Assigning correct painting based on riddle index
            availableIndices.RemoveAt(riddleIndex); // Remove the used index
            Debug.Log("Riddle: " + currentRiddle + " - Correct Painting: " + correctPainting.name);
        }
        else
        {
            Debug.Log("No more riddles available.");
        }
    }

    public void CheckAnswer(GameObject selectedPainting)
    {
        // Check if the selected painting is the correct one
        if (selectedPainting == correctPainting)
        {
            Debug.Log("Correct Answer! Riddle was: " + currentRiddle);
            // Setup for next level, if needed
        }
        else
        {
            Debug.Log("Wrong Answer, prepare for horde mode! Riddle was: " + currentRiddle);
            // Set attackMode to true for all enemies
            GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
            foreach (GameObject enemy in enemies)
            {
                enemy.GetComponent<Enemy>().SetAttackMode();
            }
        }
    }
}
