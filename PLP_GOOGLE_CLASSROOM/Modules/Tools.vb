Imports System.ComponentModel

Module Tools
    Public Function CreateButton(form As Control, width As Integer, height As Integer, x As Integer, y As Integer) As Label
        Dim button As New Label
        Styles.Button(button, width, height, x, y)
        form.Controls.Add(button)
        Return button
    End Function
    Public Function CloseButton(form As Form, width As Integer, height As Integer, x As Integer, y As Integer) As Label
        Dim button As New Label
        Styles.Button(button, width, height, x, y)
        AddHandler button.Click, Sub(sender As Object, e As EventArgs)
                                     Handlers.CloseWindow(form)
                                 End Sub
        form.Controls.Add(button)
        Return button
    End Function
    Public Function MinimizedButton(form As Form, width As Integer, height As Integer, x As Integer, y As Integer) As Label
        Dim button As New Label
        Styles.Button(button, width, height, x, y)
        AddHandler button.Click, Sub(sender As Object, e As EventArgs)
                                     Handlers.MinimizedWindow(form)
                                 End Sub
        form.Controls.Add(button)
        Return button
    End Function
    Public Function BackButton(form As Form, openForm As Form, width As Integer, height As Integer, x As Integer, y As Integer) As Label
        Dim button As New Label
        Styles.Button(button, width, height, x, y)
        AddHandler button.Click, Sub(sender As Object, e As EventArgs)
                                     Handlers.OpenWindow(form, openForm)
                                 End Sub
        form.Controls.Add(button)
        Return button
    End Function
    Public Function CreateTextBox(form As Control, placeholder As String, width As Integer, height As Integer, x As Integer, y As Integer) As TextBox
        Dim input As New TextBox
        Styles.TextBox(input, placeholder, width, height, x, y)
        form.Controls.Add(input)
        Return input
    End Function
    Public Function CreateErrorHandlers(form As UserControl, width As Integer, height As Integer, x As Integer, y As Integer) As Label
        Dim label As New Label
        Styles.Label(label, width, height, x, y)
        form.Controls.Add(label)
        Return label
    End Function
    Public Function CreatePanel(form As Control, width As Integer, height As Integer, x As Integer, y As Integer) As Panel
        Dim panel As New Panel
        Styles.Panel(panel, width, height, x, y)
        form.Controls.Add(panel)
        Return panel
    End Function
    Public Function CreateUserLabel(form As Control, width As Integer, height As Integer, x As Integer, y As Integer)
        Dim label As New Label
        Styles.UserLabels(label, width, height, x, y)
        form.Controls.Add(label)
        Return label
    End Function
    Public Function CreateMenuButton(form As UserControl, width As Integer, height As Integer, x As Integer, y As Integer)
        Dim label As New Label
        Styles.Label(label, width, height, x, y)
        form.Controls.Add(label)
        Return label
    End Function
End Module
