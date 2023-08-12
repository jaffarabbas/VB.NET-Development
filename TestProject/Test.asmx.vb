Imports System.ComponentModel
Imports System.Data.SqlClient
Imports System.Web.Script.Serialization
Imports System.Web.Script.Services
Imports System.Web.Services
Imports System.Web.Services.Protocols

' To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line.
' <System.Web.Script.Services.ScriptService()> _
<System.Web.Services.WebService(Namespace:="http://tempuri.org/")>
<System.Web.Services.WebServiceBinding(ConformsTo:=WsiProfiles.BasicProfile1_1)>
<ToolboxItem(False)>
Public Class Test
    Inherits System.Web.Services.WebService

    Public Function GetConnection() As SqlConnection
        Dim connectionString As String = "Data Source=DESKTOP-AC9GNMR;Initial Catalog=dbMvc;Integrated Security=True;"
        Return New SqlConnection(connectionString)
    End Function

    <WebMethod()>
    Public Function GetMyData() As String
        Using connection As SqlConnection = GetConnection()
            connection.Open()
            Dim query As String = "SELECT * FROM customer"
            Dim adapter As New SqlDataAdapter(query, connection)
            Dim dataTable As New DataTable()
            adapter.Fill(dataTable)
            Dim jsonData As String = DataTableToJson(dataTable)
            Return jsonData
        End Using
    End Function

    Private Function DataTableToJson(dataTable As DataTable) As String
        Dim serializer As New System.Web.Script.Serialization.JavaScriptSerializer()
        Dim rows As New List(Of Dictionary(Of String, Object))()

        For Each row As DataRow In dataTable.Rows
            Dim dictRow As New Dictionary(Of String, Object)()
            For Each col As DataColumn In dataTable.Columns
                dictRow(col.ColumnName) = row(col)
            Next
            rows.Add(dictRow)
        Next

        Return serializer.Serialize(rows)
    End Function

    <WebMethod()>
    Public Function InsertDataAndReturnAsJson(ByVal name As String, ByVal email As String, ByVal password As String, ByVal address As String, ByVal country As Int32) As String
        Dim insertedData As New Dictionary(Of String, Object)()
        Using connection As SqlConnection = GetConnection()
            connection.Open()
            Dim query As String = "INSERT INTO customer (name, email, password, address, cid) VALUES (@Value1, @Value2, @Value3, @Value4, @Value5); SELECT SCOPE_IDENTITY();"
            Using command As New SqlCommand(query, connection)
                command.Parameters.AddWithValue("@Value1", name)
                command.Parameters.AddWithValue("@Value2", email)
                command.Parameters.AddWithValue("@Value3", password)
                command.Parameters.AddWithValue("@Value4", address)
                command.Parameters.AddWithValue("@Value5", country)

                Dim insertedId As Object = command.ExecuteScalar()
                If insertedId IsNot DBNull.Value Then
                    insertedData("id") = Convert.ToInt32(insertedId)
                    insertedData("name") = name
                    insertedData("email") = email
                    insertedData("address") = address
                    insertedData("country") = country
                End If
            End Using
        End Using

        Return ConvertDataToJson(insertedData)
    End Function

    Private Function ConvertDataToJson(data As Object) As String
        Dim serializer As New JavaScriptSerializer()
        Return serializer.Serialize(data)
    End Function


End Class