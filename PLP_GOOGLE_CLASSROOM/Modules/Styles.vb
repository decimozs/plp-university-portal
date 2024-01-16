Module Styles
    Public Sub Form(component As Form)
        component.Size = New Size(1640, 1072)
        component.Location = New Point(140, 30)
        component.BackColor = Color.Magenta
        component.TransparencyKey = Color.Magenta
        component.FormBorderStyle = FormBorderStyle.None
    End Sub
    Public Sub Dashboard(component As Form)
        component.Size = New Size(1842, 1053)
        component.Location = New Point(0, 0)
        component.BackColor = Color.Magenta
        component.TransparencyKey = Color.Magenta
        component.FormBorderStyle = FormBorderStyle.None
    End Sub
    Public Sub Button(component As Label, width As Integer, height As Integer, x As Integer, y As Integer)
        component.Size = New Size(width, height)
        component.Location = New Point(x, y)
        component.BackColor = Color.Transparent
        component.AutoSize = False
        component.Text = ""
    End Sub
    Public Sub TextBox(component As TextBox, placeholder As String, width As Integer, height As Integer, x As Integer, y As Integer)
        component.Size = New Size(width, height)
        component.BackColor = ColorTranslator.FromHtml("#F1EEEE")
        component.Location = New Point(x, y)
        component.BorderStyle = BorderStyle.None
        component.Font = New Font("Poppins", 11, FontStyle.Bold)
        component.TextAlign = HorizontalAlignment.Left
        component.Tag = placeholder
        Styles.SetPlaceholder(component, placeholder)
    End Sub
    Public Sub Label(component As Label, width As Integer, height As Integer, x As Integer, y As Integer)
        component.Size = New Size(width, height)
        component.Location = New Point(x, y)
        component.BackColor = Color.Transparent
        component.AutoSize = False
        component.Text = ""
        component.Font = New Font("Poppins", 11, FontStyle.Bold)
        component.TextAlign = HorizontalAlignment.Center
    End Sub
    Public Sub Panel(component As Panel, width As Integer, height As Integer, x As Integer, y As Integer)
        component.BackColor = Color.Transparent
        component.Size = New Size(width, height)
        component.Location = New Point(x, y)
        component.Visible = False
        component.BringToFront()
    End Sub
    Public Sub UserControl(component As UserControl, width As Integer, height As Integer)
        component.BackColor = Color.Transparent
        component.Size = New Size(width, height)
    End Sub
    Public Sub UserLabels(component As Label, width As Integer, height As Integer, x As Integer, y As Integer)
        component.Size = New Size(width, height)
        component.Location = New Point(x, y)
        component.BackColor = Color.Transparent
        component.AutoSize = False
        component.Text = ""
        component.AutoSize = False
    End Sub
    Public Sub Components(component As UserControl, width As Integer, height As Integer)
        component.BackColor = Color.White
        component.Size = New Size(width, height)
    End Sub
    Public Sub SetPlaceholder(textBox As TextBox, placeholderText As String)
        textBox.Text = placeholderText
        textBox.ForeColor = Color.Gray

        AddHandler textBox.GotFocus, Sub() TextBoxGotFocus(textBox, placeholderText)
        AddHandler textBox.LostFocus, Sub() TextBoxLostFocus(textBox, placeholderText)
    End Sub
    Private Sub TextBoxGotFocus(textBox As TextBox, placeholderText As String)
        If textBox.Text = placeholderText Then
            textBox.Text = ""
            textBox.ForeColor = Color.Black
        End If
    End Sub

    Private Sub TextBoxLostFocus(textBox As TextBox, placeholderText As String)
        If String.IsNullOrWhiteSpace(textBox.Text) Then
            textBox.Text = placeholderText
            textBox.ForeColor = Color.Gray
        End If
    End Sub
    Public Sub HoverEffect(panel As Panel, filepath As String)
        Dim currentAnimation As Image = panel.BackgroundImage

        AddHandler panel.MouseEnter, Sub(sender As Object, e As EventArgs)
                                         panel.BackgroundImage = Image.FromFile(Handlers.FilePath(filepath))
                                     End Sub

        AddHandler panel.MouseLeave, Sub(sender As Object, e As EventArgs)
                                         panel.BackgroundImage = currentAnimation
                                     End Sub
    End Sub
End Module
