using UnityEngine;

public class GraphsControl : MonoBehaviour
{
    public GameObject nodePrefabs;
    public TextAsset nodePositionTxt;
    public string[] arrayNodePositions;
    public string[] currentNodePositions;
    public ListaSimple<GameObject> AllNodes; 
    public TextAsset nodeConectionsTxt;
    public string[] arrayNodeConections;
    public string[] currentNodeConections;
    public Enemy enemy;

    void Start()
    {
        AllNodes = new ListaSimple<GameObject>(); 
        CreateNode();
        CreateConnections();
        CreateEnemyWay();
    }

    void CreateNode()
    {
        if (nodePositionTxt != null)
        {
            arrayNodePositions = nodePositionTxt.text.Split('\n');
            for (int i = 0; i < arrayNodePositions.Length; i = i + 1)
            {
                currentNodePositions = arrayNodePositions[i].Split(',');
                Vector2 position = new Vector2(float.Parse(currentNodePositions[0]), float.Parse(currentNodePositions[1]));
                GameObject tmp = Instantiate(nodePrefabs, position, transform.rotation);
                AllNodes.Add(tmp);
            }
        }
    }
    void CreateEnemyWay()
    {

        enemy.InitializePath(AllNodes);
    }

    void CreateConnections()
    {
        if (nodeConectionsTxt != null)
        {
            arrayNodeConections = nodeConectionsTxt.text.Split('\n');
            for (int i = 0; i < arrayNodeConections.Length; i = i + 1)
            {
                currentNodeConections = arrayNodeConections[i].Split(',');
                int fromNodeIndex = int.Parse(currentNodeConections[0]);
                int toNodeIndex = int.Parse(currentNodeConections[1]);
                float weight = float.Parse(currentNodeConections[2]);

                if (AllNodes.Get(fromNodeIndex) != null && AllNodes.Get(toNodeIndex) != null)
                {
                    AllNodes.Get(fromNodeIndex).GetComponent<Node>().AddAdjacentNode(AllNodes.Get(toNodeIndex).GetComponent<Node>(), weight);
                }
            }
        }
    }
}
