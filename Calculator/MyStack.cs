//https://github.com/Penkalatte00700/Calculator

namespace Calculator;

public class MyStack<T>
{
    private T[] _data;
    private int _count;

    public MyStack()
    {
        _data = new T[4];
        _count = 0;
    }

    public int Count
    {
        get { return _count;  }
    }

    public void Push(T item)
    {
        if (_count == _data.Length)
        {
            T[] newData = new T[_data.Length * 2];
            for (int i = 0; i < _data.Length; i++)
            {
                newData[i] = _data[i];
            }

            _data = newData;
        }

        _data[_count] = item;
        _count++;
    }

    public T Pop()
    {
        if (_count == 0)
        {
            throw new Exception("Stack is empty");
        }

        _count--;
        return _data[_count];
    }

    public T Peek()
    {
        if (_count == 0)
        {
            throw new Exception("Stack is empty");
        }

        return _data[_count - 1];
    }

    public bool IsEmpty()
    {
        return _count == 0;
    }
}