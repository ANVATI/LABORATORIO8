using UnityEngine;

public class Node : MonoBehaviour
{
    public ListaSimple<(Node node, float weight)> adjacentNodes;

    void Awake()
    {
        adjacentNodes = CreateListAdjacentNodes();
    }

    public void AddAdjacentNode(Node node, float weight)
    {
        var nodoAdyacente = CreateWeigthNpde(node, weight);
        adjacentNodes.Add(nodoAdyacente);
    }

    public (Node node, float weight) SelectRandomAdjacent()
    {
        return adjacentNodes.Get(ObtenerIndiceAleatorio());
    }

    private ListaSimple<(Node, float)> CreateListAdjacentNodes()
    {
        return new ListaSimple<(Node, float)>();
    }

    private (Node, float) CreateWeigthNpde(Node node, float weight)
    {
        return (node, weight);
    }

    private int ObtenerIndiceAleatorio()
    {
        return Random.Range(0, adjacentNodes.Length);
    }
}
