﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:2.0.50727.3082
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------



[System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "3.0.0.0")]
public interface ICalculator
{
    
    int Add(int a, int b);
}

[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "3.0.0.0")]
[System.Xml.Serialization.XmlRootAttribute(ElementName="Add", Namespace="http://opennetcf.wcf.sample")]
public partial class AddRequest
{
    
    [System.Xml.Serialization.XmlElementAttribute(Namespace="http://opennetcf.wcf.sample", Order=0)]
    public int a;
    
    [System.Xml.Serialization.XmlElementAttribute(Namespace="http://opennetcf.wcf.sample", Order=1)]
    public int b;
    
    public AddRequest()
    {
    }
    
    public AddRequest(int a, int b)
    {
        this.a = a;
        this.b = b;
    }
}

[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "3.0.0.0")]
[System.Xml.Serialization.XmlRootAttribute(ElementName="AddResponse", Namespace="http://opennetcf.wcf.sample")]
public partial class AddResponse
{
    
    [System.Xml.Serialization.XmlElementAttribute(Namespace="http://opennetcf.wcf.sample", Order=0)]
    public int AddResult;
    
    public AddResponse()
    {
    }
    
    public AddResponse(int AddResult)
    {
        this.AddResult = AddResult;
    }
}

[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "3.0.0.0")]
public partial class CalculatorClient : Microsoft.Tools.ServiceModel.CFClientBase<ICalculator>, ICalculator
{
    
    public static System.ServiceModel.EndpointAddress EndpointAddress = new System.ServiceModel.EndpointAddress("http://localhost:8000/calculator/Calculator");
    
    public CalculatorClient() : 
            this(CreateDefaultBinding(), EndpointAddress)
    {
    }
    
    public CalculatorClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
            base(binding, remoteAddress)
    {
        addProtectionRequirements(binding);
    }
    
    private AddResponse Add(AddRequest request)
    {
        CFInvokeInfo info = new CFInvokeInfo();
        info.Action = "http://opennetcf.wcf.sample/ICalculator/Add";
        info.RequestIsWrapped = true;
        info.ReplyAction = "http://opennetcf.wcf.sample/ICalculator/AddResponse";
        info.ResponseIsWrapped = true;
        AddResponse retVal = base.Invoke<AddRequest, AddResponse>(info, request);
        return retVal;
    }
    
    public int Add(int a, int b)
    {
        AddRequest request = new AddRequest(a, b);
        AddResponse response = this.Add(request);
        return response.AddResult;
    }
    
    public static System.ServiceModel.Channels.Binding CreateDefaultBinding()
    {
        System.ServiceModel.Channels.CustomBinding binding = new System.ServiceModel.Channels.CustomBinding();
        binding.Elements.Add(new System.ServiceModel.Channels.TextMessageEncodingBindingElement(System.ServiceModel.Channels.MessageVersion.Soap11, System.Text.Encoding.UTF8));
        binding.Elements.Add(new System.ServiceModel.Channels.HttpTransportBindingElement());
        return binding;
    }
    
    private void addProtectionRequirements(System.ServiceModel.Channels.Binding binding)
    {
        if ((IsSecureMessageBinding(binding) == false))
        {
            return;
        }
        System.ServiceModel.Security.ChannelProtectionRequirements cpr = new System.ServiceModel.Security.ChannelProtectionRequirements();
        ApplyProtection("http://opennetcf.wcf.sample/ICalculator/Add", cpr.IncomingSignatureParts, true);
        ApplyProtection("http://opennetcf.wcf.sample/ICalculator/Add", cpr.IncomingEncryptionParts, true);
        if ((binding.MessageVersion.Addressing == System.ServiceModel.Channels.AddressingVersion.None))
        {
            ApplyProtection("*", cpr.OutgoingSignatureParts, true);
            ApplyProtection("*", cpr.OutgoingEncryptionParts, true);
        }
        else
        {
            ApplyProtection("http://opennetcf.wcf.sample/ICalculator/AddResponse", cpr.OutgoingSignatureParts, true);
            ApplyProtection("http://opennetcf.wcf.sample/ICalculator/AddResponse", cpr.OutgoingEncryptionParts, true);
        }
        this.Parameters.Add(cpr);
    }
}