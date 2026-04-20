Imports MySql.Data.MySqlClient

Module Module1

    Public tax_rate As Double = 0.25
    Public myadocon, conn As New MySqlConnection
    Public cmd As New MySqlCommand
    Public cmdread As MySqlDataReader
    Public db_server = "localhost"   ' server name
    Public db_uid = "root"           ' user id
    Public db_pwd = ""               'password
    Public db_name = "mings_craft"   'database name
    Public strconn As String = "server =" & db_server & ";uid = " & db_uid & ";password = " & db_pwd & "; database =" & db_name & ""
    Public choice As Integer
    Public tag1 As Integer
    Public operation As Integer   '1 Adding, 2 Searching, and 3 Updating
    Public Sub readquery(ByVal sql As String)
        Try
            With conn
                If .State = ConnectionState.Open Then .Close()
                .ConnectionString = strconn
                .Open()
            End With
            With cmd
                .Connection = conn
                .CommandText = sql
                cmdread = .ExecuteReader
            End With
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical)
        End Try
    End Sub
End Module