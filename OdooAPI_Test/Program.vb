Imports System.Text.Json
Imports CookComputing.XmlRpc

Module Program
    Sub Main(args As String())

        ' OdooConnection.Authenticate()
        ' Module1.Test()
        Dim product As Product = New Product()
        product.Name = "Laptop"
        product.Price = 5.0
        product.Id = 1

        Console.WriteLine(product)
        Console.WriteLine(vbCrLf)
        Dim tmp As XDocument = DBXmlMethods.GetXml(product)
        Console.WriteLine(tmp)

        Dim stringJson As String = JsonSerializer.Serialize(product)
        Console.WriteLine(stringJson)


        ''' Prueba de XMlRpcStruct
        ''' Aqui vamos
        Dim uid As Integer = Authenticate(url, db, username, password)
        Console.WriteLine($"User id Odoo: {uid}")
        If uid > 0 Then

            Dim partners = GetRecords_SearchRead(url, db, uid, password, "res.partner")
            Console.WriteLine(vbCrLf)
            Console.WriteLine(partners.GetType()) ' arreglo de objetos
            Console.WriteLine(partners.First.GetType()) ' System.Object[]
            ' Console.WriteLine(DBXmlMethods.GetXml(partners.First)) ' no se puede serializar a xml - CookComputing.XmlRpc.XmlRpcStruct
            Dim firstObject = (CType(partners.First, XmlRpcStruct)) ' conversion explicita para poder usar los metodos de XmlStruct

            Console.WriteLine("Llaves y propiedades")
            Console.WriteLine($"Array de claves: {firstObject.Keys}")
            Console.WriteLine($"Cantidad de claves: {firstObject.Keys.Count}")

            For Each key In firstObject.Keys
                Console.WriteLine(key.ToString())
                Console.WriteLine($"{firstObject.Item(key)}")
            Next

            ' Serializacion del array de objetos a Json
            Dim stringJsonArray As String = JsonSerializer.Serialize(partners, New JsonSerializerOptions With {.WriteIndented = True})
            Console.WriteLine(stringJsonArray)

            ' Serealizacion a string Json del objeto XmlRpcStruct
            Dim stringJsonXmlStruct As String = JsonSerializer.Serialize(firstObject)
            Console.WriteLine(stringJsonXmlStruct)

            ''' GUARDAR PRIMER OBJETO EN BASE
            DBConnection.GuardarDatosPartner(stringJsonXmlStruct)


        Else
            Console.WriteLine("Error de autenticación")
        End If

        Console.ReadKey()
    End Sub

End Module
