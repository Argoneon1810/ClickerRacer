using System.Runtime.Serialization;
using System.ComponentModel;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventfulList<T> : IList<T>, ISerializable {
    protected List<T> _list = new List<T>();

    public event EventHandler OnAdded;

    public T this[int index] {
        get => _list[index];
        set => _list[index] = value;
    }

    public int Count => _list.Count;
    public bool IsReadOnly => false;

    public IEnumerator<T> GetEnumerator() => (IEnumerator<T>)_list.GetEnumerator();
    IEnumerator IEnumerable.GetEnumerator() => (IEnumerator)_list.GetEnumerator();

    public bool Contains(T item) => _list.Contains(item);
    public int IndexOf(T item) => _list.IndexOf(item);
    public bool Remove(T item) => _list.Remove(item);

    public void RemoveAt(int index) {
        _list.RemoveAt(index);
    }

    public void Add(T item) {
        _list.Add(item);
        AddingNewEventArgs eventArgs = new AddingNewEventArgs();
        eventArgs.NewObject = item;
        OnAdded?.Invoke(this, eventArgs);
    }

    public void Clear() {
        _list.Clear();
    }

    public void CopyTo(T[] array, int arrayIndex) {
        _list.CopyTo(array, arrayIndex);
    }

    public void Insert(int index, T item) {
        _list.Insert(index, item);
    }

    public void GetObjectData(SerializationInfo info, StreamingContext context) {
        throw new NotImplementedException();
    }
}