using UnityEngine;
public class Enemy : MonoBehaviour
{
    public ListaSimple<GameObject> pathNodes;
    public Vector2 speedReference;
    public float energy;
    public float maxEnergy;
    public float restTime;
    private bool isResting;
    private float restTimer;
    private int currentIndex;
    private float currentWeight;
    public GameObject Nodeobjective;

    public void InitializePath(ListaSimple<GameObject> nodes)
    {
        pathNodes = nodes;
        currentIndex = 0;
        SetObjective(pathNodes.Get(currentIndex));
        currentWeight = 0;
    }

    void Start()
    {
        InitializeEnemy();
        Debug.Log("Energía Inicial: " + maxEnergy);
    }

    void Update()
    {
        if (isResting)
        {
            HandleResting();
        }
        else
        {
            MoveTowardsObjective();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Node" && !isResting)
        {
            Debug.Log("Energía Actual: " + energy);
            HandleNodeCollision(collision);
        }
    }

    private void InitializeEnemy()
    {
        energy = maxEnergy;
        isResting = false;
        restTimer = 0;
        currentIndex = 0;
        if (pathNodes != null && pathNodes.Length > 0)
        {
            SetObjective(pathNodes.Get(currentIndex));
        }
    }

    private void HandleResting()
    {
        Debug.Log("Recargando Energía...");
        restTimer +=Time.deltaTime;
        if (restTimer >= restTime)
        {
            isResting = false;
            energy = maxEnergy;
            restTimer = 0;
            Debug.Log("Energía Recargada");
        }
    }

    private void MoveTowardsObjective()
    {
        if (Nodeobjective != null)
        {
            transform.position = Vector2.SmoothDamp(transform.position, Nodeobjective.transform.position, ref speedReference, 0.5f);
        }
    }

    private void HandleNodeCollision(Collider2D collision)
    {
        if (collision.gameObject == Nodeobjective)
        {
            Node nodeController = collision.gameObject.GetComponent<Node>();

            UpdateCurrentIndex();
            (Node nextNode, float weight) = nodeController.SelectRandomAdjacent();
            SetObjective(nextNode.gameObject);
            currentWeight = weight;

            energy = energy - currentWeight;

            if (energy <= 0)
            {
                isResting = true;
            }
        }
    }

    private void UpdateCurrentIndex()
    {
        currentIndex = currentIndex + 1;
        if (currentIndex >= pathNodes.Length)
        {
            currentIndex = 0;
        }
    }

    private void SetObjective(GameObject newObjective)
    {
        Nodeobjective = newObjective;
    }
}
