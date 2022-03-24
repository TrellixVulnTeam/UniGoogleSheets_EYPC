using System;
using System.IO;
using System.Text;
 
public class CodeGenerator
{
    private readonly string LAST_MARKER = "__END__POINT__";
    private readonly StringBuilder classBuilder = new StringBuilder();
    private readonly StringBuilder fieldBuilder = new StringBuilder();
    private readonly StringBuilder methodBuilder = new StringBuilder();
    private readonly StringBuilder namespaceBuilder = new StringBuilder();
    private Status status;
    enum Status
    {
        NONE,
        CURRENT_NAMESPACE,
        CURRENT_CLASS,
        CURRENT_ADD_FIELDS,
        CURRENT_ADD_METHODS
    }
    
    public void UsingNamespace(string namespaceName)
    {      
        if(status != Status.NONE) throw  new Exception("call UsingNamespace before any other method");
        status = Status.CURRENT_NAMESPACE;
        namespaceBuilder.AppendLine("using " + namespaceName + ";");
    }
    
    public void CreateClass(string @namespace, string @classname)
    {
        if(status != Status.CURRENT_NAMESPACE) throw  new Exception("using __namespace__; must be added before methods");
        status = Status.CURRENT_CLASS;
        classBuilder.Append($"namespace {@namespace} {{\n");
        classBuilder.Append($"public class {@classname}{{\n {LAST_MARKER} }}\n");
        classBuilder.Append($"}}\n");
    }

    public void AddField(string fieldType, string fieldName)
    {
        if(status < Status.CURRENT_CLASS) throw  new Exception("class must be created before methods");
        status = Status.CURRENT_ADD_FIELDS;
        fieldBuilder.AppendLine($"public {fieldType} {fieldName};  "); 
    }

    public void AddMethod(string methodType, string methodName, string methodBody)
    {
        if(status < Status.CURRENT_CLASS) throw  new Exception("class must be created before methods");
        status = Status.CURRENT_ADD_METHODS;
        methodBuilder.AppendLine($"public {methodType} {methodName}()");
        methodBuilder.AppendLine($"{{\n {methodBody} \n}}");
    }
    
    
    public string GenerateCode()
    {
        if(status < Status.CURRENT_CLASS) throw  new Exception("class must be created before methods");
        status = Status.NONE;
        classBuilder.Clear();
        fieldBuilder.Clear();
        methodBuilder.Clear();
        namespaceBuilder.Clear();
        return namespaceBuilder.ToString() + classBuilder.ToString().Replace(LAST_MARKER, fieldBuilder.ToString() + methodBuilder.ToString());
    }
    
    
}



