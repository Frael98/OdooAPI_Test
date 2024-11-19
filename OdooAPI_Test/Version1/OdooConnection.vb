Imports CookComputing.XmlRpc

Public Class OdooConnection
    Implements IOdooApi

    Private Shared _odooUrl As String = "http://192.168.3.97/xmlrpc/2/"
    Private Shared _odooDatabase As String = "odoo"
    Private Shared _odooUsername As String = "admin"
    Private Shared _odooPassword As String = "admin"

    Public Function LogIn(db As String, username As String, password As String) As Integer Implements IOdooApi.LogIn
        'Return CType()
        Throw New NotImplementedException()
    End Function

    Public Function ExecuteKw(db As String, uid As Integer, password As String, model As String, method As String, Optional parameters() As Object = Nothing) As Object Implements IOdooApi.ExecuteKw
        Throw New NotImplementedException()
    End Function

    ' Private Shared commonProxy As IOdooApi = CType(XmlRpcProxyGen.Create(GetType(IOdooApi)), IOdooApi)

    ' configuramos el url a common


End Class
