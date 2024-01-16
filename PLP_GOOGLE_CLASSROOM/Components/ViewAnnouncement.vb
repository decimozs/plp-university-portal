Public Class ViewAnnouncement
    Private Sub ViewAnnouncement_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Styles.UserControl(Me, 1019, 369)
    End Sub
    Sub New(announcementId As String, title As String, description As String, teacherNo As String)
        InitializeComponent()
        CreateAnnouncement(title, description, teacherNo)
        CommentButton(announcementId)
        ReactButton(announcementId)
        BackButton()
    End Sub
    Private Sub CreateAnnouncement(title As String, description As String, teacherNo As String)
        Dim announcementTitle As Label = Tools.CreateUserLabel(Me, 229, 53, 37, 28)
        announcementTitle.Text = title
        announcementTitle.Font = New Font("Poppins", 22, FontStyle.Bold)

        Dim announcementDescription As Label = Tools.CreateUserLabel(Me, 932, 157, 44, 105)
        announcementDescription.Text = description
        announcementDescription.Font = New Font("Poppins", 11, FontStyle.Regular)

        Dim teacherNoLbl As Label = Tools.CreateUserLabel(Me, 165, 33, 144, 258)
        teacherNoLbl.Text = teacherNo
        teacherNoLbl.Font = New Font("Poppins", 10, FontStyle.Bold)
    End Sub
    Private Sub CommentButton(announcementId As String)
        Dim button As Label = Tools.CreateButton(Me, 149, 44, 741, 290)
        AddHandler button.Click, Sub(sender As Object, e As EventArgs)
                                     MsgBox(announcementId)
                                 End Sub
    End Sub
    Private Sub ReactButton(announcementId As String)
        Dim button As Label = Tools.CreateButton(Me, 60, 60, 916, 282)
        AddHandler button.Click, Sub(sender As Object, e As EventArgs)
                                     MsgBox(announcementId)
                                 End Sub
    End Sub
    Private Sub BackButton()
        Dim button As Label = Tools.CreateButton(Me, 41, 24, 934, 27)
        AddHandler button.Click, Sub(sender As Object, e As EventArgs)
                                     TeacherDashboard.viewAnnouncementPanel.Visible = False
                                 End Sub
    End Sub
End Class
