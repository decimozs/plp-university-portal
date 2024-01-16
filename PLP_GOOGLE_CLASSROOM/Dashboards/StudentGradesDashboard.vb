Public Class StudentGradesDashboard
    Protected Overloads Overrides ReadOnly Property CreateParams() As CreateParams
        Get
            Dim cp As CreateParams = MyBase.CreateParams
            cp.ExStyle = cp.ExStyle Or 33554432
            Return cp
        End Get
    End Property
    Private Sub StudentGradesDashboard_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Styles.UserControl(Me, 1631, 748)
    End Sub
End Class
