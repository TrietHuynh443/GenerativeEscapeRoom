using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using static System.Activator;

public class InjectorAttribute : Attribute
{
    
}

public class DependenciesProvider : UnitySingleton<DependenciesProvider>
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private Dictionary<Type, object> _dependencies = new Dictionary<Type, object>();

    private object Get(Type type)
    {
        if (_dependencies.ContainsKey(type))
        {
            throw new Exception("Dependency Injection Error");
        }
        var instance = CreateInstance(type);
        _dependencies.Add(type, instance);
        return instance;
    }
    
    public object Inject(object dependant)
    {
        var type = dependant.GetType();
        while (type != null)
        {
            var fields = type.GetFields(BindingFlags.Public | BindingFlags.NonPublic 
                                                            | BindingFlags.DeclaredOnly | BindingFlags.Instance);
            foreach (var field in fields)
            {
                if (field.GetCustomAttribute<InjectorAttribute>(false) == null) { continue; }

                field.SetValue(dependant, Get(field.FieldType));
            }
            type = type.BaseType;
        }
        return dependant;
    }
    
    public delegate object Delegate(DependenciesProvider dependant);
    
    public static Delegate FromClass<T>() where T : class, new ()
    {
        return (dependant) =>
        {
            return dependant.Inject(CreateInstance<T>());
        };
    }
}

