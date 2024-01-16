Public Class NoCoursesAvailable
    Private Sub NoCoursesAvailable_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Styles.Components(Me, 970, 365)
        LoadComponents()
    End Sub
    Private Sub LoadComponents()
        JoinClassButton()
    End Sub
    Private Sub JoinClassButton()
        Dim button As Label = Tools.CreateButton(Me, 147, 43, 736, 285)
        AddHandler button.Click, Sub(sender As Object, e As EventArgs)
                                     StudentDashboard.joinClassPopup.Visible = True
                                     StudentDashboard.joinClassPopup.BringToFront()
                                 End Sub
    End Sub
End Class
