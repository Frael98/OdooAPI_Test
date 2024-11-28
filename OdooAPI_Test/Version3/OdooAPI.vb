Imports Newtonsoft.Json

Module OdooAPI
    Sub Test()
        Dim url As String = "http://192.168.3.55:8069"
        Dim db As String = "odoo"
        Dim username As String = "admin"
        Dim password As String = "admin"

        Try
            Dim uidTask As Task(Of Integer) = InterfaceOdoo.Authenticate(url, db, username, password)
            uidTask.Wait()
            Dim uid As Integer = uidTask.Result
            Console.WriteLine($"Autenticado con éxito, UID: {uid}")

            Dim args As Object = New Object() {
                New Object() {New Object() {"is_company", "=", False}}, ' Dominio/condiciones
                New String() {"name", "email"} ' fields/campos a mostrar
            }

            Dim resultTask As Task(Of Object) = InterfaceOdoo.Execute_Kw(url, db, uid, password, "res.partner", "search_read", args)
            resultTask.Wait()
            Dim partners = resultTask.Result

            Console.WriteLine("Registros encontrados:")
            Console.WriteLine(JsonConvert.SerializeObject(partners, Formatting.Indented))

            Dim new_partner As Object = New Object() {
                New With {.name = "Frael"}
            }

            '' Dim newPartnerJson = JsonConvert.SerializeObject(new_partner)

            'Dim newPartner As Task(Of Object) = InterfaceOdoo.Execute_Kw(url, db, uid, password, "res.partner", "create", new_partner)
            'resultTask.Wait()

            'Dim rt = newPartner.Result

            'Console.WriteLine("Id del registro:")
            'Console.WriteLine(JsonConvert.SerializeObject(rt, Formatting.Indented))

            Dim delete_partner_id As Object = New Object() {
                New Integer() {60}
            }

            Dim deletedPartner As Task(Of Object) = InterfaceOdoo.Execute_Kw(url, db, uid, password, "res.partner", "unlink", delete_partner_id)
            resultTask.Wait()

            Dim rt2 = deletedPartner.Result

            Console.WriteLine("Id del registro:")
            Console.WriteLine(JsonConvert.SerializeObject(rt2, Formatting.Indented))
        Catch ex As Exception
            Console.WriteLine($"Error: {ex.Message}")
        End Try
    End Sub
End Module
