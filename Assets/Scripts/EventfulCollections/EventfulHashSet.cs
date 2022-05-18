using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

[Serializable]
public class EventfulHashSet<T> : ISet<T>, ISerializationCallbackReceiver {
    public event EventHandler OnAdded;
    public event EventHandler OnRemoved;
    public event EventHandler OnClear;

    [SerializeField] private HashSet<T> _hashset = new HashSet<T>();
    [SerializeField, HideInInspector] private List<T> _hashsetList = new List<T>();

    public int Count => _hashset.Count;
    public bool IsReadOnly => throw new NotImplementedException();

    public IEnumerator<T> GetEnumerator() => _hashset.GetEnumerator();
    IEnumerator IEnumerable.GetEnumerator() => _hashset.GetEnumerator();

    public bool IsProperSubsetOf(IEnumerable<T> other) => _hashset.IsProperSubsetOf(other);
    public bool IsProperSupersetOf(IEnumerable<T> other) => _hashset.IsProperSupersetOf(other);
    public bool IsSubsetOf(IEnumerable<T> other) => _hashset.IsSubsetOf(other);
    public bool IsSupersetOf(IEnumerable<T> other) => _hashset.IsSupersetOf(other);
    public bool Overlaps(IEnumerable<T> other) => _hashset.Overlaps(other);
    public bool SetEquals(IEnumerable<T> other) => _hashset.SetEquals(other);

    public void Clear() {
        _hashset.Clear();
        OnClear?.Invoke(this, EventArgs.Empty);
    }

    public void CopyTo(T[] array, int arrayIndex) {
        _hashset.CopyTo(array, arrayIndex);
    }

    public bool Contains(T item) => _hashset.Contains(item);

    public bool Add(T item) {
        if(_hashset.Add(item)) {
            AddingNewEventArgs e = new AddingNewEventArgs();
            e.NewObject = item;
            OnAdded?.Invoke(this, e);
            return true;
        }
        return false;
    }
    void ICollection<T>.Add(T item) {
        _hashset.Add(item);
    }

    public bool Remove(T item) {
        if(_hashset.Remove(item)) {
            OnRemoved?.Invoke(this, EventArgs.Empty);
            return true;
        }
        return false;
    }

    public void ExceptWith(IEnumerable<T> other) {
        _hashset.ExceptWith(other);
    }

    public void IntersectWith(IEnumerable<T> other) {
        _hashset.IntersectWith(other);
    }

    public void SymmetricExceptWith(IEnumerable<T> other) {
        _hashset.SymmetricExceptWith(other);
    }

    public void UnionWith(IEnumerable<T> other) {
        _hashset.UnionWith(other);
    }

    public void OnBeforeSerialize() {
        _hashsetList.Clear();
        foreach(var hashsetEntry in _hashset)
            _hashsetList.Add(hashsetEntry);
    }

    public void OnAfterDeserialize() {
        _hashset.Clear();
        foreach(var hashsetEntry in _hashsetList)
            _hashset.Add(hashsetEntry);
    }
}