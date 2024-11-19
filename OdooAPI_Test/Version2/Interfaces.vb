Imports CookComputing.XmlRpc

Public Interface IOdooCommon
    Inherits IXmlRpcProxy
    <XmlRpcMethod("authenticate")>
    Function authenticate(ByVal db As String, ByVal username As String, ByVal password As String, Optional ByVal evento As Object = "") As Integer

    <XmlRpcMethod("version")>
    Function GetVersion() As Object
End Interface

Public Interface IOdooObject
    Inherits IXmlRpcProxy
    <XmlRpcMethod("execute_kw")>
    Function execute_kw(ByVal db As String, ByVal uid As Integer, ByVal password As String,
                        ByVal model As String, ByVal method As String,
                        ByVal parameters As Object(), ByVal kwargs As XmlRpcStruct
                        ) As Object
End Interface

