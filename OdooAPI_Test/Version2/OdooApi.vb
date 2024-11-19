Imports System.Net
Imports System.Xml
Imports System.Xml.Linq
Imports CookComputing.XmlRpc

Module Module1

    Public ReadOnly url As String = "http://192.168.3.97:8069/xmlrpc/2/"
    Public ReadOnly db As String = "odoo"
    Public ReadOnly username As String = "admin"
    Public ReadOnly password As String = "admin"

    Sub Test()

        ' Autenticación
        Dim uid As Integer = Authenticate(url, db, username, password)
        Console.WriteLine($"{uid}")
        If uid > 0 Then
            ' Obtener registros del modelo 'res.partner'
            Dim partners_uid = GetRecords_Search(url, db, uid, password, "res.partner")
            Console.WriteLine("\n")
            'Console.WriteLine(partners_uid)
            For Each record In partners_uid
                Console.WriteLine(record.ToString())
            Next


            Dim partners = GetRecords_SearchRead(url, db, uid, password, "res.partner")
            Console.WriteLine("n")
            Console.WriteLine(partners)
            For Each record In partners
                'Console.WriteLine(record.ToString())
                Dim structura As XmlRpcStruct = CType(record, XmlRpcStruct)
                Dim unused As String = structura.Item("name")
                Console.WriteLine(unused)
            Next
        Else
            Console.WriteLine("Error de autenticación")
        End If
    End Sub

    Function Authenticate(url As String, db As String, username As String, password As String) As Integer
        Try
            ' Crea un proxy que actúa como cliente para los métodos definidos en IOdooCommon
            Dim client As IOdooCommon = XmlRpcProxyGen.Create(Of IOdooCommon)()
            client.Url = $"{url}common"
            Console.WriteLine($"Odoo version: {client.GetVersion("server_version")}")
            Dim result As Integer = client.authenticate(db, username, password)
            Return result
        Catch ex As Exception
            Console.WriteLine($"{ex.Message} {ex.StackTrace}")
            Return 0
        End Try

    End Function

    Function GetRecords_SearchRead(url As String, db As String, uid As Integer, password As String, model As String) As Object()
        Try
            ' Crea un proxy que actúa como cliente para los métodos definidos en IOdooObject
            Dim client As IOdooObject = XmlRpcProxyGen.Create(Of IOdooObject)()
            client.Url = $"{url}object"
            Dim fields As Object() = {"name", "country_id"}

            Dim options As XmlRpcStruct = New XmlRpcStruct From {
                {"fields", fields}
            }
            Dim result = client.execute_kw(db, uid, password, model, "search_read",
                New Object() {New Object() {}}, options)
            'Console.WriteLine(result)
            Return CType(result, Object())
        Catch ex As Exception
            Console.WriteLine($"{ex.Message} {ex.StackTrace}")
            Return Nothing
        End Try

    End Function

    Function GetRecords_Search(url As String, db As String, uid As Integer, password As String, model As String) As Integer()
        Try
            Dim client As IOdooObject = XmlRpcProxyGen.Create(Of IOdooObject)()
            client.Url = $"{url}object"

            Dim result = client.execute_kw(db, uid, password, model, "search",
                New Object() {New Object() {}},
                New XmlRpcStruct()
                )
            'Console.WriteLine(result)
            Return CType(result, Integer())
        Catch ex As Exception
            Console.WriteLine($"{ex.Message} {ex.StackTrace}")
            Return Nothing
        End Try

    End Function

End Module
