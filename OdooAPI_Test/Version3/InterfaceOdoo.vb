Imports System.Net.Http
Imports System.Text
Imports Newtonsoft.Json

Module InterfaceOdoo
    Public Async Function Authenticate(ByVal url As String, ByVal db As String, ByVal username As String, ByVal password As String) As Task(Of Integer)
        Dim method As String = "authenticate"
        Dim service As String = "common"
        Dim params As Object = New Object() {db, username, password, New Object() {}}

        Dim resultJson As String = Await Request_JsonRCP(url, service, method, params)
        Dim resultObj = JsonConvert.DeserializeObject(Of Dictionary(Of String, Object))(resultJson)

        If resultObj.ContainsKey("result") AndAlso resultObj("result") IsNot Nothing Then
            Return Convert.ToInt32(resultObj("result"))
        Else
            Throw New Exception("Error de autenticación: " & resultObj("error").ToString())
        End If
    End Function

    Public Async Function Execute_Kw(ByVal url As String, ByVal db As String, ByVal uid As Integer, ByVal password As String, ByVal modelo As String, ByVal metodo As String, ByVal args As Object) As Task(Of Object)
        Dim method As String = "execute_kw"
        Dim service As String = "object"

        Dim params As Object = New Object() {db, uid, password, modelo, metodo, args}

        Dim resultJson As String = Await Request_JsonRCP(url, service, method, params)
        Console.WriteLine(JsonConvert.SerializeObject(params, Formatting.Indented))

        Dim resultObj = JsonConvert.DeserializeObject(Of Dictionary(Of String, Object))(resultJson)

        If resultObj.ContainsKey("result") Then
            Return resultObj("result")
        Else
            Throw New Exception("Error en la llamada al método: " & resultObj("error").ToString())
        End If
    End Function

    Public Async Function Request_JsonRCP(ByVal url As String, ByVal servicio As String, ByVal metodo As String, ByVal args As Object) As Task(Of String)
        Dim endpoint = $"{url}/jsonrpc"
        Dim requestObj As New With {
        .jsonrpc = "2.0",
        .method = "call",
        .params = New With {
            .service = servicio,
            .method = metodo,
            .args = args
        },
        .id = 1
    }

        Dim requestJson As String = JsonConvert.SerializeObject(requestObj)

        Using client As New HttpClient()
            Dim content As New StringContent(requestJson, Encoding.UTF8, "application/json")
            Dim response As HttpResponseMessage = Await client.PostAsync(endpoint, content)

            If response.IsSuccessStatusCode Then
                Return Await response.Content.ReadAsStringAsync()
            Else
                Throw New Exception($"Error en la solicitud: {response.StatusCode}")
            End If
        End Using
    End Function

End Module
