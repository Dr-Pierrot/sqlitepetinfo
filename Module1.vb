Imports System.Data.SQLite

Module Module1
    Public cn As New SQLiteConnection
    Public cmd As New SQLiteCommand
    Public dr As SQLiteDataReader
    Public sql As String

    Public Sub openCon()
        Try
            cn = New SQLiteConnection

            If cn.State = ConnectionState.Open Then
                cn.Close()
            End If

            cn.ConnectionString = "Data Source=" & Application.StartupPath & "\petinfo.db3; Version=3"
            cn.Open()

        Catch ex As Exception
            MsgBox(ex.Message, "Database connection error!", vbOK + vbExclamation)
        End Try
    End Sub
End Module
