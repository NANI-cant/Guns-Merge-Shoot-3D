using System;
using UnityEngine;

//Необходимо использовать вместе с атрибутом [SerializeReference] а не [SerializeField]!
public class SelectImplementationAttribute : PropertyAttribute {
    public Type FieldType;

    public SelectImplementationAttribute(Type fieldType) {
        FieldType = fieldType;
    }
}

