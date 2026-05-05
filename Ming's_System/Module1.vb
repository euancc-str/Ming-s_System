Imports System.IO
Imports MySql.Data.MySqlClient

Module Module1

    Public tax_rate As Double = 0.25
    Public myadocon, conn As New MySqlConnection
    Public cmd As New MySqlCommand
    Public cmdread As MySqlDataReader
    Public db_server As String = ""
    Public db_uid As String = ""
    Public db_pwd As String = ""
    Public db_name As String = ""
    Public strconn As String = ""
    Public choice As Integer
    Public tag1 As Integer
    Public operation As Integer   '1 Adding, 2 Searching, and 3 Updating
    Public Sub LoadDBConfig()

        Dim configPath As String = Application.StartupPath & "\db_config.txt"


        If File.Exists(configPath) Then
            Dim lines() As String = File.ReadAllLines(configPath)
            If lines.Length >= 4 Then
                db_server = lines(0).Trim()
                db_uid = lines(1).Trim()
                db_pwd = lines(2).Trim()
                db_name = lines(3).Trim()
            End If
        Else

            db_server = "localhost"
            db_uid = "root"
            db_pwd = ""
            db_name = "mings_craft"


            Dim defaultData As String = "localhost" & vbCrLf & "root" & vbCrLf & "" & vbCrLf & "mings_craft"
            File.WriteAllText(configPath, defaultData)
        End If


        strconn = "server=" & db_server & ";uid=" & db_uid & ";password=" & db_pwd & ";database=" & db_name
    End Sub
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