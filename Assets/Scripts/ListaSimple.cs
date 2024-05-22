public class ListaSimple<T>
{
    private T[] array;
    private int length;
    private int capacity;

    public ListaSimple()
    {
        capacity = 4;
        array = new T[capacity];
        length = 0;
    }

    public void Add(T item)
    {
        if (length == capacity)
        {
            T[] newArray = new T[capacity = capacity * 2];
            for (int i = 0; i < length; i = i + 1)
            {
                newArray[i] = array[i];
            }
            array = newArray;
            capacity = capacity * 2;
        }
        array[length] = item;
        length = length + 1;
    }

    public T Get(int index)
    {
        if (index < 0 || index >= length)
        {
            throw new System.IndexOutOfRangeException("Mucho");
        }
        return array[index];
    }

    public int Length
    {
        get { return length; }
    }
}
