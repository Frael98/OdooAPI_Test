Imports System.Collections.ObjectModel
Imports System.Data
Imports System.Data.SqlClient

Public Class DBConnection
    Shared ReadOnly connectionString As String = "Server=FRAEL24\SQLEXPRESS;Database=DAW_LECCION2;User Id=frael4;Password=toor;"
    Shared connection As SqlConnection
    Shared Function GetConnection()
        connection = New SqlConnection(connectionString)
        connection.Open()
        Return connection
    End Function
    Shared Function GuardarDatosPartner(ByVal jsonData As String)
        Try
            ' Crear el comando para el procedimiento almacenado
            Using command As New SqlCommand("TEST_ODOOO", GetConnection())
                command.CommandType = CommandType.StoredProcedure

                ' Agregar parámetros
                command.Parameters.AddWithValue("@JSON_DATA", jsonData)

                ' Ejecutar el procedimiento
                Dim rowsAffected As Integer = command.ExecuteNonQuery()
                Console.WriteLine($"{rowsAffected} filas afectadas.")

                CloseConnection()

                Return rowsAffected
            End Using
        Catch ex As Exception
            Console.WriteLine(ex.Message)
            Return Nothing
        End Try
    End Function

    Shared Sub CloseConnection()
        connection.Close()
    End Sub

End Class
