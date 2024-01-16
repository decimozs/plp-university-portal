Public Class NoClassAvailable
    Private Sub NoClassAvailable_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Styles.Components(Me, 970, 365)
        LoadComponents()
    End Sub
    Private Sub LoadComponents()
        CreateClassButton()
    End Sub
    Private Sub CreateClassButton()
        Dim button As Label = Tools.CreateButton(Me, 147, 43, 747, 285)
        AddHandler button.Click, Sub(sender As Object, e As EventArgs)
                                     TeacherDashboard.createCoursePanelPopup.Visible = True
                                     TeacherDashboard.createCoursePanelPopup.BringToFront()
                                 End Sub
    End Sub
End Class
