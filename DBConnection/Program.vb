Imports System
Imports System.Data
Imports System.Data.SqlClient

Module Program
    Dim connectionString As String = "Server=DESKTOP-AC9GNMR;Database=TestDB;Integrated Security=True;"
    Sub Main(args As String())
        ' Define the connection string (update with your own server and database details)
        Dim dt2 As New DataTable()
        ' Call the method to get the data table
        Dim dt As DataTable = GetDataTable("SELECT Name,Email FROM employees")
        'dt.Columns.Add("employeeid")
        'dt.Columns.Add("cnumber")
        dt2 = GetDataTable("select * from contact")

        dt.Merge(dt2, False)
        Dim dts As New DataSet()

        dts.Tables.Add(dt)
        dts.Tables.Add(dt2)
        ' Display the results
        If dt IsNot Nothing Then
            For Each row As DataRow In dt.Rows
                Console.WriteLine("Column1: {0}, Column2: {1}", row("Name"), row("Email"))
            Next
        Else
            Console.WriteLine("No data returned.")
        End If

        ' Wait for user input to close the console window
        Console.WriteLine("Press any key to exit...")
        Console.ReadKey()
    End Sub

    Function GetDataTable(query As String) As DataTable
        ' Create a new DataTable
        Dim dataTable As New DataTable()

        ' Create a connection object
        Using connection As New SqlConnection(connectionString)
            ' Create a command object
            Using command As New SqlCommand(query, connection)
                ' Create a data adapter
                Using adapter As New SqlDataAdapter(command)
                    Try
                        ' Open the connection
                        connection.Open()

                        ' Fill the DataTable with the results of the query
                        adapter.Fill(dataTable)
                    Catch ex As Exception
                        ' Handle any errors that occur during the connection or query execution
                        Console.WriteLine("An error occurred: " & ex.Message)
                        Return Nothing
                    End Try
                End Using
            End Using
        End Using

        ' Return the populated DataTable
        Return dataTable
    End Function
End Module
