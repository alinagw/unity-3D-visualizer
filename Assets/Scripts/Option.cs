using UnityEngine;

public class Option<T> where T : Object
{
    private string m_name;
    public string Name { get { return m_name; } }

    private T m_item;
    public T Item { get { return m_item; } }

    public Option(string name, T item)
    {
        m_name = name;
        m_item = item;
    }
}
