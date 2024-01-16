Public Class SeeAllButton
    Private Sub SeeAllButton_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Styles.UserControl(Me, 621, 78)
    End Sub
    Sub New()
        InitializeComponent()
        CreateSeeAllButton()
    End Sub
    Private Sub CreateSeeAllButton()
        Dim button As Label = Tools.CreateButton(Me, 621, 78, 0, 0)
        AddHandler button.Click, Sub(sender As Object, e As EventArgs)
                                     TeacherDashboard.announcementPanel.Visible = True
                                 End Sub
    End Sub
End Class
