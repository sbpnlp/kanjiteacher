﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:2.0.50727.3603
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------



[System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "3.0.0.0")]
public interface IKanjiService
{
    
    bool ReceivePoints(int[] xcoords, int[] ycoords, System.DateTime[] times);
}

[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "3.0.0.0")]
[System.Xml.Serialization.XmlRootAttribute(ElementName="ReceivePoints", Namespace="http://www.japanese-coach.com")]
public partial class ReceivePointsRequest
{
    
    [System.Xml.Serialization.XmlArrayAttribute(IsNullable=true, Namespace="http://www.japanese-coach.com", Order=0)]
    [System.Xml.Serialization.XmlArrayItemAttribute(Namespace="http://schemas.microsoft.com/2003/10/Serialization/Arrays", IsNullable=false)]
    public int[] xcoords;
    
    [System.Xml.Serialization.XmlArrayAttribute(IsNullable=true, Namespace="http://www.japanese-coach.com", Order=1)]
    [System.Xml.Serialization.XmlArrayItemAttribute(Namespace="http://schemas.microsoft.com/2003/10/Serialization/Arrays", IsNullable=false)]
    public int[] ycoords;
    
    [System.Xml.Serialization.XmlArrayAttribute(IsNullable=true, Namespace="http://www.japanese-coach.com", Order=2)]
    [System.Xml.Serialization.XmlArrayItemAttribute(Namespace="http://schemas.microsoft.com/2003/10/Serialization/Arrays", IsNullable=false)]
    public System.DateTime[] times;
    
    public ReceivePointsRequest()
    {
    }
    
    public ReceivePointsRequest(int[] xcoords, int[] ycoords, System.DateTime[] times)
    {
        this.xcoords = xcoords;
        this.ycoords = ycoords;
        this.times = times;
    }
}

[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "3.0.0.0")]
[System.Xml.Serialization.XmlRootAttribute(ElementName="ReceivePointsResponse", Namespace="http://www.japanese-coach.com")]
public partial class ReceivePointsResponse
{
    
    [System.Xml.Serialization.XmlElementAttribute(Namespace="http://www.japanese-coach.com", Order=0)]
    public bool ReceivePointsResult;
    
    public ReceivePointsResponse()
    {
    }
    
    public ReceivePointsResponse(bool ReceivePointsResult)
    {
        this.ReceivePointsResult = ReceivePointsResult;
    }
}

[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "3.0.0.0")]
public partial class KanjiServiceClient : Microsoft.Tools.ServiceModel.CFClientBase<IKanjiService>, IKanjiService
{
    
    public static System.ServiceModel.EndpointAddress EndpointAddress = new System.ServiceModel.EndpointAddress("http://localhost:8000/kanji/KanjiService");
    
    public KanjiServiceClient() : 
            this(CreateDefaultBinding(), EndpointAddress)
    {
    }
    
    public KanjiServiceClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
            base(binding, remoteAddress)
    {
        addProtectionRequirements(binding);
    }
    
    private ReceivePointsResponse ReceivePoints(ReceivePointsRequest request)
    {
        CFInvokeInfo info = new CFInvokeInfo();
        info.Action = "http://www.japanese-coach.com/IKanjiService/ReceivePoints";
        info.RequestIsWrapped = true;
        info.ReplyAction = "http://www.japanese-coach.com/IKanjiService/ReceivePointsResponse";
        info.ResponseIsWrapped = true;
        ReceivePointsResponse retVal = base.Invoke<ReceivePointsRequest, ReceivePointsResponse>(info, request);
        return retVal;
    }
    
    public bool ReceivePoints(int[] xcoords, int[] ycoords, System.DateTime[] times)
    {
        ReceivePointsRequest request = new ReceivePointsRequest(xcoords, ycoords, times);
        ReceivePointsResponse response = this.ReceivePoints(request);
        return response.ReceivePointsResult;
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
        ApplyProtection("http://www.japanese-coach.com/IKanjiService/ReceivePoints", cpr.IncomingSignatureParts, true);
        ApplyProtection("http://www.japanese-coach.com/IKanjiService/ReceivePoints", cpr.IncomingEncryptionParts, true);
        if ((binding.MessageVersion.Addressing == System.ServiceModel.Channels.AddressingVersion.None))
        {
            ApplyProtection("*", cpr.OutgoingSignatureParts, true);
            ApplyProtection("*", cpr.OutgoingEncryptionParts, true);
        }
        else
        {
            ApplyProtection("http://www.japanese-coach.com/IKanjiService/ReceivePointsResponse", cpr.OutgoingSignatureParts, true);
            ApplyProtection("http://www.japanese-coach.com/IKanjiService/ReceivePointsResponse", cpr.OutgoingEncryptionParts, true);
        }
        this.Parameters.Add(cpr);
    }
}