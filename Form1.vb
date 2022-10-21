Imports System.Data.SQLite
Public Class Form1
    Dim lv As ListViewItem
    Dim selected As String


    '---------------------sub
    Public Sub poplistview()
        ListView1.Clear()

        With ListView1
            .View = View.Details
            .GridLines = True
            .Columns.Add("ID", 40)
            .Columns.Add("Pet Name", 100)
            .Columns.Add("Animal", 100)
            .Columns.Add("Breed", 100)
            .Columns.Add("Gender", 100)
            .Columns.Add("Color", 100)
        End With
        Try
            openCon()
            sql = "select * from tblpets"
            cmd = New SQLite.SQLiteCommand(sql, cn)
            dr = cmd.ExecuteReader

            Do While dr.Read() = True
                lv = New ListViewItem(dr("id").ToString)
                lv.SubItems.Add(dr("name"))
                lv.SubItems.Add(dr("animal"))
                lv.SubItems.Add(dr("breed"))
                lv.SubItems.Add(dr("gender"))
                lv.SubItems.Add(dr("color"))
                ListView1.Items.Add(lv)
            Loop
        Catch ex As Exception
            MsgBox(ex.Message, "Show List!", vbOK + vbExclamation)
        End Try
    End Sub

    Public Sub clearall()
        txtid.Clear()
        txtpetname.Clear()
        txtanimal.Clear()
        txtbreed.Clear()
        cbgender.ResetText()
        txtcolor.Clear()

    End Sub
    Public Sub disabletxt()
        txtid.Enabled = False
        txtpetname.Enabled = False
        txtanimal.Enabled = False
        txtbreed.Enabled = False
        cbgender.Enabled = False
        txtcolor.Enabled = False
    End Sub
    Public Sub enabletxt()
        txtid.Enabled = True
        txtpetname.Enabled = True
        txtanimal.Enabled = True
        txtbreed.Enabled = True
        cbgender.Enabled = True
        txtcolor.Enabled = True
    End Sub




    ' --------------------- forms
    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        poplistview()

    End Sub



    '--------------------------list view
    Private Sub ListView1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ListView1.SelectedIndexChanged
        Dim i As Integer
        For i = 0 To ListView1.SelectedItems.Count - 1
            selected = ListView1.SelectedItems.Item(i).Text
        Next
        Try
            openCon()
            sql = "select * from tblpets where id = '" & selected & "'"
            cmd = New SQLiteCommand(sql, cn)
            dr = cmd.ExecuteReader

            While dr.Read() = True
                txtid.Text = dr("id").ToString
                txtpetname.Text = dr("name")
                txtanimal.Text = dr("animal")
                txtbreed.Text = dr("breed")
                cbgender.Text = dr("gender")
                txtcolor.Text = dr("color")
            End While
        Catch ex As Exception
            MsgBox(ex.Message, "Selected items!", vbOK + vbExclamation)
        End Try


        btnadd.Enabled = False
        If btnadd.Enabled = True Then
            btnupdate.Enabled = False
            btndelete.Enabled = False
        Else
            btnupdate.Enabled = True
            btndelete.Enabled = True
        End If

    End Sub
    '---------------------buttons

    Private Sub btnadd_Click(sender As Object, e As EventArgs) Handles btnadd.Click
        enabletxt()
        btnadd.Enabled = False
        btnSave.Visible = True
        btnclose.Enabled = False
        btnCancel.Visible = True
    End Sub
    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        Try
            openCon()
            sql = "insert into tblpets(id,name,animal,breed,gender,color) values('" & txtid.Text & "','" & txtpetname.Text & "','" & txtanimal.Text & "','" & txtbreed.Text & "','" & cbgender.Text & "','" & txtcolor.Text & "')"
            cmd = New SQLiteCommand(sql, cn)
            cmd.ExecuteNonQuery()
            cn.Close()
            poplistview()
            clearall()

        Catch ex As Exception
            MsgBox(ex.Message, vbOK + vbExclamation)
        End Try
        disabletxt()
        btnadd.Enabled = True
        btnSave.Visible = False
        btnCancel.Visible = False
        btnclose.Enabled = True
    End Sub

    Private Sub btnupdate_Click(sender As Object, e As EventArgs) Handles btnupdate.Click
        enabletxt()
        txtid.Enabled = False
        btnSave2.Visible = True
        btnCancel.Visible = True
        btndelete.Enabled = False
    End Sub

    Private Sub btnSave2_Click(sender As Object, e As EventArgs) Handles btnSave2.Click
        Try
            openCon()
            sql = "update tblpets set name = '" & txtpetname.Text & "', animal = '" & txtanimal.Text & "', breed = '" & txtbreed.Text & "', gender = '" & cbgender.Text & "', color = '" & txtcolor.Text & "' where id = '" & selected & "' "
            cmd = New SQLiteCommand(sql, cn)
            cmd.ExecuteNonQuery()
        Catch ex As Exception
            MsgBox(ex.Message, "Update Items!", vbOK + vbExclamation)
        End Try
        poplistview()
        clearall()
        disabletxt()
        btnCancel.Visible = False
        btnSave2.Visible = False
        btnupdate.Enabled = False
        If btnupdate.Enabled = True Then
            btnadd.Enabled = False
        Else
            btnadd.Enabled = True
            btndelete.Enabled = False
        End If
    End Sub

    Private Sub btndelete_Click(sender As Object, e As EventArgs) Handles btndelete.Click
        openCon()
        sql = "delete from tblpets where id = '" & selected & "'"
        cmd = New SQLite.SQLiteCommand(sql, cn)
        cmd.ExecuteNonQuery()
        poplistview()
        clearall()

        btndelete.Enabled = False
        If btndelete.Enabled = True Then
            btnadd.Enabled = False
        Else
            btnadd.Enabled = True
            btnupdate.Enabled = False
        End If
    End Sub

    Private Sub btnclose_Click(sender As Object, e As EventArgs) Handles btnclose.Click
        Application.Exit()

    End Sub

    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        If btnSave.Visible = True Then
            disabletxt()
            btnSave.Visible = False
            btnadd.Enabled = True
            btnCancel.Visible = False
            btnclose.Enabled = True
        End If
        If btnSave2.Visible = True Then
            disabletxt()
            clearall()
            btnSave2.Visible = False
            btnupdate.Enabled = False
            btndelete.Enabled = False
            btnadd.Enabled = True
            btnCancel.Visible = False

        End If
    End Sub
End Class
