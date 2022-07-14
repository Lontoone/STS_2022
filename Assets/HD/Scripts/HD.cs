using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

namespace HD.Math
{
    class Math
    {
        //Add function
        public int Add(int number1, int number2)
        {
            return number1 + number2;
        }

        public float Add(float number1, float number2)
        {
            return number1 + number2;
        }
    }
}

//useing Pool reference: https://github.com/andy091045/poolTest
namespace HD.Pooling
{
    //Abstract pooling
    public interface IPool<T>
    {
        T GetInstance();
    }

    //ListPool
    class ListPool<T> : IPool<T> where T : class
    {
        Func<T> produce;
        int capacity;
        List<T> objects;
        Func<T, bool> useTest;
        bool expandable;

        public ListPool(Func<T> factoryMethod, int maxSize, Func<T, bool> inUse, bool expandable = true)
        {
            produce = factoryMethod;
            capacity = maxSize;
            objects = new List<T>(maxSize);
            useTest = inUse;
            this.expandable = expandable;
        }

        public T GetInstance()
        {
            var count = objects.Count;
            foreach (var item in objects)
            {
                if (!useTest(item))
                {
                    return item;
                }
            }
            if (count >= capacity && !expandable)
            {
                return null;
            }
            var obj = produce();
            objects.Add(obj);
            return obj;
        }
    }

    //QueuePool
    class QueuePool<T> : IPool<T>
    {
        Func<T> produce;
        int capacity;
        T[] objects;
        int index;

        public QueuePool(Func<T> factoryMethod, int maxSize)
        {
            produce = factoryMethod;
            capacity = maxSize;
            index = -1;
            objects = new T[maxSize];
        }

        public T GetInstance()
        {
            //stuff
            index = (index + 1) % capacity;

            if (objects[index] == null)
            {
                objects[index] = produce();
            }

            return objects[index];
        }
    }
}

namespace HD.Singleton{
    public class TSingletonMonoBehavior<T>: MonoBehaviour where T : MonoBehaviour{

        static T instance = null;

        public static T Instance{
            get { return instance ??= (FindObjectOfType(typeof(T))as T);}
            set {instance = value; }
        }

        protected virtual void Awake(){
            if(instance == null) instance = this as T;
            if(instance == this) DontDestroyOnLoad(this);
            else DestroyImmediate(this);
        }

        protected virtual void OnDestroy() => instance = null;
    }
}