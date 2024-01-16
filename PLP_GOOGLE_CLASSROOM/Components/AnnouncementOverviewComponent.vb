Public Class AnnouncementOverviewComponent
    Dim announcemenTitle As New Label
    Dim announcementAbout As New Label
    Private Sub AnnouncementOverviewComponent_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Styles.UserControl(Me, 621, 125)
    End Sub
    Sub New(annoncementID As String, title As String, description As String, teacherNo As String)
        InitializeComponent()
        CreateAnnouncementOverviewComponent(title, description)
        ViewButton(annoncementID, title, description, teacherNo)
    End Sub
    Private Sub CreateAnnouncementOverviewComponent(title As String, description As String)
        announcemenTitle = Tools.CreateUserLabel(Me, 300, 34, 35, 27)
        announcemenTitle.Text = title
        announcemenTitle.Font = New Font("Poppins", 14, FontStyle.Bold)

        announcementAbout = Tools.CreateUserLabel(Me, 300, 34, 35, 64)
        announcementAbout.Text = description
        announcementAbout.Font = New Font("Poppins", 11, FontStyle.Regular)
    End Sub
    Private Sub ViewButton(annoncementID As String, title As String, description As String, teacherNo As String)
        Dim button As Label = Tools.CreateButton(Me, 117, 37, 475, 70)
        AddHandler button.Click, Sub(sender As Object, e As EventArgs)
                                     TeacherDashboard.ViewAnnouncementPopup(annoncementID, title, description, teacherNo)
                                 End Sub
    End Sub
End Class
