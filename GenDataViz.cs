using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // Required when Using UI elements.

public class GenDataViz : MonoBehaviour
{
    int scaleSize = 10;
    int scaleSizeFactor = 5;
    public GameObject graphContainer;
    int binDistance = 1;
    float offset = 0;


	//Check if image target is detected
	public GameObject Target;
	private bool detected = false;

	// The anchor object of your graph
	public GameObject GraphAnchor;

    // Use this for initialization
    void Start()
    {
        
    }

	void Update()
	{
		if (Target.activeSelf == true && detected == false)
		{
			Debug.Log("Image Target Detected");
			detected = true;

			CreateGraph();
		}
	}
    public void ClearChilds(Transform parent)
    {
        offset = 0;
        foreach (Transform child in parent)
        {
            Destroy(child.gameObject);
        }
    }


    public void CreateGraph()
    {
        Debug.Log("creating the graph");
		ClearChilds(GraphAnchor.transform);
        for (var i = 0; i < LinearRegression.quantityValues.Count; i++)
        {
			createBin((float)LinearRegression.quantityValues[i] / scaleSize, GraphAnchor);
			offset += binDistance;
        }

    }


	//Reduced the number of arguments of the function
	void createBin(float Scale_y, GameObject _parent)
	{
		GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
		cube.transform.SetParent(_parent.transform, true);

		//We use the localScale of the parent object in order to have a relative size
		Vector3 scale = new Vector3(GraphAnchor.transform.localScale.x / LinearRegression.quantityValues.Count, Scale_y, GraphAnchor.transform.localScale.x / 12);
		cube.transform.localScale = scale;

		//We use the position and rotation of the parent object in order to align our graph
		cube.transform.localPosition = new Vector3(offset - GraphAnchor.transform.localScale.x, (Scale_y / 2) - (GraphAnchor.transform.localScale.y / 2), 0);
		cube.transform.rotation = GraphAnchor.transform.rotation;

		// Let's add some colours
		cube.GetComponent<MeshRenderer>().material.color = Random.ColorHSV(0f, 1f, 1f, 1f, 0.5f, 1f);

	}

}