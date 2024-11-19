Imports CookComputing.XmlRpc

''' <XmlRpcMissingMapping(MappingAction.Ignore)>
Public Interface IOdooApi

    ''' <summary>
    ''' Realiza la conexion con la API
    ''' </summary>
    ''' <param name="db">Nombre de la base</param>
    ''' <param name="username">Usuario de Odoo</param>
    ''' <param name="password">Contraseña del usuario</param>
    ''' <returns></returns>
    <XmlRpcMethod("common.login")>
    Function LogIn(ByVal db As String, ByVal username As String, ByVal password As String) As Integer



    ''' <summary>
    ''' Ejecucion de los metodos
    ''' </summary>
    ''' <param name="db"></param>
    ''' <param name="uid"></param>
    ''' <param name="password"></param>
    ''' <param name="model"></param>
    ''' <param name="method"></param>
    ''' <param name="parameters"></param>
    ''' <returns></returns>
    <XmlRpcMethod("object.execute_kw")>
    Function ExecuteKw(ByVal db As String, ByVal uid As Integer, ByVal password As String, ByVal model As String, ByVal method As String,
                       ByVal Optional parameters As Object() = Nothing) As Object


End Interface
