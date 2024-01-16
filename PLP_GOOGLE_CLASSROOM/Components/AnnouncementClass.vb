Public Class AnnouncementClass
    Public parseId As String = Handlers.IdParser(TeacherLoginForm.teacherId.Text)
    Private Sub AnnouncementClass_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Styles.UserControl(Me, 1019, 369)
    End Sub
    Sub New(announcementId As String, title As String, description As String, teacherNo As String)
        InitializeComponent()
        CreateAnnouncement(title, description, teacherNo)
        CommentButton(announcementId)
        ReactButton(announcementId)
    End Sub
    Private Sub CreateAnnouncement(title As String, description As String, teacherNo As String)
        Dim announcementTitle As Label = Tools.CreateUserLabel(Me, 229, 53, 37, 28)
        announcementTitle.Text = title
        announcementTitle.Font = New Font("Poppins", 22, FontStyle.Bold)

        Dim announcementDescription As Label = Tools.CreateUserLabel(Me, 932, 157, 44, 105)
        announcementDescription.Text = description
        announcementDescription.Font = New Font("Poppins", 11, FontStyle.Regular)

        Dim teacherNoLbl As Label = Tools.CreateUserLabel(Me, 365, 33, 144, 268)
        Dim getTeacherNameText = Handlers.GetTeacherName(teacherNo)
        teacherNoLbl.Text = getTeacherNameText
        teacherNoLbl.Font = New Font("Poppins", 10, FontStyle.Bold)

        Dim teacherPositionLBl As Label = Tools.CreateUserLabel(Me, 365, 33, 144, 298)
        Dim getTeacherPositionText = Handlers.GetTeacherPosition(teacherNo)
        teacherPositionLBl.Text = getTeacherPositionText
        teacherPositionLBl.Font = New Font("Poppins", 10, FontStyle.Bold)
        teacherPositionLBl.ForeColor = ColorTranslator.FromHtml("#9C8B8B")
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
End Class
