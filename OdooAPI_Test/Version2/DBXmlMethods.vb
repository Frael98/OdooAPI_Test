Imports System.Xml
Imports System.Xml.Serialization

Public Class DBXmlMethods
    ''' <summary>
    ''' Transforma un objeto en formato XML
    ''' </summary>
    ''' <typeparam name="T">Tipo General</typeparam>
    ''' <param name="objeto">Objeto o criterio</param>
    ''' <returns></returns>
    Shared Function GetXml(Of T)(ByVal objeto As T) As XDocument
        Dim resultado As XDocument = New XDocument(New XDeclaration("1.0", "utf-8", "true"))
        Try
            Dim xs As XmlSerializer = New XmlSerializer(objeto.GetType())
            Using writer As XmlWriter = resultado.CreateWriter()
                xs.Serialize(writer, objeto)
            End Using
            Return resultado
        Catch ex As Exception
            Console.WriteLine(ex.Message & ex.StackTrace)
            Return Nothing
        End Try
    End Function
End Class
