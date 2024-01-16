Imports System.Diagnostics

Public Class FileHolder
    Private Sub FileHolder_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Styles.UserControl(Me, 286, 42)
    End Sub

    Sub New(fileName As String, filepath As String)
        InitializeComponent()
        CreateFileHolder(fileName, filepath)
    End Sub

    Private Sub CreateFileHolder(fileName As String, filepath As String)
        Dim fileNameLbl As Label = Tools.CreateUserLabel(Me, 236, 42, 50, 10)
        fileNameLbl.ForeColor = Color.White
        fileNameLbl.Text = fileName
        fileNameLbl.TextAlign = ContentAlignment.MiddleCenter
        fileNameLbl.Font = New Font("Poppins", 8, FontStyle.Bold)

        AddHandler fileNameLbl.Click, Sub(sender As Object, e As EventArgs)
                                          ' test only
                                          'Dim path = "C:\Users\acer\Documents\CP REVIEWER - ARRAY AND STRINGS MANIPULATIONS.pdf"
                                          'Process.Start(path)
                                          MsgBox(filepath)
                                      End Sub
    End Sub
End Class
