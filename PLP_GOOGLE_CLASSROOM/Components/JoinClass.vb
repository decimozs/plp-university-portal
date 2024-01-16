Public Class JoinClass
    Public parseId = Handlers.IdParser(StudentLoginForm.studentID.Text)
    Dim classCode As New TextBox
    Private Sub JoinClass_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Styles.UserControl(Me, 676, 630)
        LoadComponents()
    End Sub
    Private Sub LoadComponents()
        ClassCodeTextBox()
        CreateJoinClass()
        BackButton()
        JoinClassButton()
    End Sub
    Private Sub CreateJoinClass()
        Dim studentNameLbl As Label = Tools.CreateUserLabel(Me, 213, 43, 161, 151)
        studentNameLbl.Text = Handlers.GetStudentName(parseId)
        studentNameLbl.Font = New Font("Poppins", 10, FontStyle.Bold)

        Dim studentSectionLbl As Label = Tools.CreateUserLabel(Me, 148, 28, 161, 190)
        studentSectionLbl.Text = Handlers.GetStudentSection(parseId)
        studentSectionLbl.Font = New Font("Poppins", 10, FontStyle.Regular)
    End Sub
    Private Sub ClassCodeTextBox()
        classCode = Tools.CreateTextBox(Me, "Class code", 550, 85, 73, 385)
    End Sub
    Private Sub BackButton()
        Dim button As Label = Tools.CreateButton(Me, 40, 23, 594, 29)
        AddHandler button.Click, Sub(sender As Object, e As EventArgs)
                                     StudentDashboard.joinClassPopup.Visible = False
                                 End Sub
    End Sub
    Private Sub JoinClassButton()
        Dim button As Label = Tools.CreateButton(Me, 591, 95, 43, 494)
        AddHandler button.Click, Sub(sender As Object, e As EventArgs)
                                     ' test only
                                     Database.InsertClassDetails(parseId, classCode.Text)
                                 End Sub
    End Sub
End Class
