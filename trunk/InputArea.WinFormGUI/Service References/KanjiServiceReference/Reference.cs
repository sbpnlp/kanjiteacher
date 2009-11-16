﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:2.0.50727.3603
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Kanji.InputArea.WinFormGUI.KanjiServiceReference {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "3.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(Namespace="http://www.japanese-coach.com", ConfigurationName="KanjiServiceReference.IKanjiService")]
    public interface IKanjiService {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://www.japanese-coach.com/IKanjiService/ReceivePoints", ReplyAction="http://www.japanese-coach.com/IKanjiService/ReceivePointsResponse")]
        bool ReceivePoints(int[] xcoords, int[] ycoords, System.DateTime[] times);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://www.japanese-coach.com/IKanjiService/GetPointsX", ReplyAction="http://www.japanese-coach.com/IKanjiService/GetPointsXResponse")]
        int[] GetPointsX();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://www.japanese-coach.com/IKanjiService/GetPointsY", ReplyAction="http://www.japanese-coach.com/IKanjiService/GetPointsYResponse")]
        int[] GetPointsY();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://www.japanese-coach.com/IKanjiService/GetPointsTime", ReplyAction="http://www.japanese-coach.com/IKanjiService/GetPointsTimeResponse")]
        System.DateTime[] GetPointsTime();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://www.japanese-coach.com/IKanjiService/MessageForDesktop", ReplyAction="http://www.japanese-coach.com/IKanjiService/MessageForDesktopResponse")]
        bool MessageForDesktop(int messages);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://www.japanese-coach.com/IKanjiService/MessageForInputArea", ReplyAction="http://www.japanese-coach.com/IKanjiService/MessageForInputAreaResponse")]
        bool MessageForInputArea(int messages);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://www.japanese-coach.com/IKanjiService/GetNewMessagesForMobile", ReplyAction="http://www.japanese-coach.com/IKanjiService/GetNewMessagesForMobileResponse")]
        int GetNewMessagesForMobile();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://www.japanese-coach.com/IKanjiService/GetNewMessagesForDesktop", ReplyAction="http://www.japanese-coach.com/IKanjiService/GetNewMessagesForDesktopResponse")]
        int GetNewMessagesForDesktop();
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "3.0.0.0")]
    public interface IKanjiServiceChannel : Kanji.InputArea.WinFormGUI.KanjiServiceReference.IKanjiService, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "3.0.0.0")]
    public partial class KanjiServiceClient : System.ServiceModel.ClientBase<Kanji.InputArea.WinFormGUI.KanjiServiceReference.IKanjiService>, Kanji.InputArea.WinFormGUI.KanjiServiceReference.IKanjiService {
        
        public KanjiServiceClient() {
        }
        
        public KanjiServiceClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public KanjiServiceClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public KanjiServiceClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public KanjiServiceClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public bool ReceivePoints(int[] xcoords, int[] ycoords, System.DateTime[] times) {
            return base.Channel.ReceivePoints(xcoords, ycoords, times);
        }
        
        public int[] GetPointsX() {
            return base.Channel.GetPointsX();
        }
        
        public int[] GetPointsY() {
            return base.Channel.GetPointsY();
        }
        
        public System.DateTime[] GetPointsTime() {
            return base.Channel.GetPointsTime();
        }
        
        public bool MessageForDesktop(int messages) {
            return base.Channel.MessageForDesktop(messages);
        }
        
        public bool MessageForInputArea(int messages) {
            return base.Channel.MessageForInputArea(messages);
        }
        
        public int GetNewMessagesForMobile() {
            return base.Channel.GetNewMessagesForMobile();
        }
        
        public int GetNewMessagesForDesktop() {
            return base.Channel.GetNewMessagesForDesktop();
        }
    }
}
