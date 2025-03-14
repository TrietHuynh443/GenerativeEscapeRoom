using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

using DI;
using Interface.Services;

public enum Lifetime
{
    Transient,
    Singleton
}

public class DependenciesProvider : MonoBehaviour
{
    private readonly Dictionary<Type, object> _singletons = new Dictionary<Type, object>();
    private readonly Dictionary<Type, Func<object>> _registrations = new Dictionary<Type, Func<object>>();

    // Register a dependency with a specific lifetime
    public void Register<T>(Func<T> factory, Lifetime lifetime = Lifetime.Singleton) where T : class
    {
        
        if (lifetime == Lifetime.Singleton)
        {
            _registrations[typeof(T)] = () =>
            {
                if (!_singletons.ContainsKey(typeof(T)))
                {
                    if (typeof(MonoService).IsAssignableFrom(typeof(T)))
                    {
                        var newInstance = new GameObject();
                        // var newInstance = Instantiate(newGameObject, transform);
                        newInstance.AddComponent(typeof(T));
                        newInstance.name = typeof(T).Name;
                        newInstance.transform.SetParent(transform);
                        _singletons[typeof(T)] = newInstance.GetComponent<T>();
                        Inject(_singletons[typeof(T)]); // inject all services into mono object
                    }
                    else
                    {
                        _singletons[typeof(T)] = factory();
                    }
                    Debug.Log($"Registered new {typeof(T).Name}");
                }
                return _singletons[typeof(T)];
            };
        }
        else
        {
            _registrations[typeof(T)] = () => factory();
        }
    }

    public void RegisterSingleton<T>(T instance) where T : class
    {
        _singletons[typeof(T)] = instance;
    }

    // Get an instance of a dependency
    public T Resolve<T>() where T : class
    {
        return (T)Resolve(typeof(T));
    }

    public object Resolve(Type type)
    {
        if (typeof(MonoService).IsAssignableFrom(type) && _singletons.TryGetValue(type, out var resolve))
        {
            return resolve;
        }
        if (_registrations.ContainsKey(type))
        {
            
            if (_registrations.TryGetValue(type, out var factory))
            {
                return factory();
            }
        }
    

        throw new Exception($"No registration for type {type.FullName}");
    }
    
    // Inject dependencies into fields and properties marked with [Injector]
    public void Inject(object dependant)
    {
        var type = dependant.GetType();
        while (type != null)
        {
            // Inject fields
            var fields = type.GetFields(BindingFlags.Public | BindingFlags.NonPublic
                                         | BindingFlags.DeclaredOnly | BindingFlags.Instance);
            foreach (var field in fields)
            {
                if (field.GetCustomAttribute<InjectorAttribute>(false) != null)
                {
                    var dependency = Resolve(field.FieldType);
                    field.SetValue(dependant, dependency);
                }
            }

            // Inject properties
            var properties = type.GetProperties(BindingFlags.Public | BindingFlags.NonPublic
                                                 | BindingFlags.DeclaredOnly | BindingFlags.Instance);
            foreach (var property in properties)
            {
                if (property.GetCustomAttribute<InjectorAttribute>(false) != null)
                {
                    var dependency = Resolve(property.PropertyType);
                    property.SetValue(dependant, dependency);
                }
                //
                // if (typeof(MonoService).IsAssignableFrom(property.PropertyType))
                // {
                //     Inject(property);
                // }
            }

            type = type.BaseType;
        }
    }
    
    /// <summary>
    /// Automatically inject dependencies into all active MonoBehaviours in the scene.
    /// </summary>
    public void AutoInjectAll()
    {
        var monoBehaviours = FindObjectsOfType<MonoBehaviour>();

        foreach (var monoBehaviour in monoBehaviours)
        {
            Inject(monoBehaviour);
        }
    }
}

// Example usage

// public interface IService
// {
//     void DoSomething();
// }

// public class Service : IService
// {
//     public void DoSomething()
//     {
//         Debug.Log("Service is doing something!");
//     }
// }

// public class Consumer
// {
//     [Injector]
//     private IService _service;
//
//     public void UseService()
//     {
//         _service?.DoSomething();
//     }
// }

// public class DIExample : MonoBehaviour
// {
//     private void Start()
//     {
//         var provider = new DependenciesProvider();
//         provider.Register<IService>(() => new Service());
//         provider.Register<Consumer>(() => new Consumer());
//
//         var consumer = provider.Resolve<Consumer>();
//         provider.Inject(consumer);
//         consumer.UseService();
//     }
// }
