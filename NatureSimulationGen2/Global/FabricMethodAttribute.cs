using System;

[AttributeUsage(AttributeTargets.Class)]
public class FabricMethodAttribute : Attribute
{
    public string Name { get; set; }
    public FabricMethodAttribute(string name)
    {
        Name = name;
    }
}